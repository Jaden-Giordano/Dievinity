using System.Collections.Generic;
using Dievinity.Managers;
using Dievinity.Managers.Input;
using Dievinity.Maps;
using Dievinity.Maps.Pathing;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dievinity.Entities {
    public class Player : Entity {

        private Point ghostStart;
        private Point[] ghostPath;
        private Point ghostDestination;
        private GhostLayer ghost;

        private bool moving;
        private Point[] movementPath;

        public Player(Scene parentScene, Point position, Texture2D texture) : base(parentScene, position, texture) {
            this.ghost = new GhostLayer(100, 100);

            turnFinished = false;
            moving = false;
        }

        private void FinishTurn() {
            if (!turnFinished) {
                turnFinished = false;
            }
            moving = false;

            movementPath = null;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (moving) {
                if (movementPath.Length > 0) {
                    if (Vector2.Distance(position, Map.GetActualPosition(movementPath[0])) <= 2.5f) {
                        position = Map.GetActualPosition(movementPath[0]);

                        List<Point> tmp = new List<Point>(movementPath);
                        tmp.RemoveAt(0);
                        movementPath = tmp.ToArray();
                    }

                    if (movementPath.Length > 0) {
                        Vector2 direction = Vector2.Normalize(Map.GetActualPosition(movementPath[0]) - position);
                        position += direction * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    } else {
                        FinishTurn();
                    }
                } else {
                    FinishTurn();
                }
            }
        }

        public override void TurnUpdate(GameTime gameTime) {
            base.TurnUpdate(gameTime);

            if (!moving) {
                Point playerCellPosition = Map.GetCellPosition(position);
                Point mouseCellPosition = Map.GetCellPosition(Mouse.GetState().Position.ToVector2() - Camera.Instance.position);
                if (ghostPath == null || playerCellPosition != ghostStart || mouseCellPosition != ghostDestination) {
                    PathFinder pf = new PathFinder(playerCellPosition, mouseCellPosition, parentScene.Map);
                    ghostPath = pf.FindPath();
                    ghostDestination = mouseCellPosition;
                    ghostStart = playerCellPosition;

                    ghost.Clear();

                    ghost.DrawPath(ghostPath, 0);
                }

                if (!moving && InputManager.Instance.GetMouseReleased(MouseButtons.Left)) {
                    PathFinder pf = new PathFinder(playerCellPosition, mouseCellPosition, parentScene.Map);
                    movementPath = pf.FindPath();
                    moving = true;

                    ghost.Clear();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            ghost.Draw(spriteBatch);
        }
    }
}
