using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Platform.userinterface;
using Microsoft.Xna.Framework.Graphics;

namespace Platform.gameflow
{
    class VictoryContext : GameContext
    {
        UIComponent gui;
        
        public VictoryContext()
        {
            gui = new UIComponent(); 
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.Gray;
            
            UILabel titleLabel = new UILabel();
            titleLabel.text = "YOU WIN";
            titleLabel.bounds = new Rectangle(Game1.CurrentGame.Window.ClientBounds.Width / 2, 200, 0, 0);
            titleLabel.hAlign = HorizontalTextAlignment.Center;
            titleLabel.textColor = Color.Black;
            gui.Add(titleLabel);

            UIButton menuButton = new UIButton(new Rectangle(0, 300, 300, 100), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext();
            }, "Main Menu");
            menuButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - menuButton.bounds.Width) / 2;
            menuButton.Border = UIBorder.Scroll;
            gui.Add(menuButton);

            UIButton exitButton = new UIButton(new Rectangle(0, 420, 300, 60), delegate()
            {
                Game1.CurrentGame.Exit();
            }, "Exit");
            exitButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - menuButton.bounds.Width) / 2;
            exitButton.Border = UIBorder.Scroll;
            gui.Add(exitButton);
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }
    }
}
