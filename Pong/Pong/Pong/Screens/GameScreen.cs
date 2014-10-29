using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FontEffectsLib.FontTypes;
using Pong.CoreTypes;
using System.Xml.Linq;



namespace Pong.Screens
{
    public class GameScreen : BaseScreen
    {
        Ball ball;
        Paddle leftPaddle;
        Paddle rightPaddle;
        PlusOne plusOne;

        FadingFont leftScoreFont;
        FadingFont rightScoreFont;

        FadingFont player1Font;
        FadingFont player2Font;

        
        KeyboardState keyboard;

        Random rnd = new Random();

        public static bool player1Won = false;

        bool leftSideScored = false;

        //Keys player1Up = Keys.W;
        

        int ballDirection;
        int paddleSpeed = 8;

        int leftScore = 0;
        int rightScore = 0;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            leftPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(0, _viewPort.Height / 2), Color.White);
            leftPaddle.SetCenterAsOrigin();
            leftPaddle.Position = new Vector2(leftPaddle.Origin.X, _viewPort.Height / 2);
            //leftPaddle.UpKey = Keys.W;
            //leftPaddle.DownKey = Keys.S;

            Global.Player1 = leftPaddle;

            rightPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(_viewPort.Width - leftPaddle.Texture.Width / 2, _viewPort.Height / 2), Color.White);
            rightPaddle.SetCenterAsOrigin();
            //rightPaddle.UpKey = Keys.Up;
            //rightPaddle.DownKey = Keys.Down;

            Global.Player2 = rightPaddle;

            XDocument optionsXml = XDocument.Load(@"XML\Options.xml");
            
            //Ben's dumb work around because he hasn't worked with xdoc enough
            XElement player1 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[0];
            XElement player2 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[1];
            
            leftPaddle.UpKey = (Keys)int.Parse(player1.Element(XName.Get("Up")).Value);
            leftPaddle.DownKey = (Keys)int.Parse(player1.Element(XName.Get("Down")).Value);
            rightPaddle.UpKey = (Keys)int.Parse(player2.Element(XName.Get("Up")).Value);
            rightPaddle.DownKey = (Keys)int.Parse(player2.Element(XName.Get("Down")).Value);

            ball = new Ball(Content.Load<Texture2D>("temp ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            ball.SetCenterAsOrigin();

            plusOne = new PlusOne(Content.Load<Texture2D>("Plus1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.Red);
            plusOne.SlideCompleted += new FontEffectsLib.SpriteTypes.SlidingSprite.SlideCompletedState(plusOne_SlideCompleted);

            leftScoreFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(10, 0), 0.1f, 1.0f, 0.01f, 1.0f, leftScore.ToString(), Color.White, false);
            leftScoreFont.EnableShadow = false;


            rightScoreFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width - 30, 0), 0.1f, 1.0f, 0.01f, 1.0f, rightScore.ToString(), Color.White, false);
            rightScoreFont.EnableShadow = false;

            player1Font = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(10, 50), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player1"), Color.White, false);
            player1Font.EnableShadow = false;
            player2Font = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width - 85, 50), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player2"), Color.White, false);
            player2Font.EnableShadow = false;



            _sprites.Add(rightPaddle);
            _sprites.Add(leftPaddle);
            //_sprites.Add(ball);
            _sprites.Add(plusOne);
            _sprites.Add(leftScoreFont);
            _sprites.Add(rightScoreFont);
            _sprites.Add(player1Font);
            _sprites.Add(player2Font);

        }

        void plusOne_SlideCompleted()
        {
            if (leftSideScored)
            {
                leftScore += 1;
                leftScoreFont.Text.Clear().Append(leftScore);
                leftScoreFont.Fade = true;
            }
            else
            {
                rightScore += 1;
                rightScoreFont.Text.Clear().Append(rightScore);
                rightScoreFont.Fade = true;
            }

            //Reset the ball
            ball.BallState = BallState.Rested;
            ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width / 2, _viewPort.Height / 2 - ball.Texture.Height / 2);
            ball.Speed = Vector2.Zero;
            ball.TintColor = Color.White;


            //Check for win
            if (leftScore >= 10)
            {
                player1Won = true;
                ScreenManager.Change(ScreenState.GameOver);
                
            }
            else if (rightScore >= 10)
            {
                player1Won = false;
                ScreenManager.Change(ScreenState.GameOver);

            }

        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (gameTime.IsRunningSlowly)
            {
                //System.Diagnostics.Debugger.Break();
            }

            if(keyboard.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Change(ScreenState.Pause);
            }

            if (keyboard.IsKeyDown(Keys.Space))
             {
                if (ball.BallState == BallState.Rested)
                {
                    ballDirection = rnd.Next(0, 2);

                    switch (ballDirection)
                    {
                        case 0:
                            ball.Speed = new Vector2(5, 2);
                            break;

                        case 1:
                            ball.Speed = new Vector2(-5, 2);
                            break;

                        default:
                            break;
                    }
                }

                ball.BallState = BallState.Moving;
            }

            if (ball.BallState == BallState.Moving)
            {
                if (ball.Position.Y + ball.Origin.Y >= _viewPort.Height)
                {
                  
                    if (ball.SpeedY > 0)
                    {
                        ball.SpeedY *= -1;
                    }

                }
                else if (ball.Position.Y - ball.Origin.Y <= 0)
                {
                    if (ball.SpeedY < 0)
                    {
                        ball.SpeedY *= -1;
                    }
                }

                if (ball.Position.X - ball.Origin.X <= 0)
                {
                    //Ball goes through the wall
                    //right scored

                    ball.Speed = Vector2.Zero;
                    ball.TintColor = Color.Red;

                    leftSideScored = false;

                    plusOne.Position = new Vector2(_viewPort.Width * 3 / 4 - plusOne.Texture.Width / 2, _viewPort.Height / 2 - plusOne.Texture.Height / 2);
                    plusOne.SlideTo = new Vector2(_viewPort.Width - 30, 0);
                    plusOne.IsVisible = true;

                }
                else if (ball.Position.X + ball.Origin.X >= _viewPort.Width)
                {
                    //Ball goes through the wall
                    //left scored

                    ball.Speed = Vector2.Zero;
                    ball.TintColor = Color.Red;

                    leftSideScored = true;

                    plusOne.Position = new Vector2(_viewPort.Width * 1 / 4 - plusOne.Texture.Width / 2, _viewPort.Height / 2 - plusOne.Texture.Height / 2);
                    plusOne.SlideTo = new Vector2(20, 0);
                    plusOne.IsVisible = true;

                }

            }
            
            ////STAN: This code might need to live elsewhere... maybe... for sure... I think... 
            //Keys[] pressedKeys = keyboard.GetPressedKeys();
            //if (pressedKeys.Length > 0)
            //{
            //    Keys firstPressedKey = pressedKeys[0];
            //}
            ////...

            //Rightpaddle Movement
            if (keyboard.IsKeyDown(rightPaddle.UpKey) && rightPaddle.Position.Y - rightPaddle.Origin.Y > 0)
            {
                rightPaddle.VectorY -= paddleSpeed;
            }

            if (keyboard.IsKeyDown(rightPaddle.DownKey) && rightPaddle.Position.Y + rightPaddle.Origin.Y < _viewPort.Height)
            {
                rightPaddle.VectorY += paddleSpeed;
            }

            //Leftpaddle Movement
            if (keyboard.IsKeyDown(leftPaddle.UpKey) && leftPaddle.Position.Y - leftPaddle.Origin.Y > 0)
            {
                leftPaddle.VectorY -= paddleSpeed;
            }

            if (keyboard.IsKeyDown(leftPaddle.DownKey) && leftPaddle.Position.Y + leftPaddle.Origin.Y < _viewPort.Height)
            {
                leftPaddle.VectorY += paddleSpeed;
            }

            Vector2 amountSpeed = Vector2.Zero;

            while (Math.Abs(amountSpeed.X) < Math.Abs(ball.Speed.X))
            {
                Vector2 speed = ball.Speed;
                
                speed.Normalize();
                ball.Position += speed;
                
                amountSpeed += speed;

                //ball.Update(gameTime);
                //Checking if ball hit rightPaddle
                if (ball.Right > rightPaddle.Left && ball.Bottom > rightPaddle.Top && ball.Top < rightPaddle.Bottom)
                {
                    //ball intersected with rightPaddle!!! Is it traveling to the right? If so, inverse direction; otherwise, leave it alone
                    if (ball.SpeedX > 0)
                    {

                        ball.SpeedX *= -1.05f;
                        ball.SpeedY *= 1.05f;

                    }
                }
                 
                //Checking if ball hit leftPaddle
                if (ball.Left < leftPaddle.Right && ball.Bottom > leftPaddle.Top && ball.Top < leftPaddle.Bottom)
                {
                    //ball intersected with leftPaddle!!! Is it traveling to the left? If so, inverse direction; otherwise, leave it alone
                    if (ball.SpeedX < 0)
                    {

                        ball.SpeedX *= -1.05f;
                        ball.SpeedY *= 1.05f;
                    }
                }
            }

            plusOne.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            ball.Draw(spriteBatch);
        }
    }
}
