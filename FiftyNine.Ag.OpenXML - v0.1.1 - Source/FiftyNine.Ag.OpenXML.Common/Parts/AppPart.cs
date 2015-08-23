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
    public class AppPart : PackagePart
    {
        protected override void SaveContent(XmlWriter writer)
        {
            writer.WriteStartElement("Properties", Constants.Namespaces.ExtProps.Value);
            writer.WriteElementString("Application", Application);
            writer.WriteElementString("Company", Company);
            writer.WriteEndElement();
        }

        protected internal override string ContentType
        {
            get { return ContentTypes.ExtendedProps; }
        }
        protected internal override string RelationshipType
        {
            get { return RelationshipTypes.ExtProp; }
        }

        public string Application
        {
            get;
            set;
        }
        public string Company
        {
            get;
            set;
        }
    }
}
