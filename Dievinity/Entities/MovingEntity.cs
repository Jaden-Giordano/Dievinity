using Dievinity.Maps;
using Dievinity.Maps.Pathing;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Dievinity.Entities {
    public class MovingEntity : Entity {

        protected int speed;

        protected bool moving;
        private Point[] movementPath;

        public MovingEntity(Scene parentScene, Point position, Texture2D texture) : base(parentScene, position, texture) {
            speed = 250;

            moving = false;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (moving) {
                if (movementPath.Length > 0) {
                    if (Vector2.Distance(Position, Map.GetActualPosition(movementPath[0])) <= 2.5f) {
                        Position = Map.GetActualPosition(movementPath[0]);

                        List<Point> tmp = new List<Point>(movementPath);
                        tmp.RemoveAt(0);
                        movementPath = tmp.ToArray();
                    }

                    if (movementPath.Length > 0) {
                        Vector2 direction = Vector2.Normalize(Map.GetActualPosition(movementPath[0]) - Position);
                        Position += direction * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    } else {
                        FinishedMovement();
                    }
                } else {
                    FinishedMovement();
                }
            }
        }

        protected virtual bool ExecuteMovement(Point target) {
            Tile targetTile = parentScene.Map.GetTile(target);
            if (targetTile == null || targetTile.blocked || parentScene.IsBlockedByEntity(target)) {
                return false;
            }

            Point cellPosition = Map.GetCellPosition(Position);
            Point targetCellPosition = target;

            PathFinder pf = new PathFinder(cellPosition, targetCellPosition, parentScene.Map);
            movementPath = pf.FindPath();

            if (movementPath != null && movementPath.Length <= Stats.ActionPoints) {
                Stats.ActionPoints -= movementPath.Length;
                moving = true;
                return true;
            }

            return false;
        }

        protected virtual void FinishedMovement() {
            moving = false;
            movementPath = null;
        }
    }
}
