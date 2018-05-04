using Microsoft.Xna.Framework.Graphics;
using Dievinity.Maps;
using Dievinity.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Dievinity.Maps.Pathing;
using Dievinity.Managers;
using System;
using Dievinity.Entities;
using Dievinity.Managers.Input;

namespace Dievinity.Scenes {
    public class PathTestingScene : Scene {

        private Map debugLayer;

        private Player player;

        Vector2 mousePosition;

        private bool moving;
        private Vector2i movementPosition;

        private bool movingToNext = false;
        private Vector2 nextPosition;

        public PathTestingScene(Map map, Player player) : base(map) {
            debugLayer = new Map(map.width, map.height);

            this.player = player;
        }

        public override void Input(GameTime gameTime) {
            base.Input(gameTime);

            mousePosition = Mouse.GetState().Position.ToVector2();

            if (InputManager.Instance.GetMousePressed(MouseButtons.Right)) {
                moving = true;
                movementPosition = map.GetCellPosition(mousePosition + camera);
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (moving) {
                PathFinder pf = new PathFinder(map.GetCellPosition(player.position), movementPosition, map);
                Vector2i[] path = pf.FindPath();

                if (!movingToNext && path != null && path.Length > 1) {
                    nextPosition = map.GetActualPosition(path[1]);
                    movingToNext = true;
                }

                if (movingToNext) {
                    Vector2 direction = Vector2.Normalize(nextPosition - player.position);

                    player.position += direction * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    float distance = Vector2.Distance(player.position, nextPosition);

                    if (distance <= 2.5f) {
                        player.position = nextPosition;
                        movingToNext = false;
                    }
                }

                if (map.GetCellPosition(player.position) == movementPosition && !movingToNext) {
                    moving = false;
                }
            }

            debugLayer.Clear();

            PathFinder selectionPF = new PathFinder(map.GetCellPosition(player.position), map.GetCellPosition(mousePosition + camera), map);
            Vector2i[] selectionPath = selectionPF.FindPath();

            if (selectionPath != null && selectionPath.Length > 0) {
                foreach (Vector2i position in selectionPath) {
                    debugLayer.SetTile(position, new Tile(AtlasManager.Instance.GetAtlas("Debug"), 0, position));
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            for (int x = -debugLayer.width / 2; x < debugLayer.width / 2; x += 1) {
                for (int y = -debugLayer.height / 2; y < debugLayer.height / 2; y += 1) {
                    Tile tile = debugLayer.GetTile(new Vector2i(x, y));
                    if (tile != null) {
                        tile.atlas.Draw(spriteBatch, camera, tile.id, tile.position.ToVector2(), 3);
                    }
                }
            }

            player.Draw(spriteBatch);
        }
    }
}
