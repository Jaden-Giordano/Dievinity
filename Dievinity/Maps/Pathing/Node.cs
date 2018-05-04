using System;
using Microsoft.Xna.Framework;

namespace Dievinity.Maps.Pathing {
    public class Node : IEquatable<Node> {

        public Node parent;

        public Point position;

        public int cost;
        public int distance;

        public int Score {
            get { return cost + distance;  }
        }

        public Node(Node parent, Point position, int cost, int distance) {
            this.parent = parent;
            this.position = position;
            this.cost = cost;
        }

        public bool Equals(Node other) {
            return this.position == other.position;
        }
    }
}
