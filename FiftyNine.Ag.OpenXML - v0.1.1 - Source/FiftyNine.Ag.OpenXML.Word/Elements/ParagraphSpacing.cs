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
    public class ParagraphSpacing : WordElement
    {
        public override void Save(XmlWriter writer)
        {
            if (HasValues())
            {
                base.Save(writer);
            }
        }
        protected override void SaveContent(XmlWriter writer)
        {
            if (Before.HasValue)
            {
                writer.WritePrefixedAttributeString(Namespace,"before", Before.Value.ToString());
            }
            if (After.HasValue)
            {
                writer.WritePrefixedAttributeString(Namespace, "after", After.Value.ToString());
            }
            if (Line.HasValue)
            {
                writer.WritePrefixedAttributeString(Namespace, "line", Line.Value.ToString());
            }
            if (LineRule.HasValue)
            {
                writer.WritePrefixedAttributeString(Namespace, "lineRule", LineRule.Value.ToString());
            }
        }

        private bool HasValues()
        {
            return Before.HasValue || After.HasValue ||
                Line.HasValue || LineRule.HasValue;
        }

        protected override string NodeName
        {
            get { return "spacing"; }
        }
        public int? Before
        {
            get;
            set;
        }
        public int? After
        {
            get;
            set;
        }
        public int? Line
        {
            get;
            set;
        }
        public LineSpacingRule? LineRule
        {
            get;
            set;
        }
    }

    public enum LineSpacingRule
    {
        AtLeast,
        Auto,
        Exact
    }
}
