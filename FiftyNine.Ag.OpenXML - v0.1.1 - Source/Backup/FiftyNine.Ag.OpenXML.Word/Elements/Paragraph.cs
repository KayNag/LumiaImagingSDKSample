using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common;
using FiftyNine.Ag.OpenXML.Common.Extensions;
using System.Collections.Generic;
using System.Xml;
using FiftyNine.Ag.OpenXML.Word.BaseClasses;

namespace FiftyNine.Ag.OpenXML.Word.Elements
{
    public class Paragraph : WordContainerElement<Run>
    {
        protected override void Initialize()
        {
            Properties = Part.CreateElement<ParagraphProperties>();
        }

        public void Save(XmlWriter writer, Section prevSection)
        {
            if (prevSection == null)
            {
                Save(writer);
                return;
            }

            writer.WritePrefixedStartElement(Namespace, NodeName);
            prevSection.WriteSection(writer);
            if (Properties != null)
            {
                Properties.Save(writer);
            }
            WriteRuns(writer);
            writer.WriteEndElement();
        }
        protected override void SaveContent(XmlWriter writer)
        {
            WriteRuns(writer);
        }
        private void WriteRuns(XmlWriter writer)
        {
            foreach (var r in Runs)
            {
                r.Save(writer);
            }
        }

        protected override string NodeName
        {
            get { return "p"; }
        }
        public List<Run> Runs
        {
            get { return Items; }
        }
        public ParagraphProperties Properties
        {
            get;
            set;
        }
    }
}
