using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Strikeout
{
    public class GameOverComponent : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        private Color regularColor = Color.Black;
        public int score { get; set; }

        public GameOverComponent(Game game,
            SpriteBatch spriteBatch,
            int score) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            this.score = score;
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();

            spriteBatch.DrawString(regularFont, "Good job!",
                        new Vector2(Shared.stage.X / 2, (Shared.stage.Y + 50)), regularColor);

            spriteBatch.DrawString(regularFont, $"Your score was: {score}",
                        new Vector2(Shared.stage.X / 2, (Shared.stage.Y + 150)), regularColor);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
