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
    public class TypewriterDisplay : IDrawableObject
    {
        private const int BlinkCount = 20;
        private const char Caret = '█';

        // Setup parameters
        private SpriteFont _font;
        private string _fontName;
        private int _maxWidth;

        // Text we have already displayed
        private string _displayedText;
        // The text we are about to display
        private Queue<char> _newText;
        // Number of draws since last new character
        private int _drawCounter;

        private int _caretCounter;

        /// <summary>
        /// Create a new display with the given font and the max width in pixels
        /// </summary>
        public TypewriterDisplay(string fontName, int maxWidth)
        {
            _fontName = fontName;
            _maxWidth = maxWidth;
            TextSpeed = 1;
            _displayedText = "";
            _newText = new Queue<char>();
            _drawCounter = 0;
            _caretCounter = 0;
        }

        /// <summary>
        /// 1 to draw one letter per draw request.
        /// 2 to draw one letter per 2 requests... etc
        /// </summary>
        public int TextSpeed
        {
            get;
            set;
        }

        public void Clear()
        {
            _displayedText = "";
            _newText.Clear();
        }

        public void AddText(string text)
        {
            text.ToList().ForEach(c => _newText.Enqueue(c));
        }

        public void Draw(SpriteBatch sb, Vector2 location)
        {
            if (_drawCounter < TextSpeed)
            {
                _drawCounter++;
            }
            else
            {
                // time for a new char
                if (_newText.Count > 0)
                {
                    _displayedText += _newText.Dequeue();
                }
                _drawCounter = 0;
            }


            string text = _displayedText;


            // Blink the caret
            if (_caretCounter >= BlinkCount)
            {
                text += Caret;

                if (_caretCounter >= BlinkCount * 2)
                {
                    _caretCounter = 0;
                }
            }

            _caretCounter++;

            sb.DrawString(_font, text, location, Color.Green);
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Point GetSize()
        {
            var size = _font.MeasureString(_displayedText + Caret);
            return new Point((int)size.X, (int)size.Y);
        }

        /// <summary>
        /// Load the required font for this text
        /// </summary>
        public void LoadContent(ContentManager cm)
        {
            _font = cm.Load<SpriteFont>(_fontName);
        }
    }
}
