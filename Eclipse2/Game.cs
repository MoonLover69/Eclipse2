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
    public enum GameState
    {
        Menu,
        Game,
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GameState _currentState = GameState.Menu;
        private Vector2 _position = new Vector2(300, 100);
        private Sprite testSprite = new Sprite("Images/SplashScreen");
        private Sound _mainMusic = new Sound("Sounds/Music/maintheme", true);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Current state of the game window (game, menu, etc)
        /// </summary>
        public GameState State
        {
            get
            {
                return _currentState;
            }
            private set
            {
                if (_currentState != value)
                {
                    _currentState = value;

                    var handler = StateChanged;
                    if (handler != null)
                    {
                        handler(this, new StateChangedEventArgs(State, value));
                    }
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
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
            if (State == GameState.Menu)
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
                State = GameState.Game;
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                State = GameState.Menu;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (State == GameState.Menu)
            {
                testSprite.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public event EventHandler<StateChangedEventArgs> StateChanged;
    }
}
