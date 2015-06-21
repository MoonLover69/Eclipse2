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
    /// Class that contains a drawable area.
    /// 
    /// This can be used to break the window into sub-areas.
    /// </summary>
    public class ComponentPanel : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Dictionary<int, List<IDrawableObject>> _elements;

        public ComponentPanel(Game parent, int width, int height)
            : this(parent, width, height, new Vector2(0, 0))
        { }

        public ComponentPanel(Game parent, int width, int height, Vector2 position)
            : base(parent)
        {
            Width = width;
            Height = height;
            _elements = new Dictionary<int, List<IDrawableObject>>();

            Position = position;

            parent.Components.Add(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            foreach (var layer in _elements.Values)
            {
                foreach (var item in layer)
                {
                    item.LoadContent(Game.Content);
                }
            }
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Add a drawable item to the specified layer.
        /// The higher the layer, the later it gets drawn.
        /// </summary>
        public void AddItem(IDrawableObject item, int layer)
        {
            if (!_elements.ContainsKey(layer))
            {
                _elements[layer] = new List<IDrawableObject>();
            }

            if (!_elements[layer].Contains(item))
            {
                _elements[layer].Add(item);
            }
        }

        public void RemoveItem(IDrawableObject item)
        {
            foreach (var layer in _elements.Values)
            {
                if (layer.Contains(item))
                {
                    layer.Remove(item);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var layer in _elements.Values)
            {
                foreach (IDrawableObject obj in layer)
                {
                    if (obj is IUpdateableObject)
                    {
                        ((IUpdateableObject)obj).Update(gameTime);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!this.Enabled)
            {
                return;
            }

            var matrix = Matrix.CreateTranslation(Position.X, Position.Y, 0);

            _spriteBatch.Begin(transformMatrix: matrix);

#if DEBUG
            _spriteBatch.DrawRectangle(new Rectangle(Position.ToPoint(), this.GetSize()), Color.Gray);
#endif
            foreach (var layer in _elements.Values)
            {
                foreach (var item in layer)
                {
                    if (ItemVisible(item))
                    {
                        item.Draw(gameTime, _spriteBatch);
                    }
                }
            }

            _spriteBatch.End();
        }

        private bool ItemVisible(IDrawableObject item)
        {
            var size = item.GetSize();
            var pos = item.Position;

            if (pos.X < 0 || pos.X > this.Width)
            {
                return false;
            }

            if (pos.Y < 0 || pos.X > this.Width)
            {
                return false;
            }

            return item.Visible;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Point GetSize()
        {
            return new Point(Width, Height);
        }
    }
}
