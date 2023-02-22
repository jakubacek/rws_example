using Moravia.Domain.Internal;

namespace Moravia.Test.Domain
{
    [TestFixture]
    public class PairListTests
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void AddItemTest()
        {
            var pairList = new PairList<int, string>
            {
                { 100, "100" },
                { 1, "1" }
            };

            Assert.That(pairList.Get(1), Is.EqualTo("1"));
            Assert.That(pairList.Get(100), Is.EqualTo("100"));
            Assert.That(pairList.Count, Is.EqualTo(2));
        }

        [Test]
        public void OrderItemTest()
        {
            var pairList = new PairList<string, string>
            {
                { "z", "z" },
                { "a", "a" },
                { "c", "c" }
            };

            var expectedOrder = new List<string> { "z", "a", "c" };
            int i = 0;
            foreach (var item in pairList)
            {
                Assert.That(item.Key, Is.EqualTo(expectedOrder[i]));
                i++;
            }
        }
    }
}