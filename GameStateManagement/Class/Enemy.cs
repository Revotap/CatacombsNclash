using CatacombsNClash.Class;
using GameStateManagement.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Class
{
    internal class Enemy : Character
    {
        #region Variables
        private Item loot;
        private int exp;

        private int width;
        private int height;

        private List<Texture2D> textureList;
        private int nextTexture = 1;
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 150;
        #endregion

        #region Properties
        public Item Loot { get => loot; }
        public int Exp { get => exp; }
        #endregion

        #region Constructor
        public Enemy(String name, List<Texture2D> enemyTexture, int width, int height)
        {
            
                base.Name = name;
                //base.Texture = playerTexture;
                textureList = enemyTexture;
                base.Texture = textureList.FirstOrDefault();

                //Debugging
                base.T_class = "Enemy";
                base.Strength = 1;
                base.Intelligence = 1;
                base.Dexterity = 1;
                base.StrengthMod = 1;
                base.IntelligenceMod = 1;
                base.DexterityMod = 1;
                base.ArmorClass = 1;
                base.HealthPoints = 100;
                base.ManaPoints = 100;
                //base.BoundingBox = new Rectangle();
                base.Speed = 4f;

                this.width = width;
                this.height = height;
                base.BoundingBox = new Rectangle((int)base.PlayerPositionX + (width / 2), (int)base.PlayerPositionY + (height / 3 * 2), width, height);
            

        }
        #endregion



        #region Methods
        public Item dropItem()
        {
            return Loot;
        }

        public int ExpGrant()
        {
            return Exp;
        }

        #endregion


        public override void moveUp()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                base.PlayerPositionY -= base.Speed;
            }
        }
        public override void moveDown()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                base.PlayerPositionY += base.Speed;
            }
        }
        public override void moveLeft()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                base.PlayerPositionX -= base.Speed;
            }
        }
        public override void moveRight()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                base.PlayerPositionX += base.Speed;
            }
        }

        public void Update(GameTime gameTime)
        {
            base.BoundingBoxX = (int)base.EnemyPositionX;
            base.BoundingBoxY = (int)base.EnemyPositionY;

            //Update der Animation
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                base.Texture = textureList.ElementAt(nextTexture);
                nextTexture++;
                if (nextTexture >= textureList.Count)
                {
                    nextTexture = 0;
                }
            }


        }

        //Getter and Setter
        public new Texture2D Texture { get => base.Texture; set => base.Texture = value; }
        public new Vector2 EnemyPosition { get => base.EnemyPosition; set => base.EnemyPosition = value; }
        public new Rectangle BoundingBox { get => base.BoundingBox; }
    }
}




