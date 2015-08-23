using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using FiftyNine.Ag.OpenXML.Common.Parts;
using FiftyNine.Ag.OpenXML.Word.Parts;

namespace FiftyNine.Ag.OpenXML.Word
{
    public class WordDocument : Package
    {
        public WordDocument()
        {
            AppPart = this.CreatePart<AppPart>("/docProps/app.xml");
            CorePart = this.CreatePart<CorePart>("/docProps/core.xml");
            Document = this.CreatePart<DefaultDocumentPart>("/word/document.xml");

            Styles = this.CreatePart<StylesPart>("/word/style.xml");
            Document.AddRelation(Styles);

            FontTable = this.CreatePart<FontTablePart>("/word/fontTable.xml");
            Document.AddRelation(FontTable);
            
            AddRelationship(AppPart);
            AddRelationship(CorePart);
            AddRelationship(Document);
        }

        public DocumentPart Document
        {
            get;
            private set;
        }
        private AppPart AppPart
        {
            get;
            set;
        }
        private CorePart CorePart
        {
            get;
            set;
        }
        public StylesPart Styles
        {
            get;
            private set;
        }
        public FontTablePart FontTable
        {
            get;
            private set;
        }
        public string ApplicationName
        {
            get { return AppPart.Application; }
            set { AppPart.Application = value; }
        }
        public string Company
        {
            get { return AppPart.Company; }
            set { AppPart.Company = value; }
        }
        public string Creator
        {
            get { return CorePart.Creator; }
            set { CorePart.Creator = value; }
        }
        public DateTime Created
        {
            get { return CorePart.Created; }
            set { CorePart.Created = value; }
        }
    }
}
