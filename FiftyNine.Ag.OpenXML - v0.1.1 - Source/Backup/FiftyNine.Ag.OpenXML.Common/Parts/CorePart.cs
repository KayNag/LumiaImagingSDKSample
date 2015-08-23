using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Constants;
using System.Xml;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using Constants = FiftyNine.Ag.OpenXML.Common.Constants;

namespace FiftyNine.Ag.OpenXML.Common.Parts
{
    public class CorePart : PackagePart
    {
        protected internal override void Initialize()
        {
            Created = DateTime.Now;
            AddRequiredNamespace(Constants.Namespaces.Purl.Dc);
            AddRequiredNamespace(Constants.Namespaces.Purl.DcTerms);
            AddRequiredNamespace(Constants.Namespaces.Purl.DcmiType);
            AddRequiredNamespace(Constants.Namespaces.W3C.Xsi);
        }

        protected override void SaveContent(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Constants.Namespaces.CoreProps, "coreProperties");
            WriteRequiredNamespaces(writer);

            writer.WritePrefixedStartElement(Constants.Namespaces.Purl.Dc, "creator");
            writer.WriteValue(Creator);
            writer.WriteEndElement();

            writer.WritePrefixedStartElement(Constants.Namespaces.Purl.DcTerms, "created");
            writer.WritePrefixedAttributeString(Constants.Namespaces.W3C.Xsi, "type", "dcterms:W3CDTF");
            writer.WriteValue(Created.ToXmlString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        protected internal override string ContentType
        {
            get { return ContentTypes.CoreProps; }
        }
        protected internal override string RelationshipType
        {
            get { return RelationshipTypes.CoreProp; }
        }

        public string Creator
        {
            get;
            set;
        }
        public DateTime Created
        {
            get;
            set;
        }
    }
}
