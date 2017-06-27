using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace RefactoringExample
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private List<Person> persons = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();

            this.ReadPersons();
        }

        private void ReadPersons()
        {
            if (File.Exists("persondata.xml"))
            {
                var xmlSerializer = new XmlSerializer(this.persons.GetType());

                this.persons = (List<Person>) xmlSerializer.Deserialize(File.OpenRead("persondata.xml"));
            }
        }

        private void SavePersons()
        {
            var file = File.Create("persondata.xml");
            var serializer = new XmlSerializer(this.persons.GetType());

            serializer.Serialize(file, this.persons);
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
                this.persons.Add(person);
            }
            else
            {
                person = this.persons.Single(x => x.FirstName == person.FirstName);

                person.FirstName = this.FirstNameTextBox.Text;
                person.LastName = this.LastNameTextBox.Text;
                person.BirthDate = this.BirthDateTextBox.Text;
            }
        }

        public void Dispose()
        {
            this.SavePersons();
        }
    }
}
