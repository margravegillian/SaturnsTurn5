#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//

#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SaturnsTurn5.Utility;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        public static Texture2D ParticleTextTexture;
        ParticleText particleText;
        SpriteFont particleFont;
        ContentManager content;
        
       // SoundEffect titleMusic;
        //SoundEffectInstance titleMusictInstance;
        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("") //blank as using particle 
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

          
            
            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
           
            
        }


        #endregion
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            //todo
                Vector2 screenSize;

            //position of particle text
                screenSize = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height-400);
                particleFont = content.Load<SpriteFont>(@"Graphics\arialblack50");
                ParticleTextTexture = content.Load<Texture2D>(@"Graphics\TextParticle");
                particleText = new ParticleText(ScreenManager.GraphicsDevice, particleFont, "Saturn's Turn", ParticleTextTexture,2.0f,screenSize);
                
        }
        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
          
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to Quit";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            //play title music for main menus screen
            //stop if other screens have focus
            if (otherScreenHasFocus.Equals(false))
            {
                if (AudioManager.IsInitialized.Equals(true))
                    AudioManager.PlaySound("titlemusic");

               // Song titleSong = content.Load<Song>(@"Sounds\titlemusic");
               // MediaPlayer.Play(titleSong);

            }
            else
            {
               // MediaPlayer.Stop();
               AudioManager.StopSound("titlemusic");
            }
            particleText.Update();
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin(0, BlendState.Additive);
            particleText.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
