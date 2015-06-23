using Eclipse2Game.GameObjects;
using Eclipse2Game.State;
using Eclipse2Game.Strings;
using Eclipse2Game.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.Modules
{
    class IntroModule : GameModule
    {
        private TypewriterDisplay _text;
        
        private int _stringIndex = 0;

        public IntroModule(Game parent, ComponentPanel upper, ComponentPanel lower)
            : base(parent, upper, lower)
        {
            _text = new TypewriterDisplay("Fonts/Typewriter", CoordinateHelper.WindowWidth - 100);
            _text.Position = new Vector2(50, 50);
            _text.TextSpeed = 15;

            lower.AddItem(_text, 0);
        }

        protected override void OnModuleStarted()
        {
            base.OnModuleStarted();

            AdvanceText();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _text.Update(gameTime);

            var newKeys = KeyboardUtils.GetDebouncedKeys(CurrentKeyboard, LastKeyboard);

            if ((_text.Idle) && newKeys.Contains(Keys.Enter))
            {
                _text.Clear();
                AdvanceText();
            }
        }

        private void AdvanceText()
        {
            switch (_stringIndex)
            {
                case 0:
                {
                    _text.AddText(TutorialStrings.IntroString01);
                    break;
                }
                case 1:
                {
                    _text.AddText(TutorialStrings.IntroString02);
                    _text.Input += NameInput;
                    _text.TakeInput();
                    break;
                }
                case 2:
                {
                    _text.Input -= NameInput;
                    _text.AddText(String.Format(TutorialStrings.IntroString03, GameState.Instance.Player.Name));
                    break;
                }
                case 3:
                {
                    _text.AddText(TutorialStrings.IntroString04);
                    _text.Input += ShipNameInput;
                    _text.TakeInput();
                    break;
                }
                case 4:
                {
                    _text.AddText(String.Format(TutorialStrings.IntroString05, GameState.Instance.Player.ShipName));
                    break;
                }
                default:
                {
                    OnModuleComplete();
                    break;
                }
            }

            _stringIndex++;
        }

        private void ShipNameInput(object sender, UserInputEventArgs e)
        {
            GameState.Instance.Player.ShipName = e.Input;
            ((TypewriterDisplay)sender).Clear();
        }

        void NameInput(object sender, UserInputEventArgs e)
        {
            GameState.Instance.Player.Name = e.Input;
            ((TypewriterDisplay)sender).Clear();
        }
    }
}
