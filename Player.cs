using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class Player
    {
        public Texture2D Texture { get; set; }
        public string Name { get; set; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        public Rectangle DestinationRectangle => new Rectangle((int) Position.X, (int) Position.Y, Size, Size);

        public Player(Texture2D texture, string name, float speed, Vector2 position, int size)
        {
            Texture = texture;
            Name = name;
            Speed = speed;
            Position = position;
            Size = size;
        }
    }
}