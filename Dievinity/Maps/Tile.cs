using Dievinity.Graphics;
using Dievinity.Managers;
using Microsoft.Xna.Framework;

namespace Dievinity.Maps {
    public class Tile {

        public string atlas;

        public int id;
        public Point position;
        public bool blocked;

        public Tile(string atlas, int id) {
            this.atlas = atlas;
            this.id = id;
            this.position = Point.Zero;
            this.blocked = false;
        }

        public Tile(string atlas, int id, Point position) {
            this.atlas = atlas;
            this.id = id;
            this.position = position;
            this.blocked = false;
        }

        public Tile(string atlas, int id, Point position, bool blocked) {
            this.atlas = atlas;
            this.id = id;
            this.position = position;
            this.blocked = blocked;
        }
    }
}
