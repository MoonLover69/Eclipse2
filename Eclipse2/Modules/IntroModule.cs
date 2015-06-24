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
        private int _moduleIndex = 0;
        private bool _forceAdvance = false;

        public IntroModule(Game parent, ComponentPanel upper, ComponentPanel lower)
            : base(parent, upper, lower)
        {
            _text = new TypewriterDisplay("Fonts/Typewriter", CoordinateHelper.WindowWidth - 100);
            _text.Position = new Vector2(50, 50);
            _text.TextSpeed = 100;
            _text.Visible = false;

            lower.AddItem(_text, 0);
        }

        protected override void OnModuleStarted()
        {
            base.OnModuleStarted();
            _text.Visible = true;

            AdvanceText();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            LowerPanel.RemoveItem(_text);
            _text = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _text.Update(gameTime);

            var newKeys = KeyboardUtils.GetDebouncedKeys(CurrentKeyboard, LastKeyboard);

            if (((_text.Idle) && newKeys.Contains(Keys.Enter)) || _forceAdvance)
            {
                _forceAdvance = false;
                _text.Clear();
                AdvanceText();
            }
        }

        private void AdvanceText()
        {
            switch (_moduleIndex)
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
                case 5:
                {
                    _text.AddText(TutorialStrings.IntroString06);
                    _text.Choice += MakeTutorialChoice;
                    _text.AddChoices(TutorialStrings.IntroString06_1, TutorialStrings.IntroString06_2);
                    break;
                }
                default:
                {
                    CompleteModule();
                    break;
                }
            }

            _moduleIndex++;
        }

        void ShipNameInput(object sender, UserInputEventArgs e)
        {
            GameState.Instance.Player.ShipName = e.Input;
        }

        void NameInput(object sender, UserInputEventArgs e)
        {
            GameState.Instance.Player.Name = e.Input;
        }

        void MakeTutorialChoice(object sender, UserChoiceEventArgs e)
        {
            var tmp = NextModule;

            if (e.Choice == 2)
            {
                // Skip the tutorial module
                this.NextModule = tmp.NextModule;
            }

            // Force advancement to the next module
            _forceAdvance = true;
        }
    }
}
