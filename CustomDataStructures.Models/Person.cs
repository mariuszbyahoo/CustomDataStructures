namespace CustomDataStructures.Models
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(Person? other)
        {
            if(other is null)
            {
                return 1;
            }
            else
            {
                return Age.CompareTo(other.Age);
            }
        }
    }
}