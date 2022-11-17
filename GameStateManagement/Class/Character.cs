using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Class
{
    internal abstract class Character
    {
        #region Variables
        private String name;
        private String t_class;
        private int strength;
        private int intelligence;
        private int dexterity;
        private int strengthMod;
        private int intelligenceMod;
        private int dexterityMod;
        private int armorClass;
        private int healthPoints;
        private int manaPoints;
        //Placeholder for inventory
        //placeholder for weapon
        private Rectangle boundingBox;

        //Not in UML
        private float speed;
        private Texture2D texture;
        private Vector2 playerPosition;

        #endregion

        #region Methods
        //public abstract void update();
        public abstract void moveUp();
        public abstract void moveDown();
        public abstract void moveLeft();
        public abstract void moveRight();
        //public abstract void attack(int modifier)

        //Getters and Setters
        protected string Name { get => name; set => name = value; }
        protected string T_class { get => t_class; set => t_class = value; }
        protected int Strength { get => strength; set => strength = value; }
        protected int Intelligence { get => intelligence; set => intelligence = value; }
        protected int Dexterity { get => dexterity; set => dexterity = value; }
        protected int StrengthMod { get => strengthMod; set => strengthMod = value; }
        protected int IntelligenceMod { get => intelligenceMod; set => intelligenceMod = value; }
        protected int DexterityMod { get => dexterityMod; set => dexterityMod = value; }
        protected int ArmorClass { get => armorClass; set => armorClass = value; }
        protected int HealthPoints { get => healthPoints; set => healthPoints = value; }
        protected int ManaPoints { get => manaPoints; set => manaPoints = value; }
        protected Rectangle BoundingBox { get => boundingBox; set => boundingBox = value; }
        protected int BoundingBoxX { get => boundingBox.X; set => boundingBox.X = value; }
        protected int BoundingBoxY { get => boundingBox.Y; set => boundingBox.Y = value; }
        protected float Speed { get => speed; set => speed = value; }
        protected Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 PlayerPosition { get => playerPosition; set => playerPosition = value; }
        public float PlayerPositionX { get => playerPosition.X; set => playerPosition.X = value; }
        public float PlayerPositionY { get => playerPosition.Y; set => playerPosition.Y = value; }
        #endregion
    }
}
