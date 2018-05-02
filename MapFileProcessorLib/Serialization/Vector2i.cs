using System;

namespace MapFileProcessorLib.Serialization {
    [Serializable]
    public class Vector2i {

        public int x, y;

        public Vector2i(int x, int y) {
            this.x = x;
            this.y = y;
        }

    }
}
