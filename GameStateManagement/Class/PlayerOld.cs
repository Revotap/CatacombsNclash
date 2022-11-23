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
    internal class PlayerOld
    {
        private Texture2D ShipTexture;
        private Vector2 shipPosition;
        private float shipSpeed = 5f;

        private Texture2D LaserTexture;
        private float laserSpeed = 10f;
        private SoundEffect laserSound;

        public PlayerOld(Texture2D shipTexture, Texture2D laserTexture, SoundEffect laserSound)
        {
            this.ShipTexture = shipTexture;
            this.LaserTexture = laserTexture;
            this.laserSound = laserSound;
        }

        public void FireLaser(List<Vector2> laserShots)
        {
            // aktuelle Position des Schiffes auf dem Bildschirm speichern
            Vector2 position = shipPosition;

            // Laserschuss vor das Schiff mittig platzieren
            position.Y -= ShipTexture.Height / 2;
            position.X -= LaserTexture.Width / 2;

            // Position in der Liste speichern
            laserShots.Add(position);

            laserSound.Play();
        }

        #region Methods
        public void MoveShipLeft()
        {
            // TODO
            // Schiff nach links bewegen und verhindern, 
            // dass das Schiff den Bildschirm verlässt
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                shipPosition.X -= shipSpeed;
            }

        }

        public void MoveShipRight()
        {
            // TODO
            // Schiff nach rechts bewegen und verhindern, 
            // dass das Schiff den Bildschirm verlässt
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                shipPosition.X += shipSpeed;
            }
        }
        #endregion

        #region GetterSetter
        public Texture2D getShipTexture()
        {
            return ShipTexture;
        }

        public Vector2 getShipPosition()
        {
            return this.shipPosition;
        }

        public void setShipPosition(Vector2 position)
        {
            this.shipPosition = position;
        }

        public float getShipSpeed()
        {
            return shipSpeed;
        }

        public Texture2D getLaserTexture()
        {
            return LaserTexture;
        }

        public float getLaserSpeed()
        {
            return laserSpeed;
        }
        
        public SoundEffect getLaserSound()
        {
            return laserSound;
        }
        #endregion
    }
}
