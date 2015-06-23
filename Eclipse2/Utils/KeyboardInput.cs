using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.Utils
{
    public static class KeyboardUtils
    {
       /// <summary>
       /// Tries to convert keyboard input to characters and prevents repeatedly returning the 
       /// same character if a key was pressed last frame, but not yet unpressed this frame.
       /// </summary>
       /// <param name="keyboard">The current KeyboardState</param>
       /// <param name="oldKeyboard">The KeyboardState of the previous frame</param>
       /// <param name="key">When this method returns, contains the correct character if conversion succeeded.
       /// Else contains the null, (000), character.</param>
       /// <returns>True if conversion was successful</returns>
        public static bool TryConvertKeyboardInput(KeyboardState keyboard, KeyboardState oldKeyboard, ref string currentInput)
        {
            Keys[] keys = keyboard.GetPressedKeys();
            bool shift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);

            foreach (var k in keys)
            {
                if (oldKeyboard.IsKeyDown(k))
                {
                    continue;
                }

                char key = (char)0;

                switch (k)
                {
                    //Alphabet keys
                    case Keys.A: if (shift) { key = 'A'; } else { key = 'a'; } break;
                    case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } break;
                    case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } break;
                    case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } break;
                    case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } break;
                    case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } break;
                    case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } break;
                    case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } break;
                    case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } break;
                    case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } break;
                    case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } break;
                    case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } break;
                    case Keys.M: if (shift) { key = 'M'; } else { key = 'm'; } break;
                    case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } break;
                    case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } break;
                    case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } break;
                    case Keys.Q: if (shift) { key = 'Q'; } else { key = 'q'; } break;
                    case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } break;
                    case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } break;
                    case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } break;
                    case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } break;
                    case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } break;
                    case Keys.W: if (shift) { key = 'W'; } else { key = 'w'; } break;
                    case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } break;
                    case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } break;
                    case Keys.Z: if (shift) { key = 'Z'; } else { key = 'z'; } break;

                    //Decimal keys
                    case Keys.D0: if (shift) { key = ')'; } else { key = '0'; } break;
                    case Keys.D1: if (shift) { key = '!'; } else { key = '1'; } break;
                    case Keys.D2: if (shift) { key = '@'; } else { key = '2'; } break;
                    case Keys.D3: if (shift) { key = '#'; } else { key = '3'; } break;
                    case Keys.D4: if (shift) { key = '$'; } else { key = '4'; } break;
                    case Keys.D5: if (shift) { key = '%'; } else { key = '5'; } break;
                    case Keys.D6: if (shift) { key = '^'; } else { key = '6'; } break;
                    case Keys.D7: if (shift) { key = '&'; } else { key = '7'; } break;
                    case Keys.D8: if (shift) { key = '*'; } else { key = '8'; } break;
                    case Keys.D9: if (shift) { key = '('; } else { key = '9'; } break;

                    //Decimal numpad keys
                    case Keys.NumPad0: key = '0'; break;
                    case Keys.NumPad1: key = '1'; break;
                    case Keys.NumPad2: key = '2'; break;
                    case Keys.NumPad3: key = '3'; break;
                    case Keys.NumPad4: key = '4'; break;
                    case Keys.NumPad5: key = '5'; break;
                    case Keys.NumPad6: key = '6'; break;
                    case Keys.NumPad7: key = '7'; break;
                    case Keys.NumPad8: key = '8'; break;
                    case Keys.NumPad9: key = '9'; break;

                    //Special keys
                    case Keys.OemTilde: if (shift) { key = '~'; } else { key = '`'; } break;
                    case Keys.OemSemicolon: if (shift) { key = ':'; } else { key = ';'; } break;
                    case Keys.OemQuotes: if (shift) { key = '"'; } else { key = '\''; } break;
                    case Keys.OemQuestion: if (shift) { key = '?'; } else { key = '/'; } break;
                    case Keys.OemPlus: if (shift) { key = '+'; } else { key = '='; } break;
                    case Keys.OemPipe: if (shift) { key = '|'; } else { key = '\\'; } break;
                    case Keys.OemPeriod: if (shift) { key = '>'; } else { key = '.'; } break;
                    case Keys.OemOpenBrackets: if (shift) { key = '{'; } else { key = '['; } break;
                    case Keys.OemCloseBrackets: if (shift) { key = '}'; } else { key = ']'; } break;
                    case Keys.OemMinus: if (shift) { key = '_'; } else { key = '-'; } break;
                    case Keys.OemComma: if (shift) { key = '<'; } else { key = ','; } break;
                    case Keys.Space: key = ' '; break;
                }

                if (key != (char)0)
                {
                    currentInput += key;
                }
            }

            return false;
        }

        /// <summary>
        /// Handle keyboard input for the give string
        /// </summary>
        /// <param name="keyboard"></param>
        /// <param name="oldKeyboard"></param>
        /// <param name="currentInput">The string to modify based on keyboard input</param>
        /// <returns></returns>
        public static void HandleKeyboardInput(KeyboardState keyboard, KeyboardState oldKeyboard, ref string currentInput)
        {
            TryConvertKeyboardInput(keyboard, oldKeyboard, ref currentInput);

            var keys = keyboard.GetPressedKeys();
            var lastKeys = oldKeyboard.GetPressedKeys();

            // Debounce the button by only getting newly pressed keys
            var debouncedKeys = keys.Where(k => !lastKeys.Contains(k));

            foreach (var key in debouncedKeys)
            { 
                switch (key)
                {
                    case Keys.Back:
                    {
                        if (currentInput.Length > 0)
                        {
                            currentInput = currentInput.Substring(0, currentInput.Length - 1);
                        }
                        break;
                    }
                }
            }
        }
    }
}
