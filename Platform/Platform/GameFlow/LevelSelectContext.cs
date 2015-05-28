using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.userinterface;
using Platform.mobs;
using Microsoft.Xna.Framework.Media;

namespace Platform.gameflow
{
    public class LevelSelectContext : GameContext
    {
        private enum NumberType { one = 1, two = 2, three = 3, four = 4, five = 5, none }

        UIComponent gui;

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        UILabel modeTitle;

        UIButton nextButton;

        private NumberType selected;

        public LevelSelectContext()
        {
            selected = NumberType.none;

            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.Gray;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext();
            }, "Back");
            gui.Add(backButton);

            modeTitle = new UILabel();
            modeTitle.bounds = new Rectangle(100, 0, Game1.CurrentGame.Window.ClientBounds.Width - 100, 50);
            modeTitle.text = "Level Select";
            modeTitle.hAlign = HorizontalTextAlignment.Center;
            modeTitle.vAlign = VerticalTextAlignment.Center;
            gui.Add(modeTitle);

            UIButton levelOne = new UIButton(new Rectangle(200, 200, 150, 150), delegate()
            {
                selected = NumberType.one;
            }, "Level One");
            gui.Add(levelOne);

            UIButton levelTwo = new UIButton(new Rectangle(450, 200, 150, 150), delegate()
            {
                selected = NumberType.two;
            }, "Level Two");
            gui.Add(levelTwo);

            UIButton levelThree = new UIButton(new Rectangle(700, 200, 150, 150), delegate()
            {
                selected = NumberType.three;
            }, "Level Three");
            gui.Add(levelThree);

            UIButton levelFour = new UIButton(new Rectangle(325, 400, 150, 150), delegate()
            {
                selected = NumberType.four;
            }, "Level Four");
            gui.Add(levelFour);

            UIButton levelFive = new UIButton(new Rectangle(575, 400, 150, 150), delegate()
            {
                selected = NumberType.five;
            }, "Level Five");
            gui.Add(levelFive);

            nextButton = new UIButton(new Rectangle(Game1.CurrentGame.Window.ClientBounds.Width - 100, 0, 100, 50), delegate()
            {
                switch (selected)
                {
                    case NumberType.one: level = 1; break;
                    case NumberType.two: level = 2; break;
                    case NumberType.three: level = 3; break;
                    case NumberType.four: level = 4; break;
                    case NumberType.five: level = 5; break;
                    default: level = 1; break;
                }
                CharSelectContext nextSlide = new CharSelectContext(level);
                Game1.CurrentGame.GameMode = nextSlide;
            }, "Next");
            nextButton.visible = false;
            gui.Add(nextButton);
        }

        public override void Update(GameTime gameTime)
        {
            nextButton.visible = selected != NumberType.none;
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }
    }
}
