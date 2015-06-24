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
    public class TypewriterDisplay : IDrawableComponent, IUpdateableObject
    {
        #region static parameters

        enum DisplayState
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
            /// Awaiting user input
            /// </summary>
            UserInput,

            /// <summary>
            /// Awaiting a user choice
            /// </summary>
            UserChoice,
        }

        private const double BlinkInterval = 0.5;
        private const char Caret = '█';

        #endregion

        // Setup parameters
        private Sound[] _clicks;

        private DisplayState _state;
        private DisplayState _nextState;

        private SpriteFont _font;
        private string _fontName;
        private int _maxWidth;

        // Text we have already displayed
        private string _displayedText;
        // The text we are about to display
        private Queue<char> _newText;
        // Latest user input
        private string _input;
        // User choices to display
        private string[] _choices;

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

        /// <summary>
        /// Whether the textbox is idle (done typing)
        /// </summary>
        public bool Idle
        {
            get
            {
                return _state == DisplayState.Idle;
            }
        }

        /// <summary>
        /// Clear all text and input from this display
        /// </summary>
        public void Clear()
        {
            _input = String.Empty;
            _displayedText = String.Empty;
            _newText.Clear();
            _choices = new string[0];

            // Clear event handlers
            Input = null;
            Choice = null;

            _state = DisplayState.Idle;
            _nextState = DisplayState.Idle;
        }

        /// <summary>
        /// Add text to this display
        /// </summary>
        /// <param name="text"></param>
        public void AddText(string text)
        {
            if (text.Length > 0)
            {
                _state = DisplayState.Typing;
                text.ToList().ForEach(c => _newText.Enqueue(c));
            }
        }

        /// <summary>
        /// Signals that the display should take user input after displaying its current string
        /// </summary>
        public void TakeInput()
        {
            if (_nextState != DisplayState.Idle)
            {
                throw new InvalidOperationException("Cannot take user input without clearing first!");
            }

            _nextState = DisplayState.UserInput;
        }

        /// <summary>
        /// Add the given choices to the display.
        /// </summary>
        /// <param name="choices"></param>
        public void AddChoices(params string[] choices)
        { 
            if (choices.Length >= 10 || choices.Length < 1)
            {
                throw new ArgumentException("Must have between 1 and 9 choices");
            }

            if (_nextState != DisplayState.Idle)
            {
                throw new InvalidOperationException("Cannot add user choices without clearing first!");
            }

            _nextState = DisplayState.UserChoice;
            _choices = choices;
        }

        KeyboardState _lastKeys;

        public void Update(GameTime gameTime)
        {
            var ks = Keyboard.GetState();

            if (_state == DisplayState.UserInput)
            {
                KeyboardUtils.ConvertKeyboardInput(ks, _lastKeys, ref _input);
                var newKeys = KeyboardUtils.GetDebouncedKeys(ks, _lastKeys);

                if (newKeys.Contains(Keys.Enter))
                {
                    // User is done entering
                    _state = DisplayState.Idle;

                    OnUserInput(_input);
                }
            }
            else if (_state == DisplayState.UserChoice)
            {
                string nums = String.Empty;
                KeyboardUtils.ConvertKeyboardInput(ks, _lastKeys, ref nums);
                
                // Check the user input string to see if it matches a choice
                foreach (char c in nums)
                {
                    int num;
                    bool success = int.TryParse(c.ToString(), out num);

                    if (success)
                    {
                        // This is a number
                        if ((num <= _choices.Length) && (num > 0))
                        {
                            // It is a valid choice!
                            OnUserChoice(num);
                        }
                    }
                }

            }

            _lastKeys = ks;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (_state == DisplayState.Typing)
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
                        _state = _nextState;
                    }

                    _newCharCounter = 0;
                }
            }

            CheckWordWrap();

            string text = _displayedText;

            // Add typed input to the display
            if ((_state == DisplayState.UserInput) && !String.IsNullOrEmpty(_input))
            {
                text += _input;
            }

            // Display the user choices
            if (_state == DisplayState.UserChoice)
            {
                text += Environment.NewLine;
                int num = 0;
                foreach (string choice in _choices)
                {
                    text += Environment.NewLine;
                    text += String.Format("{0}.  {1}", ++num, choice);
                }
                text += Environment.NewLine;
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

        protected void OnUserChoice(int choice)
        {
            var handler = Choice;
            if (handler != null)
            {
                handler(this, new UserChoiceEventArgs(choice));
            }
        }

        public event EventHandler<UserInputEventArgs> Input;
        public event EventHandler<UserChoiceEventArgs> Choice;

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

    public class UserChoiceEventArgs : EventArgs
    {
        public UserChoiceEventArgs(int choice)
        {
            Choice = choice;
        }

        public int Choice
        {
            get;
            private set;
        }
    }
}
