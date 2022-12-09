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
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Texture2D tex;
        public AboutScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/about");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Q))
            {
                // Hides the current scene 
                AboutScene ab = (AboutScene)g.Components[2];
                ab.hide();

                // Pulls up the menu scene once more
                StartScene s1 = (StartScene)g.Components[0];
                s1.show();
            }
        }
    }
}
