using Dievinity.Entities;
using Dievinity.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dievinity.Scenes {
    public class TurnBasedScene : Scene {

        private int currentTurn;

        public TurnBasedScene(Map map) : base(map) {
            currentTurn = 0;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            Entity current = entities[currentTurn];

            if (current.turnFinished) {
                currentTurn = (currentTurn + 1) % entities.Count;
                current = entities[currentTurn];
                current.turnFinished = false;
                current.Stats.ActionPoints = current.Stats.MaxActionPoints;
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

        public override void AddEntities(Entity[] entities) {
            base.AddEntities(entities);

            this.entities.Sort();
        }
    }
}
