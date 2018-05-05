using Dievinity.Managers;
using Dievinity.Maps;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dievinity.Entities {
    public abstract class Entity : IComparable<Entity> {

        protected Scene parentScene;

        public Vector2 position;
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        protected Texture2D texture;

        protected Stats stats;
        public Stats Stats {
            get { return stats; }
            set { stats = value; }
        }

        public Entity(Scene parentScene) {
            this.parentScene = parentScene;
            Position = Vector2.Zero;
            stats = new Stats();
        }

        public Entity(Scene parentScene, Vector2 position, Texture2D texture) {
            this.parentScene = parentScene;
            this.position = position;
            this.texture = texture;
            stats = new Stats();
        }

        public Entity(Scene parentScene, Point position, Texture2D texture) {
            this.parentScene = parentScene;
            this.position = Map.GetActualPosition(position);
            this.texture = texture;
            stats = new Stats();
        }

        public virtual void Update(GameTime gameTime) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (texture != null) {
                Point screenPosition = (Position - Camera.Instance.position).ToPoint();
                Rectangle destination = new Rectangle(screenPosition.X, screenPosition.Y, texture.Width * 3, texture.Height * 3);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

                spriteBatch.Draw(texture, destination, Color.White);

                spriteBatch.End();
            }
        }

        public int CompareTo(Entity other) {
            return Stats.CompareTo(other.Stats);
        }
    }
}
