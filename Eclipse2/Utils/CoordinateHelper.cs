using Eclipse2Game.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game
{
    /// <summary>
    /// Static class to help transform coordinates between local and global
    /// </summary>
    public static class CoordinateHelper
    {
#if WINDOWS
        public const int WindowWidth = 1280;
        public const int WindowHeight = 960;
#else
        public const int WindowWidth = 640;
        public const int WindowHeight = 480;
#endif
        /// <summary>
        /// Location of the center of the window
        /// </summary>
        public static Vector2 CenterScreen = GetCenterLoc(WindowWidth, WindowHeight);

        /// <summary>
        /// Get the center location of the given area
        /// </summary>
        public static Vector2 GetCenterLoc(int width, int height)
        {
            return new Vector2(width / 2.0f, height / 2.0f);
        }

        /// <summary>
        /// Get the center location of the given canvas
        /// </summary>
        public static Vector2 GetCenterLoc(this ComponentPanel c)
        {
            return GetCenterLoc(c.Width, c.Height);
        }
    }
}
