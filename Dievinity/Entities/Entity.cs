using Dievinity.Managers;
using Dievinity.Maps;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Entities {
    public abstract class Entity {

        protected Scene parentScene;

        public Vector2 position;

        protected Texture2D texture;

        public bool turnFinished;

        public Entity(Scene parentScene) {
            this.parentScene = parentScene;
            position = Vector2.Zero;
            turnFinished = true;
        }

        public Entity(Scene parentScene, Vector2 position, Texture2D texture) {
            this.parentScene = parentScene;
            this.position = position;
            this.texture = texture;
            turnFinished = true;
        }

        public Entity(Scene parentScene, Point position, Texture2D texture) {
            this.parentScene = parentScene;
            this.position = Map.GetActualPosition(position);
            this.texture = texture;
            turnFinished = true;
        }

        public virtual void Update(GameTime gameTime) {

        }

        public virtual void TurnUpdate(GameTime gameTime) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (texture != null) {
                Point screenPosition = (position - Camera.Instance.position).ToPoint();
                Rectangle destination = new Rectangle(screenPosition.X, screenPosition.Y, texture.Width * 3, texture.Height * 3);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

                spriteBatch.Draw(texture, destination, Color.White);

                spriteBatch.End();
            }
        }
    }
}
