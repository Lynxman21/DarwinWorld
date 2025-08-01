using DarwinWorld.Components;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PointTests
    {
        private Point p;

        [SetUp]
        public void Setup()
        {
            p = new Point(3,3);
        }

        [Test]
        public void MoveForwardTest()
        {
            Point res = p.MoveForward(MoveDirection.North);
            Assert.That(res,Is.EqualTo(new Point(3, 4)));
        }
        
        [Test]
        public void MoveBackwordTest()
        {
            Point res = p.MoveBackward(MoveDirection.North);
            Assert.That(res, Is.EqualTo(new Point(3, 2)));
        }
    }
}