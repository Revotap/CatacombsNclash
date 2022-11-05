using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatacombsNClash.Class
{
    public abstract class Item
    {
        #region Variables

        private long id;
        private string name;
        private string description;
        private Texture2D texture;

        #endregion

        #region Constructor
        public Item()
        {

        }
        #endregion

    }
}
