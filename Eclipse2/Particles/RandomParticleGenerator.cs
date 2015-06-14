using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.Particles
{
    public class RandomParticleGenerator : ParticleGenerator
    {
        private Random _random;

        public RandomParticleGenerator(params Texture2D[] textures)
            : base(textures)
        {
            _random = new Random();
        }

        public override Particle GenerateNewParticle(Vector2 mouseLocation)
        {
            Texture2D texture = Textures[_random.Next(Textures.Count)];
            Vector2 velocity = new Vector2((float)_random.NextDouble() * 2 - 1, (float)_random.NextDouble() * 2 - 1) * 50;

            float angularVelocity = 0.1f * (float)(_random.NextDouble() * 20 - 1);
            Color color = new Color(
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble(),
                        (float)_random.NextDouble());
            float size = (float)_random.NextDouble();
            int ttl = _random.Next(3); // in seconds

            return new Particle(texture, color, mouseLocation, velocity, new Vector2(0, 0), ttl, size, angularVelocity);
        }
    }
}
