using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eclipse2Game.GameObjects.Particles
{
    public class ParticleSource : IInteractive
    {
        private List<ParticleGenerator> _particleGenerators;
        private List<Particle> _currentParticles;

        public ParticleSource()
        {
            _particleGenerators = new List<ParticleGenerator>();
            _currentParticles = new List<Particle>();
        }

        public void AddParticleGenerator(ParticleGenerator pg)
        {
            _particleGenerators.Add(pg);
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)(gameTime.ElapsedGameTime).TotalSeconds;

            foreach (var p in _currentParticles)
            {
                p.Update(dt);
            }

            // Then remove expiring particles
            _currentParticles.RemoveAll(p => !p.Active);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var p in _currentParticles)
            {
                p.Draw(spriteBatch, p.Position);
            }
        }

        public void HandleKeyboardInput(KeyboardState keyboard)
        {
            // Do nothing
        }

        public void HandleMouseInput(MouseState mouse)
        {
            // Get some new particles
            foreach (var pg in _particleGenerators)
            {
                _currentParticles.Add(
                    pg.GenerateNewParticle(new Vector2(mouse.X, mouse.Y)));
            }
        }
    }
}
