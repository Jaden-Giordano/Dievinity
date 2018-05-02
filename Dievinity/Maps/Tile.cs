using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dievinity.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Maps {
    class Tile {

        public Atlas atlas;
        public int id;
        public Vector2i position;
        public bool blocked;

        public Tile(Atlas atlas, int id) {
            this.atlas = atlas;
            this.id = id;
            this.position = Vector2i.Zero;
            this.blocked = false;
        }

        public Tile(Atlas atlas, int id, Vector2i position) {
            this.atlas = atlas;
            this.id = id;
            this.position = position;
            this.blocked = false;
        }

        public Tile(Atlas atlas, int id, Vector2i position, bool blocked) {
            this.atlas = atlas;
            this.id = id;
            this.position = position;
            this.blocked = blocked;
        }
    }
}
