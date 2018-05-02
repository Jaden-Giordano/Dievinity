using System;
using MapFileProcessorLib.Serialization;

namespace MapFileProcessorLib {
    [Serializable]
    public class MapFile {

        public int width, height;

        public Tile[] tiles;

        public MapFile(int width, int height, Tile[] tiles) {
            this.width = width;
            this.height = height;
            this.tiles = tiles;
        }

    }
}
