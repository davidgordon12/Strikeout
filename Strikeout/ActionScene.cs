using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
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
        private Texture2D targetTex;
        private MouseState lastMouseState;
        private Timer timer;
        private Stopwatch stopwatch;
        private GameOverComponent go;
        public ActionScene(Game game) : base(game)
        {
            timer = new();
            stopwatch = new();

            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            //instantiate all game components
            targetTex = game.Content.Load<Texture2D>("images/target");
            Vector2 targetPos = new Vector2(Shared.stage.X / 2,
                Shared.stage.Y / 2);
            target = new Target(game, spriteBatch, targetPos, targetTex);
            this.Components.Add(target);

            Vector2 scorePos = new Vector2(Shared.stage.X - 105, Shared.stage.Y - Shared.stage.Y + 10);
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            score = new ScoreComponent(game, spriteBatch, scoreFont, scorePos, 0);
            this.Components.Add(score);
        }

        public override void Update(GameTime gameTime)
        {
            // Send the time to the ScoreComponent to be updated
            if (stopwatch.IsRunning)
                score.elapsed = (int)stopwatch.ElapsedMilliseconds / 1000;

            // Only start the timer once the first target is popped
            if(score.score == 1 && !stopwatch.IsRunning)
                StartTimer();

            MouseState ms = Mouse.GetState();

            //Check if the left button was clicked and released
            if (lastMouseState.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed)
            {
                // If the cursor was in the bounds of our target object when clicked,
                // add a point
                if (target.getBounds().Contains(ms.Position))
                {
                    score.score++;

                    // remove the old target
                    // and create a new one somewhere else
                    GenerateNewTarget();
                }
            }

            lastMouseState = ms;

            base.Update(gameTime);
        }

        void GenerateNewTarget()
        {
            // Grab a random coordinate within the game's bounds
            Random rd = new Random();
            int rand_num_X = rd.Next(50, (int)Math.Round(Shared.stage.X - 50));
            int rand_num_Y = rd.Next(50, (int)Math.Round(Shared.stage.Y - 50));

            // Since we only have 1 target at a time,
            // remove the old target before adding a new one
            this.Components.Remove(target);
            Vector2 targetPos = new Vector2(rand_num_X, rand_num_Y);
            target = new Target(g, spriteBatch, targetPos, targetTex);
            this.Components.Add(target);
        }

        void StartTimer()
        {
            // Starts the countdown timer
            timer.Elapsed += new ElapsedEventHandler(EndTimer); // After 10 seconds has passed 
            timer.Interval = 10000;                             // Call my EndTimer() method
            timer.Enabled = true;
            stopwatch.Start();
        }

        void EndTimer(object source, ElapsedEventArgs e)
        {
            // Disable the timer
            timer.Enabled = false;
            stopwatch.Stop();
            timer.Dispose();

            this.Components.Remove(target);

            MessageBox.Show("Strikeout", $"Time's up! You scored {score.score} points", new[] { "Ok" });

            // Hides the current scene 
            ActionScene a1 = (ActionScene)g.Components[3];
            a1.hide();

            g.RestartGame(); // Calls the RestartGame method in Game1.cs

            // And finally, pulls up the menu scene once more
            StartScene s1 = (StartScene)g.Components[0];
            s1.show();

        }
    }
}
