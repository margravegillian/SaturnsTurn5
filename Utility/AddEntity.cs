using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnsTurn5.Utility
{
    public class AddEntity
    {
        Animation animation;
        Vector2 position;
        Texture2D texture;

        public void AddToGame(Animation animation, Vector2 position, Texture2D texture)
        {
            this.animation = animation;
            this.position = position;
            this.texture = texture;

            animation = new Animation();

        }
    }
}
