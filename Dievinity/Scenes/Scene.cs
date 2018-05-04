using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dievinity.Utilities;

namespace Dievinity.Scenes {
    public class Scene {

        protected Map map;

        protected Vector2 camera;

        public Scene(Map map) {
            this.map = map;
            this.camera = Vector2.Zero;
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
                        Tile tile = map.GetTile(new Vector2i(x, y));
                        if (tile != null) {
                            tile.atlas.Draw(spriteBatch, camera, tile.id, tile.position.ToVector2(), 3);
                        }
                    }
                }
            }
        }
    }
}
