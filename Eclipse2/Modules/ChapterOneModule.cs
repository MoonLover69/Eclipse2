using Eclipse2Game.GameObjects;
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
    class ChapterOneModule : GameModule
    {
        private TypewriterDisplay _text;
        private int _moduleIndex = 0;

        public ChapterOneModule(Game parent, ComponentPanel upper, ComponentPanel lower)
            : base(parent, upper, lower)
        {
            _text = new TypewriterDisplay("Fonts/Typewriter", CoordinateHelper.WindowWidth - 100);
            _text.Position = new Vector2(50, 50);
            _text.TextSpeed = 15;
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

            if ((_text.Idle) && newKeys.Contains(Keys.Enter))
            {
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
                        _text.AddText(ChapterOneStrings.String001);
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
    }
}
