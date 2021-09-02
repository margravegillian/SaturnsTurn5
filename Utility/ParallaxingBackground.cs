using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace SaturnsTurn5.Utility
{
    class ParallaxingBackground
    {

        #region Fields
        Texture2D texture;
        Vector2[] positions;
        int speed;
        #endregion
        #region Methods
        public void Initialize(ContentManager content, String texturePath, int screenWidth, int speed)
        {

            //load the background
            texture = content.Load<Texture2D>(texturePath);
            this.speed = speed;
            positions = new Vector2[screenWidth / texture.Width + 1];

            for (int i = 0; i < positions.Length; i++)
            {
                //need the tiles side by side
                positions[i] = new Vector2(i * texture.Width, 0);
            }

        }

        public void Update()
        {
            // update the positions of teh background
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed;
                //if the speed has the background moving to the left

                if (speed <= 0)
                {
                    //check texture is out of view then put that texture at the end of the screen
                    if (positions[i].X <= -texture.Width)
                    {
                        positions[i].X = texture.Width * (positions.Length - 1);
                    }
                }
                //if the speed has the background moving to the right
                else
                {
                    // check if the texture is out of view then position it to the start of the screen
                    if (positions[i].X >= texture.Width * (positions.Length - 1))
                    {
                        positions[i].X = -texture.Width;
                    }
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }

        }


        #endregion

    }
}
