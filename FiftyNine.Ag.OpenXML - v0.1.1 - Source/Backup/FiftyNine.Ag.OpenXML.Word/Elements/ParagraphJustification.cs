using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;
using System.Xml;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class ParagraphJustification : WordElement
    {
        public override void Save(XmlWriter writer)
        {
            if (HasValue())
            {
                base.Save(writer);
            }
        }
        protected override void SaveContent(XmlWriter writer)
        {
            if (Alignment.HasValue)
            {
                writer.WritePrefixedAttributeString(Namespace, "val", GetJustificationAsString(Alignment.Value));
            }
        }

        private string GetJustificationAsString(Justification j)
        {
            switch (j)
            {
                case Justification.Center:
                    return "center";
                case Justification.Right:
                    return "right";
                case Justification.Justified:
                    return "both";
                default:
                    return "left";
            }
        }
        private bool HasValue()
        {
            return Alignment.HasValue;
        }

        protected override string NodeName
        {
            get { return "jc"; }
        }
        public Justification? Alignment
        {
            get;
            set;
        }
    }

    public enum Justification
    {
        Left,
        Center,
        Right,
        Justified
    }
}
