using CatacombsNClash.Class;
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
    public class Player : Character
    {
        #region Variables

        private int currentExp;
        private int maxExp;
        private int unspendSkillpoint;
        // private Shield offHand;

        #endregion

        #region Properties
        public int CurrentExp { get => currentExp; set => currentExp = value; }
        public int MaxExp { get => maxExp; set => maxExp = value; }
        public int UnSpendSkillpoint { get => unspendSkillpoint; set => unspendSkillpoint = value; }

        #endregion

        #region Constructor
        public Player()
        {
            
        }
        #endregion

        #region Methods

        public void addExp(int exp)
        {   //if abfrage für maxexp
            CurrentExp += exp;
        }

        private void levelUp()
        {

        }

        public bool increaseSkill()
        {
            return CurrentExp > MaxExp;
        }
        #endregion

        
    }
}
