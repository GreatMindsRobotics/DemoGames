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
        Texture2D pixel;
        List<Vector2> predictionPath = new List<Vector2>();
        Vector2 predictionPosition = new Vector2();
        Vector2 speedofBall;
        Vector2 predictionSpeed = new Vector2();

        //GameSprite numbers;

        GamePadState gs;

        FadingFont leftScoreFont;
        FadingFont rightScoreFont;

        FadingFont player1Font;
        FadingFont player2Font;

        FadingFont infoFont;
        FadingFont errorFont;

        Random rnd = new Random();

        public static bool player1Won = false;

        bool controllerDisconnected = false;

        bool leftSideScored = false;

        bool stuckToLeftPaddle = true;

        //Keys player1Up = Keys.W;
        int BounceCount = 0;


        int swichCount = 0;

        int hitBall;
        int ballDirection;
        int randx;
        int randy;

        int paddleSpeed = 8;

        int leftScore = 0;
        int rightScore = 0;

        int maxPoints = 5;

        float rotationspeed = .01f;

        void debug()
        {
            if (speedofBall.X != 0 || speedofBall.Y != 0)
            {
                for (predictionPosition.X = ball.Position.X, predictionPosition.Y = ball.Position.Y; (predictionPosition.X - ball.Texture.Width / 2 > Global.LeftPlayer.Right && ball.SpeedX < 0) || (predictionPosition.X + ball.Texture.Width / 2 < Global.RightPlayer.Left && ball.SpeedX > 0); predictionPosition.X += speedofBall.X, predictionPosition.Y += speedofBall.Y)
                {
                    if (predictionPosition.Y < ball.Texture.Height / 2 || predictionPosition.Y > ball.Texture.GraphicsDevice.Viewport.Height - ball.Texture.Height / 2)
                    {
                        speedofBall.Y *= -1;
                        BounceCount++;
                    }
                    predictionPath.Add(predictionPosition);
                }
            }
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {

            GameSprite background = new GameSprite(Content.Load<Texture2D>("Background\\Play"), Vector2.Zero, Color.White);
            background.Scale = Global.Scale;

            pixel = new Texture2D(background.Texture.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            Global.LeftPlayer = new Paddle(Content.Load<Texture2D>("PaddleLeft"), new Vector2(30, _viewPort.Height / 2), Color.White);
            //Global.LeftPlayer.Scale = new Vector2(0.5f);
            Global.LeftPlayer.SetCenterAsOrigin();
            Global.LeftPlayer.Position = new Vector2(Global.LeftPlayer.Origin.X + 30, _viewPort.Height / 2);
            //leftPaddle.UpKey = Keys.W;
            //leftPaddle.DownKey = Keys.S;

            //Global.LeftPlayer = Global.LeftPlayer;

            Global.RightPlayer = new Paddle(Content.Load<Texture2D>("PaddleRight"), new Vector2(_viewPort.Width - Global.LeftPlayer.Texture.Width / 2 - 30, _viewPort.Height / 2), Color.White);
            //Global.RightPlayer.Scale = new Vector2(0.5f);
            Global.RightPlayer.SetCenterAsOrigin();
            //rightPaddle.UpKey = Keys.Up;
            //rightPaddle.DownKey = Keys.Down;

            // Global.RightPlayer = rightPaddle;

            XDocument optionsXml = XDocument.Load(@"XML\Options.xml");

            //Keyboard controls
            XElement player1 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Keyboard")).Elements(XName.Get("Player")).ToList()[0];
            XElement player2 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("Keyboard")).Elements(XName.Get("Player")).ToList()[1];

            Global.LeftPlayer.UpKey = (Keys)int.Parse(player1.Element(XName.Get("Up")).Value);
            Global.LeftPlayer.DownKey = (Keys)int.Parse(player1.Element(XName.Get("Down")).Value);
            Global.RightPlayer.UpKey = (Keys)int.Parse(player2.Element(XName.Get("Up")).Value);
            Global.RightPlayer.DownKey = (Keys)int.Parse(player2.Element(XName.Get("Down")).Value);

            //GamePad
            player1 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("GamePad")).Elements(XName.Get("Player")).ToList()[0];
            player2 = optionsXml.Root.Elements(XName.Get("PlayerControls")).Elements(XName.Get("GamePad")).Elements(XName.Get("Player")).ToList()[1];

            Global.LeftPlayer.UpButton = (GamePadMapper.GamePadButtons)int.Parse(player1.Element(XName.Get("Up")).Value);
            Global.LeftPlayer.DownButton = (GamePadMapper.GamePadButtons)int.Parse(player1.Element(XName.Get("Down")).Value);
            Global.RightPlayer.UpButton = (GamePadMapper.GamePadButtons)int.Parse(player2.Element(XName.Get("Up")).Value);
            Global.RightPlayer.DownButton = (GamePadMapper.GamePadButtons)int.Parse(player2.Element(XName.Get("Down")).Value);

            
            ball = new Ball(Content.Load<Texture2D>("Ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            //ball.Scale = new Vector2(0.5f);
            ball.SetCenterAsOrigin();


            arrow = new GameSprite(Content.Load<Texture2D>("ArrowRight"), new Vector2(0, 0), Color.CornflowerBlue);
            //arrow.Scale = new Vector2(0.5f);
            arrow.IsVisible = false;
            arrow.Origin = new Vector2(-ball.Texture.Width / 2, arrow.Texture.Height / 2);


            //numbers = new GameSprite(Content.Load<Texture2D>("Numbers"), new Vector2(0, 0), Color.CornflowerBlue);
            plusOne = new PlusOne(Content.Load<Texture2D>("Plus1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.AliceBlue);
            plusOne.SlideCompleted += new FontEffectsLib.SpriteTypes.SlidingSprite.SlideCompletedState(plusOne_SlideCompleted);

            leftScoreFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\JumboOutage"), new Vector2(_viewPort.Width / 2 - 200, 15), 0.1f, 1.0f, 0.01f, 1.0f, leftScore.ToString(), Color.White, false);
            leftScoreFont.EnableShadow = false;

            rightScoreFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\JumboOutage"), new Vector2(_viewPort.Width / 2 + 50, 15), 0.1f, 1.0f, 0.01f, 1.0f, rightScore.ToString(), Color.White, false);
            rightScoreFont.EnableShadow = false;

            //player1Font = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(10, 50), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player1"), Color.White, false);
            //player1Font.EnableShadow = false;

            //player2Font = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(_viewPort.Width - 85, 50), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Player2"), Color.White, false);
            //player2Font.EnableShadow = false;

            if (Global.UsingKeyboard)
            {
                infoFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Press Space to start"), Color.LightGoldenrodYellow, false);
            }
            else
            {
                infoFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), 0.1f, 1.0f, 0.01f, 1.0f, string.Format("Press A to start"), Color.LightGoldenrodYellow, false);
            }
            infoFont.EnableShadow = false;
            infoFont.SetCenterAsOrigin();


            errorFont = new FadingFont(Content.Load<SpriteFont>("Fonts\\Outage"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2 + 100), 0.1f, 1.0f, 0.01f, 1.0f, string.Format(""), Color.LightGoldenrodYellow, false);
            errorFont.EnableShadow = false;
            errorFont.SetCenterAsOrigin();
            errorFont.IsVisible = true;

            _sprites.Add(background);
            _sprites.Add(Global.RightPlayer);
            _sprites.Add(Global.LeftPlayer);
            _sprites.Add(plusOne);
            _sprites.Add(arrow);
            _sprites.Add(leftScoreFont);
            _sprites.Add(rightScoreFont);
            _sprites.Add(infoFont);
            _sprites.Add(errorFont);


            ballDirection = rnd.Next(0, 2);
            switch (ballDirection)
            {
                case 0:
                    randx = rnd.Next(5, 11);
                    randy = rnd.Next(2, 6);
                    break;

                case 1:
                    randx = rnd.Next(-11, -5);
                    randy = rnd.Next(2, 6);
                    break;

                default:
                    break;
            }
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
                //ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width * ball.Scale.X / 2, _viewPort.Height / 2 - ball.Texture.Height * ball.Scale.Y / 2);
                ball.Position = new Vector2(_viewPort.Width / 2, _viewPort.Height / 2);
            }
            else if (Global.GameMode == GameMode.PingPong)
            {
                //set ball.state = ballstate.stucktospecificplayer
                if (stuckToLeftPaddle)
                {
                    //ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height / 2);
                    ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width * ball.Scale.X / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height * Global.LeftPlayer.Scale.Y / 2);
                }
                else
                {
                    //ball.Position = new Vector2(Global.RightPlayer.Left - ball.Texture.Width / 2 - 5, Global.RightPlayer.Top + Global.RightPlayer.Texture.Height / 2);
                    ball.Position = new Vector2(Global.RightPlayer.Left - ball.Texture.Width * ball.Scale.X / 2 - 5, Global.RightPlayer.Top + Global.LeftPlayer.Texture.Height * Global.LeftPlayer.Scale.Y / 2);
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

            if (!Global.UsingKeyboard)
            {
                GamePadState controller = GamePad.GetState(PlayerIndex.One);
            }

            arrow.Rotation += rotationspeed;

            if (Global.Reset)
            {
                reset();
                Global.Reset = false;
            }

            if (ball.BallState == BallState.Rested)
            {
                if (Global.GameMode == GameMode.PingPong)
                {
                    predictionSpeed = new Vector2((float)Math.Cos(arrow.Rotation), (float)Math.Sin(arrow.Rotation));
                }
                else
                {
                    predictionSpeed.X = randx;
                    predictionSpeed.Y = randy;
                }
                predictionSpeed.Normalize();
                predictionSpeed *= 4;
                BounceCount = 0;
                speedofBall = predictionSpeed / 4;
                predictionPath = new List<Vector2>();
                if (speedofBall.X != 0 || speedofBall.Y != 0)
                {
                    for (predictionPosition.X = ball.Position.X, predictionPosition.Y = ball.Position.Y; (predictionPosition.X - ball.Texture.Width / 2 > Global.LeftPlayer.Right && speedofBall.X < 0) || (predictionPosition.X + ball.Texture.Width / 2 < Global.RightPlayer.Left && speedofBall.X > 0); predictionPosition.X += speedofBall.X, predictionPosition.Y += speedofBall.Y)
                    {
                        if (predictionPosition == ball.Position)
                        {
                            predictionPosition += predictionSpeed * 43;
                        }
                        if (predictionPosition.Y < ball.Texture.Height / 2)
                        {
                            speedofBall.Y = Math.Abs(speedofBall.Y);
                            predictionPosition.Y = ball.Texture.Height / 2;
                            BounceCount++;
                        }
                        else if (predictionPosition.Y > ball.Texture.GraphicsDevice.Viewport.Height - ball.Texture.Height / 2)
                        {
                            predictionPosition.Y = ball.Texture.GraphicsDevice.Viewport.Height - ball.Texture.Height / 2;
                            speedofBall.Y = -Math.Abs(speedofBall.Y);
                            BounceCount++;
                        }
                        predictionPath.Add(predictionPosition);
                    }
                }
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
            if (Global.UsingKeyboard)
            {
                if (InputManager.JustPressed(Keys.Escape))
                {
                    ScreenManager.Change(ScreenState.Pause);
                }

                if (InputManager.JustPressed(Keys.Space))
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
                            speedofBall = ball.Speed;
                            //making it a bit faster
                            ball.Speed *= 6;
                        }
                        else
                        {

                            ball.SpeedX = randx;
                            ball.SpeedY = randy;
                        }
                    }
                    predictionSpeed = ball.Speed;
                    BounceCount = 0;
                    predictionPath = new List<Vector2>();
                    debug();

                    ball.BallState = BallState.Moving;
                }
            }
            else
            {
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.Back))
                {
                    ScreenManager.Change(ScreenState.Pause);
                }

                if (InputManager.IsGamepadButtonTapped(PlayerIndex.One, GamePadMapper.GamePadButtons.A) && (Global.Mode == Mode.SinglePlayer || Global.GameMode == GameMode.Classical || (Global.GameMode == GameMode.PingPong && stuckToLeftPaddle)))
                {
                    new DebugTypes.PathMapper(Vector2.Zero, new Vector2(20, 30), _viewPort.Bounds, DebugTypes.PathMapper.BoundingBoxSide.Right).Update(gameTime);

                    arrow.IsVisible = false;
                    infoFont.Text.Clear();
                    infoFont.Text.Append("Press Back to pause");

                    if (ball.BallState == BallState.Rested)
                    {
                        if (Global.GameMode == GameMode.PingPong)
                        {
                            //using trigonometry to read the current rotation in degrees and have it spit out a vector2 to use for the speed.
                            ball.Speed = new Vector2((float)Math.Cos(arrow.Rotation), (float)Math.Sin(arrow.Rotation));
                            //make it a regular speed, but storing the direction as well
                            ball.Speed.Normalize();
                            speedofBall = ball.Speed;
                            //making it a bit faster
                            ball.Speed *= 6;
                        }
                        else
                        {

                            ball.SpeedX = randx;
                            ball.SpeedY = randy;
                        }
                    }
                    predictionSpeed = ball.Speed;
                    BounceCount = 0;
                    predictionPath = new List<Vector2>();
                    debug();

                    ball.BallState = BallState.Moving;
                }
                if (InputManager.IsGamepadButtonTapped(PlayerIndex.Two, GamePadMapper.GamePadButtons.A) && (Global.GameMode == GameMode.Classical || (Global.GameMode == GameMode.PingPong && !stuckToLeftPaddle)))
                {
                    new DebugTypes.PathMapper(Vector2.Zero, new Vector2(20, 30), _viewPort.Bounds, DebugTypes.PathMapper.BoundingBoxSide.Right).Update(gameTime);

                    arrow.IsVisible = false;
                    infoFont.Text.Clear();
                    infoFont.Text.Append("Press Back to pause");

                    if (ball.BallState == BallState.Rested)
                    {
                        if (Global.GameMode == GameMode.PingPong)
                        {
                            //using trigonometry to read the current rotation in degrees and have it spit out a vector2 to use for the speed.
                            ball.Speed = new Vector2((float)Math.Cos(arrow.Rotation), (float)Math.Sin(arrow.Rotation));
                            //make it a regular speed, but storing the direction as well
                            ball.Speed.Normalize();
                            speedofBall = ball.Speed;
                            //making it a bit faster
                            ball.Speed *= 6;
                        }
                        else
                        {

                            ball.SpeedX = randx;
                            ball.SpeedY = randy;
                        }
                    }
                    predictionSpeed = ball.Speed;
                    BounceCount = 0;
                    predictionPath = new List<Vector2>();
                    debug();

                    ball.BallState = BallState.Moving;
                }


            }

            if (!Global.UsingKeyboard)
            {
                ball.IsPaused = controllerDisconnected;

            }
            if (ball.BallState == BallState.Moving)
            {
                if (ball.Bottom >= _viewPort.Height)
                {

                    if (ball.SpeedY > 0)
                    {
                        ball.SpeedY *= -1;
                    }

                }
                else if (ball.Top <= 0)
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
                    if (Global.UsingKeyboard)
                    {
                        infoFont.Text.Append("Press Space to start");
                    }
                    else
                    {
                        infoFont.Text.Append("Press A to start");
                    }
                    ball.Speed = Vector2.Zero;
                    ball.TintColor = Color.Red;

                    leftSideScored = false;

                    ballDirection = rnd.Next(0, 2);
                    switch (ballDirection)
                    {
                        case 0:
                            randx = rnd.Next(5, 11);
                            randy = rnd.Next(2, 6);
                            break;

                        case 1:
                            randx = rnd.Next(-11, -5);
                            randy = rnd.Next(2, 6);
                            break;

                        default:
                            break;
                    }

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
                    if (Global.UsingKeyboard)
                    {
                        infoFont.Text.Append("Press Space to start");
                    }
                    else
                    {
                        infoFont.Text.Append("Press A to start");
                    }

                    ball.Speed = Vector2.Zero;
                    ball.TintColor = Color.Red;

                    ballDirection = rnd.Next(0, 2);
                    switch (ballDirection)
                    {
                        case 0:
                            randx = rnd.Next(5, 11);
                            randy = rnd.Next(2, 6);
                            break;

                        case 1:
                            randx = rnd.Next(-11, -5);
                            randy = rnd.Next(2, 6);
                            break;

                        default:
                            break;
                    }

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
                    ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width * ball.Scale.X / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height * ball.Scale.Y / 2);
                    arrow.Position = new Vector2(ball.Position.X, ball.Position.Y);
                    arrow.Effects = SpriteEffects.None;
                    arrow.IsVisible = true;

                }
                else
                {
                    ball.Position = new Vector2(Global.RightPlayer.Left - ball.Texture.Width * ball.Scale.X / 2 - 5, Global.RightPlayer.Top + Global.RightPlayer.Texture.Height * ball.Scale.Y / 2);
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
                    if (Global.UsingKeyboard)
                    {
                        if (InputManager.IsDown(Global.LeftPlayer.UpKey) && Global.LeftPlayer.Top > 0)
                        {
                            Global.LeftPlayer.VectorY -= Math.Abs(paddleSpeed);
                        }

                        if (InputManager.IsDown(Global.LeftPlayer.DownKey) && Global.LeftPlayer.Bottom < _viewPort.Height)
                        {
                            Global.LeftPlayer.VectorY += Math.Abs(paddleSpeed);
                        }
                    }
                    else
                    {
                        if (!InputManager.PressedKeysPlayer1.IsConnected)
                        {
                            errorFont.Text.Clear();
                            errorFont.Text.Append("Controller is disconected");
                            errorFont.SetCenterAsOrigin();
                            controllerDisconnected = true;
                        }
                        else
                        {
                            errorFont.Text.Clear();
                            controllerDisconnected = false;
                        }

                        if (InputManager.PressedKeysPlayer1.GetPressedButtons().Contains(Global.LeftPlayer.UpButton) && Global.LeftPlayer.Top > 0 && !controllerDisconnected)
                        {
                            Global.LeftPlayer.VectorY -= Math.Abs(paddleSpeed);
                        }

                        if (InputManager.PressedKeysPlayer1.GetPressedButtons().Contains(Global.LeftPlayer.DownButton) && Global.LeftPlayer.Bottom < _viewPort.Height && !controllerDisconnected)
                        {
                            Global.LeftPlayer.VectorY += Math.Abs(paddleSpeed);
                        }
                    }
                    switch (Global.Difficulty)
                    {
                        case Difficulty.Easy:
                            if (ball.SpeedX > 0)
                            {
                                if (BounceCount > 0)
                                {
                                    if (Global.RightPlayer.Bottom >= _viewPort.Height || Global.RightPlayer.Top <= 0)
                                    {
                                        paddleSpeed *= -1;
                                    }
                                }
                                else
                                {
                                    if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y + 20 < predictionPosition.Y && Global.RightPlayer.Bottom < _viewPort.Height)
                                    {
                                        paddleSpeed = Math.Abs(paddleSpeed);
                                    }
                                    else if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y - 20 > predictionPosition.Y && Global.RightPlayer.Top > 0)
                                    {
                                        paddleSpeed = -Math.Abs(paddleSpeed);
                                    }
                                    else
                                    {
                                        Global.RightPlayer.VectorY -= paddleSpeed;
                                    }
                                }

                                Global.RightPlayer.VectorY += paddleSpeed;
                            }
                            break;
                        case Difficulty.Medium:
                            if (ball.SpeedX > 0)
                            {
                                if (BounceCount > 1)
                                {
                                    if (Global.RightPlayer.Bottom >= _viewPort.Height || Global.RightPlayer.Top <= 0)
                                    {
                                        paddleSpeed *= -1;
                                    }
                                }
                                else
                                {
                                    if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y + 20 < predictionPosition.Y && Global.RightPlayer.Bottom < _viewPort.Height)
                                    {
                                        paddleSpeed = Math.Abs(paddleSpeed);
                                    }
                                    else if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y - 20 > predictionPosition.Y && Global.RightPlayer.Top > 0)
                                    {
                                        paddleSpeed = -Math.Abs(paddleSpeed);
                                    }
                                    else
                                    {
                                        Global.RightPlayer.VectorY -= paddleSpeed;
                                    }
                                }

                                Global.RightPlayer.VectorY += paddleSpeed;
                            }
                            break;
                        case Difficulty.Hard:
                            if (ball.SpeedX > 0)
                            {
                                if (BounceCount > 2)
                                {
                                    if (Global.RightPlayer.Bottom >= _viewPort.Height || Global.RightPlayer.Top <= 0)
                                    {
                                        paddleSpeed *= -1;
                                    }
                                }
                                else
                                {
                                    if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y + 20 < predictionPosition.Y && Global.RightPlayer.Bottom < _viewPort.Height)
                                    {
                                        paddleSpeed = Math.Abs(paddleSpeed);
                                    }
                                    else if (Global.RightPlayer.Top + Global.RightPlayer.Origin.Y - 20 > predictionPosition.Y && Global.RightPlayer.Top > 0)
                                    {
                                        paddleSpeed = -Math.Abs(paddleSpeed);
                                    }
                                    else
                                    {
                                        Global.RightPlayer.VectorY -= paddleSpeed * 1.25f;
                                    }
                                }
                                Global.RightPlayer.VectorY += paddleSpeed * 1.25f;
                            }
                            break;
                        default:
                            break;
                    }


                    if (Global.GameMode == GameMode.PingPong && !stuckToLeftPaddle && ball.BallState == BallState.Rested)
                    {
                        new DebugTypes.PathMapper(Vector2.Zero, new Vector2(20, 30), _viewPort.Bounds, DebugTypes.PathMapper.BoundingBoxSide.Right).Update(gameTime);

                        arrow.IsVisible = false;
                        infoFont.Text.Clear();
                        infoFont.Text.Append("Press Esc to pause");

                        //using trigonometry to read the current rotation in degrees and have it spit out a vector2 to use for the speed.
                        ball.Speed = new Vector2((float)Math.Cos(arrow.Rotation), (float)Math.Sin(arrow.Rotation));
                        //make it a regular speed, but storing the direction as well
                        ball.Speed.Normalize();
                        speedofBall = ball.Speed;
                        //making it a bit faster
                        ball.Speed *= 6;

                        predictionSpeed = ball.Speed;
                        BounceCount = 0;
                        predictionPath = new List<Vector2>();
                        debug();

                        ball.BallState = BallState.Moving;
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

                        if (Global.UsingKeyboard)
                        {
                            if (InputManager.IsDown(Global.LeftPlayer.UpKey) && Global.LeftPlayer.Top > 0)
                            {
                                Global.LeftPlayer.VectorY -= Math.Abs(paddleSpeed);
                            }

                            if (InputManager.IsDown(Global.LeftPlayer.DownKey) && Global.LeftPlayer.Bottom < _viewPort.Height)
                            {
                                Global.LeftPlayer.VectorY += Math.Abs(paddleSpeed);
                            }

                            if (InputManager.IsDown(Global.RightPlayer.UpKey) && Global.RightPlayer.Top > 0)
                            {
                                Global.RightPlayer.VectorY -= paddleSpeed;
                            }

                            if (InputManager.IsDown(Global.RightPlayer.DownKey) && Global.RightPlayer.Bottom < _viewPort.Height)
                            {
                                Global.RightPlayer.VectorY += paddleSpeed;
                            }
                        }
                        else
                        {
                            if (!InputManager.PressedKeysPlayer1.IsConnected && !InputManager.PressedKeysPlayer2.IsConnected)
                            {
                                errorFont.Text.Clear();
                                errorFont.Text.Append("Player 1 & Player 2 controllers are disconected");
                                errorFont.SetCenterAsOrigin();
                                controllerDisconnected = true;
                            }
                            else if (!InputManager.PressedKeysPlayer1.IsConnected)
                            {
                                errorFont.Text.Clear();
                                errorFont.Text.Append("Player 1 controller is disconected");
                                errorFont.SetCenterAsOrigin();
                                controllerDisconnected = true;
                            }
                            else if (!InputManager.PressedKeysPlayer2.IsConnected)
                            {
                                errorFont.Text.Clear();
                                errorFont.Text.Append("Player 2 controller is disconected");
                                errorFont.SetCenterAsOrigin();
                                controllerDisconnected = true;
                            }
                            else
                            {
                                errorFont.Text.Clear();
                                controllerDisconnected = false;
                            }

                            if (InputManager.PressedKeysPlayer1.GetPressedButtons().Contains(Global.LeftPlayer.UpButton) && Global.LeftPlayer.Top > 0 && !controllerDisconnected)
                            {
                                Global.LeftPlayer.VectorY -= Math.Abs(paddleSpeed);
                            }

                            if (InputManager.PressedKeysPlayer1.GetPressedButtons().Contains(Global.LeftPlayer.DownButton) && Global.LeftPlayer.Bottom < _viewPort.Height && !controllerDisconnected)
                            {
                                Global.LeftPlayer.VectorY += Math.Abs(paddleSpeed);
                            }

                            if (InputManager.PressedKeysPlayer2.GetPressedButtons().Contains(Global.RightPlayer.UpButton) && Global.RightPlayer.Top > 0 && !controllerDisconnected)
                            {
                                Global.RightPlayer.VectorY -= Math.Abs(paddleSpeed);
                            }

                            if (InputManager.PressedKeysPlayer2.GetPressedButtons().Contains(Global.RightPlayer.DownButton) && Global.RightPlayer.Bottom < _viewPort.Height && !controllerDisconnected)
                            {
                                Global.RightPlayer.VectorY += Math.Abs(paddleSpeed);
                            }
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
            if (Global.UsingKeyboard || !controllerDisconnected)
            {
                while (Math.Abs(amountSpeed.X) < Math.Abs(ball.Speed.X))
                {
                    Vector2 speed = ball.Speed;
                    predictionSpeed = ball.Speed;
                    speed.Normalize();

                    ball.Position += speed;

                    amountSpeed += speed;
                    if (predictionPath.Count > 0)
                    {
                        predictionPath.RemoveAt(0);
                    }
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
                        BounceCount = 0;
                        speed = ball.Speed;
                        speed.Normalize();
                        speedofBall = speed;
                        predictionPath = new List<Vector2>();
                        debug();
                    }

                    //Checking if ball hit leftPaddle
                    if (ball.Left < Global.LeftPlayer.Right && ball.Bottom > Global.LeftPlayer.Top && ball.Top < Global.LeftPlayer.Bottom)
                    {

                        //ball intersected with leftPaddle!!! Is it traveling to the left? If so, inverse direction; otherwise, leave it alone
                        //generate based on difficulty
                        if (Global.Difficulty == Difficulty.Medium)
                        {
                            hitBall = rnd.Next(0, 2);
                        }
                        else if (Global.Difficulty == Difficulty.Hard)
                        {
                            int temp = rnd.Next(0, 10);
                            if (temp >= 1)
                            {
                                temp = 1;
                            }
                            else
                            {
                                temp = 0;
                            }

                            hitBall = temp;
                        }
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
                        BounceCount = 0;
                        speed = ball.Speed;
                        speed.Normalize();
                        speedofBall = speed;
                        predictionPath = new List<Vector2>();
                        debug();
                    }
                }
            }
            if ((speedofBall.Y < 0 && predictionSpeed.Y > 0) || (speedofBall.Y > 0 && predictionSpeed.Y < 0))
            {
                predictionSpeed.Y *= -1;
            }
            if ((speedofBall.X < 0 && predictionSpeed.X > 0) || (speedofBall.X > 0 && predictionSpeed.X < 0))
            {
                predictionSpeed.X *= -1;
            }
            if (predictionSpeed.X > 0)
            {
                predictionSpeed.X *= -1.05f;
                predictionSpeed.Y *= 1.05f;
                if (Global.GameMode == GameMode.PingPong)
                {
                    predictionSpeed.Y -= (Global.RightPlayer.VectorY - predictionPosition.Y) / 10;
                    predictionSpeed.Y = MathHelper.Clamp(predictionSpeed.Y, -5, 5);
                }
            }
            else
            {
                predictionSpeed.X *= -1.05f;
                predictionSpeed.Y *= 1.05f;
                if (Global.GameMode == GameMode.PingPong)
                {
                    predictionSpeed.Y -= (Global.LeftPlayer.VectorY - predictionPosition.Y) / 10;
                    predictionSpeed.Y = MathHelper.Clamp(predictionSpeed.Y, -5, 5);
                }

            }
            plusOne.Update(gameTime);
            base.Update(gameTime);
        }

        private void reset()
        {
            swichCount = 0;

            ballDirection = rnd.Next(0, 2);
            switch (ballDirection)
            {
                case 0:
                    randx = rnd.Next(5, 11);
                    randy = rnd.Next(2, 6);
                    break;

                case 1:
                    randx = rnd.Next(-11, -5);
                    randy = rnd.Next(2, 6);
                    break;

                default:
                    break;
            }

            leftScore = 0;
            rightScore = 0;
            stuckToLeftPaddle = true;
            ball.BallState = BallState.Rested;
            ball.SpeedX = 0;
            ball.SpeedY = 0;
            arrow.IsVisible = false;
            if (Global.GameMode == GameMode.PingPong)
            {
                ball.Position = new Vector2(Global.LeftPlayer.Right + ball.Texture.Width * ball.Scale.X / 2 + 5, Global.LeftPlayer.Top + Global.LeftPlayer.Texture.Height * ball.Scale.Y / 2);
            }
            else
            {
                ball.Position = new Vector2(_viewPort.Width / 2, _viewPort.Height / 2);
            }
            leftScoreFont.Text.Clear();
            leftScoreFont.Text.Append(leftScore);
            rightScoreFont.Text.Clear();
            rightScoreFont.Text.Append(rightScore);
            Global.LeftPlayer.Position = new Vector2(Global.LeftPlayer.Origin.X + 30, _viewPort.Height / 2);
            Global.RightPlayer.Position = new Vector2(_viewPort.Width - Global.RightPlayer.Texture.Width / 2 - 30, _viewPort.Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            /*
                foreach (Vector2 pos in predictionPath)
                {
                    spriteBatch.Draw(pixel, pos, null, Color.Turquoise, 0f, new Vector2(.5f), new Vector2(4), SpriteEffects.None, 0);
                }

                spriteBatch.Draw(pixel, predictionPosition, null, Color.Turquoise, 0f, new Vector2(.5f), new Vector2(40), SpriteEffects.None, 0);
            */
            spriteBatch.DrawString(infoFont.Font, predictionSpeed.ToString() + "\n" + ball.Speed + "\n" + BounceCount, Vector2.Zero, Color.Black);
            ball.Draw(spriteBatch);
        }


    }
}
