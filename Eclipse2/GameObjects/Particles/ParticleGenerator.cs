using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects.Particles
{
    public abstract class ParticleGenerator
    {
        protected List<Texture2D> Textures
        {
            get;
            private set;
        }

        public ParticleGenerator(params Texture2D[] textures)
        {
            Textures = new List<Texture2D>(textures);
        }

        public abstract Particle GenerateNewParticle(Vector2 mouseLocation);
    }
}
