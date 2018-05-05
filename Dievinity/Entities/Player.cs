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
    public class Player : MovingEntity {

        private Point ghostStart;
        private Point[] ghostPath;
        private Point ghostDestination;
        private GhostLayer ghost;

        public Player(Scene parentScene, Point position, Texture2D texture) : base(parentScene, position, texture) {
            this.ghost = new GhostLayer(100, 100);

            turnFinished = false;
        }

        protected override void FinishedMovement() {
            base.FinishedMovement();

            if (!turnFinished) {
                turnFinished = true;
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

                if (InputManager.Instance.GetMouseReleased(MouseButtons.Left)) {
                    ExecuteMovement(Map.GetCellPosition(Mouse.GetState().Position.ToVector2() - Camera.Instance.position));

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
