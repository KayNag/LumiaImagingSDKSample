using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Word.Elements;
using FiftyNine.Ag.OpenXML.Word.Constants;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FiftyNine.Ag.OpenXML.Word.Parts
{
    public class StylesPart : PackagePart
    {
        protected override void Initialize()
        {
            DefaultRunProperties = CreateElement<RunProperties>();
            DefaultRunProperties.ComplexScriptFontSize = 22;
            DefaultRunProperties.FontSize = 22;
            DefaultParagraphProperties = CreateElement<ParagraphProperties>();
            DefaultParagraphProperties.Spacing.After = 200;
            DefaultParagraphProperties.Spacing.Line = 276;
            DefaultParagraphProperties.Spacing.LineRule = LineSpacingRule.Auto;
            Styles = new List<Style>();

            ParagraphStyle defaultStyle = new ParagraphStyle(this);
            SetStyleProperties(defaultStyle, "Default", "Default Style", true);
            DefaultStyle = defaultStyle;
        }
        protected override void SaveContent(System.Xml.XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "styles");
            WriteRequiredNamespaces(writer);

            #region Defaults
            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "docDefaults");

            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "rPrDefaults");
            DefaultRunProperties.Save(writer);
            writer.WriteEndElement();

            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "pPrDefaults");
            DefaultParagraphProperties.Save(writer);
            writer.WriteEndElement();

            writer.WriteEndElement();
            #endregion

            DefaultStyle.Save(writer);
            foreach (var style in Styles)
            {
                style.Save(writer);
            }

            writer.WriteEndElement();
        }

        public CharacterStyle AddCharacterStyle(string id, string name)
        {
            CharacterStyle style = new CharacterStyle(this);
            SetStyleProperties(style, id, name, false);
            Styles.Add(style);
            return style;
        }
        public ParagraphStyle AddParagraphStyle(string id, string name)
        {
            ParagraphStyle style = new ParagraphStyle(this);
            SetStyleProperties(style, id, name, false);
            Styles.Add(style);
            return style;
        }
        private void SetStyleProperties(Style style, string id, string name, bool isDefault)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id cannot be empty");
            if (id.Contains(" "))
                throw new ArgumentException("id cannot contain spaces");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");
            if (Styles.FirstOrDefault(s => s.Id == id) != null)
                throw new InvalidOperationException("There is already a style defined with the id " + id);

            style.Id = id;
            style.Name = name;
            style.IsDefault = isDefault;
        }

        protected override string ContentType
        {
            get { return ContentTypes.Styles; }
        }
        protected override string RelationshipType
        {
            get { return RelationshipTypes.Styles; }
        }
        public RunProperties DefaultRunProperties
        {
            get;
            private set;
        }
        public ParagraphProperties DefaultParagraphProperties
        {
            get;
            private set;
        }
        public ParagraphStyle DefaultStyle
        {
            get;
            private set;
        }
        private List<Style> Styles
        {
            get;
            set;
        }
    }
}
