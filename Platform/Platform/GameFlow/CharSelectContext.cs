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

        UILabel charPick;
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

            
            //TODO: char descriptions and such

            charName = new UILabel();
            
            charDesc = new UILabel();
            charDesc.bounds = new Rectangle(160, Game1.CurrentGame.Window.ClientBounds.Height - 50, Game1.CurrentGame.Window.ClientBounds.Width - 140, 300);
            charDesc.Add(charDesc);

            UIButton springSelect = new UIButton(new Rectangle(50, 100, 150, 150), delegate() {
                selected = CharType.Spring;
            }, "Spring");
            gui.Add(springSelect);

            UIButton summerSelect = new UIButton(new Rectangle(210, 100, 150, 150), delegate() {
                selected = CharType.Summer;
            }, "Summer");
            gui.Add(summerSelect);

            UIButton autumnSelect = new UIButton(new Rectangle(210, 260, 150, 150), delegate() {
                selected = CharType.Autumn;
            }, "Autumn");
            gui.Add(autumnSelect);

            UIButton winterSelect = new UIButton(new Rectangle(50, 260, 150, 150), delegate() {
                selected = CharType.Winter;
            }, "Winter");
            gui.Add(winterSelect);

            playButton = new UIButton(new Rectangle(10, Game1.CurrentGame.Window.ClientBounds.Height - 70, 150, 60), delegate() {
                switch (selected){
                    case CharType.Spring: Game1.CurrentGame.Player = new SpringCharacter(); break;
                    case CharType.Summer: Game1.CurrentGame.Player = new SummerCharacter(); break;
                    case CharType.Autumn: Game1.CurrentGame.Player = new AutumnCharacter(); break;
                    case CharType.Winter: Game1.CurrentGame.Player = new WinterCharacter(); break;
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
