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
    public class MainMenu : IStateManager
    {
        static Vector2 _origin = new Vector2(640, 500);
        private Sound _mainMusic = new Sound("Sounds/Music/maintheme", true);
        private Canvas _mainCanvas = new Canvas(CoordinateHelper.WindowWidth, CoordinateHelper.WindowHeight);
        private ParticleEngine _engine = new ParticleEngine();
        private Texture2D testButton;
        private bool _active;
        private Button _testButton = new Button("BeginButton", _origin, Color.White, 0, 0.5f);
        public MainMenu()
        {
            var headerLoc = new Vector2(CoordinateHelper.WindowWidth / 2.0f, 100);

            var header = new TextLabel("Eclipse II", "Fonts/OCR A Extended", Color.Yellow, headerLoc);
            _mainCanvas.AddItem(header, 0);
            _mainCanvas.AddItem(_testButton, 10);
        }

        public void LoadContent(ContentManager cm)
        {
            _mainMusic.LoadContent(cm);
            var star = cm.Load<Texture2D>("Particles/ParticleCircle");

            ParticleGenerator pg = new WarpDriveParticleGenerator(CoordinateHelper.CenterScreen, star);
            _engine.AddParticleGenerator(pg);

            _mainCanvas.LoadContent(cm);
        }

        public void Update(GameTime gameTime)
        {
            if (!_mainMusic.IsPlaying)
            {
                _mainMusic.Play();
            }

            var loc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _engine.Update(gameTime, loc);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            _mainCanvas.Draw(sb);
            _engine.Draw(sb);
        }

        public bool IsActive
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active != value)
                {
                    _active = value;

                    if (!_active)
                    {
                        // No longer active... stop music
                        _mainMusic.Stop();
                    }

                }
            }
        }
    }
}
