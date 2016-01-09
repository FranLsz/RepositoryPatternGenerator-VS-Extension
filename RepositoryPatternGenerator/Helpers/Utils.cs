using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace RepositoryPatternGenerator.Helpers
{
    public class Utils
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string GetManifestAttribute(string attribute)
        {
            var doc = new XmlDocument();
            var path = Path.Combine(AssemblyDirectory, "extension.vsixmanifest");
            doc.Load(path);
            var metaData = doc.DocumentElement.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Metadata");
            var identity = metaData.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Identity");
            return identity.GetAttribute(attribute);
        }
    }
}
