using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using FiftyNine.Ag.OpenXML.Common.Storage;
using FiftyNine.Ag.OpenXML.Common.Constants;
using FiftyNine.Ag.OpenXML.Common.Extensions;

namespace FiftyNine.Ag.OpenXML.Common.Packaging
{
    public abstract class Package : PackageEntity
    {
        List<PackagePart> _parts = new List<PackagePart>();
        Dictionary<string, string> _defaultExtensions = new Dictionary<string, string>();
        Dictionary<string, string> _extensionOverrides = new Dictionary<string, string>();

        public Package()
        {
            _defaultExtensions.Add("xml", ContentTypes.Xml);
            _defaultExtensions.Add("rels", ContentTypes.Relationship);
            _defaultExtensions.Add("jpg", ContentTypes.Jpg);
            _defaultExtensions.Add("jpeg", ContentTypes.Jpg);
        }

        public T CreatePart<T>(string path) where T : PackagePart
        {
            T ret = Activator.CreateInstance(typeof(T)) as T;
            ret.Package = this;
            ret.Path = path;
            if (!ret.Path.StartsWith("/"))
            {
                ret.Path = "/" + ret.Path;
            }
            ret.Initialize();

            string extension = Path.GetExtension(ret.Path).Substring(1);
            if (_defaultExtensions.Keys.FirstOrDefault(ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)) == null)
            {
                _defaultExtensions.Add(extension, ret.ContentType);
            }
            else
            {
                _extensionOverrides.Add(ret.Path, ret.ContentType);
            }

            _parts.Add(ret);
            return ret;
        }
        public virtual void Save(IStreamProvider streamProvider)
        {
            SaveContentTypes(streamProvider);
            SaveRelationships(streamProvider, "_rels/.rels", null);
            SaveParts(streamProvider);
        }
        protected virtual void SaveContentTypes(IStreamProvider streamProvider)
        {
            streamProvider.CreateXMLPartFile("[Content_Types].xml", writer =>
                {
                    writer.WriteStartElement("Types", Namespaces.ContentTypes.Value);
                    foreach (var ext in _defaultExtensions)
                    {
                        AddExtension(writer, ext.Key, ext.Value);
                    }
                    foreach (var ext in _extensionOverrides)
                    {
                        AddExtensionOverride(writer, ext.Key, ext.Value);
                    }
                    writer.WriteEndElement();
                }
            );
        }
        protected virtual void SaveParts(IStreamProvider streamProvider)
        {
            foreach (var part in Parts)
            {
                part.Save(streamProvider);
            }
        }

        private void AddExtension(XmlWriter writer, string extension, string contentType)
        {
            writer.WriteStartElement("Default");
            writer.WriteAttributeString("Extension", extension);
            writer.WriteAttributeString("ContentType", contentType);
            writer.WriteEndElement();
        }
        private void AddExtensionOverride(XmlWriter writer, string path, string contentType)
        {
            writer.WriteStartElement("Override");
            writer.WriteAttributeString("PartName", path);
            writer.WriteAttributeString("ContentType", contentType);
            writer.WriteEndElement();
        }

        public IEnumerable<PackagePart> Parts
        {
            get { return _parts; }
        }
    }
}
