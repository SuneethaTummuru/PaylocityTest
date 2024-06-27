using Newtonsoft.Json.Schema;
using System.IO;

namespace API.AutomationChallenge.Domain.Utils
{
    internal static class Helper
    {
        public static JSchema GetSchema(string schemaFileName)
        {
            string path = "./Core/Schemas/" + schemaFileName;
            return JSchema.Parse(File.ReadAllText(@path));
        }
    }
}
