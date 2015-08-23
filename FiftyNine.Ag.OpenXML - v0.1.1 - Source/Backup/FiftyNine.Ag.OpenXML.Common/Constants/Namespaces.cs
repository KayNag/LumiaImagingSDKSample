using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FiftyNine.Ag.OpenXML.Common.Constants
{
    public static class Namespaces
    {
        public static readonly Namespace ContentTypes = new Namespace("ct", "http://schemas.openxmlformats.org/package/2006/content-types");
        public static readonly Namespace PackageRelationships = new Namespace("r", "http://schemas.openxmlformats.org/package/2006/relationships");
        public static readonly Namespace Relationships = new Namespace("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
        public static readonly Namespace ExtProps = new Namespace("extProp", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties");
        public static readonly Namespace CoreProps = new Namespace("cp", "http://schemas.openxmlformats.org/package/2006/metadata/core-properties");

        public static class Purl
        {
            public static readonly Namespace Dc = new Namespace("dc", "http://purl.org/dc/elements/1.1/");
            public static readonly Namespace DcTerms = new Namespace("dcterms", "http://purl.org/dc/terms/");
            public static readonly Namespace DcmiType = new Namespace("dcmitype", "http://purl.org/dc/dcmitype/");
        }

        public static class W3C
        {
            public static readonly Namespace Xsi = new Namespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }
    }
}
