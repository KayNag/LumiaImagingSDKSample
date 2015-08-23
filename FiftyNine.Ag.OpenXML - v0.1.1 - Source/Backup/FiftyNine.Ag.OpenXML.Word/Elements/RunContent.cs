using System;
using System.Net;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using System.Xml;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public abstract class RunContent : WordElement
    {
        protected override Namespace Namespace
        {
            get { return Constants.Namespaces.Main; }
        }
    }
    public abstract class RunContent<T> : RunContent
    {
        protected override void SaveContent(XmlWriter writer)
        {
            writer.WriteValue(Content);
        }

        public T Content
        {
            get;
            set;
        }
    }
}
