using Eclipse2Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game
{
    public class MainGame : IStateManager, IInteractive
    {
        private Canvas _gameCanvas;
        private Canvas _textCanvas;

        public MainGame()
        {
            // Game is upper part of screen
            _gameCanvas = new Canvas(CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight * 2 / 3);

            // Text display is lower part
            _textCanvas = new Canvas(CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight / 3,
                new Vector2(0, CoordinateHelper.WindowHeight * 2 / 3));

            var text = new TypewriterDisplay("Fonts/Typewriter", 0);
            text.Position = new Vector2(50, 50);
            text.TextSpeed = 20;

            text.AddText("This is a test of the typewriter engine.");

            _textCanvas.AddItem(text, 0);
        }

        public void LoadContent(ContentManager cm)
        {
            _gameCanvas.LoadContent(cm);
            _textCanvas.LoadContent(cm);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch sprites)
        {
            _gameCanvas.Draw(sprites);
            _textCanvas.Draw(sprites);
        }

        public bool IsActive
        {
            get;
            set;
        }

        public void HandleKeyboardInput(KeyboardState keyboard)
        {

        }

        public void HandleMouseInput(MouseState mouse)
        {

        }
    }
}
