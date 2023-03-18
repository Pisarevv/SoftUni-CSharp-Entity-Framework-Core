﻿using ProductShop.DTOs.Import;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop.Utilities;

public class XmlHelper : IXmlHelper
{
    public T Deserialize<T>(string inputXml)
    {

        using StringReader reader = new StringReader(inputXml);

        string xmlRootName = GetRootName(inputXml);
        XmlRootAttribute xmlRoot = new XmlRootAttribute(xmlRootName);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
        T result = (T)xmlSerializer.Deserialize(reader);

        return result;
             
    }

    public T[] DeserializeCollection<T>(string inputXml)
    {

        using StringReader reader = new StringReader(inputXml);

        string xmlRootName = GetRootName(inputXml);
        XmlRootAttribute xmlRoot = new XmlRootAttribute(xmlRootName);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]), xmlRoot);
        T[] result = (T[])xmlSerializer.Deserialize(reader);

        return result;

    }

    public string GetRootName(string inputXml)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(inputXml);
        return xmlDocument.DocumentElement.Name.ToString();
    }
}