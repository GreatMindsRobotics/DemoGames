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

        KeyboardState keyboard;

        Random rnd = new Random();

        bool leftSideScored = false;


        int ballDirection;
        int paddleSpeed = 8;

        int leftScore = 0;
        int rightScore = 0;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            leftPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(0, 176), Color.White);
            rightPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(_viewPort.Width - leftPaddle.Texture.Width, 176), Color.White);
            ball = new Ball(Content.Load<Texture2D>("temp ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);
            ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width / 2, _viewPort.Height / 2 - ball.Texture.Height / 2);

            plusOne = new PlusOne(Content.Load<Texture2D>("Plus1"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.Red);
            plusOne.SlideCompleted += new FontEffectsLib.SpriteTypes.SlidingSprite.SlideCompletedState(plusOne_SlideCompleted);

            leftScoreFont = new FadingFont(Content.Load<SpriteFont>("SpriteFont1"), new Vector2(10, 0), 0.1f, 1.0f, 0.01f, 1.0f, leftScore.ToString(), Color.White, false);
            leftScoreFont.EnableShadow = false;


            rightScoreFont = new FadingFont(Content.Load<SpriteFont>("SpriteFont1"), new Vector2(_viewPort.Width - 30, 0), 0.1f, 1.0f, 0.01f, 1.0f, rightScore.ToString(), Color.White, false);
            rightScoreFont.EnableShadow = false;


            _sprites.Add(rightPaddle);
            _sprites.Add(leftPaddle);
            _sprites.Add(ball);
            _sprites.Add(plusOne);
            _sprites.Add(leftScoreFont);
            _sprites.Add(rightScoreFont);
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

            //Check for win
            if (leftScore >= 10)
            {

                ScreenManager.Change(ScreenState.GameOver);
                
            }
            else if (rightScore >= 10)
            {

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
                if (ball.Position.Y <= 0 || ball.Position.Y + ball.Texture.Height >= _viewPort.Height)
                {
                    ball.SpeedY *= -1;

                }
                else if (ball.Position.X <= 0)
                {
                    //Ball goes through the wall
                    //TODO Add scoring

                    leftSideScored = false;

                    plusOne.Position = new Vector2(_viewPort.Width * 3 / 4 - plusOne.Texture.Width / 2, _viewPort.Height / 2 - plusOne.Texture.Height / 2);
                    plusOne.SlideTo = new Vector2(_viewPort.Width - 30, 0);
                    plusOne.IsVisible = true;

                    //Reset the ball
                    ball.BallState = BallState.Rested;
                    ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width / 2, _viewPort.Height / 2 - ball.Texture.Height / 2);
                    ball.Speed = Vector2.Zero;
                }
                else if (ball.Position.X + ball.Texture.Width >= _viewPort.Width)
                {
                    //Ball goes through the wall
                    //TODO Add scoring

                    leftSideScored = true;

                    plusOne.Position = new Vector2(_viewPort.Width * 1 / 4 - plusOne.Texture.Width / 2, _viewPort.Height / 2 - plusOne.Texture.Height / 2);
                    plusOne.SlideTo = new Vector2(20, 0);
                    plusOne.IsVisible = true;


                    //Reset the ball
                    ball.BallState = BallState.Rested;
                    ball.Position = new Vector2(_viewPort.Width / 2 - ball.Texture.Width / 2, _viewPort.Height / 2 - ball.Texture.Height / 2);
                    ball.Speed = Vector2.Zero;
                }

            }

            //Rightpaddle Movement
            if (keyboard.IsKeyDown(Keys.Up) && rightPaddle.Position.Y > 0)
            {
                rightPaddle.VectorY -= paddleSpeed;
            }

            if (keyboard.IsKeyDown(Keys.Down) && rightPaddle.Position.Y + rightPaddle.Texture.Height < _viewPort.Height)
            {
                rightPaddle.VectorY += paddleSpeed;
            }

            //Leftpaddle Movement
            if (keyboard.IsKeyDown(Keys.W) && leftPaddle.Position.Y > 0)
            {
                leftPaddle.VectorY -= paddleSpeed;
            }

            if (keyboard.IsKeyDown(Keys.S) && leftPaddle.Position.Y + leftPaddle.Texture.Height < _viewPort.Height)
            {
                leftPaddle.VectorY += paddleSpeed;
            }

            //Checking if ball hit rightPaddle
            if (ball.Position.X < rightPaddle.Position.X + rightPaddle.Texture.Width && ball.Position.X + ball.Texture.Width > rightPaddle.Position.X && ball.Position.Y < rightPaddle.Position.Y + rightPaddle.Texture.Height && ball.Texture.Height + ball.Position.Y > rightPaddle.Position.Y)
            {
                //ball intersected with rightPaddle!!! Is it traveling to the right? If so, inverse direction; otherwise, leave it alone
                if (ball.SpeedX > 0)
                {
                    ball.SpeedX *= -1;
                }

            }

            //Checking if ball hit leftPaddle
            if (ball.Position.X < leftPaddle.Position.X + leftPaddle.Texture.Width && ball.Position.X + ball.Texture.Width > leftPaddle.Position.X && ball.Position.Y < leftPaddle.Position.Y + rightPaddle.Texture.Height && ball.Texture.Height + ball.Position.Y > leftPaddle.Position.Y)
            {
                //ball intersected with leftPaddle!!! Is it traveling to the left? If so, inverse direction; otherwise, leave it alone
                if (ball.SpeedX < 0)
                {
                    ball.SpeedX *= -1;
                }
            }

            plusOne.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
