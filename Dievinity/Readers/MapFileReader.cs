using Microsoft.Xna.Framework.Content;
using MapFileProcessorLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Dievinity.Readers {
    class MapFileReader : ContentTypeReader<MapFile> {
        protected override MapFile Read(ContentReader input, MapFile existingInstance) {
            int mapFileSize = input.ReadInt32();
            byte[] bytes = input.ReadBytes(mapFileSize);

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                MapFile mapFile = (MapFile)bf.Deserialize(ms);

                return mapFile;
            }
        }
    }
}
