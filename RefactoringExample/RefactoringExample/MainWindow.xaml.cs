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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RefactoringExample
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var context = new PersonContext())
            {
                var dbSet = context.Persons.ToList();
                foreach (var person in dbSet)
                {
                    this.ListBox.Items.Add(person);
                }
            }
        }
        
        private void ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var person = this.ListBox.SelectedItem as Person;
            if (person != null)
            {
                this.FirstNameTextBox.Text = person.FirstName;
                this.LastNameTextBox.Text = person.LastName;
                this.BirthDateTextBox.Text = person.BirthDate;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var person = this.ListBox.SelectedItem as Person;
            if (person == null)
            {
                person = new Person
                    {
                        BirthDate = this.BirthDateTextBox.Text,
                        FirstName = this.FirstNameTextBox.Text,
                        LastName = this.LastNameTextBox.Text
                    };

                this.ListBox.Items.Add(person);
            }
            else
            {
                person.FirstName = this.FirstNameTextBox.Text;
                person.LastName = this.LastNameTextBox.Text;
                person.BirthDate = this.BirthDateTextBox.Text;
            }

            using (var context = new PersonContext())
            {
                context.Persons.AddOrUpdate(person);
                context.SaveChanges();
            }
        }
    }

    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
