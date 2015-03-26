using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.UserInterface;
using Platform.Characters;
using Platform.Mobs;

namespace Platform.GameFlow
{
    class CharSelectContext: GameContext
    {
        private enum CharType { Spring, Summer, Autumn, Winter, None }


        UIComponent gui;

        UILabel modeTitle;

        UIComponent charSplash;
        UILabel charName;
        UILabel charDesc;

        UIButton playButton;

        private CharType selected;

        public CharSelectContext()
        {
            selected = CharType.None;

            gui = new UIComponent();
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.Gray;

            UIButton backButton = new UIButton(new Rectangle(0, 0, 100, 50), delegate()
            {
                Game1.CurrentGame.GameMode = new MainMenuContext(); 
            }, "Back");
            gui.Add(backButton);

            modeTitle = new UILabel();
            modeTitle.bounds = new Rectangle(100, 0, Game1.CurrentGame.Window.ClientBounds.Width-100, 50);
            modeTitle.text = "Character Select";
            modeTitle.hAlign = HorizontalTextAlignment.Center;
            modeTitle.vAlign = VerticalTextAlignment.Center;
            gui.Add(modeTitle);

            charSplash = new UIComponent();
            charSplash.bounds = new Rectangle(
                Game1.CurrentGame.Window.ClientBounds.Width - 600,
                Game1.CurrentGame.Window.ClientBounds.Height - 650,
                600, 650);
            charSplash.color = Color.LightGray;
            charSplash.depth = 0;
            gui.Add(charSplash);
            
            charDesc = new UILabel();
            charDesc.bounds = new Rectangle(10, Game1.CurrentGame.Window.ClientBounds.Height - 260, Game1.CurrentGame.Window.ClientBounds.Width - 20, 250); 
            gui.Add(charDesc);

            charName = new UILabel();
            charName.bounds = new Rectangle(Game1.CurrentGame.Window.ClientBounds.Width - 260, charDesc.bounds.Y - 90, 250, 60);
            charName.hAlign = HorizontalTextAlignment.Right;
            charName.vAlign = VerticalTextAlignment.Center;
            charName.Border = UIBorder.Scroll;
            gui.Add(charName);

            UIButton springSelect = new UIButton(new Rectangle(50, 100, 150, 150), delegate() {
                selected = CharType.Spring;
                charName.text = "SPRINGNAME";
                charDesc.text = "SPRINGDESC";
                //TODO: set splash for selected char
            }, "Spring");
            gui.Add(springSelect);

            UIButton summerSelect = new UIButton(new Rectangle(210, 100, 150, 150), delegate() {
                selected = CharType.Summer;
                charName.text = "SUMMERNAME";
                charDesc.text = "SUMMERDESC";
                //TODO: set splash for selected char

            }, "Summer");
            gui.Add(summerSelect);

            UIButton autumnSelect = new UIButton(new Rectangle(210, 260, 150, 150), delegate() {
                selected = CharType.Autumn;
                charName.text = "AUTUMNNAME";
                charDesc.text = "AUTUMNDESC";
                //TODO: set splash for selected char
            }, "Autumn");
            gui.Add(autumnSelect);

            UIButton winterSelect = new UIButton(new Rectangle(50, 260, 150, 150), delegate() {
                selected = CharType.Winter;
                charName.text = "WINTERNAME";
                charDesc.text = "WINTERDESC";
                //TODO: set splash for selected char
            }, "Winter");
            gui.Add(winterSelect);

            playButton = new UIButton(new Rectangle(370, 100, 150, 60), delegate() {
                switch (selected){
                    case CharType.Spring: Game1.CurrentGame.Player = new SpringCharacter(); break;
                    case CharType.Summer: Game1.CurrentGame.Player = new SummerCharacter(); break;
                    case CharType.Autumn: Game1.CurrentGame.Player = new AutumnCharacter(); break;
                    case CharType.Winter: Game1.CurrentGame.Player = new TestPlayer(); break;
                    default: Game1.CurrentGame.Player = new Player(); break;
                }
                Game1.CurrentGame.GameMode = new CombatContext();
            }, "Start Game");
            playButton.visible = false;
            gui.Add(playButton);


        }

        public override void Update(GameTime gameTime)
        {
            playButton.visible = selected != CharType.None;
            gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }

    }
}
