using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shooting_Gallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Sprites
        Texture2D targetSprite;
        Texture2D backgroundSprite;
        Texture2D crosshairsSprite;

        // Font
        SpriteFont gameFont;

        // Target/ Crosshair variables
        Vector2 targetPosition = new Vector2(300, 300);
        const int targetRadius = 45;
        const int crossHairsRadius = 25;

        // User Inputs
        MouseState mState;
        int score = 0;
        bool mreleased = true;

        // Timer
        double timer = 10;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Timer
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (timer < 0)
            {
                timer = 0;
            }

            // Mouse Click and Scoring
            mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Pressed && mreleased == true)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                if (mouseTargetDist < targetRadius && timer > 0)
                {
                    score++;

                    Random rand = new Random();

                    targetPosition.X = rand.Next(0 + targetRadius,_graphics.PreferredBackBufferWidth - targetRadius);
                    targetPosition.Y = rand.Next(0 + targetRadius, _graphics.PreferredBackBufferHeight - targetRadius);
                }
                mreleased = false;
            }
            if (mState.LeftButton == ButtonState.Released)
            {
                mreleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            if (timer > 0)
            {
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            }
            _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(gameFont, "Time Left: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.White);
            _spriteBatch.Draw(crosshairsSprite, new Vector2 (mState.X - crossHairsRadius, mState.Y - crossHairsRadius), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}