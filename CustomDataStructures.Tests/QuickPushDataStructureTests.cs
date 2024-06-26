using CustomDataStructures.Domain.DataStructures;
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

        #region DataStructureWithObjectsTest

        // NOTE: due to the fact there will not be many differences in class's work when it comes to different types stored,
        // I am only adding one test with example Person class - this has to implement IComparable<T> in order to make it possible 
        // to distinguish which one is bigger and which one is smaller.

        [Test]
        public async Task QuickPushDataStructure_WithPersonObjectAsGenericArgPassedInWhenPop_ReturnsOldestOne()
        {
            var emma = new Person("Emma", 65);
            await PersonDataStructure.Push(new Person("Ann", 16));
            await PersonDataStructure.Push(emma);
            await PersonDataStructure.Push(new Person("Jack", 40));
            await PersonDataStructure.Push(new Person("John", 8));
            await PersonDataStructure.Push(new Person("Jenny", 25));

            var res = await PersonDataStructure.Pop();
            var res2 = await PersonDataStructure.Pop();
            // Teraz wyciąga Jack i Emma, potem wypluwa w kółko Jenny
            var res3 = await PersonDataStructure.Pop();
            var res4 = await PersonDataStructure.Pop();
            var res5 = await PersonDataStructure.Pop();

            res.Should().Be(emma);
        }

        #endregion

        #region PerformanceTests
        [Test]
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAnd120000NumbersPassedInOnEachPop_WorksSameAmountOfTime()
        {
            var randomNum = new Random();

            for (int i = 1; i < 12100; i++)
            {
                var currentValue = randomNum.Next(0, 4000);
                await (IntDataStructure.Push(currentValue));
            }
            var output = new long[12100];
            var result = new List<long>();
            for (int i = 0; i < 12100; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var value = await IntDataStructure.Pop();
                watch.Stop();
                output[i] = watch.ElapsedTicks;
            }

            // NOTE: Most of the calls takes 1-3 ticks to finish work. But due to the fact what as the dataset becames larger and larger, 
            // per Nth element of a result array (amount of ticks) can be unordinarily greater, for example 7 elements out of 120, two first,
            // and later on - each 20th or so, those can get even 30 ticks instead of just 1
            // Moreover, first two occurences takes most of the time.

            output[3].Should().BeCloseTo(output[3800], 40);
            output[7].Should().BeCloseTo(output[2200], 40);
            output[5].Should().BeCloseTo(output[11100], 40);
            result = new List<long>(output.Where(n => n < 4));
            result.Count.Should().BeCloseTo(12000, 500);
        }

        [Test]
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAnd120000NumbersPassedInOnEachPush_TakesMoreTimeToComplete()
        {
            var result = new long[12000];
            var randomNum = new Random();
            for (int i = 0; i < 12000; i++)
            {
                var currentValue = randomNum.Next(0, 4000);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                await IntDataStructure.Push(currentValue);
                watch.Stop();
                result[i] = watch.ElapsedTicks;
            }

            // NOTE: Here there's completely reversed situation when compared to Pop, because regularly, as the dataset grows, it takes more ticks
            // to insert next number to the dataset, but, from time to time randomNum.Next() will return a number greater than currently stored 
            // Greatest.Value inside of an IntDataStructure, therefore inserting such a number will take only few ticks - still that's not 
            // common as it may be seen using debugger and inspecting the results when it comes to 12000 values stored in QuickPopDataStructure

            result[3].Should().BeLessThan(result[9000]);
            result[7].Should().BeLessThan(result[10000]);
            result[5].Should().BeLessThan(result[11100]);
        }

        #endregion

        #region UnitTests
        [Test]
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAndElevenNumbersPassedInWhenPop_ReturnsValuesInDescendingOrder()
        {
            await IntDataStructure.Push(3);
            await IntDataStructure.Push(4);
            await IntDataStructure.Push(2);
            await IntDataStructure.Push(6);
            await IntDataStructure.Push(9);
            await IntDataStructure.Push(8);
            await IntDataStructure.Push(4);
            await IntDataStructure.Push(3);
            await IntDataStructure.Push(4);
            await IntDataStructure.Push(2);
            await IntDataStructure.Push(6);
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
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAndNineNumbersPassedInWhenPopCalledOnMultipleThreads_ReturnsExpectedResult()
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
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAndFourNumbersPassedInWhenPop_ReturnsGreatestAndDeletesItFromDataStructure()
        {
            await IntDataStructure.Push(3);
            await IntDataStructure.Push(4);
            await IntDataStructure.Push(2);
            await IntDataStructure.Push(6);
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
        #endregion
    }
}