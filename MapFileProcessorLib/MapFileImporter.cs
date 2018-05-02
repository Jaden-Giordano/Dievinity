using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace MapFileProcessorLib {
    [ContentImporter(".map", DisplayName = "Map Importer", DefaultProcessor = "Map Processor")]
    public class MapFileImporter : ContentImporter<MapFileSource> {

        public override MapFileSource Import(string filename, ContentImporterContext context) {
            string mapContent = File.ReadAllText(filename);
            return new MapFileSource(mapContent);
        }
    }
}
