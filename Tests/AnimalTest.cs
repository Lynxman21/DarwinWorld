using DarwinWorld.Components;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class AnimalTest
{
    Animal animal;

    [SetUp]
    public void SetUp()
    {
        animal = new Animal(new Point(3,3),50,1);
    }

    [Test]
    public void MoveTest()
    {
        Animal res = new Animal(new Point(3, 2), 49,1);

        animal.Move(MoveDirection.South);
        List<MoveDirection> l = new List<MoveDirection>();

        Assert.That(animal, Is.EqualTo(res));
    }

    [Test]
    public void IncreaseEnergyTest()
    {
        animal.IncreaseEnergy(3);

        Assert.That(animal, Is.EqualTo(new Animal(new Point(3, 3), 53,1)));
    }

    [Test]
    public void IsDeadTest()
    {
        Animal animal = new Animal(new Point(3, 3), 1,1);
        animal.Move(MoveDirection.South);

        Assert.That(animal.IsDead(),Is.True);
    }

    [Test]
    public void ReproductTest()
    {
        Animal a1 = new Animal(new Point(3,3),50,2);

        Animal child = animal.Reproduct(a1, 10,2);

        Assert.That(child, Is.EqualTo(new Animal(new Point(3, 3), 10,3)));
    }
}
