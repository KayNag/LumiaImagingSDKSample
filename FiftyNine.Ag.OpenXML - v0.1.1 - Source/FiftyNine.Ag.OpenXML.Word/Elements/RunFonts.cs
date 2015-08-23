using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using FiftyNine.Ag.OpenXML.Word.Utilities;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class RunFonts : WordElement
    {
        public override void Save(System.Xml.XmlWriter writer)
        {
            if (HasValues())
            {
                base.Save(writer);
            }
        }
        protected override void SaveContent(System.Xml.XmlWriter writer)
        {
            if (ASCII != null)
            {
                writer.WritePrefixedAttributeString(Namespace, "ascii", ASCII.FontName);
            }
            if (HighAnsi != null)
            {
                writer.WritePrefixedAttributeString(Namespace, "hAnsi", HighAnsi.FontName);
            }
            if (ComplexScript != null)
            {
                writer.WritePrefixedAttributeString(Namespace, "cs", ComplexScript.FontName);
            }
            if (EastAsia != null)
            {
                writer.WritePrefixedAttributeString(Namespace, "eastAsia", EastAsia.FontName);
            }
        }

        private bool HasValues()
        {
            return ASCII != null || HighAnsi != null ||
                ComplexScript != null || EastAsia != null;
        }

        protected override string NodeName
        {
            get { return "rFonts"; }
        }
        public FontReference ASCII
        {
            get;
            set;
        }
        public FontReference HighAnsi
        {
            get;
            set;
        }
        public FontReference ComplexScript
        {
            get;
            set;
        }
        public FontReference EastAsia
        {
            get;
            set;
        }
    }
}
