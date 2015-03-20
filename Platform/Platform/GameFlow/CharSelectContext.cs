using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;
using Platform.Characters;

namespace Platform.GameFlow
{
    class CharSelectContext: GameContext
    {


        UIComponent gui;

        UILabel modeTitle;

        UILabel charName;
        UILabel charDesc;

        public CharSelectContext()
        {
            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.Gray;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext(); 
            }, "Back");
            gui.Add(backButton);

            UIButton springSelect = new UIButton(new Rectangle(100, 100, 150, 150), delegate() { }, "Spring");
            gui.Add(springSelect);

            UIButton summerSelect = new UIButton(new Rectangle(260, 100, 150, 150), delegate() { }, "Summer");
            gui.Add(summerSelect);

            UIButton autumnSelect = new UIButton(new Rectangle(260, 260, 150, 150), delegate() { }, "Autumn");
            gui.Add(autumnSelect);

            UIButton winterSelect = new UIButton(new Rectangle(100, 260, 150, 150), delegate() { }, "Winter");
            gui.Add(winterSelect);

            modeTitle = new UILabel();
            modeTitle.bounds = new Rectangle(100, 0, Game1.CurrentGame.Window.ClientBounds.Width-100, 50);
            modeTitle.text = "Character Select";
            gui.Add(modeTitle);

            //TODO: implement selection of character, start button, char descriptions and such

        }

        public override void Update(GameTime gameTime)
        {
            //Game1.CurrentGame.Player = new Player();
            //Game1.CurrentGame.GameMode = new CombatContext();
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }

    }
}
