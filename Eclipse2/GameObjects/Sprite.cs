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
    public class Sprite : IDrawableObject
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
            ColorTint = Color.White;
        }

        /// <summary>
        /// Create a new sprite with the given texture.
        /// </summary>
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        /// <summary>
        /// Rotation, in radians
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        public float Scale
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Color ColorTint
        {
            get;
            set;
        }

        /// <summary>
        /// Load the required content for this sprite
        /// </summary>
        public void LoadContent(ContentManager cm)
        {
            if (!String.IsNullOrWhiteSpace(_assetName))
            {
                _texture = cm.Load<Texture2D>(_assetName);
            }
        }

        /// <summary>
        /// Draw the sprite with the specified sprite batch
        /// </summary>
        public void Draw(SpriteBatch sb, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            // White means no color tinting.
            sb.Draw(_texture, location, sourceRectangle, ColorTint,
                Rotation, origin, Scale, SpriteEffects.None, 0f);
        }
        
        public Point GetSize()
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            return new Point(_texture.Width, _texture.Height);
        }

        public override string ToString()
        {
            return _assetName;
        }
    }
}
