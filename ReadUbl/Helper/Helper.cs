﻿using ReadUbl.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ReadUbl.Helper
{
    public class Helper
    {
        public static Type GetModelType(XmlDocument xmlDoc)
        {
            var assemblies = Assembly.GetCallingAssembly();

            string nameSpace = xmlDoc.DocumentElement.Name;
            if (xmlDoc.DocumentElement.Name.Contains(":"))
                nameSpace = xmlDoc.DocumentElement.Name.Split(":")[1];
            var classes = assemblies.DefinedTypes.Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(XmlRootAttribute)));
            foreach(var _class in classes)
            {
                var attr = _class.GetCustomAttribute(typeof(XmlRootAttribute));
                if (((XmlRootAttribute)attr).ElementName == nameSpace)
                {
                    return _class;
                }
            }
            return assemblies.DefinedTypes.FirstOrDefault(x => x.Name == nameSpace);
        }
    }
}
