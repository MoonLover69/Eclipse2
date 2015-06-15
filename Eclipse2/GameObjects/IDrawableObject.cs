using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects
{
    /// <summary>
    /// Represents an item that can be drawn using a sprite batch
    /// </summary>
    public interface IDrawableObject
    {
        /// <summary>
        /// Load the required content
        /// </summary>
        void LoadContent(ContentManager cm);

        /// <summary>
        /// Draw the object with the specified sprite batch at the specified location.
        /// 
        /// The location in the draw event will be a global location.
        /// </summary>
        void Draw(SpriteBatch sb, Vector2 location);

        /// <summary>
        /// Local position of the drawable object.
        /// 
        /// A local position differs from a drawable location because a local position
        /// is relative to the current object's canvas frame.
        /// </summary>
        Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Get the total drawable size of the item 
        /// </summary>
        Point GetSize();
    }
}
