using System.IO;

namespace Reverci
{
    public class FileIO
    {
        public static void SaveObjectToFile(string i_FullPath, object i_Object)
        {
            var stream = new FileStream(i_FullPath, FileMode.Create, FileAccess.Write);
            var objectIO = new ObjectIO(stream);
            objectIO.Write(i_Object);
            stream.Close();
        }

        public static object ReadObjectFrom(string i_FullPath)
        {
            try
            {
                var stream = new FileStream(i_FullPath, FileMode.Open, FileAccess.Read);
                var objectIO = new ObjectIO(stream);
                var obj = objectIO.Read();
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