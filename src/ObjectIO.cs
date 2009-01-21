using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Othello
{
    public class ObjectIO
    {
        private readonly Stream r_Stream;

        public ObjectIO(Stream i_Stream)
        {
            r_Stream = i_Stream;
        }

        public void Write(object i_ObjToWrite)
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(r_Stream, i_ObjToWrite); 
        }

        public object Read()
        {
            var binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(r_Stream);
        }
    }
}