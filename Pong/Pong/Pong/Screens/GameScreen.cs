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
        public override void Load(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            leftPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), Vector2.Zero, Color.White);
            rightPaddle = new Paddle(Content.Load<Texture2D>("temp paddle"), new Vector2(_viewPort.Width - leftPaddle.Texture.Width,0), Color.White);

            ball = new Ball(Content.Load<Texture2D>("temp ball"), new Vector2(_viewPort.Width / 2, _viewPort.Height / 2), Color.White);

            _sprites.Add(rightPaddle);
            _sprites.Add(leftPaddle);
            _sprites.Add(ball);
        }

        public override void Update(GameTime gameTime)
        {

            if (ball.Position.Y <= 0 || ball.Position.Y + ball.Texture.Height >= _viewPort.Height)
            {

                ball.SpeedY *= -1;
            }
            else if (ball.Position.X <= 0 || ball.Position.X + ball.Texture.Width >= _viewPort.Width)
            {
                ball.SpeedX *= -1;
            }

            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Up) && rightPaddle.Position.Y > 0 )
            {
                rightPaddle.VectorY -= 1;
            }

            if (keyboard.IsKeyDown(Keys.Down) && rightPaddle.Position.Y + rightPaddle.Texture.Height < _viewPort.Height)
            {
                rightPaddle.VectorY += 1;
            }


            base.Update(gameTime);
        }
    }
}
