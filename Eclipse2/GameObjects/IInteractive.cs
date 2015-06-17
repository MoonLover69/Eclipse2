using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects
{
    /// <summary>
    /// Interface for objects that handle user input
    /// </summary>
    public interface IInteractive
    {
        void HandleKeyboardInput(KeyboardState keyboard);
        void HandleMouseInput(MouseState mouse);
    }
}
