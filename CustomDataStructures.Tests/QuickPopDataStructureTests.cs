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
        private static readonly object _asyncTestLock = new object();
        public QuickPopDataStructure<int> IntDataStructure { get; set; }
        public QuickPopDataStructure<Person> PersonDataStructure { get; set; }

        [SetUp]
        public void Setup()
        {
            IntDataStructure = new QuickPopDataStructure<int>();
            PersonDataStructure = new QuickPopDataStructure<Person>();
        }

        [Test]
        public async Task QuickPopDataStructure_WithInt32AsGenericArgAndElevenNumbersPassedInWhenPop_Returns9()
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
            var res1 = await IntDataStructure.Pop();
            var res2 = await IntDataStructure.Pop();
            var res3 = await IntDataStructure.Pop();
            var res4 = await IntDataStructure.Pop();
            var res5 = await IntDataStructure.Pop();
            var res6 = await IntDataStructure.Pop();
            var res7 = await IntDataStructure.Pop();
            var res8 = await IntDataStructure.Pop();
            var res9 = await IntDataStructure.Pop();
            var res10 = await IntDataStructure.Pop();
            var res11 = await IntDataStructure.Pop();

            res1.Should().Be(9);
            res2.Should().Be(8);
            res3.Should().Be(6);
            res4.Should().Be(6);
            res5.Should().Be(4);
            res6.Should().Be(4);
            res7.Should().Be(4);
            res8.Should().Be(3);
            res9.Should().Be(3);
            res10.Should().Be(2);
            res11.Should().Be(2);
        }

        [Test]
        public async Task QuickPopDataStructure_WithInt32AsGenericArgAndNineNumbersPassedInWhenPopCalledOnMultipleThreads_Returns9WithoutMess()
        {
            var tasks = new List<Task>();

                for (int i = 0; i < 9; i++)
                {
                    var currentValue = i;
                    if (i == 2) tasks.Add(Task.Run(() => IntDataStructure.Push(4)));
                    else if (i == 5) tasks.Add(Task.Run(() => IntDataStructure.Push(3)));
                    else if (i == 9) tasks.Add(Task.Run(() => IntDataStructure.Push(1)));
                    else tasks.Add(Task.Run(() => IntDataStructure.Push(currentValue)));
                }

            await Task.WhenAll(tasks);
            var res1 = await IntDataStructure.Pop();
            var res2 = await IntDataStructure.Pop();
            var res3 = await IntDataStructure.Pop();
            var res4 = await IntDataStructure.Pop();
            var res5 = await IntDataStructure.Pop();
            var res6 = await IntDataStructure.Pop();
            var res7 = await IntDataStructure.Pop();
            var res8 = await IntDataStructure.Pop();
            var res9 = await IntDataStructure.Pop();

            res1.Should().Be(8);
            res2.Should().Be(7);
            res3.Should().Be(6);
            res4.Should().Be(4);
            res5.Should().Be(4);
            res6.Should().Be(3);
            res7.Should().Be(3);
            res8.Should().Be(1);
            res9.Should().Be(0);            
        }

        [Test]
        public async Task QuickPopDataStructure_WithInt32AsGenericArgAndFourNumbersPassedInWhenPop_ReturnsGreatestAndDeletesItFromDataStructure()
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

            var res5 = await IntDataStructure.Pop(); // res5 receives default value

            res1.Should().Be(6);
            res2.Should().Be(4);
            res3.Should().Be(3);
            res4.Should().Be(2);

            res5.Should().Be(default(int)); // res5 receives default value
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
