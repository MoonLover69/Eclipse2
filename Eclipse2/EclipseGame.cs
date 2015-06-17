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
    public enum WindowContent
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
        MainMenu _mainMenu;
        MainGame _game;

        public EclipseGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);

            GraphicsManager.IsFullScreen = false;
            GraphicsManager.PreferredBackBufferWidth = CoordinateHelper.WindowWidth;
            GraphicsManager.PreferredBackBufferHeight = CoordinateHelper.WindowHeight;
            GraphicsManager.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Current state of the game window (game, menu, etc)
        /// </summary>
        public WindowContent ActiveContent
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

            _mainMenu = new MainMenu();

            _game = new MainGame();

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

            _mainMenu.LoadContent(Content);
            _game.LoadContent(Content);
            //List<Texture2D> textures = new List<Texture2D>();
            //textures.Add(Content.Load<Texture2D>("ParticleCircle"));
            //textures.Add(Content.Load<Texture2D>("ParticleStar"));
            //textures.Add(Content.Load<Texture2D>("ParticleMoon"));
            //particleEngine = new ParticleEngine(textures, new Vector2(400, 240));
            //_mainMusic.LoadContent(Content);
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
            if (ActiveContent == WindowContent.MainMenu)
            {
                _mainMenu.IsActive = true;
                _mainMenu.Update(gameTime);
            }
            else
            {
                _mainMenu.IsActive = false;
            }

            if (ActiveContent == WindowContent.Game)
            {
                _game.IsActive = true;
                _game.Update(gameTime);
            }
            else
            {
                _game.IsActive = false;
            }

            // Get some input.
            UpdateInputs();

            base.Update(gameTime);
        }

        protected void UpdateInputs()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ActiveContent = WindowContent.Game;
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                ActiveContent = WindowContent.MainMenu;
            }

            if (_mainMenu.IsActive)
            {
                _mainMenu.HandleKeyboardInput(keyboardState);
                _mainMenu.HandleMouseInput(mouseState);
            }

            if (_game.IsActive)
            {
                _game.HandleKeyboardInput(keyboardState);
                _game.HandleMouseInput(mouseState);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Update the screen scale
            var scaleX = (float)GraphicsDevice.Viewport.Width / CoordinateHelper.WindowWidth;
            var scaleY = (float)GraphicsDevice.Viewport.Height / CoordinateHelper.WindowHeight;

            Vector3 screenScale = new Vector3(scaleX, scaleY, 1.0f);

            GraphicsDevice.Clear(Color.Black);

            SpriteManager.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(screenScale));

            if (_mainMenu.IsActive)
            {
                _mainMenu.Draw(gameTime, SpriteManager);
            }

            if (_game.IsActive)
            {
                _game.Draw(gameTime, SpriteManager);
            }

            SpriteManager.End();

            base.Draw(gameTime);
        }
    }
}
