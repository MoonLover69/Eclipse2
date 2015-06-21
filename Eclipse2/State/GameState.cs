using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.State
{
    /// <summary>
    /// Used to store all game state information
    /// </summary>
    public class GameState
    {
        private static GameState _state;
        public static GameState Instance
        {
            get
            {
                if (_state == null)
                {
                    _state = new GameState();
                }
                return _state;
            }
        }

        private GameState()
        {
            Player = new PlayerInfo();
        }

        public PlayerInfo Player
        {
            get;
            private set;
        }
    }
}
