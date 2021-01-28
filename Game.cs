using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private int _windowWidth;
        private int _windowHeight;
        private int _score;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _windowWidth = _graphics.PreferredBackBufferWidth;
            _windowHeight = _graphics.PreferredBackBufferHeight;
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player = new Player(Content.Load<Texture2D>("images/icedcoffee"), "John", 5,
                new Vector2(_windowWidth / 2.0f - 50, _windowHeight - 100), 100);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (_player.Position.X < _windowWidth - _player.Size)
                {
                    _player.Position = new Vector2(_player.Position.X + _player.Speed, _player.Position.Y);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (_player.Position.X > 0)
                {
                    _player.Position = new Vector2(_player.Position.X - _player.Speed, _player.Position.Y);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_player.Texture, _player.DestinationRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}