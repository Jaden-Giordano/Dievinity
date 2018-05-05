using Dievinity.Entities;
using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Dievinity.Scenes {
    public class Scene {

        protected Map map;
        public Map Map {
            get {
                return map;
            }
        }

        protected List<Entity> entities;
        public Entity[] Entities {
            get {
                return entities.ToArray();
            }
        }

        public Scene(Map map) {
            this.map = map;
            entities = new List<Entity>();
        }

        public void AddEntities(Entity[] entities) {
            this.entities.AddRange(entities);
        }

        public Player GetPlayer() {
            foreach (Entity e in entities) {
                if (e is Player) {
                    return e as Player;
                }
            }

            return null;
        }

        public virtual void Begin() {

        }

        public virtual void Input(GameTime gameTime) {

        }

        public virtual void Update(GameTime gameTime) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (map != null) {
                for (int x = -map.width / 2; x < map.width / 2; x += 1) {
                    for (int y = -map.height / 2; y < map.height / 2; y += 1) {
                        Tile tile = map.GetTile(new Point(x, y));
                        if (tile != null) {
                            tile.atlas.Draw(spriteBatch, tile.id, tile.position.ToVector2(), 3);
                        }
                    }
                }
            }
        }
    }
}
