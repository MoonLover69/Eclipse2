using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eclipse2Game.GameObjects
{
    /// <summary>
    /// Class used to display a sprite on the screen
    /// </summary>
    class Sprite
    {
        private Texture2D _texture;
        private string _assetName;

        /// <summary>
        /// Create a new sprite with the given asset name at (0,0)
        /// </summary>
        public Sprite(string assetName)
            : this(assetName, new Vector2(0,0))
        {
        }

        /// <summary>
        /// Create a new sprite with the given asset name at the specified position
        /// </summary>
        public Sprite(string assetName, Vector2 initialPos)
        {
            _assetName = assetName;
            Position = initialPos;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Load the required content for this sprite
        /// </summary>
        public void LoadContent(ContentManager cm)
        {
            _texture = cm.Load<Texture2D>(_assetName);
        }

        /// <summary>
        /// Draw the sprite with the specified sprite batch
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            // White means no color tinting.
            sb.Draw(_texture, Position, Color.White);
        }

        public override string ToString()
        {
            return _assetName;
        }
    }
}
