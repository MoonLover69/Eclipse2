using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Eclipse2Game.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Eclipse2Game.GameObjects
{
    public class Button : Sprite
    {
        public Button(String assetName, Vector2 position) : base(assetName)
        {
            this.Position = position;
            HighlightTint = Color.DarkGray;
        }

        /// <summary>
        /// determine if the location is contained within the button
        /// </summary>
        /// <param name="mouseLoc"></param>
        /// <returns></returns>
        public void HandleMouse(MouseState mouse)
        {
            if (this.Contains(mouse.Position))
            {
                base.ColorTint = HighlightTint;

                // Mouse is on button... check input

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    // TODO
                }

            }
            else
            {
                base.ColorTint = Color.White;
            }
        }

        private bool Contains(Point loc)
        {
            var center = this.Position.ToPoint();
            var size = this.GetSize();

            var upperLeft = new Point(center.X - size.X / 2, center.Y - size.Y / 2);

            var rect = new Rectangle(upperLeft, size);
            return rect.Contains(loc);
        }


        public Color HighlightTint
        {
            get;
            set;
        }
    }
}
