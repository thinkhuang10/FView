using Newtonsoft.Json;
using System.IO;

namespace Util
{
    public class JSONHelper
    {
        public static void WriteFile(object content,string path)
        {
            var str = JsonConvert.SerializeObject(content);
            using var file = new StreamWriter(path, false);
            file.WriteLineAsync(str);
        }

        public static T ReadFile<T>(string path)
        {
            using var file = new StreamReader(path);
            var str = file.ReadLine();

            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
