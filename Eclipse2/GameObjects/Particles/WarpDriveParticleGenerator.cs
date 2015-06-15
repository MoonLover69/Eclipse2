using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.GameObjects.Particles
{
    public class WarpDriveParticleGenerator : ParticleGenerator
    {
        private Random _random;
        private Vector2 _origin;

        public WarpDriveParticleGenerator(Vector2 particleOrigin, params Texture2D[] textures)
            : base(textures)
        {
            _origin = particleOrigin;
            _random = new Random();
        }

        public override Particle GenerateNewParticle(Vector2 mouseLocation)
        {
            Texture2D texture = Textures[_random.Next(Textures.Count)];

            var rads = _random.NextDouble() * 2 * Math.PI;

            Vector2 velocity = new Vector2((float)Math.Cos(rads), (float)Math.Sin(rads)) * 10;
            float angularVelocity = 0;
            Color color = Color.White;

            float size = (float)_random.NextDouble() / 2;

            return new Particle(texture, color, _origin, velocity, velocity * 100, 30, size, angularVelocity);
        }
    }
}
