using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects
{
    /// <summary>
    /// Class used to display text on the screen
    /// </summary>
    public class TextDisplay
    {
        private SpriteFont _font;
        private string _fontName;

        public string Text
        {
            get;
            set;
        }

        public Vector2 Location
        {
            get;
            set;
        }

        public Color TextColor
        {
            get;
            set;
        }

        /// <summary>
        /// Create a new text object with the given font
        /// </summary>
        public TextDisplay(string text, string fontName, Color color, Vector2 loc)
        {
            Text = text;
            _fontName = fontName;
            TextColor = color;
            Location = loc;
        }

        /// <summary>
        /// Load the required font for this text
        /// </summary>
        public void LoadContent(ContentManager cm)
        {
            _font = cm.Load<SpriteFont>(_fontName);
        }

        /// <summary>
        /// Draw the sprite with the specified sprite batch
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            var size = _font.MeasureString(Text);
            sb.DrawString(_font, Text, Location - (size / 2), TextColor);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
