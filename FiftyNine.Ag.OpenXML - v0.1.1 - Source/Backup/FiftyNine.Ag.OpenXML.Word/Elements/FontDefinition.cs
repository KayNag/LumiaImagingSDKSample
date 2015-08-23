using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;
using FiftyNine.Ag.OpenXML.Common.Packaging;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class FontDefinition
    {
        FontSignature _signature;

        internal FontDefinition(string name)
        {
            Name = name;
        }

        protected internal virtual void Save(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Namespace, "font");
            SaveContent(writer);
            writer.WriteEndElement();
        }
        protected virtual void SaveContent(XmlWriter writer)
        {
            writer.WritePrefixedAttributeString(Namespace, "name", Name);
            if (!string.IsNullOrEmpty(Panose1))
            {
                writer.WritePrefixedStartElement(Namespace, "panose1");
                writer.WritePrefixedAttributeString(Namespace, "val", Panose1);
                writer.WriteEndElement();
            }
            if (!string.IsNullOrEmpty(CharSet))
            {
                writer.WritePrefixedStartElement(Namespace, "charset");
                writer.WritePrefixedAttributeString(Namespace, "val", CharSet);
                writer.WriteEndElement();
            }

            writer.WritePrefixedStartElement(Namespace, "family");
            writer.WritePrefixedAttributeString(Namespace, "val", Family.ToString().ToLower());
            writer.WriteEndElement();

            if (Pitch != FontPitchEnumeration.Default)
            {
                writer.WritePrefixedStartElement(Namespace, "pitch");
                writer.WritePrefixedAttributeString(Namespace, "val", Pitch.ToString().ToLower());
                writer.WriteEndElement();
            }
            if (IsNotTrueType.HasValue && IsNotTrueType.Value != false)
            {
                writer.WritePrefixedStartElement(Namespace, "notTrueType");
                writer.WriteEndElement();
            }
            if (Signature != null)
            {
                Signature.Save(writer);
            }
        }

        private Namespace Namespace
        {
            get { return Constants.Namespaces.Main; }
        }
        public string Name
        {
            get;
            private set;
        }
        public string Panose1
        {
            get;
            set;
        }
        public string CharSet
        {
            get;
            set;
        }
        public FontFamilyEnumeration Family
        {
            get;
            set;
        }
        public FontPitchEnumeration Pitch
        {
            get;
            set;
        }
        public bool? IsNotTrueType
        {
            get;
            set;
        }
        public FontSignature Signature
        {
            get
            {
                if (_signature == null)
                {
                    _signature = new FontSignature();
                }
                return _signature;
            }
        }
    }
    public enum FontPitchEnumeration
    {
        Default,
        Fixed,
        Variable
    }
    public enum FontFamilyEnumeration
    {
        Auto,
        Decorative,
        Modern,
        Roman,
        Script,
        Swiss
    }
}
