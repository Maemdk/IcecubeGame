using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SpriteFont _font;
        
        private Texture2D _iceCubeTexture;
        private int _iceCubeLastSpawnTime;
        private Random _random = new Random();
        private SoundEffect _iceCubeEscapeSound;
        private SoundEffect _iceCubeEatSound;

        private List<IceCube> _iceCubes = new List<IceCube>();

        private int _maxSprintTime = 3;
        private float _currentSprintTime;

        public void SpawnIceCube(int positionX)
        {
            var iceCube = new IceCube(_iceCubeTexture, 4, new Vector2(positionX, -50), 50);
            _iceCubes.Add(iceCube);
        }

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

            _font = Content.Load<SpriteFont>("font");
            _iceCubeTexture = Content.Load<Texture2D>("images/icecube");
            _iceCubeEscapeSound = Content.Load<SoundEffect>("sounds/iceyay");

            _currentSprintTime = _maxSprintTime;
;        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PlayerMovement(gameTime);

            if (_iceCubeLastSpawnTime + 1 < gameTime.TotalGameTime.TotalSeconds)
            {
                SpawnIceCube(_random.Next(0, _windowWidth - 50));
                _iceCubeLastSpawnTime = (int) gameTime.TotalGameTime.TotalSeconds;
            }

            var aliveIceCubes = new List<IceCube>();
            foreach (var iceCube in _iceCubes)
            {
                iceCube.Position = new Vector2(iceCube.Position.X, iceCube.Position.Y + iceCube.Speed);

                if (iceCube.Position.Y >= _windowHeight)
                {
                    _score--;
                    _iceCubeEscapeSound.Play();
                } else if (_player.DestinationRectangle.Intersects(iceCube.DestinationRectangle))
                {
                    _score++;
                }
                else
                {
                    aliveIceCubes.Add(iceCube);
                }
            }

            _iceCubes = aliveIceCubes;

            base.Update(gameTime);
        }

        private void PlayerMovement(GameTime Gametime)
        {
            var currentSpeed = _player.Speed;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (_currentSprintTime > 0)
                {
                    currentSpeed *= 2;

                    _currentSprintTime -= (float) Gametime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                _currentSprintTime += (float) Gametime.ElapsedGameTime.TotalSeconds;
            }
            _currentSprintTime = MathHelper.Clamp(_currentSprintTime, 0, 3);
            
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (_player.Position.X < _windowWidth - _player.Size)
                {
                    _player.Position = new Vector2(_player.Position.X + currentSpeed, _player.Position.Y);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (_player.Position.X > 0)
                {
                    _player.Position = new Vector2(_player.Position.X - currentSpeed, _player.Position.Y);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_player.Texture, _player.DestinationRectangle, Color.White);
            _spriteBatch.DrawString(_font, $"Score: {_score}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(_font, $"Sprint: {Math.Ceiling(_currentSprintTime)}", new Vector2(10, 30), Color.White);
            foreach (var iceCube in _iceCubes)
            {
                _spriteBatch.Draw(iceCube.Texture, iceCube.DestinationRectangle, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}