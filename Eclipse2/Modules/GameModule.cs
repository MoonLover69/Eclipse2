﻿using Eclipse2Game.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse2Game.Modules
{
    public abstract class GameModule : DrawableGameComponent
    {
        public GameModule(Game parent, ComponentPanel upperPanel, ComponentPanel lowerPanel)
            : base(parent)
        {
            UpperPanel = upperPanel;
            LowerPanel = lowerPanel;
            Enabled = false;
            State = ModuleState.NotStarted;
        }

        /// <summary>
        /// Current state of the module
        /// </summary>
        public ModuleState State
        {
            get;
            protected set;
        }

        /// <summary>
        /// Start this module
        /// </summary>
        public void Start()
        {
            this.Enabled = true;
            OnModuleStarted();
        }

        /// <summary>
        /// Upper panel on the screen
        /// </summary>
        protected ComponentPanel UpperPanel
        {
            get;
            private set;
        }

        /// <summary>
        /// Lower panel on the screen
        /// </summary>
        protected ComponentPanel LowerPanel
        {
            get;
            private set;
        }

        protected KeyboardState CurrentKeyboard
        {
            get;
            private set;
        }

        protected KeyboardState LastKeyboard
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when the module starts
        /// </summary>
        protected virtual void OnModuleStarted()
        {
            State = ModuleState.Running;
        }

        /// <summary>
        /// Signals the module complete event
        /// </summary>
        protected virtual void OnModuleComplete()
        {
            State = ModuleState.Complete;

            var handler = Complete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            LastKeyboard = CurrentKeyboard;
            CurrentKeyboard = Keyboard.GetState();
        }

        /// <summary>
        /// Whether this module is active in the game
        /// </summary>
        protected override void OnEnabledChanged(object sender, EventArgs args)
        {
            base.OnEnabledChanged(sender, args);

            if (this.Enabled)
            {
                Game.Components.Add(this);
            }
            else
            {
                Game.Components.Remove(this);
            }
        }

        /// <summary>
        /// Occurs when the module is completed by the player
        /// </summary>
        public event EventHandler Complete;
    }

    public enum ModuleState
    {
        NotStarted,
        Running,
        Complete
    }
}
