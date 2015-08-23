using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.Packaging;
using System.Xml;
using FiftyNine.Ag.OpenXML.Common.Extensions;

namespace FiftyNine.Ag.OpenXML.Common.BaseClasses
{
    public abstract class OpenXMLElement
    {
        protected internal virtual void Initialize()
        {
        }
        public virtual void Save(XmlWriter writer)
        {
            writer.WritePrefixedStartElement(Namespace, NodeName);
            SaveContent(writer);
            writer.WriteEndElement();
        }
        protected virtual void SaveContent(XmlWriter writer)
        {
        }

        protected internal PackagePart Part
        {
            get;
            internal set;
        }
        protected internal abstract string NodeName { get; }
        protected internal abstract Namespace Namespace { get; }
    }
}
