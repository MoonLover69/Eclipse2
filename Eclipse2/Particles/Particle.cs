using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eclipse2Game.Particles
{
    /// <summary>
    /// particles are the little bits that will make up an effect. each effect will
    /// be comprised of many of these particles. They have basic physical properties,
    /// such as position, velocity, acceleration, and rotation. They'll be drawn as
    /// sprites, all layered on top of one another, and will be very pretty.
    /// </summary>
    public class Particle
    {
        private Texture2D _texture;
        private Color _color;

        // Position, Velocity, and Acceleration represent exactly what their names
        // indicate. They are public fields rather than properties so that users
        // can directly access their .X and .Y properties.
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        /// <summary>
        /// how long this particle will "live", in seconds
        /// </summary>
        public float Lifetime
        {
            get;
            set;
        }

        /// <summary>
        /// how long it has been since initialize was called
        /// </summary>
        public float TimeSinceStart
        {
            get;
            set;
        }

        /// <summary>
        /// the scale of this particle
        /// </summary>
        public float Scale
        {
            get;
            set;
        }

        /// <summary>
        /// its rotation, in radians
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// how fast does it rotate?
        /// </summary>
        public float RotationSpeed
        {
            get;
            set;
        }

        // is this particle still alive? once TimeSinceStart becomes greater than
        // Lifetime, the particle should no longer be drawn or updated.
        public bool Active
        {
            get { return TimeSinceStart < Lifetime; }
        }


        /// <summary>
        /// Create a new particle with the given characterisitics
        /// </summary>
        public Particle(Texture2D texture, Color color, Vector2 position, Vector2 velocity, Vector2 acceleration,
            float lifetime, float scale, float rotationSpeed)
        {
            _texture = texture;
            _color = color;

            // set the values to the requested values
            this.Position = position;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
            this.Lifetime = lifetime;
            this.Scale = scale;
            this.RotationSpeed = rotationSpeed;

            // reset TimeSinceStart - we have to do this because particles will be
            // reused.
            this.TimeSinceStart = 0.0f;

            // set rotation to some random value between 0 and 360 degrees.
            this.Rotation = (float)((new Random()).NextDouble() * 2 * Math.PI);
        }

        // update is called by the ParticleSystem on every frame. This is where the
        // particle's position and that kind of thing get updated.
        public void Update(float dt)
        {
            Velocity += Acceleration * dt;
            Position += Velocity * dt;

            Rotation += RotationSpeed * dt;

            TimeSinceStart += dt;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            spriteBatch.Draw(_texture, Position, sourceRectangle, _color,
                Rotation, origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
