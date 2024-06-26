using CustomDataStructures.DTOs;

namespace CustomDataStructures.Tests
{
    public class QuickPushDataStructureTests
    {
        public QuickPushDataStructure<int> IntDataStructure { get; set; }
        public QuickPushDataStructure<Person> PersonDataStructure { get; set; }

        [SetUp]
        public void Setup()
        {
            IntDataStructure = new QuickPushDataStructure<int>();
            PersonDataStructure = new QuickPushDataStructure<Person>();
        }

        [Test]
        public void QuickPushDataStructure_WithInt32AsGenericArgAnd3426984PassedInWhenPop_Returns9()
        {
            IntDataStructure.Push(3);
            IntDataStructure.Push(4);
            IntDataStructure.Push(2);
            IntDataStructure.Push(6);
            IntDataStructure.Push(9);
            IntDataStructure.Push(8);
            IntDataStructure.Push(4);

            var res = IntDataStructure.Pop();

            res.Should().Be(9);
        }

        [Test]
        public void QuickPushDataStructure_WithPersonObjectAsGenericArgPassedInWhenPop_ReturnsOldestOne()
        {
            var emma = new Person("Emma", 65);
            PersonDataStructure.Push(new Person("Ann", 16));
            PersonDataStructure.Push(emma);
            PersonDataStructure.Push(new Person("Jack", 40));
            PersonDataStructure.Push(new Person("John", 8));
            PersonDataStructure.Push(new Person("Jenny", 25));

            var res = PersonDataStructure.Pop();

            res.Should().Be(emma);
        }

        // HACK TODO: Test is QuickPushDataStructure performing Push operation in O(1)

        // HACK TODO: Test is QuickPushDataStructure performing Pop operation in O(n)
    }
}