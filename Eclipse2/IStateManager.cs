using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game
{
    /// <summary>
    /// Interface for all state managers which live on the top-level game loop
    /// </summary>
    public interface IStateManager
    {
        /// <summary>
        /// Performed once on initialization
        /// </summary>
        void LoadContent(ContentManager cm);

        /// <summary>
        /// Update states
        /// </summary>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw items
        /// </summary>
        void Draw(GameTime gameTime, SpriteBatch sprites);

        /// <summary>
        /// Whether the current state manager is active
        /// </summary>
        bool IsActive
        {
            get;
            set;
        }
    }
}
