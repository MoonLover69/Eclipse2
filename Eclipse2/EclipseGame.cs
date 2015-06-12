using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Eclipse2Game.GameObjects;

namespace Eclipse2Game
{
    public enum GameContent
    {
        MainMenu,
        Game,
        Pause,
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EclipseGame : Microsoft.Xna.Framework.Game
    {
        private Sprite testSprite = new Sprite("Images/SplashScreen");
        private Sound _mainMusic = new Sound("Sounds/Music/maintheme", true);
        ParticleEngine particleEngine;
        SpriteBatch spriteBatch;
        public EclipseGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Current state of the game window (game, menu, etc)
        /// </summary>
        public GameContent ActiveContent
        {
            get;
            set;
        }

        /// <summary>
        /// Manager of window graphics
        /// </summary>
        public GraphicsDeviceManager GraphicsManager
        {
            get;
            set;
        }

        /// <summary>
        /// Manager of sprites
        /// </summary>
        public SpriteBatch SpriteManager
        {
            get;
            set;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Mouse.WindowHandle = this.Window.Handle;
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteManager = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("ParticleCircle"));
            textures.Add(Content.Load<Texture2D>("ParticleStar"));
            textures.Add(Content.Load<Texture2D>("ParticleMoon"));
            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));
            testSprite.LoadContent(Content);
            _mainMusic.LoadContent(Content);
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
            if (ActiveContent == GameContent.MainMenu)
            {
                if (!_mainMusic.IsPlaying)
                {
                    _mainMusic.PlayRepeated();
                }
                
            }
            else
            {
                _mainMusic.Stop();
            }
            particleEngine.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            particleEngine.Update();
            // Get some input.
            UpdateInputs();

            base.Update(gameTime);
        }

        protected void UpdateInputs()
        {
            // Get the game pad state.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsConnected)
            {
            }

            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ActiveContent = GameContent.Game;
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (ActiveContent == GameContent.MainMenu)
                {
                    this.Exit();
                }
                else
                {
                    ActiveContent = GameContent.MainMenu;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            particleEngine.Draw(spriteBatch);
            SpriteManager.Begin();

            if (ActiveContent == GameContent.MainMenu)
            {
                testSprite.Draw(SpriteManager);
            }

            SpriteManager.End();

            base.Draw(gameTime);
        }
    }
}
