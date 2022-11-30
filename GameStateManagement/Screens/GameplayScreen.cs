using GameStateManagement.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        //World
        private Texture2D worldTexture;
        private int singleTextureResolution = 16;
        private List<TileEntry> tileMap;
        private Tile wall_left_01;
        private Tile wall_top_01;
        private Tile wall_right_01;
        private Tile wall_bottom_01;
        private Tile wall_corner_left;
        private Tile wall_corner_right;
        private Tile door_left_1;
        private Tile door_right_1;
        private Tile ground_1;
        private int targetTextureResolution = 64;


        /*
         wl =
         wt =
         g = 
         cl = 
         wb = 
         wr =
         cr = 
         
         */

        private String[,] map = new String[,] { {"wl","wt","wt","dl","dr","wt","wt","wr"},
                                                {"wl","g","g","g","g","g","g","wr"},
                                                {"wl","g","g","g","g","g","g","wr"},
                                                {"wl","g","g","g","g","g","g","wr"},
                                                {"wl","g","g","g","g","g","g","wr"},
                                                {"wl","g","wb","g","g","wb","g","wr"},
                                                {"wl","g","g","g","g","g","g","wr"},
                                                {"cl","wb","wb","wb","wb","wb","wb","cr"}};

        private List<Rectangle> collisionObjects;
        private Vector2 oldPlayerPosition;

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
                //player = new Player("Spieler", Content.Load<Texture2D>(@"OurContent\Player\Knight\knight_f_idle_anim_f0"));
                //player = new Player("Spieler", Content.Load<Texture2D>(@"OurContent\Player\Knight2\Protect"));
                List<Texture2D> playerTextures = new List<Texture2D>();
                playerTextures.Add(Content.Load<Texture2D>(@"Test\knight_f_idle_anim_f0"));
                playerTextures.Add(Content.Load<Texture2D>(@"Test\knight_f_idle_anim_f1"));
                playerTextures.Add(Content.Load<Texture2D>(@"Test\knight_f_idle_anim_f2"));
                playerTextures.Add(Content.Load<Texture2D>(@"Test\knight_f_idle_anim_f3"));
                player = new Player("Spieler", playerTextures,64,112);
            }

            _spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);

            viewport = ScreenManager.GraphicsDevice.Viewport;

            player.PlayerPosition = new Vector2((map.GetLength(0)/2)*targetTextureResolution,(map.GetLength(1)/2)*targetTextureResolution);

            //TESTING
            worldTexture = Content.Load<Texture2D>(@"Test\Dungeon_Tileset");
            tileMap = new List<TileEntry>();

            //worldTexture, resolution of single texture in texture map, coordinates of texture in texturemap
            wall_left_01 = new Tile(worldTexture, singleTextureResolution, new Vector2(0,0),true);
            wall_top_01 = new Tile(worldTexture, singleTextureResolution, new Vector2(16,0),true);
            wall_right_01 = new Tile(worldTexture, singleTextureResolution, new Vector2(80,0),true);
            wall_bottom_01 = new Tile(worldTexture, singleTextureResolution, new Vector2(16, 64),true);
            wall_corner_left = new Tile(worldTexture, singleTextureResolution, new Vector2(0,64), true);
            wall_corner_right = new Tile(worldTexture, singleTextureResolution, new Vector2(80,64), true);
            door_left_1 = new Tile(worldTexture, singleTextureResolution, new Vector2(96,48), true);
            door_right_1 = new Tile(worldTexture, singleTextureResolution, new Vector2(112,48), true);
            ground_1 = new Tile(worldTexture, singleTextureResolution, new Vector2(16, 16), false);

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    float newTileXCoord = y * targetTextureResolution;
                    float newTileYCoord = x * targetTextureResolution;
                    Vector2 newTileCoordinates = new Vector2(newTileXCoord, newTileYCoord);

                    if (map[x, y] == "wl")
                    {
                        tileMap.Add(new TileEntry(wall_left_01, newTileCoordinates));
                    }
                    else if (map[x, y] == "wb")
                    {
                        tileMap.Add(new TileEntry(wall_bottom_01, newTileCoordinates));
                    }
                    else if (map[x, y] == "cl")
                    {
                        tileMap.Add(new TileEntry(wall_corner_left, newTileCoordinates));
                    }
                    else if (map[x, y] == "cr")
                    {
                        tileMap.Add(new TileEntry(wall_corner_right, newTileCoordinates));
                    }
                    else if (map[x, y] == "dl")
                    {
                        tileMap.Add(new TileEntry(door_left_1, newTileCoordinates));
                    }
                    else if (map[x, y] == "dr")
                    {
                        tileMap.Add(new TileEntry(door_right_1, newTileCoordinates));
                    }
                    else if (map[x, y] == "g")
                    {
                        tileMap.Add(new TileEntry(ground_1, newTileCoordinates));
                    }
                    else if (map[x, y] == "wt")
                    {
                        tileMap.Add(new TileEntry(wall_top_01, newTileCoordinates));
                    }
                    else if (map[x, y] == "wr")
                    {
                        tileMap.Add(new TileEntry(wall_right_01, newTileCoordinates));
                    }
                }
            }

            collisionObjects = new List<Rectangle>();
            foreach(TileEntry tileEntry in tileMap)
            {
                if (tileEntry.Tile.HasCollision)
                {
                    collisionObjects.Add(new Rectangle((int)tileEntry.DrawVector.X, (int) tileEntry.DrawVector.Y, 64, 1));
                }
            }

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

            //Testing
            /*tileMap.Add(new TileEntry(wall_left_01, new Vector2(0, 0)));
            tileMap.Add(new TileEntry(wall_top_01, new Vector2(128, 0)));
            tileMap.Add(new TileEntry(wall_right_01, new Vector2(256, 0)));
            tileMap.Add(new TileEntry(wall_bottom_01, new Vector2(0, 128)));*/

            

            player.Update(gameTime);
            foreach(Rectangle rect in collisionObjects)
            {
                if (rect.Intersects(player.BoundingBox))
                {
                    player.PlayerPosition = oldPlayerPosition;
                }
            }
            /*Rectangle testRectangle = new Rectangle(0,0,64,64);
            if (testRectangle.Intersects(player.BoundingBox))
            {
                player.PlayerPositionX += 30;
            }*/

            oldPlayerPosition = player.PlayerPosition;

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
            ScreenManager.GraphicsDevice.Clear(Color.Black);

            //_spriteBatch.Begin();
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

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
            //_spriteBatch.Draw(test,new Rectangle(0,0, 64,64),new Rectangle(0,0,16,16), Color.White);
            foreach(TileEntry tileEntry in tileMap)
            {
                _spriteBatch.Draw(tileEntry.Tile.Texture, new Rectangle((int)tileEntry.DrawVector.X, (int)tileEntry.DrawVector.Y, targetTextureResolution, targetTextureResolution), new Rectangle((int)tileEntry.Tile.TextureVector.X, (int) tileEntry.Tile.TextureVector.Y, tileEntry.Tile.TextureResolution, tileEntry.Tile.TextureResolution),Color.White);
            }
        }

        private void DrawPlayer()
        {
            _spriteBatch.Draw(player.Texture, new Rectangle((int) player.PlayerPositionX, (int) player.PlayerPositionY,64,112), Color.White);
            //_spriteBatch.Draw(player.Texture, new Rectangle((int)player.PlayerPositionX, (int)player.PlayerPositionY, 64,112), Color.White);
        }

        private void DrawOverlay()
        {

        }
        #endregion
    }
}
