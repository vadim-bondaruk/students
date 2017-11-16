using System;
using System.Xml.Serialization;

namespace StudentsApp
{
    [Serializable]
    public enum Sex
    {
        [XmlEnum("0")]
        Male,
        [XmlEnum("1")]
        Female
    }
}
