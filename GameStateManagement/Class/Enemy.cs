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
    public class Enemy : Character
    {
        #region Variables
        private Item loot;
        private int exp;
        #endregion

        #region Properties
        public Item Loot { get => loot;}
        public int Exp { get => exp;}
        #endregion

        #region Constructor
        public Enemy()
        {
            
            
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

    }


}
 

