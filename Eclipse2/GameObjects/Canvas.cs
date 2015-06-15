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
    /// This can be used with other canvases to break the window into sub-areas.
    /// </summary>
    public class Canvas : IDrawableObject
    {
        private Dictionary<int, List<IDrawableObject>> _elements;

        public Canvas(int width, int height)
            : this (width, height, new Vector2(0,0))
        { }

        public Canvas(int width, int height, Vector2 position)
        {
            Width = width;
            Height = height;
            _elements = new Dictionary<int, List<IDrawableObject>>();
            Position = position;
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

        public void LoadContent(ContentManager cm)
        {
            foreach (var layer in _elements.Values)
            {
                foreach (var item in layer)
                {
                    item.LoadContent(cm);
                }
            }
        }

        /// <summary>
        /// Draw the canvas without a position transform
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            Draw(sb, Position);
        }

        public void Draw(SpriteBatch sb, Vector2 globalLocation)
        {
#if DEBUG
            sb.DrawRectangle(new Rectangle(new Point((int)globalLocation.X, (int)globalLocation.Y), this.GetSize()), Color.Gray);
#endif
            foreach (var layer in _elements.Values)
            {
                foreach (var item in layer)
                {
                    if (ItemVisible(item))
                    {
                        item.Draw(sb, globalLocation + item.Position);
                    }
                }
            }
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

            return true;
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
