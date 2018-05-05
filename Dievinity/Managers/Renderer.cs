using Dievinity.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dievinity.Managers {
    public class Renderer {

        private static Renderer instance;
        public static Renderer Instance {
            get {
                if (instance == null) {
                    instance = new Renderer();
                }

                return instance;
            }
        }

        private List<Sprite> sprites;

        private Renderer() {
            sprites = new List<Sprite>();
        }

        public void Draw(Sprite sprite) {
            sprites.Add(sprite);
        }

        public void DrawAllSprites(SpriteBatch spriteBatch) {
            sprites.Sort();

            foreach (Sprite i in sprites) {
                i.Draw(spriteBatch);
            }

            sprites.Clear();
        }
    }
}
