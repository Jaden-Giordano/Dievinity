using Dievinity.Maps;
using Dievinity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dievinity.Entities {
    public class Enemy : TurnBasedEntity {

        public Enemy(Scene parentScene, Point position, Texture2D texture) : base(parentScene, position, texture) {
            Stats.Initiative = 1;
        }

        public override void TurnUpdate(GameTime gameTime) {
            base.TurnUpdate(gameTime);

            Player player = parentScene.GetPlayer();
            if (!moving && player != null) {
                Point playerCellPosition = Map.GetCellPosition(player.Position);

                bool selectionFound = false;
                Point selection = Point.Zero;
                for (int i = -1; i < 2; i += 1) {
                    if (selectionFound) break;

                    for (int j = -1; j < 2; j += 1) {
                        if ((Math.Abs(i) == 1 && Math.Abs(j) == 1) || (i == 0 && j == 0)) {
                            continue;
                        }

                        Tile adjacent = parentScene.Map.GetTile(playerCellPosition + new Point(i, j));
                        if (adjacent != null && !adjacent.blocked && !parentScene.IsBlockedByEntity(adjacent.position)) {
                            selection = adjacent.position;
                            selectionFound = true;

                            if (ExecuteMovement(selection)) {
                                break;
                            } else {
                                selectionFound = false;
                            }
                        }
                    }
                }

                if (!selectionFound) {
                    TurnFinished = true;
                }
            }
        }

        protected override void FinishedMovement() {
            base.FinishedMovement();

            if (!TurnFinished) {
                TurnFinished = true;
            }
        }
    }
}
