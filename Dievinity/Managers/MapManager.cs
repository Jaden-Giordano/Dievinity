using System.Collections.Generic;
using MapFileProcessorLib;
using Dievinity.Utilities;
using Dievinity.Maps;

namespace Dievinity.Managers {
    class MapManager {

        private static MapManager instance;

        public static MapManager Instance {
            get {
                if (instance == null) {
                    instance = new MapManager();
                }

                return instance;
            }
        }

        private Dictionary<string, Map> maps;

        private MapManager() {
            maps = new Dictionary<string, Map>();
        }

        public void SaveMap(string mapName, MapFile mapFile) {
            Map map = new Map(mapFile.width, mapFile.height);

            foreach (MapFileProcessorLib.Serialization.Tile tile in mapFile.tiles) {
                Vector2i position = new Vector2i(tile.position.x, tile.position.y);
                Tile newTile = new Tile(AtlasManager.Instance.GetAtlas(tile.textureAtlas), tile.id, position, tile.blocked);
                map.SetTile(position, newTile);
            }

            maps.Add(mapName, map);
        }

        public Map GetMap(string mapName) {
            Map map;

            maps.TryGetValue(mapName, out map);

            return map;
        }
    }
}
