using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RefactoringExample
{
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
                if (person.Id != null)
                {
                    context.Persons.Attach(person);
                }
                else
                {
                    context.Persons.Add(person);
                }

                context.SaveChanges();
            }
        }
    }
}
