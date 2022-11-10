using GameStateManagement.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Screens
{
    internal class GameplayScreen : GameScreen
    {
        #region Fields
        private ContentManager Content;
        private SpriteFont gameFont;
        #endregion

        #region Variablen
        private Player player;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private Viewport viewport;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        #endregion

        #region Initialization
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            if(Content == null)
            {
                Content = new ContentManager(ScreenManager.Game.Services, "Content") ;
            }

            if(player == null)
            {
                player = new Player("Spieler", Content.Load<Texture2D>(@"OurContent\Player\Knight\knight_f_idle_anim_f0"));
            }

            _spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);

            viewport = ScreenManager.GraphicsDevice.Viewport;

            player.PlayerPosition = new Vector2(viewport.Width/2,viewport.Height/2);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
        #endregion

        #region Update and Draw
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            currentKeyboardState = Keyboard.GetState();

            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void HandleInput(InputState input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            if(input.IsPauseGame(ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    player.moveLeft();
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    player.moveRight();
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    player.moveUp();
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    player.moveDown();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            DrawBackground();

            DrawLevel();
            
            DrawPlayer();

            DrawOverlay();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        private void DrawBackground()
        {

        }

        private void DrawLevel()
        {

        }

        private void DrawPlayer()
        {
            //_spriteBatch.Draw(player.Texture, player.PlayerPosition, Color.White);
            _spriteBatch.Draw(player.Texture, new Rectangle((int)player.PlayerPositionX, (int)player.PlayerPositionY, 64,112), Color.White);
        }

        private void DrawOverlay()
        {

        }
        #endregion
    }
}
