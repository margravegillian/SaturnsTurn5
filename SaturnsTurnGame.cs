#region Using Statements
//using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaturnsTurn5.Utility;
#endregion

namespace SaturnsTurn5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SaturnsTurnGame : Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        ScreenManager screenManager;
        // GraphicsAdapter adapter;
        // float playerMoveSpeed;
        // Player player;

        //SpriteBatch spriteBatch;

        // By preloading any assets used by UI rendering, we avoid framerate glitches
        // when they suddenly need to be loaded in the middle of a menu transition.
        static readonly string[] preloadAssets =
        {
            //can add more assets hre if we want
            "Graphics\\gradientgreen",
        };

        #endregion

        #region Initialization

        public SaturnsTurnGame()

        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            // graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;



            Window.Title = "Saturn's Turn";




            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);
            AudioManager.Initialize(this);

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 720;
            //graphics.PreferredBackBufferWidth = 1024;
            //graphics.PreferredBackBufferHeight = 768;
            // graphics.IsFullScreen = false;
            // graphics.ApplyChanges();



            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            AudioManager.LoadSounds();
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
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
        /// 
        #endregion
        protected override void Update(GameTime gameTime)
        {
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            /* 
             if (OptionsMenuScreen.setFullscreen == true)
             {
                 graphics.ToggleFullScreen();
                 graphics.ApplyChanges();

             }
             */

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            // TODO: Add your drawing code here
            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }


    }


}
