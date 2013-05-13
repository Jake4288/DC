using System.Windows.Forms;
using DungeonCrawl.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XRpgLibrary;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Point = System.Drawing.Point;

namespace DungeonCrawl
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public readonly Rectangle ScreenRectangle;
// ReSharper disable UnaccessedField.Local
        private readonly GraphicsDeviceManager _graphics;
// ReSharper restore UnaccessedField.Local
        private readonly GameStateManager _stateManager;
        public GamePlayScreen GamePlayScreen;

        public int ScreenHeight = 600;
        public int ScreenWidth = 900;
        public TitleScreen TitleScreen;
        public SpriteBatch SpriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
                            {
                                PreferredBackBufferWidth = ScreenWidth,
                                PreferredBackBufferHeight = ScreenHeight
                            };

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
            var form = (Form) Control.FromHandle(Window.Handle);
            form.Location = new Point(0, 10);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
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