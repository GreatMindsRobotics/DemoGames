using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Screens;
using Pong.Sprites;
using Pong.CoreTypes;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameScreen gameScreen;
        TitleScreen titleScreen;
        MainMenuScreen mainMenuScreen;
        GameOverScreen gameOverScreen;
        PlayerSelectScreen playerSelectScreen;
        OnePlayerSelectScreen oneplayerSelectScreen;
        TwoPlayerSelectScreen twoplayerSelectScreen;
        OptionsScreen optionsScreen;
        EditControlsScreen editControls;
        PauseScreen pauseScreen;
        ErrorScreen errorScreen;
        GameModeScreen gameModeScreen;

        ScreenState screenState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //graphics.ToggleFullScreen();

            screenState = ScreenState.Title;

            gameScreen = new GameScreen();
            mainMenuScreen = new MainMenuScreen();
            titleScreen = new TitleScreen();
            gameOverScreen = new GameOverScreen();
            playerSelectScreen = new PlayerSelectScreen();
            oneplayerSelectScreen = new OnePlayerSelectScreen();
            twoplayerSelectScreen = new TwoPlayerSelectScreen();
            optionsScreen = new OptionsScreen();
            editControls = new EditControlsScreen();
            pauseScreen = new PauseScreen();
            errorScreen = new ErrorScreen();
            gameModeScreen = new GameModeScreen();
            

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
           

            Global.Scale = new Vector2(GraphicsDevice.Viewport.Width / 1920f);

            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameScreen.Load(Content);

            titleScreen.Load(Content);
            gameOverScreen.Load(Content);
            playerSelectScreen.Load(Content);
            oneplayerSelectScreen.Load(Content);
            twoplayerSelectScreen.Load(Content);
            optionsScreen.Load(Content);
            editControls.Load(Content);
            pauseScreen.Load(Content);
            errorScreen.Load(Content);
            gameModeScreen.Load(Content);

            mainMenuScreen.Load(Content);


            ScreenManager.AddScreen(ScreenState.Title, titleScreen);
            ScreenManager.AddScreen(ScreenState.Game, gameScreen);
            ScreenManager.AddScreen(ScreenState.MainMenu, mainMenuScreen);
            ScreenManager.AddScreen(ScreenState.GameOver, gameOverScreen);
            ScreenManager.AddScreen(ScreenState.PlayerSelect, playerSelectScreen);
            ScreenManager.AddScreen(ScreenState.OnePlayerSelect, oneplayerSelectScreen);
            ScreenManager.AddScreen(ScreenState.TwoPlayerSelect, twoplayerSelectScreen);
            ScreenManager.AddScreen(ScreenState.Options, optionsScreen);
            ScreenManager.AddScreen(ScreenState.EditControls, editControls);
            ScreenManager.AddScreen(ScreenState.Pause, pauseScreen);
            ScreenManager.AddScreen(ScreenState.Error, errorScreen);
            ScreenManager.AddScreen(ScreenState.GameMode, gameModeScreen);

            ScreenManager.Change(ScreenState.MainMenu);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            ScreenManager.Update(gameTime);
            //GamePad.SetVibration(PlayerIndex.One, 1, 1);
            if (Global.Close)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            ScreenManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
