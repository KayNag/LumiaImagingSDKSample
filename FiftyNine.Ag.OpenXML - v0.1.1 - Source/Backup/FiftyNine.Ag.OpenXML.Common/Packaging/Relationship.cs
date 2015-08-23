using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;

namespace FiftyNine.Ag.OpenXML.Common.Packaging
{
    public class Relationship
    {
        internal Relationship(PackagePart part, int relId)
            : this(part.Path, part.RelationshipType, false, relId)
        {
        }
        internal Relationship(string target, string type, bool isExternal, int relId)
        {
            ID = "rId" + relId;
            Target = target;
            RelationshipType = type;
            IsExternal = isExternal;
        }

        protected internal void Save(XmlWriter writer, PackagePart relativeTo)
        {
            writer.WriteStartElement("Relationship");
            writer.WriteAttributeString("Id", ID);
            writer.WriteAttributeString("Type", RelationshipType);
            string target = Target;
            if (relativeTo != null)
            {
                target = GetRelativePath(target, relativeTo.Path);
            }
            writer.WriteAttributeString("Target", target.TrimStart('/'));
            if (IsExternal)
            {
                writer.WriteAttributeString("TargetMode", "External");
            }
            writer.WriteEndElement();
        }

        private string GetRelativePath(string path, string relativeTo)
        {
            string[] pathParts = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] relativeToParts = relativeTo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            int i;
            for (i = 0; i < pathParts.Length; i++)
            {
                if (!pathParts[i].Equals(relativeToParts[i], StringComparison.OrdinalIgnoreCase))
                    break;
            }

            StringBuilder ret = new StringBuilder();
            for (int z = i; z < relativeToParts.Length - 1; z++)
            {
                ret.Append("../");
            }

            for (int z = i; z < pathParts.Length - 1; z++)
            {
                ret.AppendFormat("{0}/", pathParts[z]);
            }
            ret.Append(pathParts[pathParts.Length - 1]);
            return ret.ToString();
        }

        public string ID
        {
            get;
            private set;
        }
        public string RelationshipType
        {
            get;
            private set;
        }
        public string Target
        {
            get;
            private set;
        }
        public bool IsExternal
        {
            get;
            private set;
        }
    }

}
