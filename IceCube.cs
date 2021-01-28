using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class IceCube
    {
        public Texture2D Texture { get; set; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        public Rectangle DestinationRectangle => new Rectangle((int) Position.X, (int) Position.Y, Size, Size);

        public IceCube(Texture2D texture, float speed, Vector2 position, int size)
        {
            Texture = texture;
            Speed = speed;
            Position = position;
            Size = size;
        }
    }
}