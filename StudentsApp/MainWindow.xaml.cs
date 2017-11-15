using StudentsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Student> source = null;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string path = @"D:\PROJECTS\StudentsApp\StudentsApp\Data\Students.xml";
            List<Student> elements = new List<Student>();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Students";
            xRoot.IsNullable = true;

            var serializer = new XmlSerializer(typeof(List<Student>), xRoot);

            using (var reader = new StreamReader(path))
            {
                 elements = (List<Student>)serializer.Deserialize(reader);
            }

            source = new ObservableCollection<Student>(elements);
            //elements.Add(new Student { Id = 10, FirstName = "Valera", Gender = 0, Last = "XUI" });

            //using (var writer = new StreamWriter(path))
            //{
            //    serializer.Serialize(writer, elements);
            //}

            students.ItemsSource = source;
            students.UpdateLayout();
        }

        private void OpenAddStudentWindow(object sender, RoutedEventArgs e)
        {
            //var max = source.Select(x => x.Id).Max();
            //source.Add(new Student { Id = max+1, FirstName = "Arnold", Last = "Ramsy", Age = 12, Gender = 1 });

            AddNewItem newItemWindow = new AddNewItem();
            newItemWindow.Owner = this;
            newItemWindow.ShowDialog();
        }

        public ObservableCollection<Student> Students
        {
            get { return this.source; }
            set { this.source = value; }
        }

        public void AddStudent(Student student)
        {
            source.Add(student);
        }
    }
}
