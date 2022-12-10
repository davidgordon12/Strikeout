using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension; // 64x64
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int ROWS = 5;
        private const int COLS = 5;

        public Explosion(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            Hide();

            //create all frames
            CreateFrames();

            Show();
        }

        private void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        private void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }


        public void Restart()
        {
            frameIndex = -1;

            delayCounter = 0;
            Show();
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y,
                        (int)dimension.X, (int)dimension.Y);

                    frames.Add(r);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if(delayCounter > delay)
            {
                frameIndex++;
                if(frameIndex > frames.Count - 1)
                {
                    frameIndex = -1;
                    Hide();
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                //version 4
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.Wheat);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
