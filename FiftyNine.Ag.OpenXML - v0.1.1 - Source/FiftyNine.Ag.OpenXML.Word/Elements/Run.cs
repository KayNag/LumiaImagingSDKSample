using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using System.Collections.Generic;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class Run : WordContainerElement<RunContent>
    {
        protected override void Initialize()
        {
            Properties = Part.CreateElement<RunProperties>();
        }

        protected override void SaveContent(XmlWriter writer)
        {
            if (Properties != null)
            {
                Properties.Save(writer);
            }
            foreach (var r in Content)
            {
                r.Save(writer);
            }
        }

        protected override string NodeName
        {
            get { return "r"; }
        }
        public List<RunContent> Content
        {
            get { return Items; }
        }
        public RunProperties Properties
        {
            get;
            set;
        }
    }
}
