using System;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Util
{
    public class XmlManager<T>
    {
        public Type Type { get; set; }

        public XmlManager()
        {
            Type = typeof(T);
        }

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                var xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                var xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
