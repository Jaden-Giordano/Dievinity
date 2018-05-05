using Dievinity.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Dievinity.Managers {
    public class AtlasManager {

        private static AtlasManager instance;

        public static AtlasManager Instance {
            get {
                if (instance == null) {
                    instance = new AtlasManager();
                }

                return instance;
            }
        }

        private Dictionary<string, Texture2D> atlasDictionary;

        private AtlasManager() {
            atlasDictionary = new Dictionary<string, Texture2D>();
        }

        public Texture2D GetAtlas(string atlas) {
            Texture2D atlasInfo;

            atlasDictionary.TryGetValue(atlas, out atlasInfo);

            return atlasInfo;
        }

        public void SaveAtlas(string atlasName, Texture2D atlas) {
            atlasDictionary.Add(atlasName, atlas);
        }

        public Atlas CreateAtlas(string atlas, int id, Vector2 position, float scale = 1, int order = 0) {
            Texture2D atlasTexture = GetAtlas(atlas);

            if (atlasTexture != null) {
                return new Atlas(atlasTexture, position, 16, id, scale, order);
            }

            return null;
        }
    }
}
