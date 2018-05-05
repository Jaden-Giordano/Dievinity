using Dievinity.Entities;
using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Dievinity.Scenes {
    public class TurnBasedScene : Scene {

        private int currentTurn;
        private TurnBasedEntity current;

        public TurnBasedScene() : base() {
            currentTurn = 0;
        }

        public override void Update(GameTime gameTime) {
            if (current != null) {
                if (current.TurnFinished) {
                    TurnBasedEntity[] turnBasedEntities = GetTurnBasedEntities();

                    currentTurn = (currentTurn + 1) % turnBasedEntities.Length;
                    current = turnBasedEntities[currentTurn] as TurnBasedEntity;
                    current.TurnFinished = false;
                    current.Stats.ActionPoints = current.Stats.MaxActionPoints;
                } else {
                    current.TurnUpdate(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);

            foreach (Entity i in entities) {
                i.Draw(spriteBatch);
            }
        }

        public override void AddEntities(Entity[] entities) {
            base.AddEntities(entities);

            this.entities.Sort();

            current = this.entities[0] as TurnBasedEntity;
        }

        public TurnBasedEntity[] GetTurnBasedEntities() {
            return (from a in entities where a is TurnBasedEntity select a as TurnBasedEntity).ToArray();
        }
    }
}
