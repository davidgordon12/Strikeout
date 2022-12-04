using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strikeout
{
    public class Target : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Vector2 position { get; set; }
        public Texture2D tex { get; set; }
        public Target(Game game, SpriteBatch spriteBatch, Vector2 position, Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v2
            spriteBatch.Draw(tex,position, Color.White); 
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
