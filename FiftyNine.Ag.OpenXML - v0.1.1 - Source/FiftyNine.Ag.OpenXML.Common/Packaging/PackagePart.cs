using System;
using System.Net;
using System.Windows;
using FiftyNine.Ag.OpenXML.Common.BaseClasses;
using FiftyNine.Ag.OpenXML.Common.Storage;
using System.IO;
using System.Xml;
using Common = FiftyNine.Ag.OpenXML.Common;
using System.Collections.Generic;
using System.Linq;
using FiftyNine.Ag.OpenXML.Common.Extensions;

namespace FiftyNine.Ag.OpenXML.Common.Packaging
{
    public abstract class PackagePart : PackageEntity
    {
        List<Namespace> _namespaces = new List<Namespace>();

        protected internal PackagePart()
        {
        }

        public T CreateElement<T>() where T : OpenXMLElement
        {
            T ret = Activator.CreateInstance(typeof(T)) as T;
            ret.Part = this;
            ret.Initialize();
            AddRequiredNamespace(ret.Namespace);
            return ret;
        }
        public void AddRequiredNamespace(Namespace ns)
        {
            if (Namespaces.FirstOrDefault(t => t.Equals(ns)) == null)
            {
                _namespaces.Add(ns);
            }
        }

        internal protected virtual void Initialize()
        {
        }
        public virtual void Save(IStreamProvider streamProvider)
        {
            SavePart(streamProvider);

            string fileName = System.IO.Path.GetFileName(Path);
            string relName = Path.Replace(fileName, "_rels/" + fileName + ".rels");
            SaveRelationships(streamProvider, relName, this);
        }
        protected virtual void SavePart(IStreamProvider streamProvider)
        {
            streamProvider.CreateXMLPartFile(Path, SaveContent);
        }
        protected virtual void SaveContent(XmlWriter writer)
        {
        }
        protected virtual void WriteRequiredNamespaces(XmlWriter writer)
        {
            foreach (var ns in Namespaces)
            {
                writer.WriteAttributeString("xmlns", ns.Prefix, null, ns.Value);
            }
        }

        protected internal Package Package
        {
            get;
            internal set;
        }
        protected internal string Path
        {
            get;
            internal set;
        }
        protected internal abstract string ContentType
        {
            get;
        }
        protected internal abstract string RelationshipType
        {
            get;
        }
        public IEnumerable<Namespace> Namespaces
        {
            get { return _namespaces; }
        }
    }
}
