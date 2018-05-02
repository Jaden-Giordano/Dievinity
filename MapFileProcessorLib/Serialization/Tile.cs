using System;

namespace MapFileProcessorLib.Serialization {
    [Serializable]
    public class Tile {

        public string textureAtlas;
        public int id;

        public Vector2i position;

        public bool blocked;

        public Tile(string textureAtlas, int id, Vector2i position, bool blocked) {
            this.textureAtlas = textureAtlas;
            this.id = id;
            this.position = position;
            this.blocked = blocked;
        }

    }
}
