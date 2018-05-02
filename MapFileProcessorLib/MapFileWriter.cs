using System;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;

namespace MapFileProcessorLib {
    [ContentTypeWriter]
    public class MapFileWriter : ContentTypeWriter<MapFile> {

        protected override void Write(ContentWriter output, MapFile value) {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, value);

                byte[] bytes = ms.ToArray();

                output.Write(bytes.Length);
                output.Write(bytes);
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform) {
            return typeof(MapFile).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform) {
            return "Dievinity.Readers.MapFileReader, Dievinity, Version=1.0.0.0";
        }

    }
}
