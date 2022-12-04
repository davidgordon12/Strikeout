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
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Target target;
        private ScoreComponent score;
        private MouseState lastMouseState;
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            //instantiate all game components
            Texture2D targetTex = game.Content.Load<Texture2D>("images/target");
            Vector2 targetPos = new Vector2(Shared.stage.X / 2 - targetTex.Width / 2,
                Shared.stage.Y - targetTex.Height);
            target = new Target(game, spriteBatch, targetPos, targetTex);
            this.Components.Add(target);

            Vector2 scorePos = new Vector2(Shared.stage.X - 95, Shared.stage.Y - Shared.stage.Y + 10);
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            score = new ScoreComponent(game, spriteBatch, scoreFont, scorePos, 0);
            this.Components.Add(score);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

            //big mistake -- never write the following line
            //KeyboardState ks = new KeyboardState();
            //-------------------------------------------

            if (lastMouseState.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed)
            {
                if (target.getBounds().Contains(ms.Position))
                {
                    score.score++;

                    // remove the old target

                    this.Components.Remove(target);
                    // and create a new one somewhere else
                }
            }

            lastMouseState = ms;

            base.Update(gameTime);
        }
    }
}
