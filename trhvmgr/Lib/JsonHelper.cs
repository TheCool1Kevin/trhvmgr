using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace trhvmgr.Lib
{
    /// <summary>
    /// Class to wrap the JToken.ReadFrom and provides file locking.
    /// </summary>
    public class JsonHelper : IDisposable
    {
        private FileStream fs;
        private TextReader tr;
        
        public JObject JObject { get; private set; }

        public JsonHelper(string path, FileShare share)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.Read, share);
            tr = new StreamReader(fs);
            JObject = (JObject) JToken.ReadFrom(new JsonTextReader(tr));
        }

        public void Dispose()
        {
            fs.Close();
            fs.Dispose();
            tr.Close();
            tr.Dispose();
        }
    }
}
