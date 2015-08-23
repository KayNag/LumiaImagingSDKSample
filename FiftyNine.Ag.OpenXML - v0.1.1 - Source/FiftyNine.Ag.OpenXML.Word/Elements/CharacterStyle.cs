using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.Packaging;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class CharacterStyle : Style
    {
        internal CharacterStyle(PackagePart part) : base(part)
        {
            RunProperties = Part.CreateElement<RunProperties>();
        }

        protected override void SaveProperties(System.Xml.XmlWriter writer)
        {
            RunProperties.Save(writer);
        }

        public override string Type
        {
            get { return "character"; }
        }
        public RunProperties RunProperties
        {
            get;
            private set;
        }
    }
}
