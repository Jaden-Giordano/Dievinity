using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dievinity.Utilities;

namespace Dievinity.Maps.Pathing {
    class PathFinder {

        private Map map;
        private Vector2i start;
        private Vector2i end;

        private List<Node> openList;
        private List<Node> closedList;

        public PathFinder(Vector2i start, Vector2i end, Map map) {
            this.map = map;
            this.start = start;
            this.end = end;

            openList = new List<Node>();
            closedList = new List<Node>();
        }

        public Vector2i[] FindPath() {
            Tile startTile = map.GetTile(start);
            Tile endTile = map.GetTile(end);
            if (startTile == null || endTile == null || startTile.blocked || endTile.blocked) {
                return null;
            }

            Node startingNode = new Node(null, start, 0, Vector2i.Distance(start, end));
            openList.Add(startingNode);

            if (ProcessNextNode()) {
                List<Vector2i> points = new List<Vector2i>();

                Node endNode = closedList.Find(node => node.position == end);

                if (endNode != null) {
                    points.Add(endNode.position);

                    Node next = endNode.parent;
                    while (next != null) {
                        points.Add(next.position);
                        next = next.parent;
                    }

                    points.Reverse();

                    return points.ToArray();
                }
            }

            return null;
        }

        private bool ProcessNextNode(Node use = null) {
            Node next = use;
            if (next == null) {
                foreach (Node node in openList) {
                    if (next == null || node.Score < next.Score) {
                        next = node;
                    }
                }
            }

            // If no open nodes left.
            if (next == null) return false;
            // If we reached the goal.
            if (next.position == end) {
                closedList.Add(next);
                openList.Remove(next);
                return true;
            }

            for (int i = -1; i < 2; i += 1) {
                for (int j = -1; j < 2; j += 1) {
                    if (i == 0 && j == 0) continue;

                    Vector2i position = next.position + new Vector2i(i, j);
                    Node node = new Node(next, position, 10, Vector2i.Distance(position, end));
                    Tile checkTile = map.GetTile(position);
                    if (checkTile != null && !checkTile.blocked && !closedList.Contains(node)) {
                        Node openListCheck = openList.Find(n => n.position == node.position);
                        if (openListCheck != null && openListCheck.Score < next.Score) {
                            return ProcessNextNode(openListCheck);
                        }

                        openList.Add(node);
                    }
                }
            }

            closedList.Add(next);
            openList.Remove(next);
            
            return ProcessNextNode();
        }
    }
}
