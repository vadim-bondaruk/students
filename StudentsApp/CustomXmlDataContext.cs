using StudentsApp.Interfaces;
using StudentsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsApp
{
    public class CustomXmlDataContext : IDataContext<Student>
    {
        private ObservableCollection<Student> _items;
        private List<Student> _proxy;
        private string _path;  


        public CustomXmlDataContext(string path)
        {
            _path = path;
            LoadData();
            _proxy = _items.ToList();
        }

        public void AddOrUpdate(Student item)
        {
            var student = _proxy.SingleOrDefault(s => s.Id == item.Id);

            if(student == null)
            {
                var id = _proxy.Count > 0 ? _proxy.Select(s => s.Id).Max() + 1 : 0;
                item.Id = id;
                _proxy.Add(item);
            }
            else
            {
                item.Id = student.Id;
                _proxy.Remove(student);
                _proxy.Add(item);
            }
        }

        public ObservableCollection<Student> GetAll()
        {
            return _items;
        }

        public void SaveChanges()
        {
            List<Student> elements = new List<Student>();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Students";
            xRoot.IsNullable = true;

            var serializer = new XmlSerializer(typeof(List<Student>), xRoot);

            using (var writer = new StreamWriter(_path))
            {
                serializer.Serialize(writer, _proxy);
            }

            var unique = _proxy.Except(_items);
           

            if (unique.Count() > 0)
            {
                unique.ToList().ForEach(s => _items.Add(s));
            }

            if(_proxy.Count()< _items.Count())
            {
                var itemsToDelete = _items.Except(_proxy);
                itemsToDelete.ToList().ForEach(i => _items.Remove(i));
            }
        }

        public void Delete(Student item)
        {
            _proxy.Remove(item);
        }

        private void LoadData()
        {
            List<Student> elements = new List<Student>();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Students";
            xRoot.IsNullable = true;

            var serializer = new XmlSerializer(typeof(List<Student>), xRoot);

            using (var reader = new StreamReader(_path))
            {
                elements = (List<Student>)serializer.Deserialize(reader);
            }

            _items = new ObservableCollection<Student>(elements);
        }
    }
}
