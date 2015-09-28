namespace RefactoringExample
{
    using System.Data.Entity;

    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }
}