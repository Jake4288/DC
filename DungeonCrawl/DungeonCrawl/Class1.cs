using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCrawl
{
    public class Player
    {
        // Change these to control your player
        private Vector2 _playerPosition = Vector2.Zero;

        // Change these to control the sword
        private float _swordDistance = 50f;
        private float _swordRotation = MathHelper.ToRadians(0);

        // Movement constants used in input handling
        private const float SwordRotationSpeed = 720f;

        // Rendering only constants
        private readonly Rectangle _swordSize = new Rectangle(0, 0, 10, 10);
        private readonly Vector2 _swordOrigin = new Vector2(5, 5);
        readonly Texture2D _pixel;

        public Player(GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
            _playerPosition = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height) / 2f;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboard.IsKeyDown(Keys.Q)) _swordDistance -= 50f * elapsed;
            if (keyboard.IsKeyDown(Keys.E)) _swordDistance += 50f * elapsed;
            if (keyboard.IsKeyDown(Keys.Space)) _swordRotation += MathHelper.ToRadians(SwordRotationSpeed) * elapsed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Calculate sword position in world space
            Vector2 localSword = new Vector2(0, _swordDistance);
            Matrix swordMatrix = Matrix.CreateRotationZ(_swordRotation) * GetPlayerWorldMatrix();
            Vector2 swordPosition = Vector2.Transform(localSword, swordMatrix);

            // Draw sword
            spriteBatch.Draw(_pixel, swordPosition, _swordSize, Color.Red, _playerRotation, _swordOrigin, _playerScale, SpriteEffects.None, 0f);
        }

        private Matrix GetPlayerWorldMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(_playerPosition, 0f));
        }
    }
}