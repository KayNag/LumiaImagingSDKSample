using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.Parts;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;
using FiftyNine.Ag.OpenXML.Common.Packaging;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public abstract class Style
    {
        internal Style(PackagePart part)
        {
            Part = part;
            PrimaryStyle = true;
            UiPriority = 99;
        }

        public virtual void Save(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Namespace, "style");
            SaveContent(writer);
            writer.WriteEndElement();
        }
        protected virtual void SaveContent(XmlWriter writer)
        {
            writer.WritePrefixedAttributeString(Namespace, "type", Type);
            if (IsDefault)
            {
                writer.WritePrefixedAttributeString(Namespace, "default", "1");
            }
            writer.WritePrefixedAttributeString(Namespace, "styleId", Id);

            writer.WritePrefixedStartElement(Namespace, "name");
            writer.WritePrefixedAttributeString(Namespace, "val", Name);
            writer.WriteEndElement();

            if (BasedOn != null)
            {
                writer.WritePrefixedStartElement(Namespace, "basedOn");
                writer.WritePrefixedAttributeString(Namespace, "val", BasedOn.Id);
                writer.WriteEndElement();
            }

            if (Link != null)
            {
                writer.WritePrefixedStartElement(Namespace, "link");
                writer.WritePrefixedAttributeString(Namespace, "val", Link.Id);
                writer.WriteEndElement();
            }

            if (Next != null)
            {
                writer.WritePrefixedStartElement(Namespace, "next");
                writer.WritePrefixedAttributeString(Namespace, "val", Next.Id);
                writer.WriteEndElement();
            }

            writer.WritePrefixedStartElement(Namespace, "uiPriority");
            writer.WritePrefixedAttributeString(Namespace, "val", UiPriority.ToString());
            writer.WriteEndElement();

            if (SemiHidden)
            {
                writer.WritePrefixedStartElement(Namespace, "semiHidden");
                writer.WriteEndElement();
            }

            if (PrimaryStyle)
            {
                writer.WritePrefixedStartElement(Namespace, "qFormat");
                writer.WriteEndElement();
            }

            SaveProperties(writer);
        }
        protected abstract void SaveProperties(XmlWriter writer);

        protected PackagePart Part
        {
            get;
            private set;
        }
        private Namespace Namespace
        {
            get { return Constants.Namespaces.Main; }
        }
        internal bool IsDefault
        {
            get;
            set;
        }
        public string Id
        {
            get;
            internal set;
        }
        public string Name
        {
            get;
            internal set;
        }
        public abstract string Type { get; }
        public Style BasedOn
        {
            get;
            set;
        }
        public bool PrimaryStyle
        {
            get;
            set;
        }
        public bool SemiHidden
        {
            get;
            set;
        }
        public bool UnhideWhenUsed
        {
            get;
            set;
        }
        public Style Next
        {
            get;
            set;
        }
        public Style Link
        {
            get;
            set;
        }
        public int UiPriority
        {
            get;
            set;
        }
    }
}
