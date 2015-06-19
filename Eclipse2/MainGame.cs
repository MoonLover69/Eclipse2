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
    public class MainGame : DrawableGameComponent
    {
        private ComponentPanel _upperPanel;
        private ComponentPanel _lowerPanel;

        public MainGame(Game parent)
            : base(parent)
        {
            // Game is upper part of screen
            _upperPanel = new ComponentPanel(parent, CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight * 2 / 3);

            // Text display is lower part
            _lowerPanel = new ComponentPanel(parent,
                CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight / 3,
                new Vector2(0, CoordinateHelper.WindowHeight * 2 / 3));

            var text = new TypewriterDisplay("Fonts/Typewriter", CoordinateHelper.WindowWidth - 100);
            text.Position = new Vector2(50, 50);
            text.TextSpeed = 10;

            text.AddText("This is a test of the typewriter engine. This is a test of the typewriter engine. This is a test of the typewriter engine. This is a test of the typewriter engine.");

            _lowerPanel.AddItem(text, 0);
            
            parent.Components.Add(this);
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
