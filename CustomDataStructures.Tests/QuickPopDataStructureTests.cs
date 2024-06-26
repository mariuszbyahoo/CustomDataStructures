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
        public async Task QuickPopDataStructure_WithInt32AsGenericArgAnd3426984PassedInWhenPop_Returns9()
        {
            IntDataStructure.Push(3);
            IntDataStructure.Push(4);
            IntDataStructure.Push(2);
            IntDataStructure.Push(6);
            IntDataStructure.Push(9);
            IntDataStructure.Push(8);
            IntDataStructure.Push(4);
            IntDataStructure.Push(3);
            IntDataStructure.Push(4);
            IntDataStructure.Push(2);
            IntDataStructure.Push(6);
            /*
                values stored in order:
                98664443322
             */
            var res = await IntDataStructure.Pop();

            res.Should().Be(9);
        }

        [Test]
        public async Task QuickPopDataStructure_WithInt32AsGenericArgAndThreeNumbersPassedInWhenPop_ReturnsGreatestAndDeletesItFromDataStructure()
        {
            IntDataStructure.Push(3);
            IntDataStructure.Push(4);
            IntDataStructure.Push(2);
            IntDataStructure.Push(6);
            /*
                values stored in order:
                6432
             */
            var res1 = await IntDataStructure.Pop();
            var res2 = await IntDataStructure.Pop();
            var res3 = await IntDataStructure.Pop();
            var res4 = await IntDataStructure.Pop();

            res1.Should().Be(6);
            res2.Should().Be(4);
            res3.Should().Be(3);
            res4.Should().Be(2);
        }

        [Test]
        public async Task QuickPopDataStructure_WithPersonObjectAsGenericArgPassedInWhenPop_ReturnsOldestOne()
        {
            var emma = new Person("Emma", 65);
            PersonDataStructure.Push(new Person("Ann", 16));
            PersonDataStructure.Push(emma);
            PersonDataStructure.Push(new Person("Jack", 40));
            PersonDataStructure.Push(new Person("John", 8));
            PersonDataStructure.Push(new Person("Jenny", 25));

            var res = await PersonDataStructure.Pop();

            res.Should().Be(emma);
        }

        // HACK TODO: Test is QuickPopDataStructure performing Push operation in O(n)

        // HACK TODO: Test is QuickPopDataStructure performing Pop operation in O(1)
    }
}
