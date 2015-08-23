using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using FiftyNine.Ag.OpenXML.Common.Storage;
using FiftyNine.Ag.OpenXML.Common.Extensions;

namespace FiftyNine.Ag.OpenXML.Common.Packaging
{
    public class PackageEntity
    {
        List<Relationship> _relationships = new List<Relationship>();

        protected Relationship AddRelationship(PackagePart part)
        {
            Relationship rel = new Relationship(part, _relationships.Count + 1);
            _relationships.Add(rel);
            return rel;
        }
        protected Relationship AddRelationship(string target, string type, bool isExternal)
        {
            Relationship rel = new Relationship(target, type, isExternal, _relationships.Count + 1);
            _relationships.Add(rel);
            return rel;
        }
        protected virtual void SaveRelationships(IStreamProvider streamProvider, string path, PackagePart relativeTo)
        {
            if (_relationships.Count > 0)
            {
                streamProvider.CreateXMLPartFile(path, w =>
                {
                    w.WriteStartElement("Relationships", Common.Constants.Namespaces.PackageRelationships.Value);
                    foreach (var rel in Relationships)
                    {
                        rel.Save(w, relativeTo);
                    }
                    w.WriteEndElement();
                }
                );
            }
        }

        public IEnumerable<Relationship> Relationships
        {
            get { return _relationships; }
        }
    }
}
