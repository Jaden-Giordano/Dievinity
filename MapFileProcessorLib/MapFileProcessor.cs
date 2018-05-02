using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace MapFileProcessorLib {
    [ContentProcessor(DisplayName = "Map Processor")]
    public class MapFileProcessor : ContentProcessor<MapFileSource, MapFile> {

        public override MapFile Process(MapFileSource input, ContentProcessorContext context) {
            return JsonConvert.DeserializeObject<MapFile>(input.MapSource);
        }

    }
}
