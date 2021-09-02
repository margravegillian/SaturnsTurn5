using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaturnsTurn5.Utility;


namespace SaturnsTurn5.Entities
{
    public abstract class Entity
    {

        #region fields

        Viewport viewport;
        public Texture2D Texture;
        //animation for the enemy
        public Animation EntityAnimation;
        //the position of enemy of ship
        public Vector2 Position;
        //state of the shipo
        public bool Active;
        public int Health;
        //the damage the enemy inflicts
        public int Damage;
        public bool OnScreen;
        //the score you will get from killing the entity
        public int Value;
        //get the width of the enemy ship
        public int Width
        {
            get { return Texture.Width; } 

        }
        //get the height of the enemy ship
        public int Height
        {
            get { return Texture.Height; }
        }
            
        
        //the speed of the enemy
        public float entityMoveSpeed;
        #endregion

        #region Methods

        //texture entity
        public virtual void Initialize(Viewport viewport, Texture2D texture, Vector2 position, int damage)
        {
            Texture = texture;
            this.Position = position;
            this.viewport = viewport;
            Active = true;
            Health = 0;
            Value = 100;
            OnScreen = true;
            this.Damage = damage;

            entityMoveSpeed = 0f;
        }
        //animated entity
        public virtual void Initialize(Animation animation, Vector2 position)
        {

            //abstract or virtual method lookup
            //load enemy ship texture
            this.EntityAnimation = animation;
            //set the position of enemy
            this.Position = position;
            // initizlize the enemy to be active
            Active = true;
            //set the helath
            Health = 0;
            //set damage it can do
            Damage = 0;
            // set how fast the enemy moves
            entityMoveSpeed = 0f;
            //set the score value
            Value = 100;
            OnScreen = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            //the enemy always move to the left so decrement its xposition
            //Position.X -= entityMoveSpeed;
            //update the position of the animation
           // EntityAnimation.Position = Position;
            //update animation
           // EntityAnimation.Update(gameTime);
            
            
            //if the enemy is past the screen or its health reaches 0 then deactivate it
           /* if (Position.X < -Width)
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
            */

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //draw the animationexample
           // EntityAnimation.Draw(spriteBatch);

            //example texture draw
           // spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);

        }

        #endregion
    }
}
