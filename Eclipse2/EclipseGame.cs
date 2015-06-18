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

            _mainMenu = new MainMenu(this);

            _game = new MainGame(this);

            _mainMenu.EnabledChanged += _mainMenu_EnabledChanged;
            _game.EnabledChanged += _game_EnabledChanged;
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

            _game.Enabled = false;
            _mainMenu.Enabled = true;
        }

        void _game_EnabledChanged(object sender, EventArgs e)
        {
            if (!_game.Enabled)
            {
                _mainMenu.Enabled = true;
            }
        }

        void _mainMenu_EnabledChanged(object sender, EventArgs e)
        {
            if (!_mainMenu.Enabled)
            {
                _game.Enabled = true;
            }
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.Clear(Color.Black);
            return base.BeginDraw();
        }
    }
}
