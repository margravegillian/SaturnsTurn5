using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SaturnsTurn5
{
    class Projectile
    {
        //projectile image
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;
        public int Damage { get; set; }


        Viewport viewport;
        //get the width of the projectile
        public int Width
        {
            get { return Texture.Width; }
        }

        //height of projectile
        public int Height
        {
            get { return Texture.Height; }
        }
        //how fast projectile moves
        float projectileMoveSpeed;

        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position,int damage)
        {
            Texture = texture;
            Position = position;
            this.viewport = viewport;
            Active = true;
            this.Damage = damage;
           
            projectileMoveSpeed = 20f;
        }

        public void Update()
        {
            //projectiles move to the right always
            Position.X += projectileMoveSpeed;
            //deacivate the bullet if it goes out of screen
            if (Position.X + Texture.Width / 2 > viewport.Width)
                Active = false;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);

        }
    }
}
