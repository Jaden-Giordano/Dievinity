using System;
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
    public class Player : TurnBasedEntity {

        private Point ghostStart;
        private Point[] ghostPath;
        private Point ghostDestination;
        private GhostLayer ghost;

        private State state;

        public Player(Scene parentScene, Point position, Texture2D texture) : base(parentScene, position, texture) {
            this.ghost = new GhostLayer(parentScene, 100, 100);

            TurnFinished = false;
        }

        protected override void FinishedMovement() {
            base.FinishedMovement();

            if (!TurnFinished) {
                TurnFinished = true;
            }
        }

        public override void TurnUpdate(GameTime gameTime) {
            base.TurnUpdate(gameTime);

            if (InputManager.Instance.GetKeyPressed(Keys.M)) {
                state = State.Moving;
            } else if (InputManager.Instance.GetKeyPressed(Keys.A)) {
                state = State.Attacking;
            }

            if (state == State.Moving) {
                if (!moving) {
                    Point playerCellPosition = Map.GetCellPosition(Position);
                    Point mouseCellPosition = Map.GetCellPosition(Mouse.GetState().Position.ToVector2() - Camera.Instance.position);
                    if (ghostPath == null || playerCellPosition != ghostStart || mouseCellPosition != ghostDestination) {
                        PathFinder pf = new PathFinder(playerCellPosition, mouseCellPosition, parentScene.Map);
                        ghostPath = pf.FindPath();
                        ghostDestination = mouseCellPosition;
                        ghostStart = playerCellPosition;

                        ghost.Clear();

                        if (ghostPath != null) {
                            List<Point> tmp = new List<Point>(ghostPath);
                            tmp.RemoveAt(0);
                            ghostPath = tmp.ToArray();

                            if (ghostPath.Length + 1 <= Stats.ActionPoints) {
                                ghost.DrawPath(ghostPath, 0);
                            }
                        }
                    }

                    if (InputManager.Instance.GetMouseReleased(MouseButtons.Left)) {
                        ExecuteMovement(Map.GetCellPosition(Mouse.GetState().Position.ToVector2() - Camera.Instance.position));

                        ghost.Clear();
                    }
                }
            } else if (state == State.Attacking) {
                ghost.Clear();
                Point playerCellPosition = Map.GetCellPosition(Position);
                Point mouseCellPosition = Map.GetCellPosition(Mouse.GetState().Position.ToVector2() - Camera.Instance.position);
                if (Vector2.Distance(playerCellPosition.ToVector2(), mouseCellPosition.ToVector2()) <= 1.5
                    && parentScene.IsBlockedByEntity(mouseCellPosition)) {
                    ghostPath = new Point[] { mouseCellPosition };

                    ghost.DrawPath(ghostPath, 0);

                    if (InputManager.Instance.GetMouseReleased(MouseButtons.Left)) {

                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            ghost.Draw(spriteBatch);
        }
    }
}
