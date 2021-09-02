using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaturnsTurn5.Utility;

namespace SaturnsTurn5.Entities
{
   
    class FireHair : Entity
    {

        Texture2D fireHairTexture;
        //public int Width
        //{
        //    get { return fireHairTexture.Width; }
        //}

        //height of projectile
        //public int Height
        //{
        //    get { return fireHairTexture.Height; }
        //}

        public override void Initialize(Viewport viewport, Texture2D texture, Vector2 position, int damage)
        {
            base.Initialize(viewport, texture, position, damage);
            this.fireHairTexture = texture;
            damage = 500;
            Health = 50;
            entityMoveSpeed = 3f;
        }


        public override void Update(GameTime gameTime)
        {
            //the enemy always move to the left so decrement its xposition
            Position.X -= entityMoveSpeed;
            //EntityAnimation.Position = Position;

            if (Position.X < -Width)
            {
                //by setting the active flat to false the game will remove this object

                //active game list
                // Active = false;
                OnScreen = false;
            }
            if (Health <= 0)
            {
                Active = false;
                OnScreen = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);
        }

        

    }
}
