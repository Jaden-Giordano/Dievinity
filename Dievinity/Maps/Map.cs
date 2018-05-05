using Dievinity.Entities;
using Dievinity.Managers;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;

namespace Dievinity.Maps {
    public class Map {

        public Scene ParentScene {
            get;
            private set;
        }

        private Tile[,] tiles;

        public int width;
        public int height;

        public Map(Scene parent, int width, int height) {
            this.ParentScene = parent;
            this.width = width;
            this.height = height;
            tiles = new Tile[width, height];
        }

        private Point ToWorldCoords(Point position) {
            return position - new Point(width / 2, height / 2);
        }

        private Point ToArrayCoords(Point position) {
            return position + new Point(width / 2, height / 2);
        }

        public Tile GetTile(Point position) {
            if (position.X >= width / 2 || position.X < -width / 2 || position.Y >= height / 2 || position.Y < -height / 2) {
                return null;
            }

            Point arrayCoords = ToArrayCoords(position);
            return tiles[arrayCoords.X, arrayCoords.Y];
        }

        public void SetTile(Point position, Tile tile) {
            Point arrayCoords = ToArrayCoords(position);
            tiles[arrayCoords.X, arrayCoords.Y] = tile;
        }

        public void Clear() {
            tiles = new Tile[width, height];
        }

        public static Vector2 GetActualPosition(Point position) {
            return position.ToVector2() * 16 * 3;
        }

        public static Point GetCellPosition(Vector2 position) {
            Vector2 newPosition = position / (16 * 3);
            return newPosition.ToPoint();
        }
    }
}
