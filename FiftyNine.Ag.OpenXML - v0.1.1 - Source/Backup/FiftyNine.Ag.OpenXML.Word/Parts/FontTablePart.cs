using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Collections.Generic;
using FiftyNine.Ag.OpenXML.Word.Elements;
using System.Linq;
using FiftyNine.Ag.OpenXML.Word.Utilities;

namespace FiftyNine.Ag.OpenXML.Word.Parts
{
    public class FontTablePart : PackagePart
    {
        private List<FontDefinition> _fonts = new List<FontDefinition>();

        protected override void SaveContent(System.Xml.XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Constants.Namespaces.Main, "fonts");
            foreach (var font in _fonts)
            {
                font.Save(writer);
            }
            writer.WriteEndElement();
        }

        public FontDefinition CreateFontDefinition(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");

            FontDefinition def = new FontDefinition(name);
            return def;
        }
        public FontReference AddFont(FontDefinition font)
        {
            if (_fonts.FirstOrDefault(f => f.Name == font.Name) != null)
            {
                throw new InvalidOperationException("FontTable already contains a font named " + font.Name);
            }
            _fonts.Add(font);
            return new FontReference(font);
        }
        public void RemoveFont(FontReference font)
        {
            _fonts.Remove(font.Font);
        }

        protected override string ContentType
        {
            get { return Constants.ContentTypes.FontTable; }
        }
        protected override string RelationshipType
        {
            get { return Constants.RelationshipTypes.FontTable; }
        }
        public IEnumerable<FontDefinition> Fonts
        {
            get { return _fonts.AsEnumerable(); }
        }
    }
}
