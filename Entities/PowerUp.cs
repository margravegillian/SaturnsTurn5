using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace SaturnsTurn5
{
    class PowerUp
    {

        public int Shield { get; set; }
        public Texture2D PowerUpTexture;
        public Vector2 Position;
        public bool Active;
        //public List<String> PowerUpType;
        public int Damage { get; set; }
        //public string ShieldPowerUp;
       // public string DamagePowerUp;
        public string PowerUpName;
        public Player Player { get; set; }
        //todo add move speed etc..




        Viewport viewport;
        //get the width of the projectile
        public int Width
        {
            get { return PowerUpTexture.Width; }
        }

        //height of projectile
        public int Height
        {
            get { return PowerUpTexture.Height; }
        }
        //how fast projectile moves
        float PowerUpMoveSpeed;


        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position, string powerUpName, Player player)
        {

            Player = player;
            PowerUpTexture = texture;
            Position = position;
            this.viewport = viewport;
            Active = true;
            this.Damage = Damage;
            PowerUpName = powerUpName;
           // PowerUpType = new List<String>();
            //PowerUpType.Add(PowerUpName);
            PowerUpMoveSpeed = 3f;







        }


        public void PowerUpCollision()
        {
            if (PowerUpName == "DamagePowerUp")
            {
                UpdateDamage();
            }
            else if (PowerUpName == "ShieldPowerUp")
            {
                UpdateShield();
            }

        }
        //not used yet
        private void UpdateDamage()
        {
            //todo
            if (Player.DamageMod <= 20)
            {
                Player.DamageMod += 5;

            }
            

        }

        private void UpdateShield()
        {

            if (Player.Shield < 200)
            {
                Player.Shield += 100;
                if (Player.Shield > 200)
                {
                    Player.Shield = 200;
                }
            }
            

            

        }
        public void Update(GameTime gameTime)
        {
            



            Position.X -= PowerUpMoveSpeed;
            //deacivate the powerupif it goes out of screen
            if (Position.X < -Width)
            {
                //by setting the active flat to false the game will remove this object

                //active game list
                Active = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {





            spriteBatch.Draw(PowerUpTexture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);

        }



    }
}
