using System;
using System.Collections.Generic;
using Dievinity.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dievinity.Managers {
    public class SceneManger {

        private static SceneManger instance;
        public static SceneManger Instance {
            get {
                if (instance == null) {
                    instance = new SceneManger();
                }

                return instance;
            }
        }

        private Dictionary<string, Scene> scenes;
        private string currentScene;

        private SceneManger() {
            scenes = new Dictionary<string, Scene>();
        }

        public void AddScene(string name, Scene scene) {
            scenes.Add(name, scene);
        }

        public void SetCurrent(string name) {
            if (scenes.ContainsKey(name)) {
                currentScene = name;
            } else {
                throw new KeyNotFoundException("Scene name must be added.");
            }
        }

        public Scene GetCurrent() {
            if (currentScene != null) {
                Scene current;

                scenes.TryGetValue(currentScene, out current);

                if (current != null) {
                    return current;
                }
            }

            return null;
        }

        public void Begin() {
            GetCurrent().Begin();
        }

        public void Input(GameTime gameTime) {
            GetCurrent().Input(gameTime);
        }

        public void Update(GameTime gameTime) {
            GetCurrent().Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            GetCurrent().Draw(spriteBatch);
        }

    }
}
