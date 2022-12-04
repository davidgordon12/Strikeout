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
    public class StartScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        public MenuComponent Menu { get; set; }
        string[] menuItems = { "Start Game", 
            "Help", 
            "High Score", 
            "Credit", 
            "Quit" 
        };


        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/hilightFont");
            Menu = new MenuComponent(game, spriteBatch, 
                regularFont,hilightFont, menuItems);
            this.Components.Add(Menu);
        }
    }
}
