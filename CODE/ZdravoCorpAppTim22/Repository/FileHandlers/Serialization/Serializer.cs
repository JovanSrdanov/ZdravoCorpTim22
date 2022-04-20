using System.IO;
using System.Text.Json;

namespace ZdravoCorpAppTim22.Repository.FileHandlers.Serialization
{
    public class Serializer<T> : ISerializer<T> where T : class
    {
        public string dirPath = "../../Data/";
        public string path;
        public Serializer(string fileName)
        {
            path = dirPath + fileName;
        }
        public void Serialize(T obj)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(path, jsonString);
        }

        public T Deserialize()
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            return null;
        }
    }
}
