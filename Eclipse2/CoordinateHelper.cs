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
        /// Convert to relative coordinates from global coordinates
        /// </summary>
        public static Vector2 ToRelativeCoordinates(int width, int height, Vector2 coord)
        {
            float wScale = (float)width / WindowWidth;
            float hScale = (float)height / WindowHeight;

            return coord * new Vector2(wScale, hScale);
        }

        /// <summary>
        /// Convert from relative coordinates to global coordinates
        /// </summary>
        public static Vector2 ToGlobalCoordinates(int width, int height, Vector2 coord)
        {
            float wScale = (float)WindowWidth / width;
            float hScale = (float)WindowHeight / height;

            return coord * new Vector2(wScale, hScale);
        }

        /// <summary>
        /// Get the center location of the given area
        /// </summary>
        public static Vector2 GetCenterLoc(int width, int height)
        {
            return new Vector2(width / 2.0f, height / 2.0f);
        }

    }
}
