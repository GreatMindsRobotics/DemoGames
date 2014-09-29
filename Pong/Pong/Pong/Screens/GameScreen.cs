using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



namespace Pong.Screens
{
    public class GameScreen : BaseScreen
    {
        Ball ball;
        Paddle leftPaddle;
        Paddle rightPaddle;

        KeyboardState keyboard;

        Random rnd = new Random();

        int ballDirection;
        int paddleSpeed = 8;

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            leftPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(0, 176), Color.White);
            rightPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(_viewPort.Width - leftPaddle.Texture.Width,176), Color.White);
            ball = new Ball(Content.Load<Texture2D>("temp ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);

            _sprites.Add(rightPaddle);
            _sprites.Add(leftPaddle);
            _sprites.Add(ball);
        }

        public override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Space))
            {
                if(ball.BallState == BallState.Rested)
                {
                    ballDirection = rnd.Next(0, 2);

                    switch(ballDirection)
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

            if(ball.BallState == BallState.Moving)
            {
                if (ball.Position.Y <= 0 || ball.Position.Y + ball.Texture.Height >= _viewPort.Height)
                {
                    ball.SpeedY *= -1;

                }
                else if (ball.Position.X <= 0 || ball.Position.X + ball.Texture.Width >= _viewPort.Width)
                {
                    //Ball goes through the wall
                    //TODO Add scoring

                    //Reset the ball
                    ball.BallState = BallState.Rested;
                    ball.Position = new Vector2(_viewPort.Width / 2, _viewPort.Height / 2);
                    ball.Speed = Vector2.Zero;
                }
            }

            //Rightpaddle Movement
            if (keyboard.IsKeyDown(Keys.Up) && rightPaddle.Position.Y > 0 )
            {
                rightPaddle.VectorY -= paddleSpeed;
            }

            if (keyboard.IsKeyDown(Keys.Down) && rightPaddle.Position.Y + rightPaddle.Texture.Height < _viewPort.Height)
            {
                rightPaddle.VectorY += paddleSpeed;
            }

            //Leftpaddle Movement
            if(keyboard.IsKeyDown(Keys.W) && leftPaddle.Position.Y > 0)
            {
                leftPaddle.VectorY -= paddleSpeed;
            }

            if(keyboard.IsKeyDown(Keys.S) && leftPaddle.Position.Y + leftPaddle.Texture.Height < _viewPort.Height)
            {
                leftPaddle.VectorY += paddleSpeed;
            }

            //Checking if ball hit rightPaddle
            if(ball.Position.X < rightPaddle.Position.X + rightPaddle.Texture.Width && ball.Position.X + ball.Texture.Width > rightPaddle.Position.X && ball.Position.Y < rightPaddle.Position.Y + rightPaddle.Texture.Height && ball.Texture.Height +  ball.Position.Y > rightPaddle.Position.Y)
            {
                //ball intersected with rightPaddle!!!

                ball.SpeedX *= -1;
            }

            //Checking if ball hit leftPaddle
            if (ball.Position.X < leftPaddle.Position.X + leftPaddle.Texture.Width && ball.Position.X + ball.Texture.Width > leftPaddle.Position.X && ball.Position.Y < leftPaddle.Position.Y + rightPaddle.Texture.Height && ball.Texture.Height + ball.Position.Y > leftPaddle.Position.Y)
            {
                //ball intersected with leftPaddle!!!

                ball.SpeedX *= -1;
            }

            base.Update(gameTime);
        }
    }
}
