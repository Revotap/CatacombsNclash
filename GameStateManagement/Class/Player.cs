using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Class
{
    internal class Player : Character
    {
        #region Variables

        #endregion

        #region Construktor
        public Player(String name, Texture2D playerTexture)
        {
            base.Name = name;
            base.Texture = playerTexture;

            //Debugging
            base.T_class = "Knight";
            base.Strength = 1;
            base.Intelligence = 1;
            base.Dexterity = 1;
            base.StrengthMod = 1;
            base.IntelligenceMod = 1;
            base.DexterityMod = 1;
            base.ArmorClass = 1;
            base.HealthPoints = 100;
            base.ManaPoints = 100;
            //base.BoundingBox = null;
            base.Speed = 4f;
        }
        #endregion

        #region Methods
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

        //Getter and Setter
        public new Texture2D Texture { get => base.Texture; set => base.Texture = value; }
        public new Vector2 PlayerPosition { get => base.PlayerPosition; set => base.PlayerPosition = value; }
        #endregion
    }
}
