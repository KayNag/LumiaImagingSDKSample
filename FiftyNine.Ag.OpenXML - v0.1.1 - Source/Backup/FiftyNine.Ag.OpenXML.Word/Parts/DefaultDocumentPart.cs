using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Word.Constants;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using FiftyNine.Ag.OpenXML.Word.Utilities;
using System.Linq;
using FiftyNine.Ag.OpenXML.Word.Elements;

namespace FiftyNine.Ag.OpenXML.Word.Parts
{
    public class DefaultDocumentPart : DocumentPart
    {
        protected override string ContentType
        {
            get
            {
                return ContentTypes.MainDocument;
            }
        }
    }
}
