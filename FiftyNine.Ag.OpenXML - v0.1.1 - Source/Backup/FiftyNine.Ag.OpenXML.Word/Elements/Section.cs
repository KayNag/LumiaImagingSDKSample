using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Xml;
using System.Collections.Generic;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class Section : WordContainerElement<Paragraph>
    {
        public static readonly Size A4 = new Size(12240, 15840);

        protected override void Initialize()
        {
            Size = A4;
            Margins = new Thickness(1440);
            HeaderSpace = 708;
            FooterSpace = 708;
            GutterSpace = 0;
            Paragraphs.Add(Part.CreateElement<Paragraph>());
        }

        public override void Save(System.Xml.XmlWriter writer)
        {
            foreach (var p in Items)
            {
                p.Save(writer, PreviousSection);
            }
        }
        internal void WriteSection(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Namespace,NodeName);

            writer.WritePrefixedStartElement(Namespace, "pgSz");
            writer.WritePrefixedAttributeString(Namespace, "w", Size.Width.ToString());
            writer.WritePrefixedAttributeString(Namespace, "h", Size.Height.ToString());
            writer.WriteEndElement();

            writer.WritePrefixedStartElement(Namespace, "pgMar");
            writer.WritePrefixedAttributeString(Namespace, "top", Margins.Top.ToString());
            writer.WritePrefixedAttributeString(Namespace, "right", Margins.Right.ToString());
            writer.WritePrefixedAttributeString(Namespace, "bottom", Margins.Bottom.ToString());
            writer.WritePrefixedAttributeString(Namespace, "left", Margins.Left.ToString());
            writer.WritePrefixedAttributeString(Namespace, "header", HeaderSpace.ToString());
            writer.WritePrefixedAttributeString(Namespace, "footer", FooterSpace.ToString());
            writer.WritePrefixedAttributeString(Namespace, "gutter", GutterSpace.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        internal Section PreviousSection
        {
            get;
            set;
        }
        protected override string NodeName
        {
            get { return "sectPr"; }
        }
        public Size Size
        {
            get;
            set;
        }
        public Thickness Margins
        {
            get;
            set;
        }
        public int HeaderSpace
        {
            get;
            set;
        }
        public int FooterSpace
        {
            get;
            set;
        }
        public int GutterSpace
        {
            get;
            set;
        }
        public List<Paragraph> Paragraphs
        {
            get { return Items; }
        }
    }
}
