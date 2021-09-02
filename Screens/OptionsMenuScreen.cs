#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
        MenuEntry soundMenuEntry;
        //MenuEntry ungulateMenuEntry;
       // MenuEntry languageMenuEntry;
        MenuEntry screenMenuEntry;
        MenuEntry elfMenuEntry;

        enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        static Ungulate currentUngulate = Ungulate.Dromedary;

        static string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        static int currentLanguage = 0;

        public static bool setFullscreen {get;set;}
        public static bool soundOnOff{get;set;}

        static int elf = 23;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            soundMenuEntry = new MenuEntry(string.Empty);
            //languageMenuEntry = new MenuEntry(string.Empty);
            screenMenuEntry = new MenuEntry(string.Empty);
            //elfMenuEntry = new MenuEntry(string.Empty);
            setFullscreen = false;
            soundOnOff = true;
            //graphics = new GraphicsDeviceManager(this);
            SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            soundMenuEntry.Selected += soundMenuEntrySelected;
            //languageMenuEntry.Selected += LanguageMenuEntrySelected;
            screenMenuEntry.Selected += screenMenuEntrySelected;
           // elfMenuEntry.Selected += ElfMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            MenuEntries.Add(soundMenuEntry);
           // MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(screenMenuEntry);
            //MenuEntries.Add(elfMenuEntry);
            MenuEntries.Add(back);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            soundMenuEntry.Text = "Sound: " + (soundOnOff ? "Yes" : "No");
           // languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            screenMenuEntry.Text = "Full Screen Mode: " + (setFullscreen ? "Yes" : "No");
           // elfMenuEntry.Text = "elf: " + elf;
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void soundMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            soundOnOff = !soundOnOff;

            if (soundOnOff == true)
            {

            }
            else if (soundOnOff == false)
            {

            }
            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
       // void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
      //  {
       //     currentLanguage = (currentLanguage + 1) % languages.Length;

      //      SetMenuEntryText();
      //  }


        /// <summary>
        /// Event handler for when the screen menu entry is selected.
        /// </summary>
        void screenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            

            setFullscreen = !setFullscreen;

            
            
            if (setFullscreen == true)
            {

                
                
            }

            else
            {
                if (setFullscreen ==false)
                {
                    
                    
                }
            }
            
            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
       // void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
       // {
       //     elf++;

       //     SetMenuEntryText();
      //  }
    //

        #endregion


        
    }
}
