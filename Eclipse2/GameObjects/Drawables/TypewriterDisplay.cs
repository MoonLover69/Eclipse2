using Eclipse2Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects
{
    public class TypewriterDisplay : IDrawableObject, IUpdateableObject
    {
        #region static parameters

        public enum DisplayState
        {
            /// <summary>
            /// Nothing happening but blinking cursor
            /// </summary>
            Idle,

            /// <summary>
            /// Typing new text
            /// </summary>
            Typing,

            /// <summary>
            /// Typing, and then take user input afterwards
            /// </summary>
            UserInputPending,

            /// <summary>
            /// Awaiting user input
            /// </summary>
            UserInput,
        }

        private const double BlinkInterval = 0.5;
        private const char Caret = '█';

        #endregion

        // Setup parameters
        private Sound[] _clicks;

        private DisplayState _state;

        private SpriteFont _font;
        private string _fontName;
        private int _maxWidth;

        // Text we have already displayed
        private string _displayedText;
        // The text we are about to display
        private Queue<char> _newText;
        // Latest user input
        private string _input;

        // Keep track of time since last new character and last caret blink
        private double _newCharCounter;
        private double _caretCounter;

        /// <summary>
        /// Create a new display with the given font and the max width in pixels
        /// </summary>
        public TypewriterDisplay(string fontName, int maxWidth)
        {
            _fontName = fontName;
            _maxWidth = maxWidth;

            TextSpeed = 10;
            _newText = new Queue<char>();
            _newCharCounter = 0;
            _caretCounter = 0;
            Visible = true;

            Clear();

            _state = DisplayState.Idle;

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
            _input = String.Empty;
            _displayedText = String.Empty;
            _newText.Clear();

            _state = DisplayState.Idle;
        }

        public void AddText(string text)
        {
            if (text.Length > 0)
            {
                _state = DisplayState.Typing;
                text.ToList().ForEach(c => _newText.Enqueue(c));
            }
        }

        public void TakeInput()
        {
            _state = DisplayState.UserInputPending;
        }

        KeyboardState _lastKeys;

        public void Update(GameTime gameTime)
        {
            if (_state != DisplayState.UserInput)
            {
                return;
            }

            var ks = Keyboard.GetState();

            if (_lastKeys != null)
            {
                KeyboardUtils.HandleKeyboardInput(ks, _lastKeys, ref _input);

                if (ks.IsKeyDown(Keys.Enter) && !_lastKeys.IsKeyDown(Keys.Enter))
                {
                    // User is done entering
                    _state = DisplayState.Idle;

                    OnUserInput(_input);
                }
            }

            _lastKeys = ks;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (_state == DisplayState.Typing || _state == DisplayState.UserInputPending)
            {
                Random r = new Random();

                _newCharCounter += gameTime.ElapsedGameTime.TotalSeconds;

                // Randomize the new char counter a bit so typing looks a bit more natural
                var diff = r.NextDouble() * 2 - 1;
                _newCharCounter += diff * gameTime.ElapsedGameTime.TotalSeconds;


                if (_newCharCounter >= (1.0 / TextSpeed))
                {
                    // time for a new char
                    if (_newText.Count > 0)
                    {
                        _displayedText += _newText.Dequeue();
                        _clicks[r.Next(_clicks.Length - 1)].Play();
                    }
                    else
                    {
                        // No more characters
                        if (_state == DisplayState.UserInputPending)
                        {
                            _state = DisplayState.UserInput;
                        }
                        else
                        {
                            _state = DisplayState.Idle;
                        }
                    }

                    _newCharCounter = 0;
                }
            }

            CheckWordWrap();

            string text = _displayedText;

            if ((_state == DisplayState.UserInput) && !String.IsNullOrEmpty(_input))
            {
                text += _input;
            }

            // Blink the caret
            if (_caretCounter >= BlinkInterval)
            {
                text += Caret;

                if (_caretCounter >= BlinkInterval * 2)
                {
                    _caretCounter = 0;
                }
            }

            _caretCounter += gameTime.ElapsedGameTime.TotalSeconds;

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

        protected void OnUserInput(string input)
        {
            var handler = Input;
            if (handler != null)
            {
                handler(this, new UserInputEventArgs(input));
            }
        }

        public event EventHandler<UserInputEventArgs> Input;
    }

    public class UserInputEventArgs : EventArgs
    {
        public UserInputEventArgs(string input)
        {
            Input = input;
        }

        public string Input 
        {
            get;
            private set; 
        }
    }
}
