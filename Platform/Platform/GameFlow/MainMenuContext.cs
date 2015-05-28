using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.userinterface;
using Platform.mobs;
using Platform.logger;
using Microsoft.Xna.Framework.Media;

namespace Platform.gameflow
{
    public class MainMenuContext: GameContext
    {
        public UIComponent gui;

        public UIComponent GUI
        {
            get { return gui; }
            set { gui = value; }
        }

        public MainMenuContext()
        {
            gui = new UIComponent();
            gui.texture = Game1.CurrentGame.Textures["MenuBack"];
            gui.sourceRect = gui.texture.Bounds;
            gui.bounds = new Rectangle(0, 0, Game1.CurrentGame.Window.ClientBounds.Width, Game1.CurrentGame.Window.ClientBounds.Height);
            gui.color = Color.White;

            //Title
            UILabel titleLabel = new UILabel();
            titleLabel.text = "ARISTAEA";
            titleLabel.bounds = new Rectangle(Game1.CurrentGame.Window.ClientBounds.Width/2, 200,0,0);
            titleLabel.hAlign = HorizontalTextAlignment.Center;
            titleLabel.textColor = Color.Black;
            gui.Add(titleLabel);

            UIButton startButton = new UIButton(new Rectangle(0, 300, 300, 100), delegate()
            {
                Game1.CurrentGame.GameMode = new LevelSelectContext();
            }, "New Game");
            startButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - startButton.bounds.Width) / 2;
            startButton.Border = UIBorder.Scroll;
            gui.Add(startButton);

            if (Game1.CurrentGame.SaveCombatContext != null)
            {
                UIButton resumeGameButton = new UIButton(new Rectangle(1, 410, 300, 60), delegate()
                {
                    Game1.CurrentGame.GameMode = Game1.CurrentGame.SaveCombatContext;
                }, "Resume Game");
                resumeGameButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - resumeGameButton.bounds.Width) / 2;
                resumeGameButton.Border = UIBorder.Scroll;
                gui.Add(resumeGameButton);
            }

            UIButton controlButton = new UIButton(new Rectangle(0, 480, 300, 60), delegate()
            {
                Game1.CurrentGame.GameMode = new ControlContext(this);
            }, "Controls");
            controlButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - controlButton.bounds.Width) / 2;
            controlButton.Border = UIBorder.Scroll;
            gui.Add(controlButton);

            UIButton creditsButton = new UIButton(new Rectangle(0, 550, 300, 60), delegate()
            {
                Game1.CurrentGame.GameMode = new CreditsContext(this);
            }, "Credits");
            creditsButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - creditsButton.bounds.Width) / 2;
            creditsButton.Border = UIBorder.Scroll;
            gui.Add(creditsButton);

            UIButton exitButton = new UIButton(new Rectangle(0, 620, 300, 60), delegate()
            {
                Game1.CurrentGame.Exit();
            }, "Exit");
            exitButton.bounds.X = (Game1.CurrentGame.Window.ClientBounds.Width - exitButton.bounds.Width) / 2;
            exitButton.Border = UIBorder.Scroll;
            gui.Add(exitButton);
        }

        public override void Update(GameTime gameTime)
        {
            gui.Update(gameTime);
            MediaPlayer.Volume = 1.0f;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gui.Draw(gameTime, spriteBatch);
        }

    }
}
