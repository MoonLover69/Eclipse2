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
        private Sound[] _clicks;

        private SpriteFont _font;
        private string _fontName;
        private int _maxWidth;

        // Text we have already displayed
        private string _displayedText;
        // The text we are about to display
        private Queue<char> _newText;

        // Number of draws since last new character
        private double _drawCounter;

        private int _caretCounter;

        /// <summary>
        /// Create a new display with the given font and the max width in pixels
        /// </summary>
        public TypewriterDisplay(string fontName, int maxWidth)
        {
            _fontName = fontName;
            _maxWidth = maxWidth;
            TextSpeed = 10;
            _displayedText = "";
            _newText = new Queue<char>();
            _drawCounter = 0;
            _caretCounter = 0;
            Visible = true;

            // Create a lot of instances so we can play more than one at a time
            _clicks = new Sound[] {
                new Sound("Sounds/SFX/click1"), 
                new Sound("Sounds/SFX/click2"),
                new Sound("Sounds/SFX/click3"),
                new Sound("Sounds/SFX/click4"),
                new Sound("Sounds/SFX/click1"), 
                new Sound("Sounds/SFX/click2"),
                new Sound("Sounds/SFX/click3"),
                new Sound("Sounds/SFX/click4")
            };
        }

        /// <summary>
        /// In letter/sec
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

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            Random r = new Random();

            _drawCounter += gameTime.ElapsedGameTime.TotalSeconds;

            var diff = r.NextDouble() - 1;

            _drawCounter += diff * gameTime.ElapsedGameTime.TotalSeconds;

            if (_drawCounter >= (1.0 / TextSpeed))
            {
                // time for a new char
                if (_newText.Count > 0)
                {
                    _displayedText += _newText.Dequeue();
                    _clicks[r.Next(_clicks.Length - 1)].Play();
                    CheckWordWrap();
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

            sb.DrawString(_font, text, Position, Color.Green);
        }

        private void CheckWordWrap()
        {
            var size = GetSize();
            if (size.X > _maxWidth)
            {
                // need to wrap the text

                var words = _displayedText.Split();

                string lastWord = words.Last();
                _displayedText = _displayedText.Substring(0, _displayedText.Length - lastWord.Length);
                _displayedText += Environment.NewLine + lastWord;
            }
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
            foreach (var c in _clicks)
            {
                c.LoadContent(cm);
            }
        }


        public bool Visible
        {
            get;
            set;
        }
    }
}
