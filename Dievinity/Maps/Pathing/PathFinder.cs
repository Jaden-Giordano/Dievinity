using System;
using System.Collections.Generic;
using Dievinity.Entities;
using Dievinity.Managers;
using Microsoft.Xna.Framework;

namespace Dievinity.Maps.Pathing {
    public class PathFinder {

        private Map map;
        private Point start;
        private Point end;

        private List<Node> openList;
        private List<Node> closedList;

        public PathFinder(Point start, Point end, Map map) {
            this.map = map;
            this.start = start;
            this.end = end;

            openList = new List<Node>();
            closedList = new List<Node>();
        }

        public Point[] FindPath() {
            Tile startTile = map.GetTile(start);
            Tile endTile = map.GetTile(end);
            if (startTile == null || endTile == null || startTile.blocked || endTile.blocked) {
                return null;
            }

            Node startingNode = new Node(null, start, 0, (int) Vector2.Distance(start.ToVector2(), end.ToVector2()) * 10);
            openList.Add(startingNode);

            if (ProcessNextNode()) {
                List<Point> points = new List<Point>();

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

                    int cost = 10;

                    if (Math.Abs(i) == 1 && Math.Abs(j) == 1) {
                        cost = 14;
                    }

                    Point position = next.position + new Point(i, j);
                    Node node = new Node(next, position, next.cost + cost, (int)Vector2.Distance(start.ToVector2(), end.ToVector2()) * 10);
                    Tile checkTile = map.GetTile(position);
                    if (checkTile != null && !checkTile.blocked && !closedList.Contains(node) && !map.ParentScene.IsBlockedByEntity(position)) {
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
