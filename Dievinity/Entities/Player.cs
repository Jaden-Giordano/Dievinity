using System;
using Dievinity.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Entities {
    public class Player {

        public Vector2 position;

        private Texture2D texture;

        public Player() {
            position = Vector2.Zero;
        }

        public Player(Vector2i position, Texture2D texture) {
            this.position = new Vector2(position.x * 16 * 3, position.y * 16 * 3);
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (texture != null) {
                Rectangle destination = new Rectangle((int)position.X, (int)position.Y, texture.Width * 3, texture.Height * 3);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

                spriteBatch.Draw(texture, destination, Color.White);

                spriteBatch.End();
            }
        }

    }
}
