using Dievinity.Graphics;
using Dievinity.Managers;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Maps.Pathing {
    public class GhostLayer {

        private Map debugLayer;

        public Color Color { get; set; }

        public GhostLayer(Scene parent, int width, int height) {
            debugLayer = new Map(parent, width, height);
            Color = Color.White;
        }

        public void DrawPath(Point[] path, int id) {
            if (path != null) {
                foreach (Point point in path) {
                    debugLayer.SetTile(point, new Tile("Ghost", id, point));
                }
            }
        }

        public void Clear() {
            debugLayer.Clear();
        }

        public void Draw() {
            for (int x = -debugLayer.width / 2; x < debugLayer.width / 2; x += 1) {
                for (int y = -debugLayer.height / 2; y < debugLayer.height / 2; y += 1) {
                    Tile tile = debugLayer.GetTile(new Point(x, y));
                    if (tile != null) {
                        
                        Renderer.Instance.Draw(AtlasManager.Instance.CreateAtlas(tile.atlas, tile.id, tile.position.ToVector2(), 3, 2));
                    }
                }
            }
        }
    }
}
