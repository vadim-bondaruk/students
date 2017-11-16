using StudentsApp.Interfaces;
using StudentsApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace StudentsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Student> source = null;
        private IDataContext<Student> _context;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string path = @"D:\PROJECTS\StudentsApp\StudentsApp\Data\Students.xml";
            _context = new CustomXmlDataContext(path);
            source = _context.GetAll();

            StudentsList.ItemsSource = source;
        }

        private void OpenAddStudentWindow(object sender, RoutedEventArgs e)
        {

            AddEditNewItem newItemWindow = new AddEditNewItem();
            newItemWindow.Owner = this;
            newItemWindow.DataContext = new Student { Gender = Sex.Male, Id = -1 };
            if (newItemWindow.ShowDialog().Value)
            {
                var student = (Student)newItemWindow.DataContext;
                _context.AddOrUpdate(student);
                _context.SaveChanges();
            }
        }

        private void DeleteStudents(object sender, RoutedEventArgs e)
        {
            
            var students = StudentsList.SelectedItems.Cast<Student>().ToList();

            if (students.Count > 0)
            {
               if( MessageBox.Show(this, "Are you sure you want to delete item (items)?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    students.ForEach(s => _context.Delete(s));
                    _context.SaveChanges();
                }
            }
        }

        private void EditStudent(object sender, RoutedEventArgs e)
        {
            var students = StudentsList.SelectedItems.Cast<Student>().ToList();

            if (students.Count < 1)
            {
                MessageBox.Show(this, "Plese select item", "Info", MessageBoxButton.OK);
                return;
            }

            if (students.Count > 1)
            {
                MessageBox.Show(this, "Plese select only one item", "Info", MessageBoxButton.OK);
                return;
            }

            AddEditNewItem editewItemWindow = new AddEditNewItem();
            editewItemWindow.Owner = this;

            editewItemWindow.DataContext = students.First();
            if (editewItemWindow.ShowDialog().Value)
            {
                var student = (Student)editewItemWindow.DataContext;
                _context.AddOrUpdate(student);
                _context.SaveChanges();
            }
        }
    }
}
