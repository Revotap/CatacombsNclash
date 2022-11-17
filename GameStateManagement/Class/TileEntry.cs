using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Class
{
    internal class TileEntry
    {
        private Tile tile;
        private Vector2 drawVector;

        public TileEntry(Tile tile, Vector2 drawVector)
        {
            this.Tile = tile;
            this.DrawVector = drawVector;
        }

        public Vector2 DrawVector { get => drawVector; set => drawVector = value; }
        internal Tile Tile { get => tile; set => tile = value; }
    }
}
