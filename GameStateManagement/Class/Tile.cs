using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagement.Class
{
    internal class Tile
    {
        private Texture2D texture;
        private int textureResolution;
        private Vector2 textureVector;
        private bool hasCollision;

        public Tile(Texture2D texture, int textureResolution, Vector2 textureVector, bool hasCollision)
        {
            this.Texture = texture;
            this.TextureResolution = textureResolution;
            this.TextureVector = textureVector;
            this.HasCollision = hasCollision;
        }

        public Texture2D Texture { get => texture; set => texture = value; }
        public int TextureResolution { get => textureResolution; set => textureResolution = value; }
        public Vector2 TextureVector { get => textureVector; set => textureVector = value; }
        public bool HasCollision { get => hasCollision; set => hasCollision = value; }
    }
}
