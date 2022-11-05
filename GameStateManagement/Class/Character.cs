using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatacombsNClash.Class
{
    public abstract class Character
    {
        #region Variables
        private string name;
        private string klasse; // class 
        //Attribute
        private int strength;
        private int dexterity;
        private int intelligence;
        private int armouryClass;
        private int constitution;
        //Modfikitaoren
        private int strMod;
        private int dexMod;
        private int intMod;
        private int conMod;
   
        private int healthPoints;
        private int manaPoints;

        private List<Item> inventory;
        private Weapon equippedWeapon;
        private Rectangle boundingBox;

        #endregion

        #region Properties
        public string Name { get => name; set => name = value; }
        public String Klasse { get => klasse; set => klasse = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Intelligence { get => intelligence; set => intelligence = value; }
        public int Dexterity { get => dexterity; set => dexterity = value; }
        public int Constitution { get => constitution; set => constitution = value; }
        public int Armor { get => armouryClass; set => armouryClass = value; }
        public int StrengthMod { get => strMod; set => strMod = value; }
        public int IntelligenceMod { get => intMod; set => intMod = value; }
        public int DexterityMod { get => dexMod; set => dexMod = value; }
        public int ConstitutionMod { get => conMod; set => conMod = value; }
        public int HealthPoints { get => healthPoints; set => healthPoints = value; }
        public int ManaPoints { get => manaPoints; set => manaPoints = value; }

        #endregion

        #region Constructor
        public Character()
        {

        }
        #endregion

        #region Methods
        public void update()
        {

        }

        public void move(int x, int y)
        {

        }

        public void attack(int modifyer) 
        {

        }


        public void defend()
        {

        }

        public int calcModifyer(int stat)
        {
            return stat;
        }

        public Item useItem(Item item)
        {
            return item;
        }
        
        public void addInventory(Item item)
        {

        }

        public bool intersect()
        {
            return true;
        }

        #endregion

    }
}
