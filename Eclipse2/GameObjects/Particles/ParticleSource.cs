using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Eclipse2Game.GameObjects.Particles
{
    public class ParticleSource : IDrawableComponent, IUpdateableObject
    {
        private List<ParticleGenerator> _particleGenerators;
        private List<Particle> _currentParticles;

        private double _newParticleCounter = 0;

        public ParticleSource()
        {
            _particleGenerators = new List<ParticleGenerator>();
            _currentParticles = new List<Particle>();

            Position = new Vector2(0, 0);
            Visible = true;

            ParticlesPerSecond = 20;
        }

        public int ParticlesPerSecond
        {
            get;
            set;
        }

        private double ParticleInterval
        {
            get
            {
                return 1.0 / ParticlesPerSecond;
            }
        }

        public void AddParticleGenerator(ParticleGenerator pg)
        {
            _particleGenerators.Add(pg);
        }

        public void Update(GameTime gameTime)
        {
            _newParticleCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (_newParticleCounter >= ParticleInterval)
            {
                _newParticleCounter = 0;

                var mouse = Mouse.GetState();

                // Get some new particles
                foreach (var pg in _particleGenerators)
                {
                    _currentParticles.Add(
                        pg.GenerateNewParticle(new Vector2(mouse.X, mouse.Y)));
                }
            }

            // Then update them
            foreach (var p in _currentParticles)
            {
                p.Update(gameTime);
            }

            // Then remove expiring particles
            _currentParticles.RemoveAll(p => !p.Active);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var p in _currentParticles)
            {
                p.Draw(gameTime, spriteBatch);
            }
        }

        public void LoadContent(ContentManager cm)
        {
            // Do nothing
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Point GetSize()
        {
            return new Point(0, 0);
        }

        public bool Visible
        {
            get;
            set;
        }
    }
}
