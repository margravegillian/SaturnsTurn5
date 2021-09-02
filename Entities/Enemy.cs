using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaturnsTurn5.Utility;
namespace SaturnsTurn5
{
    class Enemy
    {
        #region fields
        //animation for the enemy
        public Animation EnemyAnimation;
        //the position of enemy of ship
        public Vector2 Position;
        //state of the shipo
        public bool Active;
        public int Health;
        //the damage the enemy inflicts
        public int Damage;
        public bool OnScreen;
        //the score you will get from killing the enemy
        public int Value;
        //get the width of the enemy ship
        public int Width
        {
            get{return EnemyAnimation.FrameWidth;}
        }

        //get the height of the enemy ship
        public int Height
        {
            get {return EnemyAnimation.FrameHeight;}
        }
        //the speed of the enemy
        public float enemyMoveSpeed;
        #endregion
        #region Methods

        public void Initialize(Animation animation, Vector2 position)
        {
            //load enemy ship texture
            this.EnemyAnimation = animation;
            //set the position of enemy
            this.Position = position;
            // initizlize the enemy to be active
            Active = true;
            //set the helath
            Health = 10;
            //set damage it can do
            Damage = 5;
            // set how fast the enemy moves
            enemyMoveSpeed = 5f;
            //set the score value
            Value = 100;
            OnScreen = true;
        }
        public void Update(GameTime gameTime)
        {
            //the enemy always move to the left so decrement its xposition
            Position.X -= enemyMoveSpeed;
            //update the position of the animation
            EnemyAnimation.Position = Position;
            //update animation
            EnemyAnimation.Update(gameTime);
            //if the enemy is past the screen or its health reaches 0 then deactivate it
            if (Position.X < -Width)
            {
                //by setting the active flat to false the game will remove this object

                //active game list
               // Active = false;
                OnScreen = false;
            }
            if (Health<=0)
            {
                Active = false;
                OnScreen = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the animation
            EnemyAnimation.Draw(spriteBatch);

        }
        #endregion

    }
}
