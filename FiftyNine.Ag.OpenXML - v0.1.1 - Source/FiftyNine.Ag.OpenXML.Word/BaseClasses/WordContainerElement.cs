using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;

namespace FiftyNine.Ag.OpenXML.Word.BaseClasses
{
    public abstract class WordContainerElement<T> : OpenXMLContainerElement<T> where T : OpenXMLElement
    {
        protected override Namespace Namespace
        {
            get { return Constants.Namespaces.Main; }
        }
    }
}
