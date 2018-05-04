using Dievinity.Entities;
using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Dievinity.Scenes {
    public class TurnBasedScene : Scene {

        public List<Entity> entities;

        private int currentTurn;

        public TurnBasedScene(Map map) : base(map) {
            this.entities = new List<Entity>();

            // TODO: Sort the players.

            currentTurn = 0;
        }

        public void AddEntities(Entity[] entities) {
            this.entities.AddRange(entities);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            Entity current = entities[currentTurn];

            if (current.turnFinished) {
                currentTurn = (currentTurn + 1) % entities.Count;
            } else {
                current.TurnUpdate(gameTime);
            }

            foreach (Entity i in entities) {
                i.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            foreach (Entity i in entities) {
                i.Draw(spriteBatch);
            }
        }
    }
}
