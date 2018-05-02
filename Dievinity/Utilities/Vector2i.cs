using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dievinity.Utilities {
    class Vector2i : IEquatable<Vector2i> {

        public int x, y;

        public Vector2i() {
            this.x = 0;
            this.y = 0;
        }

        public Vector2i(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Vector2i Zero {
            get { return new Vector2i(0, 0); }
        }

        public static int MDistance(Vector2i start, Vector2i end) {
            return (end.x - start.x) + (end.y - start.y);
        }

        public static int Distance(Vector2i start, Vector2i end) {
            return (int)Math.Sqrt(Math.Pow(end.x - start.x, 2) + Math.Pow(end.y - start.y, 2));
        }

        public static Vector2i operator+(Vector2i a, Vector2i b) {
            return new Vector2i(a.x + b.x, a.y + b.y);
        }

        public static Vector2i operator -(Vector2i a, Vector2i b) {
            return new Vector2i(a.x - b.x, a.y - b.y);
        }

        public override string ToString() {
            return $"({x}, {y})";
        }

        public bool Equals(Vector2i other) {
            return this.x == other.x && this.y == other.y;
        }

        public static bool operator ==(Vector2i a, Vector2i b) {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2i a, Vector2i b) {
            return a.x != b.x || a.y != b.y;
        }
    }
}
