#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagement.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    internal class GameplayScreenOld : GameScreen
    {
        #region Fields

        private ContentManager Content;
        private SpriteFont gameFont;

        //private Vector2 playerPosition = new Vector2(100, 100);
        //private Vector2 enemyPosition = new Vector2(100, 100);

        private Random random = new Random();

        private float pauseAlpha;

        #endregion Fields

        #region Variablen

        private PlayerOld player;

        // Grafische Ausgabe
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // Font
        private SpriteFont spriteFont;

        // Viewport
        private Viewport viewport;


        // Tastatur abfragen
        private KeyboardState currentKeyboardState;

        private KeyboardState previousKeyboardState;

        // Sprites
        //private Texture2D ShipTexture;
        private Texture2D StarTexture;
        //private Texture2D LaserTexture;
        private Texture2D EnemyTexture;

        // Raumschiff Variablen
        //private Vector2 shipPosition;

        //private float shipSpeed = 5f;

        // Laser Variablen
        private List<Vector2> laserShots = new List<Vector2>();

        //private float laserSpeed = 10f;

        // Gegner Variablen
        private readonly List<Vector2> enemyPositions = new List<Vector2>();

        private Vector2 enemyStartPosition = new Vector2(100, 100);
        private float enemyRadius;
        private float enemySpeed = 1f;
        private Color enemyColor;

        // Sound Effekte
        //private SoundEffect laserSound;

        private SoundEffect explosionSound;

        // Spieler-Punkte und Zeichenposition der Punkte
        private int playerScore;

        private Vector2 scorePosition;

        //Kollision
        private bool shipHit = false;
        private Rectangle safeBounds;
        private const float SafeAreaPortion = 0.05f;

        #endregion Variablen


        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreenOld()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");
            
            if(player == null)
            {
                player = new PlayerOld(Content.Load<Texture2D>("ship"), Content.Load<Texture2D>("laser"), Content.Load<SoundEffect>("laserfire"));
            }

            // Ein SpriteBatch zum Zeichnen
            _spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);

            // Viewport speichern
            viewport = ScreenManager.GraphicsDevice.Viewport;

            //Kollider initialisieren
            safeBounds = new Rectangle(
                (int)(viewport.Width * SafeAreaPortion),
                (int)(viewport.Height * SafeAreaPortion),
                (int)(viewport.Width * (1 - 2 * SafeAreaPortion)),
                (int)(viewport.Height * (1 - 2 * SafeAreaPortion)));

            // TODO
            // Texturen laden
            StarTexture = Content.Load<Texture2D>("starfield");
            //ShipTexture = Content.Load<Texture2D>("ship");
            //LaserTexture = Content.Load<Texture2D>("laser");
            EnemyTexture = Content.Load<Texture2D>("enemy");


            // TODO
            // Font laden
            spriteFont = Content.Load<SpriteFont>("Verdana");

            // TODO
            // Sounds laden
            explosionSound = Content.Load<SoundEffect>("explosion");
            //laserSound = Content.Load<SoundEffect>("laserfire");
            SoundEffect.MasterVolume = 0.05f;

            // Das Raumschiff positionieren
            //shipPosition.X = viewport.Width / 2;
            //shipPosition.Y = viewport.Height - 100;
            player.setShipPosition(new Vector2(viewport.Width/2, viewport.Height - 100));

            // Radius der Feinde festlegen
            if (EnemyTexture != null)
            {
                if (EnemyTexture.Width > EnemyTexture.Height)
                {
                    enemyRadius = EnemyTexture.Width;
                }
                else
                {
                    enemyRadius = EnemyTexture.Height;
                }

                // Gegner erzeugen
                CreateEnemies();
            }

            // Position der Score Ausgabe festlegen
            scorePosition = new Vector2(25, 25);

        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            Content.Unload();
        }

        #endregion Initialization

        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            currentKeyboardState = Keyboard.GetState();

            /*// Left
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                MoveShipLeft();
            }

            // Right
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                MoveShipRight();
            }*/

            // Prevent the person from moving off of the screen
            //shipPosition.X = MathHelper.Clamp(shipPosition.X,
            //    safeBounds.Left, safeBounds.Right - ShipTexture.Width);
            player.setShipPosition(new Vector2(MathHelper.Clamp(player.getShipPosition().X, safeBounds.Left, safeBounds.Right - player.getShipTexture().Width), player.getShipPosition().Y));

            // Space
            if (IsNewKeyPressed(Keys.Space))
            {
                //FireLaser();
                player.FireLaser(laserShots);
            }

            previousKeyboardState = currentKeyboardState;

            if (!otherScreenHasFocus)
            {
                UpdateEnemies();
            }
            
            UpdateLaserShots();

            base.Update(gameTime, otherScreenHasFocus, false);
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    //MoveShipLeft();
                    player.MoveShipLeft();
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    //MoveShipRight();
                    player.MoveShipRight();
                }

                /*Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                {
                    movement.Normalize();
                }

                playerPosition += movement * 2;*/
            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Hintergrund zeichnen
            DrawBackground();

            // Das Schiff zeichnen
            DrawSpaceShip();

            // Laser zeichnen
            DrawLaser();

            // Feinde zeichnen
            DrawEnemy();

            // Punkte anzeigen
            DrawScore();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion Update and Draw

        #region Methods
        public void CreateEnemies()
        {
            // Feinde erzeugen
            Vector2 position = enemyStartPosition;
            position.X -= EnemyTexture.Width / 2;

            // Eine Zufallszahl zwischen 3 und 10 ermitteln
            int count = random.Next(3, 11);

            // Gegener erzeugen
            for (int i = 0; i < count; i++)
            {
                enemyPositions.Add(position);
                position.X += EnemyTexture.Width + 15f;
            }

            // Farbwert ändern
            switch (count)
            {
                case 3:
                    enemyColor = Color.Red;
                    break;
                case 4:
                    enemyColor = Color.Green;
                    break;
                case 5:
                    enemyColor = Color.Yellow;
                    break;
                case 6:
                    enemyColor = Color.Blue;
                    break;
                case 7:
                    enemyColor = Color.Magenta;
                    break;
                case 8:
                    enemyColor = Color.Yellow;
                    break;
                case 9:
                    enemyColor = Color.White;
                    break;
                case 10:
                    enemyColor = Color.DarkGreen;
                    break;
                default:
                    break;
            }
        }

        

        #region Update von Lasern und Gegnern

        public void UpdateLaserShots()
        {
            int laserIndex = 0;

            while (laserIndex < laserShots.Count)
            {
                // hat der Schuss den Bildschirm verlassen?
                if (laserShots[laserIndex].Y < 0)
                {
                    laserShots.RemoveAt(laserIndex);
                }
                else
                {
                    // Position des Schusses aktualiesieren
                    Vector2 pos = laserShots[laserIndex];
                    //pos.Y -= laserSpeed;
                    pos.Y -= player.getLaserSpeed();
                    laserShots[laserIndex] = pos;

                    // Überprüfen ob ein Treffer vorliegt
                    int enemyIndex = 0;

                    while (enemyIndex < enemyPositions.Count)
                    {
                        // Abstand zwischen Feind-Position und Schuss-Position ermitteln
                        float distance = Vector2.Distance(enemyPositions[enemyIndex], laserShots[laserIndex]);

                        // Treffer?
                        if (distance < enemyRadius)
                        {
                            // Schuss entfernen
                            laserShots.RemoveAt(laserIndex);
                            // Feind entfernen
                            enemyPositions.RemoveAt(enemyIndex);
                            // Punkte erhöhen
                            playerScore++;

                            PlayExplosionSound();

                            // Schleife verlassen
                            break;
                        }
                        else
                        {
                            enemyIndex++;
                        }
                    }
                    laserIndex++;
                }
            }
        }

        public void UpdateEnemies()
        {
            // Startposition verändern
            enemyStartPosition.X += enemySpeed;

            // Bewegungsrichtung umkehren
            if (enemyStartPosition.X > 250)
            {
                enemySpeed *= -1;
            }
            else if (enemyStartPosition.X < 100f)
            {
                enemySpeed *= -1;
            }

            // Alle Feinde abgeschossen? Dann Neue Gegener
            if (enemyPositions.Count == 0 && EnemyTexture != null)
            {
                CreateEnemies();
            }

            // Aktualisieren
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                Vector2 position = enemyPositions[i];
                position.X += enemySpeed;
                enemyPositions[i] = position;
            }
        }

        #endregion

        public void PlayExplosionSound()
        {
            // TODO
            // Explosions WAV abspielen
            explosionSound.Play();
        }

        public void PlayLaserSound()
        {
            // TODO
            // Laserschuss WAV abspielen
            //laserSound.Play();

        }
        public bool IsNewKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) &&
                    !previousKeyboardState.IsKeyDown(key);
        }

        private void DrawBackground()
        {
            // TODO
            // Die Sternenfeld Grafik an der Position 0,0 zeichnen
            _spriteBatch.Draw(StarTexture, new Vector2(0, 0), Color.White);
        }

        private void DrawSpaceShip()
        {
            // TODO
            // Das Schiff mittig an den Koordinaten des Schiffes (shipPosition) zeichnen 
            //_spriteBatch.Draw(ShipTexture, shipPosition, Color.White);
            _spriteBatch.Draw(player.getShipTexture(), player.getShipPosition(), Color.White);
        }

        private void DrawLaser()
        {
            // TODO
            // Die Liste mit den Laser-Schüssen (laserShots) durchlaufen
            // und alle Schüsse (LaserTexture) zeichnen
            foreach (Vector2 laser in laserShots)
            {
                //_spriteBatch.Draw(LaserTexture, laser, Color.Green);
                _spriteBatch.Draw(player.getLaserTexture(), laser, Color.Green);
            }
        }

        private void DrawEnemy()
        {
            // TODO
            // Die Liste mit allen Gegnern (enemyPositions) durchlaufen
            // und alle Feinde (EnemyTexture) zeichnen
            foreach (Vector2 enemy in enemyPositions)
            {
                _spriteBatch.Draw(EnemyTexture, enemy, enemyColor);
            }
        }

        private void DrawScore()
        {
            // TODO
            // Die Punkte (playerScore) oben links (scorePosition) anzeigen
            _spriteBatch.DrawString(spriteFont, "Highscore: " + playerScore, scorePosition, Color.White);
        }
    }
    #endregion
}