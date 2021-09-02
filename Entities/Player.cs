using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaturnsTurn5.Utility;
namespace SaturnsTurn5
{
    class Player
    {

        #region fields
        //public Texture2D PlayerTexture3;switch to playeranimation
        public Animation PlayerAnimation;
        public Vector2 Position3;
        public bool Active;
        public int Health;
        public int Shield { get; set; }
        public bool shieldActive;
        public int DamageMod;
        public int Damage { get; set; }
        public int Power { get; set; }

        public int Score;

        public string Name { get; set; }

        public float Scale
        {
            get { return PlayerAnimation.scale; }
        }
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }
        #endregion


        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
           // PlayerTexture3 = texture;
            Position3 = position;
            Active = true;
            Health = 100;

            Score = 0;
            Power = 10;
            DamageMod = 0;
            shieldActive = false;
            Damage = 10;
            Shield = 0;




        }
        public void Respawn()
        {
            Active = true;
            Health = 100;
            shieldActive = false;
            Shield = 0;

        }

        public void Reset()
        {
            Active = true;
            Health = 100;
            Score = 0;
            Power = 10;
            DamageMod = 0;
            Damage = 1;
            Shield = 0;
            shieldActive = false;
        }

        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position3;
            PlayerAnimation.Update(gameTime);
            

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }


    }
}
