using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dievinity.Utilities;

namespace Dievinity.Maps.Pathing {
    class Node : IEquatable<Node> {

        public Node parent;

        public Vector2i position;

        public int cost;
        public int distance;

        public int Score {
            get { return cost + distance;  }
        }

        public Node(Node parent, Vector2i position, int cost, int distance) {
            this.parent = parent;
            this.position = position;
            this.cost = cost;
        }

        public bool Equals(Node other) {
            return this.position == other.position;
        }
    }
}
