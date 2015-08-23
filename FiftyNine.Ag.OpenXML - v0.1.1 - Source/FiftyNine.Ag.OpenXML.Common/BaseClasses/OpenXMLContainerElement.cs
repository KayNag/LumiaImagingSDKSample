using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using System.Xml;
using System.Collections.Generic;

namespace FiftyNine.Ag.OpenXML.Common.BaseClasses
{
    public abstract class OpenXMLContainerElement<T> : OpenXMLElement where T : OpenXMLElement
    {
        public OpenXMLContainerElement()
        {
            Items = new List<T>();
        }

        protected override void SaveContent(XmlWriter writer)
        {
            SaveItems(writer);
        }
        protected virtual void SaveItems(XmlWriter writer)
        {
            foreach (var item in Items)
            {
                item.Save(writer);
            }
        }

        protected List<T> Items
        {
            get;
            private set;
        }
        protected internal abstract override string NodeName { get; }
        protected internal abstract override Namespace Namespace { get; }
    }
}
