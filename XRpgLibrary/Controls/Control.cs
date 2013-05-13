using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.Controls
{
    public abstract class Control
    {
        #region Field Region

        protected Vector2 position;

        #endregion

        #region Event Region

        public event EventHandler Selected;

        #endregion

        #region Property Region

        public string Name { get; set; }
        public string Text { get; set; }
        public Vector2 Size { get; set; }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                position.Y = (int) position.Y;
            }
        }

        public object Value { get; set; }
        public virtual bool HasFocus { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public bool TabStop { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public Color Color { get; set; }
        public string Type { get; set; }

        #endregion

        #region Constructor Region

        protected Control()
        {
            Color = Color.White;
            Enabled = true;
            Visible = true;
            SpriteFont = ControlManager.SpriteFont;
        }

        #endregion

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput(PlayerIndex playerIndex);

        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }
    }
}