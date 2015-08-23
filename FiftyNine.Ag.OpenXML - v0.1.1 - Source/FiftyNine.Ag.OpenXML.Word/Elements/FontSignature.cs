using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class FontSignature
    {
        internal FontSignature()
        {
        }

        protected internal virtual void Save(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Namespace, "sig");
            SaveContent(writer);
            writer.WriteEndElement();
        }
        protected virtual void SaveContent(XmlWriter writer)
        {
            WriteSignature("usb0", UnicodeSignature0, writer);
            WriteSignature("usb1", UnicodeSignature1, writer);
            WriteSignature("usb2", UnicodeSignature2, writer);
            WriteSignature("usb3", UnicodeSignature3, writer);
            WriteSignature("csb0", CodePageSignature0, writer);
            WriteSignature("csb1", CodePageSignature1, writer);
        }
        private void WriteSignature(string attrName, string signature, XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(signature))
            {
                writer.WritePrefixedAttributeString(Namespace, attrName, signature);
                return;
            }
            writer.WritePrefixedAttributeString(Namespace, attrName, "00000000");
        }

        private Namespace Namespace
        {
            get { return Constants.Namespaces.Main; }
        }
        public string CodePageSignature0
        {
            get;
            set;
        }
        public string CodePageSignature1
        {
            get;
            set;
        }
        public string UnicodeSignature0
        {
            get;
            set;
        }
        public string UnicodeSignature1
        {
            get;
            set;
        }
        public string UnicodeSignature2
        {
            get;
            set;
        }
        public string UnicodeSignature3
        {
            get;
            set;
        }
    }
}
