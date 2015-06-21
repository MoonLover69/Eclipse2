using Eclipse2Game.GameObjects;
using Eclipse2Game.State;
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
    public class MainGame : DrawableGameComponent
    {
        private ComponentPanel _upperPanel;
        private ComponentPanel _lowerPanel;
        private TypewriterDisplay _text;

        public MainGame(Game parent)
            : base(parent)
        {
            // Game is upper part of screen
            _upperPanel = new ComponentPanel(parent, CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight * 2 / 3);

            // Text display is lower part
            _lowerPanel = new ComponentPanel(parent,
                CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight / 3,
                new Vector2(0, CoordinateHelper.WindowHeight * 2 / 3));

            _text = new TypewriterDisplay("Fonts/Typewriter", CoordinateHelper.WindowWidth - 100);
            _text.Position = new Vector2(50, 50);
            _text.TextSpeed = 15;

            _text.AddText("Please enter your name: ");
            _text.TakeInput();
            _text.Input += _text_Input;

            _lowerPanel.AddItem(_text, 0);
            
            parent.Components.Add(this);
        }

        void _text_Input(object sender, UserInputEventArgs e)
        {
            GameState.Instance.Player.Name = e.Input;
            _text.Clear();

            _text.AddText("Hello " + GameState.Instance.Player.Name);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                this.Enabled = false;
            }

            base.Update(gameTime);
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            _upperPanel.Enabled = this.Enabled;
            _lowerPanel.Enabled = this.Enabled;

            base.OnEnabledChanged(sender, args);
        }
    }
}
