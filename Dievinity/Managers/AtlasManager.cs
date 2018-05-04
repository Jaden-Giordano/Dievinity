using Dievinity.Utilities;
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

        private Dictionary<string, Atlas> atlasDictionary;

        private AtlasManager() {
            atlasDictionary = new Dictionary<string, Atlas>();
        }

        public Atlas GetAtlas(string atlas) {
            Atlas atlasInfo;

            atlasDictionary.TryGetValue(atlas, out atlasInfo);

            return atlasInfo;
        }

        public void SaveAtlas(string atlasName, Atlas atlas) {
            atlasDictionary.Add(atlasName, atlas);
        }
    }
}
