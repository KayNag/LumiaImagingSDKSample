using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Word.Constants;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using FiftyNine.Ag.OpenXML.Word.Utilities;
using System.Linq;
using FiftyNine.Ag.OpenXML.Word.Elements;

namespace FiftyNine.Ag.OpenXML.Word.Parts
{
    public class DocumentPart : PackagePart
    {
        protected override void Initialize()
        {
            Sections = new SectionCollection();
            Sections.Add(this.CreateElement<Section>());
        }

        protected override void SaveContent(System.Xml.XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "document");
            WriteRequiredNamespaces(writer);

            writer.WritePrefixedStartElement(Constants.Namespaces.Main,  "body");
            foreach (var section in Sections)
            {
                section.Save(writer);
            }
            Sections.Last().WriteSection(writer);

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        internal void AddRelation(PackagePart part)
        {
            AddRelationship(part);
        }
        internal void AddRelation(string target, string type, bool isExternal)
        {
            AddRelationship(target, type, isExternal);
        }

        protected override string ContentType
        {
            get 
            {
                return ContentTypes.Document;
            }
        }
        protected override string RelationshipType
        {
            get { return RelationshipTypes.Document; }
        }
        public SectionCollection Sections
        {
            get;
            private set;
        }
    }
}
