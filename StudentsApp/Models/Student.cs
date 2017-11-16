using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsApp.Models
{
    [Serializable]
    public class Student
    {
        [XmlAttribute]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Last { get; set; }
        public int? Age { get; set; }
        public Sex Gender { get; set; }
    }
}
