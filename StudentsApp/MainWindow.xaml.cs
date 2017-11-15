using StudentsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace StudentsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            List<Student> elements = new List<Student>();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Students";
            xRoot.IsNullable = true;

            var serializer = new XmlSerializer(typeof(List<Student>), xRoot);

            using (var reader = new StreamReader(@"D:\PROJECTS\StudentsApp\StudentsApp\Data\Students.xml"))
            {
                 elements = (List<Student>)serializer.Deserialize(reader);
            }

            elements.Add(new Student { Id = 10, FirstName = "Valera", Gender = 0, Last = "XUI" });

            using (var writer = new StreamWriter(@"D:\PROJECTS\StudentsApp\StudentsApp\Data\Students.xml"))
            {
                serializer.Serialize(writer, elements);
            }

            students.ItemsSource = elements;
        }
    }
}
