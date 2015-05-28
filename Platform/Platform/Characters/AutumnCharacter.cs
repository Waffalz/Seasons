using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Platform.gameflow;
using Platform.control;
using Platform.mobs;
using Platform.world;
using Platform.userinterface;
using Platform.logger;
using Platform.projectiles;

namespace Platform.characters
{
    class AutumnCharacter : Player
    {
        float maxBoostTime;
        float boostTime;
        float boostAccel;
        float boostVelocityTolerance;


        UIHealthBar boostBar;

        public AutumnCharacter()
            : base()
        {
            texture = Game1.CurrentGame.Textures["AutumnAnim"];

            attack = 10;
            maxBoostTime = .75f;
            boostTime = maxBoostTime;
            boostAccel = 200;
            airControl = 1;
            boostVelocityTolerance = 30;

            controls.Add("Autumn Basic Attack", new ContinuousAction(this, 1,
                delegate() { return Game1.CurrentGame.MouseInput.LeftButton == ButtonState.Pressed; },
                delegate(GameTime gameTime) {
                    MouseState mouse = Game1.CurrentGame.MouseInput;
                    Vector2 dif = parent.Camera.PositionFromScreen(new Point(mouse.X, mouse.Y)) - this.Position;
                    dif.Normalize();
                    AutumnBasic gust = new AutumnBasic(this, 10);
                    gust.Position = this.Position;
                    gust.Velocity = dif * 100;
                    parent.AddEntity(gust);
                }));
            
            controls.Add("Aerial Boost", new ContinuousAction(this, 0,
                delegate() { return Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.W) || Game1.CurrentGame.KeyboardInput.IsKeyDown(Keys.Space); },
                delegate(GameTime gameTime) {
                    if (boostTime > 0 && velocity.Y < boostVelocityTolerance) {

                        float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        velocity.Y += boostAccel * timePassed * (boostTime / maxBoostTime) * 2;
                        boostTime -= timePassed;
                    }
                }));

            GameContext gameCont = Game1.CurrentGame.GameMode;

            if (gameCont is CombatContext) {
                boostBar = new UIHealthBar();
                boostBar.bounds = new Rectangle(50, 110, 350, 40);
                boostBar.vColor = Color.Gold;
                boostBar.mColor = Color.White;
                boostBar.depth = 1;
                ((CombatContext)gameCont).GameHUD.Add(boostBar);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (onGround) {
                boostTime = Math.Min(boostTime + timePassed, maxBoostTime);
            } else {
               
            }

            if (boostBar != null) {
                boostBar.MaxValue = maxBoostTime;
                boostBar.Value = boostTime;
            }

        }
    }
}
