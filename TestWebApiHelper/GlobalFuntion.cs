using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TestWebApiHelper
{
    public static class GlobalFuntion
    {
        
        public static string ConvertXMLtoJson(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            return JsonConvert.SerializeXmlNode(doc);
        }
    }
}
