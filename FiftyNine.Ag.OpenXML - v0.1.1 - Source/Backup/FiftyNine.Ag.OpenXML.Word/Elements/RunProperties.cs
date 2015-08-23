using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class RunProperties : WordElement
    {
        RunFonts _font;

        public override void Save(System.Xml.XmlWriter writer)
        {
            if (HasValues())
            {
                base.Save(writer);
            }
        }
        protected override void SaveContent(System.Xml.XmlWriter writer)
        {
            if (IsBold)
            {
                writer.WritePrefixedStartElement(Namespace, "b");
                writer.WriteEndElement();
            }
            if (IsItalic)
            {
                writer.WritePrefixedStartElement(Namespace, "i");
                writer.WriteEndElement();
            }
            if (FontSize.HasValue)
            {
                writer.WritePrefixedStartElement(Namespace, "sz");
                writer.WritePrefixedAttributeString(Namespace, "val", FontSize.Value.ToString());
                writer.WriteEndElement();
            }
            if (ComplexScriptFontSize.HasValue)
            {
                writer.WritePrefixedStartElement(Namespace, "szCs");
                writer.WritePrefixedAttributeString(Namespace, "val", ComplexScriptFontSize.Value.ToString());
                writer.WriteEndElement();
            }
            if (Font != null)
            {
                Font.Save(writer);
            }
            if (Style != null)
            {
                writer.WritePrefixedStartElement(Namespace, "rStyle");
                writer.WritePrefixedAttributeString(Namespace, "val", Style.Id);
                writer.WriteEndElement();
            }
        }

        private bool HasValues()
        {
            return IsBold || IsItalic || FontSize.HasValue ||
                ComplexScriptFontSize.HasValue || Font != null;
        }

        protected override string NodeName
        {
            get { return "rPr"; }
        }
        public bool IsBold
        {
            get;
            set;
        }
        public bool IsItalic
        {
            get;
            set;
        }
        public int? FontSize
        {
            get;
            set;
        }
        public int? ComplexScriptFontSize
        {
            get;
            set;
        }
        public RunFonts Font
        {
            get
            {
                if (_font == null)
                {
                    _font = Part.CreateElement<RunFonts>();
                }
                return _font;
            }
        }
        public Style Style
        {
            get;
            set;
        }
    }
}
