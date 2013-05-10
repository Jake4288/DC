using System.Collections.Generic;
using DungeonCrawl.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XRpgLibrary;
using XRpgLibrary.CharacterClasses;
using XRpgLibrary.SpriteClasses;

namespace DungeonCrawl
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private CollisionHandler _collisionHandler;
        private Enemy _enemy;
        private Player _player;
        private SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;
        public int ScreenWidth = 900;
        public int ScreenHeight = 600;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            _stateManager = new GameStateManager(this);
            Components.Add(_stateManager);
        }

        protected override void Initialize()
        {
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(Window.Handle);
            form.Location = new System.Drawing.Point(0, 10);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var animations = new Dictionary<AnimationKey, Animation>();

            var animation = new Animation(3, 32, 32, 0, 0);
            animations.Add(AnimationKey.Down, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            animations.Add(AnimationKey.Left, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            animations.Add(AnimationKey.Right, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            animations.Add(AnimationKey.Up, animation);

            var sprite = new AnimatedSprite(@"PlayerSprites\malepriest", animations, 200.0f, 1.5f);
            var enemsprite = new AnimatedSprite(@"PlayerSprites\femalepriest", animations, 100.0f, 2.0f);
            sprite.Position = new Vector2(300, 300);

            var character = new Character(sprite);
            var enemcharacter = new Character(enemsprite);

            _player = new Player(this, character);
            _enemy = new Enemy(this, enemcharacter, _player);

            _player.LoadContent(Content);
            _enemy.LoadContent(Content);

            _collisionHandler = new CollisionHandler(this, _player, _enemy);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _player.Update(gameTime);
            _enemy.Update(gameTime);
            _collisionHandler.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player.Draw(gameTime, _spriteBatch);
            _enemy.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}