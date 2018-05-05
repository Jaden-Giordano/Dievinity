using Dievinity.Entities;
using Dievinity.Managers;
using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Scene() {
            entities = new List<Entity>();
        }

        public virtual void SetMap(Map map) {
            this.map = map;
        }

        public virtual void AddEntities(Entity[] entities) {
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

        public bool IsBlockedByEntity(Point position) {
            Entity entity = GetEntityAt(position);
            return entity != null;
        }

        public Entity GetEntityAt(Point position) {
            Entity[] all = (from e in Entities where Map.GetCellPosition(e.position) == position select e).ToArray();

            if (all.Length > 0) {
                return all[0];
            }

            return null;
        }

        public virtual void Begin() {

        }

        public virtual void Input(GameTime gameTime) {

        }

        public virtual void Update(GameTime gameTime) {
            foreach (Entity i in entities) {
                i.Update(gameTime);
            }
        }

        public virtual void Draw() {
            if (map != null) {
                for (int x = -map.width / 2; x < map.width / 2; x += 1) {
                    for (int y = -map.height / 2; y < map.height / 2; y += 1) {
                        Tile tile = map.GetTile(new Point(x, y));
                        if (tile != null) {
                            Renderer.Instance.Draw(AtlasManager.Instance.CreateAtlas(tile.atlas, tile.id, tile.position.ToVector2(), 3));
                        }
                    }
                }
            }
        }
    }
}
