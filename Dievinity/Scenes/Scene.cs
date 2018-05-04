using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Scenes {
    public class Scene {

        protected Map map;
        public Map Map {
            get {
                return map      ;
            }
        }

        public Scene(Map map) {
            this.map = map;
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
