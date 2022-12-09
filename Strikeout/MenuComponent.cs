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
    public class MenuComponent : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;
        
        private List<string> menuItems;
        public int selectedIndex { get; set; }
        private Vector2 position;

        private KeyboardState oldState;

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menus) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) )
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex  == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            
            if(ks.IsKeyDown(Keys.Enter))
            {
                switch (selectedIndex)
                {
                    case 0:
                        StartScene s1 = (StartScene)game.Components[0];
                        s1.hide();

                        ActionScene g = (ActionScene)game.Components[3];
                        g.show();
                        break;
                    case 1:
                        StartScene s2 = (StartScene)game.Components[0];
                        s2.hide();

                        HelpScene h = (HelpScene)game.Components[1];
                        h.show();
                        break;
                    case 2:
                        StartScene s3 = (StartScene)game.Components[0];
                        s3.hide();

                        AboutScene ab = (AboutScene)game.Components[2];
                        ab.show();
                        break;
                    case 3:
                        Environment.Exit(1);
                        break;

                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i], 
                        tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                       tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
