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
using FontEffectsLib.SpriteTypes;

namespace Pong.Screens
{
    public class GameScreen : BaseScreen
    {
        Ball ball;
        PlusOne plusOne;

        GameSprite arrow;

        FadingFont leftScoreFont;
        FadingFont rightScoreFont;

        FadingFont player1Font;
        FadingFont player2Font;

        FadingFont infoFont;

        KeyboardState keyboard;

        Random rnd = new Random();

        public static bool player1Won = false;

        bool leftSideScored = false;

        bool stuckToLeftPaddle = true;

        //Keys player1Up = Keys.W;

        int swichCount = 0;

        int ballDirection;
        int paddleSpeed = 8;

        int leftScore = 0;
        int rightScore = 0;

        int maxPoints = 5;

        float rotationspeed = .01f;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            Global.LeftPlayer = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(0, _viewPort.Height / 2), Color.White);
            Global.LeftPlayer.SetCenterAsOrigin();
            Global.LeftPlayer.Position = new Vector2(Global.LeftPlayer.Origin.X, _viewPort.Height / 2);
            //leftPaddle.UpKey = Keys.W;
            //leftPaddle.DownKey = Keys.S;

            //Global.LeftPlayer = Global.LeftPlayer;

            Global.RightPlayer = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(_viewPort.Width - Global.LeftPlayer.Texture.Width / 2, _viewPort.Height / 2), Color.White);
            Global.RightPlayer.SetCenterAsOrigin();
            //rightPaddle.UpKey = Keys.Up;
            //rightPaddle.DownKey = Keys.Down;

            // Global.RightPlayer = rightPaddle;

            XDocument optionsXml = XDocument.Load(@"XML\Options.xml");

            //Ben's dumb work around because he hasn't worked with xdoc enough
            XElement player1 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[0];
            XElement player2 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Player")).ToList()[1];

            Global.LeftPlayer.UpKey = (Keys)int.Parse(player1.Element(XName.Get("Up")).Value);
            Global.LeftPlayer.DownKey = (Keys)int.Parse(player1.Element(XName.Get("Down")).Value);
            Global.RightPlayer.UpKey = (Keys)int.Parse(player2.Element(XName.Get("Up")).Value);
            Global.RightPlayer.DownKey = (Keys)int.Parse(player2.Element(XName.Get("Down")).Value);


            ball = new Ball(Content.Load<Texture2D>("temp ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            ball.SetCenterAsOrigin();


            arrow = new GameSprite(Content.Load<Texture2D>("temp arrow"), new Vector2(0, 0), Color.CornflowerBlue);
            arrow.IsVisible = false;
            arrow.Origin = new Vector2(-ball.Texture.Width / 2, arrow.Texture.Height / 2);

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

            infoFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\SpriteFont1"), new Vector2(_viewPort.Width / 2, _viewPort.Height - 15), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Press Space to start"), Color.White, false);
            infoFont.EnableShadow = false;
            infoFont.SetCenterAsOrigin();

            _sprites.Add(Global.RightPlayer);
            _sprites.Add(Global.LeftPlayer);
            _sprites.Add(plusOne);
            _sprites.Add(arrow);
            _sprites.Add(leftScoreFont);
            _sprites.Add(rightScoreFont);
            _sprites.Add(player1Font);
            _sprites.Add(player2Font);
            _sprites.Add(infoFont);

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
            ball.Speed = Vector2.Zero;
            ball.TintColor = Color.White;

            if (Global.GameMode == GameMode.Classical)
            {
                ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width / 2, _viewPort.Height / 2 - ball.Texture.Height / 2);
            }
            else if (Global.GameMode == GameMode.PingPong)
            {
                //set ball.state = ballstate.stucktospecificplayer
                if (stuckToLeftPaddle)
                {
                    ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height / 2);
                }
                else
                {
                    ball.Position = new Vector2(Global.RightPlayer.Left - ball.Texture.Width / 2 - 5, Global.RightPlayer.Top + Global.RightPlayer.Texture.Height / 2);
                }
            }
            //Check for win
            if (leftScore >= maxPoints)
            {
                leftScore = 0;
                rightScore = 0;
                player1Won = true;
                leftScoreFont.Text.Clear();
                leftScoreFont.Text.Append(leftScore);
                rightScoreFont.Text.Clear();
                rightScoreFont.Text.Append(rightScore);
                ScreenManager.Change(ScreenState.GameOver);

            }
            else if (rightScore >= maxPoints)
            {
                leftScore = 0;
                rightScore = 0;
                player1Won = false;
                leftScoreFont.Text.Clear();
                leftScoreFont.Text.Append(leftScore);
                rightScoreFont.Text.Clear();
                rightScoreFont.Text.Append(rightScore);
                ScreenManager.Change(ScreenState.GameOver);

            }

        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            arrow.Rotation += rotationspeed;

            if (ball.BallState == BallState.Rested)
            {
                if (stuckToLeftPaddle)
                {

                    if (arrow.Rotation >= MathHelper.ToRadians(60))
                        {
                            rotationspeed = -Math.Abs(rotationspeed);
                            arrow.Rotation = MathHelper.ToRadians(60);
                        }
                    else if (arrow.Rotation <= -MathHelper.ToRadians(60))
                        {
                            rotationspeed = Math.Abs(rotationspeed);
                            arrow.Rotation = -MathHelper.ToRadians(60);
                        }

                }
                else
                {

                        if (arrow.Rotation <= MathHelper.ToRadians(120))
                        {
                            rotationspeed = Math.Abs(rotationspeed);
                            arrow.Rotation = MathHelper.ToRadians(120);
                        }
                        else if (arrow.Rotation >= MathHelper.ToRadians(240))
                        {
                            rotationspeed = -Math.Abs(rotationspeed);
                            arrow.Rotation = MathHelper.ToRadians(240);
                        }
                }
            }

            if (gameTime.IsRunningSlowly)
            {
                //System.Diagnostics.Debugger.Break();
            }

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                ScreenManager.Change(ScreenState.Pause);
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                new DebugTypes.PathMapper(Vector2.Zero, new Vector2(20, 30), _viewPort.Bounds, DebugTypes.PathMapper.BoundingBoxSide.Right).Update(gameTime);

                arrow.IsVisible = false;
                infoFont.Text.Clear();
                infoFont.Text.Append("Press Esc to pause");

                if (ball.BallState == BallState.Rested)
                {
                    if (Global.GameMode == GameMode.PingPong)
                    {
                        //using trigonometry to read the current rotation in degrees and have it spit out a vector2 to use for the speed.
                        ball.Speed = new Vector2((float)Math.Cos(arrow.Rotation), (float)Math.Sin(arrow.Rotation));
                        //make it a regular speed, but storing the direction as well
                        ball.Speed.Normalize();
                        //making it a bit faster
                        ball.SpeedX *= 4;
                        ball.SpeedY *= 4;
                    }
                    else
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

                    swichCount++;

                    if (swichCount >= 2)
                    {
                        swichCount = 0;
                        stuckToLeftPaddle = !stuckToLeftPaddle;
                    }

                    infoFont.Text.Clear();
                    infoFont.Text.Append("Press Space to start");

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

                    swichCount++;

                    if (swichCount >= 2)
                    {
                        swichCount = 0;
                        stuckToLeftPaddle = !stuckToLeftPaddle;
                        //if (!stuckToLeftPaddle)
                        //{
                        //    arrow.Rotation += (float)Math.PI;
                        //}
                    }

                    infoFont.Text.Clear();
                    infoFont.Text.Append("Press Space to start");

                    ball.Speed = Vector2.Zero;
                    ball.TintColor = Color.Red;

                    leftSideScored = true;

                    plusOne.Position = new Vector2(_viewPort.Width * 1 / 4 - plusOne.Texture.Width / 2, _viewPort.Height / 2 - plusOne.Texture.Height / 2);
                    plusOne.SlideTo = new Vector2(20, 0);
                    plusOne.IsVisible = true;

                }

            }

            if (ball.BallState == BallState.Rested && Global.GameMode == GameMode.PingPong)
            {
                if (stuckToLeftPaddle)
                {
                    ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height / 2);
                    arrow.Position = new Vector2(ball.Position.X, ball.Position.Y);
                    arrow.Effects = SpriteEffects.None;
                    arrow.IsVisible = true;

                }
                else
                {
                    ball.Position = new Vector2(Global.RightPlayer.Left - ball.Texture.Width / 2 - 5, Global.RightPlayer.Top + Global.RightPlayer.Texture.Height / 2);
                    arrow.Position = new Vector2(ball.Position.X, ball.Position.Y);
                    //arrow.Effects = SpriteEffects.FlipHorizontally;
                    arrow.IsVisible = true;
                }
            }

            //also check for other ballstates (stucktoleftplayer..etc)
            //if stucktoleftplayer
            //constantly position it next to left player



            ////STAN: This code might need to live elsewhere... maybe... for sure... I think... 
            //Keys[] pressedKeys = keyboard.GetPressedKeys();
            //if (pressedKeys.Length > 0)
            //{
            //    Keys firstPressedKey = pressedKeys[0];
            //}
            ////...

            switch (Global.Mode)
            {
                case Mode.SinglePlayer:
                    //Rightpaddle Movement
                    if (keyboard.IsKeyDown(Global.RightPlayer.UpKey) && Global.RightPlayer.Position.Y - Global.RightPlayer.Origin.Y > 0)
                    {
                        Global.RightPlayer.VectorY -= paddleSpeed;
                    }

                    if (keyboard.IsKeyDown(Global.RightPlayer.DownKey) && Global.RightPlayer.Position.Y + Global.RightPlayer.Origin.Y < _viewPort.Height)
                    {
                        Global.RightPlayer.VectorY += paddleSpeed;
                    }

                    switch (Global.Difficulty)
                    {
                        case Difficulty.Easy:
                            ScreenManager.Change(ScreenState.Error);
                            //TODO Add Easy AI
                            break;
                        case Difficulty.Medium:
                            ScreenManager.Change(ScreenState.Error);
                            //TODO Add Medium AI
                            break;
                        case Difficulty.Hard:
                            ScreenManager.Change(ScreenState.Error);
                            //TODO Add Hrd AI
                            break;
                        default:
                            break;
                    }

                    break;
                case Mode.MultiPlayer:

                    if (Global.isOnline)
                    {
                        ScreenManager.Change(ScreenState.Error);
                        //TODO Add Online Capability
                    }
                    else
                    {
                        //Rightpaddle Movement
                        if (keyboard.IsKeyDown(Global.RightPlayer.UpKey) && Global.RightPlayer.Position.Y - Global.RightPlayer.Origin.Y > 0)
                        {
                            Global.RightPlayer.VectorY -= paddleSpeed;
                        }

                        if (keyboard.IsKeyDown(Global.RightPlayer.DownKey) && Global.RightPlayer.Position.Y + Global.RightPlayer.Origin.Y < _viewPort.Height)
                        {
                            Global.RightPlayer.VectorY += paddleSpeed;
                        }

                        //Leftpaddle Movement
                        if (keyboard.IsKeyDown(Global.LeftPlayer.UpKey) && Global.LeftPlayer.Position.Y - Global.LeftPlayer.Origin.Y > 0)
                        {
                            Global.LeftPlayer.VectorY -= paddleSpeed;
                        }

                        if (keyboard.IsKeyDown(Global.LeftPlayer.DownKey) && Global.LeftPlayer.Position.Y + Global.LeftPlayer.Origin.Y < _viewPort.Height)
                        {
                            Global.LeftPlayer.VectorY += paddleSpeed;
                        }
                    }

                    //Changing gamemode
                    //TODO Add screen to choose GameMode
                    switch (Global.GameMode)
                    {
                        case GameMode.Classical:
                            break;
                        case GameMode.PingPong:
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
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
                if (ball.Right > Global.RightPlayer.Left && ball.Bottom > Global.RightPlayer.Top && ball.Top < Global.RightPlayer.Bottom)
                {
                    //ball intersected with rightPaddle!!! Is it traveling to the right? If so, inverse direction; otherwise, leave it alone
                    if (ball.SpeedX > 0)
                    {
                        ball.SpeedX *= -1.05f;
                        ball.SpeedY *= 1.05f;
                    }
                    if (Global.GameMode == GameMode.PingPong)
                    {
                        //get the Y distance from the center of the ball and the center of the paddle
                        //dist: paddlecenterY - ballcenterY
                        //add that onto the ball yspeed
                        ball.SpeedY -= (Global.RightPlayer.Position.Y - ball.Position.Y) / 10;
                        ball.SpeedY = MathHelper.Clamp(ball.SpeedY, -5, 5);
                    }
                }

                //Checking if ball hit leftPaddlewwwwww
                if (ball.Left < Global.LeftPlayer.Right && ball.Bottom > Global.LeftPlayer.Top && ball.Top < Global.LeftPlayer.Bottom)
                {
                    //ball intersected with leftPaddle!!! Is it traveling to the left? If so, inverse direction; otherwise, leave it alone
                    if (ball.SpeedX < 0)
                    {

                        ball.SpeedX *= -1.05f;
                        ball.SpeedY *= 1.05f;
                    }
                    if (Global.GameMode == GameMode.PingPong)
                    {
                        //get the Y distance from the center of the ball and the center of the paddle
                        //dist: paddlecenterY - ballcenterY
                        //add that onto the ball yspeed
                        ball.SpeedY -= (Global.LeftPlayer.Position.Y - ball.Position.Y) / 10;
                        ball.SpeedY = MathHelper.Clamp(ball.SpeedY, -5, 5);
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
