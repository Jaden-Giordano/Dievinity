using Dievinity.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dievinity.Graphics {
    public class Sprite : IComparable<Sprite> {

        protected Texture2D texture;
        protected Vector2 position;
        protected float scale;
        protected Color color;
        protected int order;

        public Sprite(Texture2D texture, float scale = 1, int order = 0) {
            this.texture = texture;
            position = Vector2.Zero;
            scale = 1;
            color = Color.White;
            this.order = order;
        }

        public Sprite(Texture2D texture, Vector2 position, float scale = 1, int order = 0) {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.color = Color.White;
            this.order = order;
        }

        public Sprite(Texture2D texture, Vector2 position, Color color, float scale = 1, int order = 0) {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.color = color;
            this.order = order;
        }

        public int CompareTo(Sprite other) {
            return order.CompareTo(other.order);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            Point adjustedPosition = (position - Camera.Instance.position).ToPoint();
            Rectangle destination = new Rectangle(adjustedPosition.X, adjustedPosition.Y, (int) (texture.Width * scale), (int) (texture.Height * scale));

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(texture, destination, color);

            spriteBatch.End();
        }
    }
}
