using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Eclipse2Game.GameObjects;
using Microsoft.Xna.Framework.Content;

namespace Eclipse2Game.GameObjects
{
    public class Button : Sprite
    {
        public Button(String assetName, Vector2 position, Color color, float rotation, float scale) : base(assetName)
        {
            this.Position = position;
            this.Scale = scale;
            this.Rotation = rotation;
            this.ColorTint=color;
        }
        
    }
}
