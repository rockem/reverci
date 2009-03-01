using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Reverci
{
    public class FileIO
    {
        public static void SaveObjectToFile(string i_FullPath, object i_Object)
        {
            var stream = new FileStream(i_FullPath, FileMode.Create, FileAccess.Write);
            new BinaryFormatter().Serialize(stream, i_Object);
            stream.Close();
        }

        public static object ReadObjectFrom(string i_FullPath)
        {
            try
            {
                var stream = new FileStream(i_FullPath, FileMode.Open, FileAccess.Read);
                var obj = new BinaryFormatter().Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}