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
            var ann = new Person("Ann", 16);
            var jack = new Person("Jack", 40);
            var john = new Person("John", 8);
            var jenny = new Person("Jenny", 25);

            await PersonDataStructure.Push(ann);
            await PersonDataStructure.Push(emma);
            await PersonDataStructure.Push(jack);
            await PersonDataStructure.Push(john);
            await PersonDataStructure.Push(jenny);

            var res = await PersonDataStructure.Pop();
            var res2 = await PersonDataStructure.Pop();
            var res3 = await PersonDataStructure.Pop();
            var res4 = await PersonDataStructure.Pop();
            var res5 = await PersonDataStructure.Pop();

            res.Should().Be(emma);
            res2.Should().Be(jack);
            res3.Should().Be(jenny);
            res4.Should().Be(ann);
            res5.Should().Be(john);
        }

        #endregion

        #region PerformanceTests

        [Test]
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAnd10000NumbersPassedInOnEachPop_WorksSlowerWithEachObjectStored()
        {
            var randomNum = new Random();

            for (int i = 1; i < 10000; i++)
            {
                var currentValue = randomNum.Next(0, 40000);
                await (IntDataStructure.Push(currentValue));
            }
            var output = new long[10000];
            var result = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var value = await IntDataStructure.Pop();
                watch.Stop();
                output[i] = watch.ElapsedTicks;
            }
            // HACK: It is reversely optimal to expected - the more numbers stored, the faster the data structure performs Pop()
            // NOTE: HACK TODO

            output[3].Should().BeLessThan(output[3800]);
            output[7].Should().BeLessThan(output[2200]);
            output[5].Should().BeLessThan(output[11100]);
            result = new List<long>(output.Where(n => n < 4));
            result.Count.Should().BeCloseTo(12000, 500);
        }

        [Test]
        public async Task QuickPushDataStructure_WithInt32AsGenericArgAnd10000NumbersPassedIn_TakesAlmostSameAmountOfTime()
        {
            var result = new long[10000];
            var randomNum = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var currentValue = randomNum.Next(0, 4000);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                await IntDataStructure.Push(currentValue);
                watch.Stop();
                result[i] = watch.ElapsedTicks;
            }

            // NOTE: HACK TODO

            result[3].Should().BeCloseTo(result[9000], 40);
            result[7].Should().BeCloseTo(result[7500], 40);
            result[5].Should().BeCloseTo(result[5000], 40);
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