using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class ParagraphProperties : WordElement
    {
        ParagraphSpacing _spacing;
        RunProperties _runProperties;
        Style _style;
        ParagraphJustification _alignment;

        public override void Save(XmlWriter writer)
        {
            if (HasValues())
            {
                base.Save(writer);
            }
        }
        protected override void SaveContent(XmlWriter writer)
        {
            if (RunProperties != null)
            {
                RunProperties.Save(writer);
            }
            if (Spacing != null)
            {
                Spacing.Save(writer);
            }
            if (Alignment != null)
            {
                Alignment.Save(writer);
            }
        }

        private bool HasValues()
        {
            return Spacing != null || Style != null || RunProperties != null;
        }

        protected override string NodeName
        {
            get { return "pPr"; }
        }
        public ParagraphSpacing Spacing
        {
            get
            {
                if (_spacing == null)
                {
                    _spacing = Part.CreateElement<ParagraphSpacing>();
                }
                return _spacing;
            }
        }
        public Style Style
        {
            get
            {
                return _style;
            }
            set
            {
                if (value != null)
                {
                    if (value.Type != "paragraph")
                        throw new ArgumentException("Only style of type \"paragraph\" can be used with runs");
                }
                _style = value;
            }
        }
        public RunProperties RunProperties
        {
            get
            {
                if (_runProperties == null)
                {
                    _runProperties = Part.CreateElement<RunProperties>();
                }
                return _runProperties;
            }
        }
        public ParagraphJustification Alignment
        {
            get
            {
                if (_alignment == null)
                {
                    _alignment = Part.CreateElement<ParagraphJustification>();
                }
                return _alignment;
            }
        }
    }
}
