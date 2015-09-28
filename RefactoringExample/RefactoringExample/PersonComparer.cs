namespace RefactoringExample
{
    using System;
    using System.Collections.Generic;

    public class PersonComparer : Comparer<Person>
    {
        public override int Compare(Person x, Person y)
        {
            var lastNameValue = String.Compare(x.LastName, y.LastName, StringComparison.Ordinal);

            if (lastNameValue != 0)
            {
                return lastNameValue;
            }

            var firstNameValue = String.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);

            if (firstNameValue != 0)
            {
                return firstNameValue;
            }

            return String.Compare(x.BirthDate, y.BirthDate, StringComparison.Ordinal);
        }
    }
}
