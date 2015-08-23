using System;
using System.Net;
using System.Xml;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class Text : RunContent<string>
    {
        protected override void SaveContent(XmlWriter writer)
        {
            if (Content.EndsWith(" ") || Content.StartsWith(" "))
            {
                writer.WriteAttributeString("xml", "space", null, "preserve");
                writer.WriteString(Content);
            }
            else
            {
                base.SaveContent(writer);
            }
        }

        protected override string NodeName
        {
            get { return "t"; } 
        }
    }
}
