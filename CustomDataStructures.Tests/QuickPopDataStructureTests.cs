using CustomDataStructures.Domain.DataStructures;
using CustomDataStructures.Domain.Models;
using CustomDataStructures.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataStructures.Tests
{
    public class QuickPopDataStructureTests
    {
        public QuickPopDataStructure<int> IntDataStructure { get; set; }
        public QuickPopDataStructure<Person> PersonDataStructure { get; set; }

        [SetUp]
        public void Setup()
        {
            IntDataStructure = new QuickPopDataStructure<int>();
            PersonDataStructure = new QuickPopDataStructure<Person>();
        }

        [Test]
        public void QuickPopDataStructure_WithInt32AsGenericArgAnd3426984PassedInWhenPop_Returns9()
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
        public void QuickPopDataStructure_WithPersonObjectAsGenericArgPassedInWhenPop_ReturnsOldestOne()
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

        // HACK TODO: Test is QuickPopDataStructure performing Push operation in O(1)

        // HACK TODO: Test is QuickPopDataStructure performing Pop operation in O(n)
    }
}
