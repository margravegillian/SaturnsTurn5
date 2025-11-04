#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SaturnsTurn5;
using SaturnsTurn5.Utility;
using System.Collections.Generic;
using SaturnsTurn5.Entities;

#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summaryj>
    public class GameplayScreen : GameScreen
        
    {
        #region Fields
        Effect standardEffect;
       
        
        ContentManager content;
        SpriteFont scoreFont;
        //set txt scores etc
        SpriteFont gameFont;
        const string SCORE_STRING_PREFIX = "Score: ";
        string scoreString;


        Vector2 scorePosition; 
        //Texture2D backgroundStart;
        float playerMoveSpeed;
        int level;
        Player player;
        int iLivesLeft;
        //figure out which we are going to use 
        Background newBackground;
        Texture2D mainBackground;
        //ParallaxingBackground bgLayer1;
        // ParallaxingBackground bgLayer2;
        // ScrollingBackground star1;
        // ScrollingBackground star2;
        bool gameOver;

        //enemies
        Texture2D fireHairTexture;
        Texture2D enemyTexture;
        Texture2D balloonEnemyTexture;
        //Texture2D asteroidTexture;
        Texture2D asteroidTexture2;
        List<FireHair> fireHairEnemies;

        List<AsteroidEnemy2> asteroids2;
        //List<AsteroidEnemy> asteroids;
        List<Enemy> enemies;
        List<GreenMineEnemy> balloonEnemies;
        Texture2D projectileTexture;
        List<Projectile> projectiles;
        //TimeSpan fireTime;
        //TimeSpan previousFireTime;
        //the rate for enemies to appear
        TimeSpan enemySpawnTime;
        TimeSpan balloonEnemySpawnTime;
        TimeSpan previousSpawnTime;
        TimeSpan previousBalloonSpawnTime;
        TimeSpan powerUpSpawnTime;
        TimeSpan previousPowerUpSpawnTime;
        TimeSpan deathTime;
        TimeSpan previousDeathTime;
        TimeSpan asteroidSpawnTime;
        TimeSpan previousAsteroidSpawnTime;
        TimeSpan fireHairSpawnTime;
        TimeSpan previousFireHairSpawnTime;

        //explosions
        Texture2D explosion1Texture;
        List<Animation> explosions;
        Texture2D powerupDamageTexture;
        Texture2D powerupShieldTexture;
        List<PowerUp> damagePowerUps;
        List<PowerUp> shieldPowerUps;
        //string damagePowerUp;

        MouseState currentMouseState;
        MouseState previousMouseState;
       // Random randomAsteroid;
       // Random randomPowerUp;
        //Random ranp;
       // Random randomEnemy;
       // Random randomFireHair;


        Random random = new Random();

        float pauseAlpha;

        // HUD Animation fields
        float hudPulseTimer;
        float healthBarPulse;
        float shieldBarPulse;
        float livesGlowIntensity;
        Texture2D pixelTexture; // Reusable 1x1 white pixel texture
        const float HUD_PULSE_SPEED = 3f;
        const int HUD_TOP_MARGIN = 15;
        const int HUD_LEFT_MARGIN = 20;
        const int HUD_BAR_WIDTH = 160; // Reduced by 20% from 200
        const int HUD_BAR_HEIGHT = 15; // Reduced by ~20% from 18
        const int HUD_BAR_TOTAL_HEIGHT = 52; // Adjusted for smaller components

        #endregion

        #region Initialization
        //try to see it all synchs


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);


        }



        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            //todo figure out which background to use
            newBackground = new Background(content, @"Graphics\Backgrounds\PrimaryStar", @"Graphics\Backgrounds\ParallaxStars", ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

            // bgLayer1 = new ParallaxingBackground();
            //bgLayer2 = new ParallaxingBackground();
            // star1 = new ScrollingBackground();
            // star2 = new ScrollingBackground();

            // score postion setting
            scorePosition = new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y);
           // scoreString = GetScoreString(player.Score);


            //todo not working yet for effects fix
           // standardEffect = content.Load<Effect>(@"Effects\Standard");

            iLivesLeft = 3;

            gameFont = content.Load<SpriteFont>(@"Graphics\gameFont");

            //load paralzxing background
            // bgLayer1.Initialize(content, @"Graphics\bgLayer1", ScreenManager.GraphicsDevice.Viewport.Width, -1);
            // bgLayer2.Initialize(content, @"Graphics\bglayer2", ScreenManager.GraphicsDevice.Viewport.Width, -2);
            // try scrolling
            // star1.Initialize(content, @"Graphics\Backgrounds\star1", ScreenManager.GraphicsDevice.Viewport.Width, -1);
            // star2.Initialize(content, @"Graphics\Backgrounds\star6", ScreenManager.GraphicsDevice.Viewport.Width, -1);




            //load enemies textures
            fireHairTexture = content.Load<Texture2D>(@"Graphics\firehair");
            asteroidTexture2 = content.Load<Texture2D>(@"Graphics\asteroid01");
            //asteroidTexture = content.Load<Texture2D>(@"Graphics\asteroid01");
            enemyTexture = content.Load<Texture2D>(@"Graphics\mineAnimation");
            balloonEnemyTexture = content.Load<Texture2D>(@"Graphics\mineGreenAnimation");
            powerupDamageTexture = content.Load<Texture2D>(@"Graphics\bolt_gold");
            powerupShieldTexture = content.Load<Texture2D>(@"Graphics\shield_gold");
            //todo dont think this mainbackground is drawn
           // mainBackground = content.Load<Texture2D>(@"Graphics\mainbackground");


            scoreFont = content.Load<SpriteFont>(@"Graphics\gameFont");
            //initialize projectile
            projectiles = new List<Projectile>();

            //powerups
            damagePowerUps = new List<PowerUp>();
            shieldPowerUps = new List<PowerUp>();
            //set the laser to fie every quarter second
            //fireTime = TimeSpan.FromSeconds(.15f);

            //load projectile
            projectileTexture = content.Load<Texture2D>(@"Graphics\lasergreen");

            // explosions
            explosions = new List<Animation>();
            explosion1Texture = content.Load<Texture2D>(@"Graphics\explosion");

            //initialize fire Hair
            fireHairEnemies = new List<FireHair>();
            previousFireHairSpawnTime = TimeSpan.Zero;
            fireHairSpawnTime = TimeSpan.FromSeconds(8f);
            //randomFireHair = new Random();


            //initialize asteroid
            asteroids2 = new List<AsteroidEnemy2>();
            //asteroids = new List<AsteroidEnemy>();
            previousAsteroidSpawnTime = TimeSpan.Zero;
            asteroidSpawnTime = TimeSpan.FromSeconds(10f);
            //randomAsteroid = new Random();


            //initialize enemies list etc..

            enemies = new List<Enemy>();
            //seperate enemies to balloon to add other ones.
            balloonEnemies = new List<GreenMineEnemy>();
            //set enemy spawn time keepers to zero
            previousSpawnTime = TimeSpan.Zero;
            previousBalloonSpawnTime = TimeSpan.Zero;
            //used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(2.0f);
            balloonEnemySpawnTime = TimeSpan.FromSeconds(5.0f);
            powerUpSpawnTime = TimeSpan.FromSeconds(10.0f);
            previousPowerUpSpawnTime = TimeSpan.Zero;
            previousDeathTime = TimeSpan.Zero;
            deathTime = TimeSpan.FromSeconds(1.0f);
            //initialize random number for enemies
           // randomEnemy = new Random();
            //randomPowerUp = new Random();
            //initialize a new player. not sure why have to do it here. 
            player = new Player();
            level = 1;
            //try projectile hee

            gameOver = false;

            //score = 0;

            //use animation now.

            Animation playerAnimation = new Animation();
            //load the player texture 
           // Texture2D playerTexture = content.Load<Texture2D>(@"Graphics\shipAnimation");
            Texture2D playerTexture = content.Load<Texture2D>(@"Graphics\playerShip1_greenrotate5066");
            
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 50, 66, 1, 30, Color.White, 1f, true);
            
            Vector2 playerPosition3 = new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition3);
            //if use scale here aiming wrong.
            

            //----animation section ^

            // player.Initialize(content.Load<Texture2D>(@"Graphics\player"), playerPosition3);
            playerMoveSpeed = 8.0f;
            
            // Create reusable pixel texture for HUD drawing
            pixelTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
            
            Thread.Sleep(1000);




            //todo take out music when switch menus
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion


        

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {


            

            if (otherScreenHasFocus.Equals(false))
            {
                if (AudioManager.IsInitialized.Equals(true))
                { }
                    //TODO change game music sucks
                    AudioManager.PlaySound("gamemusic");
            }
            else
            {
                AudioManager.StopSound("gamemusic");
            }
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {


                previousMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();

                // Update HUD animations
                hudPulseTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * HUD_PULSE_SPEED;
                healthBarPulse = (float)Math.Sin(hudPulseTimer) * 0.5f + 0.5f;
                shieldBarPulse = (float)Math.Sin(hudPulseTimer * 1.5f) * 0.5f + 0.5f;
                livesGlowIntensity = (float)Math.Sin(hudPulseTimer * 2f) * 0.3f + 0.7f;


                UpdatePlayer(gameTime);
                UpdateProjectiles();
                // bgLayer1.Update();
                // bgLayer2.Update();
                //udate scrolling background w \\todo
                // star1.Update();
                // star2.Update();
                //update the enemies

                UpdateEnemies(gameTime);
                UpdateCollision();
                UpdateExplosions(gameTime);

                UpdatePowerUp(gameTime);

                UpdateBackground();
                if (gameOver == true)
                {


                    ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);
                    //todo change this show scores and a top score make a high score screen
                    // LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new BackgroundScreen());
                    // LoadingScreen.Load(ScreenManager, true, PlayerIndex.One, new MainMenuScreen());
                }

            }
        }

        private void UpdateBackground()
        {
            newBackground.BackgroundOffset += 1;
            newBackground.ParallaxOffset += 2;

        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];
            KeyboardState lastKeyboardState = input.LastKeyboardStates[playerIndex];
            GamePadState lastGamePadState = input.LastGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];



            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                Vector2 movement = Vector2.Zero;
                //windows 8 gestures monogame
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gesture = TouchPanel.ReadGesture();

                    if (gesture.GestureType == GestureType.FreeDrag)
                    {
                        player.Position3 += gesture.Delta;


                    }
                }
                //mouse

                Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);


                if (currentMouseState.RightButton == ButtonState.Pressed)
                {
                    Vector2 posDelta = mousePosition - player.Position3;
                    posDelta.Normalize();
                    posDelta = posDelta * playerMoveSpeed;
                    player.Position3 = player.Position3 + posDelta;

                }
                // Otherwise move the player position.

                if (keyboardState.IsKeyDown(Keys.F))
                {
                    
                }
                if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                {
                    movement.X--;
                    //add for scroll background
                    newBackground.BackgroundOffset -= 1;
                    newBackground.ParallaxOffset -= 2;
                }



                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    movement.X++;
                    //add for scroll backgorund
                    newBackground.BackgroundOffset += 1;
                    newBackground.ParallaxOffset += 2;
                }


                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;
                // Make sure that the player does not go out of bounds
                player.Position3.X = MathHelper.Clamp(player.Position3.X, 0, ScreenManager.GraphicsDevice.Viewport.Width -(player.PlayerAnimation.FrameWidth * player.Scale));
                player.Position3.Y = MathHelper.Clamp(player.Position3.Y, 0, ScreenManager.GraphicsDevice.Viewport.Height - (player.PlayerAnimation.FrameHeight * player.Scale));






                //fire weapon normal

                if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released
                    || keyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space))
                {
                   
                    AddProjectile(player.Position3 + new Vector2(player.Width /2 , player.Height /2));
                    
                    //todo
                    //weaon fire not hitting bottom 


                    AudioManager.PlaySound("laserSound");

                }


              
                if (movement.Length() > 1)
                    movement.Normalize();



                //new player move
                player.Position3 += movement * playerMoveSpeed;
            }
        }


        private void PlayerKilled()
        {


            AddExplosion(player.Position3);
            AudioManager.PlaySound("explosionSound");
            iLivesLeft -= 1;

            player.Active = false;
            

            if (iLivesLeft > 0)
            {
                
                ScreenManager.AddScreen(new KilledMenuScreen(), ControllingPlayer);
                player.Respawn();
                
                player.Position3 = new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            }
            else
            {

                //ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);
                gameOver = true;
                player.Reset();
                player.Position3 = new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
                iLivesLeft = 3;
                ResetEnemies();

            }

        }


        //todo this is not working. 
        private void ResetEnemies()
        {
            for (int i = 0; i < balloonEnemies.Count; i++)
            {
                balloonEnemies.RemoveAt(i);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies.RemoveAt(i);
            }

            for (int i = 0; i < damagePowerUps.Count; i++)
            {
                damagePowerUps.RemoveAt(i);
            }
        }

        private void UpdatePlayer(GameTime gameTime)
        {

            player.Update(gameTime);
            if (player.Shield <= 0)
            {
                player.shieldActive = false;
                player.Shield = 0;
                
            }
           // else
           // {
           //     player.shieldActive = true;
           // }

            //activate glow part eventually now desaturation on shield 
            ////todo
            //if (player.shieldActive ==true)
            //{
            //    standardEffect.CurrentTechnique = standardEffect.Techniques["DesaturateTechnique"];
            //    standardEffect.Parameters["DesaturationAmount"].SetValue(1.0f);
            //}
            //else if(player.shieldActive == false)
            //{
            //    standardEffect.CurrentTechnique = standardEffect.Techniques["DesaturateTechnique"];
            //    standardEffect.Parameters["DesaturationAmount"].SetValue(0.0f);
            //}

            

            //set level by score
            if (player.Score > 1000 && player.Score < 1999)
            {
                level = 2;
            }
            if (player.Score >2000 )
            {
                level = 3;
            }
           

            if (player.Health <= 0 && gameTime.TotalGameTime - previousDeathTime > deathTime)
            {
                previousDeathTime = gameTime.TotalGameTime;
                //todo
                PlayerKilled();
                

            }
            // if (player.Health <= 0)
            // {

            //   PlayerKilled();


            // }
        }
        private void UpdateProjectiles()
        {
            //update projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {



                projectiles[i].Update();
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                }

            }
        }


        private void UpdateCollision()
        {
            //use the rectangles built in intersect funtion to determine if two objects are overlapping
            //only create the rectangle once for the player
            var playerRectangle = new Rectangle((int)player.Position3.X, (int)player.Position3.Y, player.Width -25, player.Height -20);
            


            //shield powerup collision
            for (int i = 0; i < shieldPowerUps.Count; i++)
            {
                var shieldPowerUpRectangle = new Rectangle((int)shieldPowerUps[i].Position.X, (int)shieldPowerUps[i].Position.Y, shieldPowerUps[i].Width, shieldPowerUps[i].Height);
                if (playerRectangle.Intersects(shieldPowerUpRectangle))
                {

                    AudioManager.PlaySound("powerup");
                    // moved the damage mod to the powerup class
                    shieldPowerUps[i].PowerUpCollision();
                    player.shieldActive = true;


                    shieldPowerUps[i].Active = false;
                }

            }

            //damage powerup collision //try a foreach loop here
            foreach (PowerUp damagepowerup in damagePowerUps)
            {
                var damagePowerUpRectangle = new Rectangle((int)damagepowerup.Position.X, (int)damagepowerup.Position.Y, damagepowerup.Width, damagepowerup.Height);
                if (playerRectangle.Intersects(damagePowerUpRectangle))
                {

                    AudioManager.PlaySound("powerup");
                    // moved the damage mod to the powerup class
                    damagepowerup.PowerUpCollision();



                    damagepowerup.Active = false;
                }
            }


               

            //todo firehair collision
            for (int i = 0; i < fireHairEnemies.Count; i++)
            {
               var fireHairRectangle = new Rectangle((int)fireHairEnemies[i].Position.X, (int)fireHairEnemies[i].Position.Y, fireHairEnemies[i].Width, fireHairEnemies[i].Height);
                if (playerRectangle.Intersects(fireHairRectangle))
                {
                    if (player.Shield > 0)
                    {
                        player.Shield -= fireHairEnemies[i].Damage;
                        fireHairEnemies[i].Health -= player.Damage;
                    }
                    else
                    {
                        player.Health -= fireHairEnemies[i].Damage;
                        fireHairEnemies[i].Health -= player.Damage;
                    }
                }
                }
            //asteroid to player collision
            for (int i = 0; i < asteroids2.Count; i++)
            {
                var asteroidRectangle = new Rectangle((int)asteroids2[i].Position.X, (int)asteroids2[i].Position.Y, asteroids2[i].Width, asteroids2[i].Height);
                if (playerRectangle.Intersects(asteroidRectangle))
                {
                    if (player.Shield > 0)
                    {
                        player.Shield -= asteroids2[i].Damage;
                        asteroids2[i].Health -= player.Damage;
                    }


                    else
                    {
                        player.Health -= asteroids2[i].Damage;
                        asteroids2[i].Health -= player.Damage;
                    }


                }



            }

            
            //todo collision with balloonenemy and player
            for (int i = 0; i < balloonEnemies.Count; i++)
            {
               var balloonEnemyRectangle2 = new Rectangle((int)balloonEnemies[i].Position.X, (int)balloonEnemies[i].Position.Y, balloonEnemies[i].Width, balloonEnemies[i].Height);
                if (playerRectangle.Intersects(balloonEnemyRectangle2))
                {

                    if (player.Shield > 0)
                    {
                        player.Shield -= balloonEnemies[i].Damage;
                        balloonEnemies[i].Health -= player.Damage;
                    }
                    else
                    {
                        player.Health -= balloonEnemies[i].Damage;
                        balloonEnemies[i].Health -= player.Damage;
                    }



                }
            }
            //do the collision between the player and the enemies
            for (int i = 0; i < enemies.Count; i++)
            {

                var enemyRectangle2 = new Rectangle((int)enemies[i].Position.X, (int)enemies[i].Position.Y, enemies[i].Width, enemies[i].Height);
                //determine if the thwo objects collided with each other
                if (playerRectangle.Intersects(enemyRectangle2))
                {
                    
                    if (player.Shield > 0)
                    {
                        player.Shield -= enemies[i].Damage;
                        enemies[i].Health -= player.Damage;
                    }

                    else
                    {
                        player.Health -= enemies[i].Damage;

                        enemies[i].Health -= player.Damage;
                    }



                }
            }


            #region projectile vs enemies collision
            //todo add fire hair enemy collisio vs projectile


            //projectile vs enemies collision
            for (int i = 0; i < projectiles.Count; i++)
            {

                //see if this works here
                var projectileRectangle = new Rectangle((int)projectiles[i].Position.X - projectiles[i].Width , (int)projectiles[i].Position.Y - projectiles[i].Height , projectiles[i].Width, projectiles[i].Height);

                for (int j = 0; j < balloonEnemies.Count; j++)
                {
                    //create the rectangles we need to determine if we collided with each other

                    // projectileRectangle = new Rectangle((int)projectiles[i].Position.X - projectiles[i].Width / 2, (int)projectiles[i].Position.Y - projectiles[i].Height / 2, projectiles[i].Width, projectiles[i].Height);
                   var balloonEnemyRectangle2 = new Rectangle((int)balloonEnemies[j].Position.X - balloonEnemies[j].Width / 2, (int)balloonEnemies[j].Position.Y - balloonEnemies[j].Height / 2, balloonEnemies[j].Width, balloonEnemies[j].Height);
                    //determine if the two objects collide with each other
                    if (projectileRectangle.Intersects(balloonEnemyRectangle2))
                    {
                        balloonEnemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }
                }
                //projectile vs asteroids
                for (int k = 0; k < asteroids2.Count; k++)
                {
                    var asteroidRectangle = new Rectangle((int)asteroids2[k].Position.X - asteroids2[k].Width / 2, (int)asteroids2[k].Position.Y - asteroids2[k].Height / 2, asteroids2[k].Width, asteroids2[k].Height);
                    if (projectileRectangle.Intersects(asteroidRectangle))
                    {
                        asteroids2[k].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }

                }

                for (int t = 0; t < enemies.Count; t++)
                {
                    //create the rectanles we need to determine if we collided with each other
                   // projectileRectangle = new Rectangle((int)projectiles[i].Position.X - projectiles[i].Width / 2, (int)projectiles[i].Position.Y - projectiles[i].Height / 2, projectiles[i].Width, projectiles[i].Height);

                    var enemyRectangle2 = new Rectangle((int)enemies[t].Position.X - enemies[t].Width / 2, (int)enemies[t].Position.Y - enemies[t].Height / 2, enemies[t].Width, enemies[t].Height);
                    //determine if the two objects collide with each other
                    if (projectileRectangle.Intersects(enemyRectangle2))
                    {
                        enemies[t].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }
                }

            }

            #endregion




        }


        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (!explosions[i].Active)
                {
                    explosions.RemoveAt(i);
                }
            }
        }

        //addding powerup
        private void UpdatePowerUp(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousPowerUpSpawnTime > powerUpSpawnTime)
            {
                previousPowerUpSpawnTime = gameTime.TotalGameTime;
                //todo
                //don't want both at same time but will look at
                int rannum;
                //ranp = new Random();
                rannum = random.Next(20);
                if (rannum >= 11)
                {
                    AddDamagePowerUp();
                }
                else
                {
                    AddShieldPowerUp();
                }


            }

            for (int i = shieldPowerUps.Count - 1; i >= 0; i--)
            {
                shieldPowerUps[i].Update(gameTime);
                if (shieldPowerUps[i].Active == false)
                {
                    shieldPowerUps.RemoveAt(i);
                }
            }

            for (int i = damagePowerUps.Count - 1; i >= 0; i--)
            {
                damagePowerUps[i].Update(gameTime);
                if (!damagePowerUps[i].Active)
                {
                    damagePowerUps.RemoveAt(i);
                    //todo do something
                }
            }
        }
       
        
        
        
        private void UpdateEnemies(GameTime gameTime)
        {

            if (level >=1)
            {
                //spawn a new enemy every 1.5 seconds
                if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
                {
                    previousSpawnTime = gameTime.TotalGameTime;
                    //add the enemy
                    AddEnemy();

                }
            }
          
           
            if (level >=2)
            {
                if (gameTime.TotalGameTime - previousFireHairSpawnTime > fireHairSpawnTime)
                {
                    previousFireHairSpawnTime = gameTime.TotalGameTime;
                    AddFireHair();
                }
                //spawn ballon enemies every 5 sec
                if (gameTime.TotalGameTime - previousBalloonSpawnTime > balloonEnemySpawnTime)
                {
                    previousBalloonSpawnTime = gameTime.TotalGameTime;
                    //add abllon enemies
                    AddBalloonEnemy();
                }

            }
            
            if(level >=3)
            {
                //spawn asteroids
                if (gameTime.TotalGameTime - previousAsteroidSpawnTime > asteroidSpawnTime)
                {
                    previousAsteroidSpawnTime = gameTime.TotalGameTime;
                    AddAsteroid();
                }
            }
           
            //update asteroids

            for (int k = asteroids2.Count - 1; k >= 0; k--)
            {
                asteroids2[k].Update(gameTime);

                if (!asteroids2[k].Active)
                {
                    AddExplosion(asteroids2[k].Position);
                    AudioManager.PlaySound("explosionSound");
                    player.Score += asteroids2[k].Value;
                    asteroids2.RemoveAt(k);
                }
                else if (asteroids2[k].Active && !asteroids2[k].OnScreen )
                {
                    asteroids2.RemoveAt(k);
                }
                
            }

            //update fire hair
            for (int i =fireHairEnemies.Count - 1; i>=0;i--)
            {
                fireHairEnemies[i].Update(gameTime);
                if (!fireHairEnemies[i].Active)
                {
                    AddExplosion(fireHairEnemies[i].Position);
                    AudioManager.PlaySound("explosionSound");
                    player.Score += fireHairEnemies[i].Value;
                    
                    fireHairEnemies.RemoveAt(i);
                }
            }
            //update balloon enemies
            for (int i = balloonEnemies.Count - 1; i >= 0; i--)
            {
                balloonEnemies[i].Update(gameTime);
                if (!balloonEnemies[i].Active)
                {
                    AddExplosion(balloonEnemies[i].Position);
                    AudioManager.PlaySound("explosionSound");
                    player.Score += balloonEnemies[i].Value;

                    balloonEnemies.RemoveAt(i);

                }

                else if (balloonEnemies[i].Active && !balloonEnemies[i].OnScreen)
                {
                    balloonEnemies.RemoveAt(i);
                }


            }
            //update the enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (!enemies[i].Active )
                {
                    AddExplosion(enemies[i].Position);
                    AudioManager.PlaySound("explosionSound");
                    player.Score += enemies[i].Value;
                    enemies.RemoveAt(i);
                }
                else if (enemies[i].Active && !enemies[i].OnScreen)
                {
                    enemies.RemoveAt(i);
                }
            }
        }


        private void AddProjectile(Vector2 position)
        {

            //moving to top
            int projDamage = player.DamageMod + 3;
            Projectile projectile = new Projectile();
            projectile.Initialize(ScreenManager.GraphicsDevice.Viewport, projectileTexture, position, projDamage);




            projectiles.Add(projectile);
        }

        private void AddAsteroid()
        {
            //this only uses a texture so need to try it this way. with animation anyuway
            //todo
            //Animation asteroidAnimation = new Animation();
            //asteroidAnimation.Initialize(asteroidTexture, Vector2.Zero, 40, 40, 1, 1, Color.White, 1f, true);
            AsteroidEnemy2 asteroid = new AsteroidEnemy2();

            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + asteroidTexture2.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 50));
            //AsteroidEnemy asteroid = new AsteroidEnemy();
            asteroid.Initialize(ScreenManager.GraphicsDevice.Viewport, asteroidTexture2, position);
            asteroids2.Add(asteroid);


        }
        //this addballoonenemy probably be taken out. but can add more later
        private void AddFireHair()
        {
            FireHair fireHairEnemy = new FireHair();
            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + fireHairTexture.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 70));
            fireHairEnemy.Initialize(ScreenManager.GraphicsDevice.Viewport, fireHairTexture, position, 40);
            fireHairEnemies.Add(fireHairEnemy);
        }
        private void AddBalloonEnemy()
        {
            //create the animation object
            Animation balloonEnemyAnimation = new Animation();
            //initizlize theanimation with the correct ahimation information
            balloonEnemyAnimation.Initialize(balloonEnemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);
            //randomly generate the position of the enemy or later change this to a specific spot
            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 100));
            //create an enemy
            GreenMineEnemy balloonEnemy = new GreenMineEnemy();
            //initizlize the enemy
            balloonEnemy.Initialize(balloonEnemyAnimation, position);
            // add the enemy to the active enemies list
            balloonEnemies.Add(balloonEnemy);

        }
        private void AddExplosion(Vector2 position)
        {
            Animation explosion = new Animation();
            explosion.Initialize(explosion1Texture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(explosion);
        }
        private void AddShieldPowerUp()
        {
            //todo would like to combine all powerups somehow
            PowerUp shieldPowerUp = new PowerUp();
            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + powerupShieldTexture.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 75));
            shieldPowerUp.Initialize(ScreenManager.GraphicsDevice.Viewport, powerupShieldTexture, position, "ShieldPowerUp", player);
            shieldPowerUps.Add(shieldPowerUp);
        }
        private void AddDamagePowerUp()
        {

            //todo

            PowerUp damagePowerUp = new PowerUp();
            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + powerupDamageTexture.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 75));
            damagePowerUp.Initialize(ScreenManager.GraphicsDevice.Viewport, powerupDamageTexture, position, "DamagePowerUp", player);

            damagePowerUps.Add(damagePowerUp);

        }
        private void AddEnemy()
        {
            //create the animation object
            Animation enemyAnimation = new Animation();
            //Animation balloonEnemyAnimation = new Animation();
            //initizlize theanimation with the correct ahimation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);
            // balloonEnemyAnimation.Initialize(balloonEnemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);
            //randomly generate the position of the enemy or later change this to a specific spot
            Vector2 position = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 100));
            // Vector2 balloonPosition = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width + balloonEnemyTexture.Width / 2, randomEnemy.Next(100, ScreenManager.GraphicsDevice.Viewport.Height - 100));

            //create an enemy
            Enemy enemy = new Enemy();
            //Enemy balloonEnemy = new Enemy();
            //initizlize the enemy
            enemy.Initialize(enemyAnimation, position);
            //balloonEnemy.Initialize(balloonEnemyAnimation, balloonPosition);
            // add the enemy to the active enemies list
            enemies.Add(enemy);
            //balloonEnemies.Add(balloonEnemy);
        }


        /// <summary>
        /// Draws an improved HUD bar with background, foreground, and glow effects
        /// </summary>
        private void DrawHUDBar(SpriteBatch spriteBatch, string label, int currentValue, int maxValue, 
            Vector2 position, Color backgroundColor, Color foregroundColor, Color glowColor, float pulseAmount)
        {
            // Calculate bar fill percentage
            float percentage = MathHelper.Clamp((float)currentValue / maxValue, 0f, 1f);
            int fillWidth = (int)(HUD_BAR_WIDTH * percentage);

            // Measure label text
            Vector2 labelSize = scoreFont.MeasureString(label);
            
            // Draw label box background (like Score/Lives) - reduced size
            int labelBoxWidth = (int)(labelSize.X * 0.85f) + 12; // Reduced by ~20%
            int labelBoxHeight = 18; // Reduced from 22
            Vector2 labelPos = position;
            
            Rectangle labelGlowRect = new Rectangle((int)labelPos.X - 6, (int)labelPos.Y - 6, labelBoxWidth + 12, labelBoxHeight + 12);
            Rectangle labelPanelRect = new Rectangle((int)labelPos.X - 4, (int)labelPos.Y - 4, labelBoxWidth + 8, labelBoxHeight + 8);
            Rectangle labelBorderRect = new Rectangle((int)labelPos.X - 3, (int)labelPos.Y - 3, labelBoxWidth + 6, labelBoxHeight + 6);
            Rectangle labelInnerRect = new Rectangle((int)labelPos.X - 2, (int)labelPos.Y - 2, labelBoxWidth + 4, labelBoxHeight + 4);
            
            spriteBatch.Draw(pixelTexture, labelGlowRect, foregroundColor * (0.2f + pulseAmount * 0.2f));
            spriteBatch.Draw(pixelTexture, labelPanelRect, Color.Black * 0.7f);
            spriteBatch.Draw(pixelTexture, labelBorderRect, foregroundColor * 0.6f);
            spriteBatch.Draw(pixelTexture, labelInnerRect, Color.Black * 0.8f);
            
            // Draw label text - slightly smaller
            Vector2 labelTextPos = new Vector2(labelPos.X + 5, labelPos.Y + labelBoxHeight / 2 - (labelSize.Y * 0.9f) / 2 + 1);
            spriteBatch.DrawString(scoreFont, label, labelTextPos + new Vector2(1, 1), Color.Black * 0.8f, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, label, labelTextPos, Color.White, 0f, Vector2.Zero, 0.9f, SpriteEffects.None, 0f);

            // Bar position (below label with increased spacing)
            Vector2 barPosition = new Vector2(position.X, position.Y + labelBoxHeight + 8);

            // Draw bar background with border
            Rectangle backgroundRect = new Rectangle((int)barPosition.X - 2, (int)barPosition.Y - 2, HUD_BAR_WIDTH + 4, HUD_BAR_HEIGHT + 4);
            spriteBatch.Draw(pixelTexture, backgroundRect, Color.Black * 0.8f);
            
            Rectangle innerBackgroundRect = new Rectangle((int)barPosition.X, (int)barPosition.Y, HUD_BAR_WIDTH, HUD_BAR_HEIGHT);
            spriteBatch.Draw(pixelTexture, innerBackgroundRect, backgroundColor * 0.3f);

            // Draw the filled portion with pulsing glow effect
            if (fillWidth > 0)
            {
                // Glow layer
                Rectangle glowRect = new Rectangle((int)barPosition.X - 1, (int)barPosition.Y - 1, fillWidth + 2, HUD_BAR_HEIGHT + 2);
                spriteBatch.Draw(pixelTexture, glowRect, glowColor * (0.3f + pulseAmount * 0.3f));

                // Main fill
                Rectangle fillRect = new Rectangle((int)barPosition.X, (int)barPosition.Y, fillWidth, HUD_BAR_HEIGHT);
                Color finalColor = Color.Lerp(foregroundColor, glowColor, pulseAmount * 0.3f);
                spriteBatch.Draw(pixelTexture, fillRect, finalColor);
            }

            // Draw value text on top of bar - better centered and smaller
            string valueText = $"{currentValue}/{maxValue}";
            Vector2 textSize = scoreFont.MeasureString(valueText);
            float textScale = 0.85f; // 15% smaller text
            Vector2 scaledTextSize = textSize * textScale;
            Vector2 textPosition = new Vector2(barPosition.X + HUD_BAR_WIDTH / 2 - scaledTextSize.X / 2, 
                                               barPosition.Y + HUD_BAR_HEIGHT / 2 - scaledTextSize.Y / 2 + 1);
            
            // Text with outline for readability
            spriteBatch.DrawString(scoreFont, valueText, textPosition + new Vector2(1, 1), Color.Black, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, valueText, textPosition + new Vector2(-1, -1), Color.Black, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, valueText, textPosition + new Vector2(1, 0), Color.Black, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, valueText, textPosition + new Vector2(0, 1), Color.Black, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, valueText, textPosition, Color.White, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws an improved numeric display with icon-like appearance
        /// </summary>
        private void DrawHUDValue(SpriteBatch spriteBatch, string label, int value, Vector2 position, Color color, float glowIntensity)
        {
            // Measure the text to size the box appropriately
            string labelText = label;
            string valueText = value.ToString();
            Vector2 labelSize = scoreFont.MeasureString(labelText);
            Vector2 valueSize = scoreFont.MeasureString(valueText);
            
            int boxWidth = (int)(labelSize.X + valueSize.X + 20);
            int boxHeight = 30;
            
            // Draw background panel with glow
            Rectangle glowRect = new Rectangle((int)position.X - 7, (int)position.Y - 7, boxWidth + 14, boxHeight + 14);
            Rectangle panelRect = new Rectangle((int)position.X - 5, (int)position.Y - 5, boxWidth + 10, boxHeight + 10);
            
            spriteBatch.Draw(pixelTexture, glowRect, color * (0.2f + glowIntensity * 0.3f));
            spriteBatch.Draw(pixelTexture, panelRect, Color.Black * 0.7f);
            
            Rectangle borderRect = new Rectangle((int)position.X - 4, (int)position.Y - 4, boxWidth + 8, boxHeight + 8);
            spriteBatch.Draw(pixelTexture, borderRect, color * 0.6f);
            
            Rectangle innerRect = new Rectangle((int)position.X - 3, (int)position.Y - 3, boxWidth + 6, boxHeight + 6);
            spriteBatch.Draw(pixelTexture, innerRect, Color.Black * 0.8f);

            // Draw label with shadow
            Vector2 labelPos = new Vector2(position.X + 5, position.Y + boxHeight / 2 - labelSize.Y / 2 + 2);
            spriteBatch.DrawString(scoreFont, labelText, labelPos + new Vector2(1, 1), Color.Black * 0.8f);
            spriteBatch.DrawString(scoreFont, labelText, labelPos, color);

            // Draw value with glow - positioned to the right
            Vector2 valuePosition = new Vector2(position.X + boxWidth - valueSize.X, position.Y + boxHeight / 2 - valueSize.Y / 2 + 2);
            
            Color glowColor = Color.Lerp(color, Color.White, glowIntensity * 0.5f);
            spriteBatch.DrawString(scoreFont, valueText, valuePosition + new Vector2(1, 1), glowColor * 0.5f);
            spriteBatch.DrawString(scoreFont, valueText, valuePosition, glowColor);
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

           

            spriteBatch.Begin();



            //spriteBatch.Draw(backgroundStart, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            //spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);
            //draw new paralaxing background
            //spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            //  star1.Draw(spriteBatch);
            //  star2.Draw(spriteBatch);
            //dont draw these for now
            // bgLayer1.Draw(spriteBatch);
            // bgLayer2.Draw(spriteBatch);
            //draw the score
            //draw sroller

            newBackground.Draw(spriteBatch);

            // Draw improved HUD elements at the top of the screen
            int hudY = HUD_TOP_MARGIN;

            // Draw Score (top-left, larger and prominent)
            Vector2 scorePos = new Vector2(HUD_LEFT_MARGIN, hudY);
            DrawHUDValue(spriteBatch, "SCORE", player.Score, scorePos, Color.Gold, livesGlowIntensity);

            // Draw Lives (top-right area)
            Vector2 livesPos = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width - 150, hudY);
            Color livesColor = iLivesLeft > 1 ? Color.Cyan : Color.Red;
            DrawHUDValue(spriteBatch, "LIVES", iLivesLeft, livesPos, livesColor, livesGlowIntensity);

            hudY += 50;

            // Draw Health bar with green color scheme
            Vector2 healthPos = new Vector2(HUD_LEFT_MARGIN, hudY);
            Color healthBg = new Color(20, 40, 20);
            Color healthFg = Color.LimeGreen;
            Color healthGlow = Color.GreenYellow;
            
            // Pulsing red if health is low
            if (player.Health < 30)
            {
                healthFg = Color.Red;
                healthGlow = Color.OrangeRed;
                healthBarPulse = (float)Math.Sin(hudPulseTimer * 5f) * 0.5f + 0.5f; // Faster pulse when critical
            }
            
            DrawHUDBar(spriteBatch, "HEALTH", player.Health, 100, healthPos, healthBg, healthFg, healthGlow, healthBarPulse);

            hudY += HUD_BAR_TOTAL_HEIGHT;

            // Draw Shield bar with blue/cyan color scheme
            Vector2 shieldPos = new Vector2(HUD_LEFT_MARGIN, hudY);
            Color shieldBg = new Color(20, 20, 40);
            Color shieldFg = Color.DeepSkyBlue;
            Color shieldGlow = Color.Cyan;
            
            int maxShield = 100; // Assuming max shield is 100, adjust if needed
            DrawHUDBar(spriteBatch, "SHIELD", player.Shield, maxShield, shieldPos, shieldBg, shieldFg, shieldGlow, shieldBarPulse);

            hudY += HUD_BAR_TOTAL_HEIGHT;

            // Draw Damage modifier with orange/yellow color scheme - smaller
            Vector2 damagePos = new Vector2(HUD_LEFT_MARGIN, hudY);
            Color damageColor = player.DamageMod > 0 ? Color.Orange : Color.Gray;
            float damageGlow = player.DamageMod > 0 ? healthBarPulse : 0.3f;
            
            // Damage display panel (simpler display) - scaled down
            string damageText = $"DAMAGE +{player.DamageMod}";
            Vector2 damageTextSize = scoreFont.MeasureString(damageText);
            float damageScale = 0.9f; // 10% smaller
            Vector2 scaledDamageSize = damageTextSize * damageScale;
            
            int damagePanelWidth = (int)(scaledDamageSize.X) + 18;
            int damagePanelHeight = 22; // Reduced from 28
            
            Rectangle damageGlowRect = new Rectangle((int)damagePos.X - 7, (int)damagePos.Y - 7, damagePanelWidth + 14, damagePanelHeight + 14);
            Rectangle damagePanelRect = new Rectangle((int)damagePos.X - 5, (int)damagePos.Y - 5, damagePanelWidth + 10, damagePanelHeight + 10);
            
            spriteBatch.Draw(pixelTexture, damageGlowRect, damageColor * (0.2f + damageGlow * 0.3f));
            spriteBatch.Draw(pixelTexture, damagePanelRect, Color.Black * 0.7f);
            
            Rectangle damageBorderRect = new Rectangle((int)damagePos.X - 4, (int)damagePos.Y - 4, damagePanelWidth + 8, damagePanelHeight + 8);
            spriteBatch.Draw(pixelTexture, damageBorderRect, damageColor * 0.6f);
            
            Vector2 damageTextPos = new Vector2(damagePos.X + 5, damagePos.Y + damagePanelHeight / 2 - scaledDamageSize.Y / 2 + 2);
            spriteBatch.DrawString(scoreFont, damageText, damageTextPos + new Vector2(1, 1), Color.Black, 0f, Vector2.Zero, damageScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(scoreFont, damageText, damageTextPos, damageColor, 0f, Vector2.Zero, damageScale, SpriteEffects.None, 0f);

            //spriteBatch.DrawString(scoreFont, "score:" + player.Score, scorePosition, Color.White);
            //spriteBatch.DrawString(scoreFont, "Health: " + player.Health, new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 35), Color.White);

            //spriteBatch.DrawString(scoreFont, "Lives: " + iLivesLeft, new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 70), Color.White);

            //spriteBatch.DrawString(scoreFont, "Shield: " + player.Shield, new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 100), Color.White);

            //spriteBatch.DrawString(scoreFont, "Damage: + " + player.DamageMod, new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 130), Color.White);
            //draw asteroids
            foreach(AsteroidEnemy2 asteroid in asteroids2)
            {
                asteroid.Draw(spriteBatch);
            }
            //for (int i = 0; i < asteroids2.Count; i++)
            //{
            //    asteroids2[i].Draw(spriteBatch);
            //}

            //draw the enemies

            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    enemies[i].Draw(spriteBatch);

            //}

            //draw balloon enemies
            foreach (GreenMineEnemy balloonenemy in balloonEnemies)
                balloonenemy.Draw(spriteBatch);
            //for (int i = 0; i < balloonEnemies.Count; i++)
            //{
            //    balloonEnemies[i].Draw(spriteBatch);
            //}
            
            
            //working now draw player from player class.
            
            //player.Draw(spriteBatch);
            //draw fire hair
            foreach(FireHair firehair in fireHairEnemies)
            {
                firehair.Draw(spriteBatch);
            }
          
            //draw projectiles
            foreach(Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            //for (int i = 0; i < projectiles.Count; i++)
            //{
            //    projectiles[i].Draw(spriteBatch);
            //}

            //draw explosions
            foreach(Animation explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }
            //for (int i = 0; i < explosions.Count; i++)
            //{
            //    explosions[i].Draw(spriteBatch);
            //}

            //draw damage powerup  //changed to foreachloop
            foreach(PowerUp damagepowerup in damagePowerUps)
            {
                damagepowerup.Draw(spriteBatch);
            }
            //for (int i = 0; i < damagePowerUps.Count; i++)
            //{
            //    damagePowerUps[i].Draw(spriteBatch);
            //}
            //draw shield powerup //changed to foreach loop
            foreach(PowerUp shieldpowerup in shieldPowerUps)
            {
                shieldpowerup.Draw(spriteBatch);
            }
            //for (int i = 0; i < shieldPowerUps.Count; i++)
            //{
            //    shieldPowerUps[i].Draw(spriteBatch);
            //}



            //todo testing 
            if (iLivesLeft == 0)
            {
                spriteBatch.DrawString(gameFont, "G A M E  O V E R", new Vector2(ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X, ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 45), Color.Red);
            }
            //spriteBatch.Draw(playerTexture, playerPosition, Color.White);
            // spriteBatch.DrawString(gameFont, "Insert Gameplay Here",
            //                     enemyPosition, Color.DarkRed);

            spriteBatch.End();


            //draw player and have effects on it as well. ?

            //effect is off as it is not working right now.
            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, standardEffect);
            //todo
            //make a technique to glow eventually when shield is up
            //todo
            //working so far if shield on do the effect on player. just desaturate for now
            
            
            
           
            if(player.Active)
            {
                player.Draw(spriteBatch);
            }

            

            spriteBatch.End();


            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion
    }
}
