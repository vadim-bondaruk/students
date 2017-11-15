using StudentsApp.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace StudentsApp
{
    /// <summary>
    /// Interaction logic for AddNewItem.xaml
    /// </summary>
    public partial class AddNewItem : Window
    {
        public AddNewItem()
        {
            InitializeComponent();
            this.DataContext = new Student() {Id = 13, Gender = 1 };
        }

        private void SubmitProduct(object sender, RoutedEventArgs e)
        {
            FirstName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            LastName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Age.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError(FirstName) || 
                Validation.GetHasError(LastName) ||
                Validation.GetHasError(Age))
            {
                return;
            }

            var student = (Student)this.DataContext;

            ((MainWindow)this.Owner).AddStudent(student);

            this.Close();
        }

    }
}
