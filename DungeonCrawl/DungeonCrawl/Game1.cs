using System.Collections.Generic;
using DungeonCrawl.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonCrawl.GameScreens;
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
        public SpriteBatch _spriteBatch;
        private GameStateManager _stateManager;
        public TitleScreen TitleScreen;
        public GamePlayScreen GamePlayScreen;

        public int ScreenWidth = 900;
        public int ScreenHeight = 600;

        public readonly Rectangle ScreenRectangle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;

            ScreenRectangle = new Rectangle(0, 0, ScreenWidth, ScreenHeight);

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            _stateManager = new GameStateManager(this);
            Components.Add(_stateManager);

            TitleScreen = new TitleScreen(this, _stateManager);
            GamePlayScreen = new GamePlayScreen(this, _stateManager);

            _stateManager.ChangeState(TitleScreen);
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

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}