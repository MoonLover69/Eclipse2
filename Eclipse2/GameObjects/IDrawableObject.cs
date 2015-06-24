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
    /// Represents an item that can be drawn in a Component Panel
    /// </summary>
    public interface IDrawableComponent
    {
        /// <summary>
        /// Load the required content
        /// </summary>
        void LoadContent(ContentManager cm);

        /// <summary>
        /// Draw the object with the specified sprite batch at the specified location.
        /// </summary>
        void Draw(GameTime gameTime, SpriteBatch sb);

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

        /// <summary>
        /// Whether the object should be drawn in the component panel
        /// </summary>
        bool Visible
        {
            get;
            set;
        }
    }
}
