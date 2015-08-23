using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Word.Elements;

namespace FiftyNine.Ag.OpenXML.Word.Utilities
{
    public class FontReference
    {
        internal FontReference(FontDefinition font)
        {
            Font = font;
        }

        internal FontDefinition Font
        {
            get;
            private set;
        }
        public string FontName
        {
            get { return Font.Name; }
        }
    }
}
