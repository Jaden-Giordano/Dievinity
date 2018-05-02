using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dievinity.Utilities;

namespace Dievinity.Maps {
    class Map {

        private Tile[,] tiles;

        public int width;
        public int height;

        public Map(int width, int height) {
            this.width = width;
            this.height = height;
            tiles = new Tile[width, height];
        }

        private Vector2i ToWorldCoords(Vector2i position) {
            return position - new Vector2i(width / 2, height / 2);
        }

        private Vector2i ToArrayCoords(Vector2i position) {
            return position + new Vector2i(width / 2, height / 2);
        }

        public Tile GetTile(Vector2i position) {
            if (position.x >= width / 2 || position.x < -width / 2 || position.y >= height / 2 || position.y < -height / 2) {
                return null;
            }

            Vector2i arrayCoords = ToArrayCoords(position);
            return tiles[arrayCoords.x, arrayCoords.y];
        }

        public void SetTile(Vector2i position, Tile tile) {
            Vector2i arrayCoords = ToArrayCoords(position);
            tiles[arrayCoords.x, arrayCoords.y] = tile;
        }

        public void Clear() {
            tiles = new Tile[width, height];
        }
    }
}
