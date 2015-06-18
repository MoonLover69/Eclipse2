using Eclipse2Game.GameObjects;
using Eclipse2Game.GameObjects.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game
{
    public class MainMenu : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Sound _mainMusic = new Sound("Sounds/Music/maintheme", true);
        private ParticleSource _particles = new ParticleSource();

        private Button _testButton;
        private TextLabel _header;


        public MainMenu(Game parent)
            : base(parent)
        {
            parent.Components.Add(this);

            var headerLoc = new Vector2(CoordinateHelper.WindowWidth / 2.0f, 100);

            _header = new TextLabel("Eclipse II", "Fonts/OCR A Extended", Color.Yellow, headerLoc);
            _testButton = new Button("BeginButton", CoordinateHelper.CenterScreen, Color.White, 0, 0.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _testButton.LoadContent(Game.Content);

            var star = Game.Content.Load<Texture2D>("Particles/ParticleStar");

            ParticleGenerator pg = new WarpDriveParticleGenerator(CoordinateHelper.CenterScreen, star);
            _particles.AddParticleGenerator(pg);
            _particles.AddParticleGenerator(new RandomParticleGenerator(star));

            _particles.LoadContent(Game.Content);
            _mainMusic.LoadContent(Game.Content);
            _header.LoadContent(Game.Content);
        }

        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            if (!this.Enabled)
            {
                _mainMusic.Stop();
            }

            base.OnEnabledChanged(sender, args);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                this.Enabled = false;
            }

            if (!this.Enabled)
            {
                return;
            }

            if (!_mainMusic.IsPlaying)
            {
                _mainMusic.Play();
            }

            _particles.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!this.Enabled)
            {
                return;
            }

            _spriteBatch.Begin();

            _particles.Draw(gameTime, _spriteBatch);
            _testButton.Draw(gameTime, _spriteBatch);
            _header.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }
    }
}
