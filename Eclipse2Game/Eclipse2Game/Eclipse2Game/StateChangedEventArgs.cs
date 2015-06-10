using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse2Game
{
    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(GameState oldState, GameState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public GameState OldState
        {
            get;
            private set;
        }

        public GameState NewState
        {
            get;
            private set;
        }
    }
}
