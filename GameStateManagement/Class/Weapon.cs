using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatacombsNClash.Class
{
    public class Weapon : Item
    {
        #region Variables
        public int damage;
        public String Message;
        public int StunDuration;
        public int FreezeDuration;
        public int FireDuration;
        public int PoisonDuration;
        public String rarity;



        #endregion

        #region Constructor
        public Weapon()
        {

        }
        #endregion

        #region Methods

        public void onEnable()
        {

        }

        public void attack() //attack(Target target)
        {

        }

        #endregion
    }
}
