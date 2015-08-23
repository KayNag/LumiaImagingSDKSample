using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class Tab : RunContent
    {
        protected override string NodeName
        {
            get { return "tab"; }
        }
    }
}
