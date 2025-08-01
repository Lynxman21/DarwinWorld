using DarwinWorld.Components;
using NUnit.Framework;
using System.Reflection;

namespace Tests;

//Then i wyra¿enia z kropkami w Assertach (zmiana konwencji w nowej wersji)
[TestFixture]
public class MapTest
{
    Map map;
    Animal animal;

    [SetUp]
    public void SetUp()
    {
        map = new Map(10, 10, 5, 100, 25, 10,2);
        animal = new Animal(new Point(2, 2), 100,1);
    }

    [Test]
    public void AddAnimalTest()
    {
        map.AddAnimal(animal);

        var actualDict = typeof(Map).GetField("_animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dict = actualDict.GetValue(map) as Dictionary<Point, List<Animal>>;

        Assert.That(dict,Is.Not.Null);
        Assert.That(1, Is.EqualTo(dict.Count));
        Assert.That(dict.ContainsKey(new Point(2, 2)),Is.True);
        Assert.That(new List<Animal> { animal }, Is.EqualTo(dict[new Point(2, 2)]));
    }

    [Test]
    public void AddAnimalOutside()
    {
        Animal animal = new Animal(new Point(10, 5), 100,1);

        var ex = Assert.Throws<ArgumentException>(() => map.AddAnimal(animal));
        Assert.That(ex.Message, Is.EqualTo("Position out of range"));
    }

    //Just good case because it works even if animal did not exist
    [Test]
    public void RemoveAnimalTest()
    {
        map.AddAnimal(animal);
        map.AddAnimal(new Animal(new Point(3, 1), 100,1));

        map.RemoveAnimal(animal);

        var actualDict = typeof(Map).GetField("_animals", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dict = actualDict.GetValue(map) as Dictionary<Point, List<Animal>>;

        Assert.That(dict, Is.Not.Null);
        Assert.That(1, Is.EqualTo(dict.Count));
        Assert.That(dict.ContainsKey(new Point(2, 2)),Is.False);
        Assert.That(dict.ContainsKey(new Point(3, 1)),Is.True);
    }

    [Test]
    public void AddPlantTest()
    {
        map.AddPlant(new Plant(new Point(3, 2), 20));

        var actualPlants = typeof(Map).GetField("_plants", BindingFlags.NonPublic | BindingFlags.Instance);
        var dict = actualPlants.GetValue(map) as Dictionary<Point, Plant>;

        Assert.That(dict, Is.Not.Null);
        Assert.That(1, Is.EqualTo(dict.Count));
        Assert.That(dict.ContainsKey(new Point(3, 2)),Is.True);
    }

    [Test]
    public void RemovePlantTest()
    {
        map.AddPlant(new Plant(new Point(3, 2), 20));

        map.RemovePlant(new Point(3, 2));

        var actualPlants = typeof(Map).GetField("_plants", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var dict = actualPlants.GetValue(map) as Dictionary<Point, Plant>;

        Assert.That(dict, Is.Not.Null);
        Assert.That(0, Is.EqualTo(dict.Count));
        Assert.That(dict.ContainsKey(new Point(3, 2)),Is.False);
    }

    [Test]
    public void EatPlantTest()
    {
        Animal a = new Animal(new Point(3, 2),100,1);

        map.AddPlant(new Plant(new Point(3, 2), 20));
        map.AddAnimal(a);

        map.EatPlant(a);

        Assert.That(120, Is.EqualTo(a.Energy));
    }

    [Test]
    public void EatNoPlantTest()
    {
        Animal a = new Animal(new Point(3, 2), 100,1);
        map.AddAnimal(a);

        Assert.DoesNotThrow(() =>
        {
            map.EatPlant(a);
        });
    }

    [Test]
    public void ReproductTest()
    {
        Point p = new Point(3, 2);

        Animal a1 = new Animal(p, 100,1);
        Animal a2 = new Animal(p, 100,2);

        map.AddAnimal(a1);
        map.AddAnimal(a2);
        map.Reproduct(a1);

        var actualDict = typeof(Map).GetField("_animals", BindingFlags.NonPublic | BindingFlags.Instance);
        var dict = actualDict.GetValue(map) as Dictionary<Point, List<Animal>>;
        List<Animal> animals = dict[p];

        Assert.That(animals.Count, Is.EqualTo(3));
        Assert.That(a1.Energy, Is.EqualTo(90));
        Assert.That(a2.Energy, Is.EqualTo(90));
    }

    [Test]
    public void MoveTest()
    {
        Animal a = new Animal(new Point(3, 2), 100,1);
        a.Genom = new List<MoveDirection> { MoveDirection.North};
        map.AddAnimal(a);
        map.MoveAnimal(a);

        var actualDict = typeof(Map).GetField("_animals", BindingFlags.NonPublic | BindingFlags.Instance);
        var dict = actualDict.GetValue(map) as Dictionary<Point, List<Animal>>;

        Assert.That(dict.ContainsKey(new Point(3,2)),Is.False);
        Assert.That(dict.ContainsKey(new Point(3,3)),Is.True);
        Assert.That(a.Position, Is.EqualTo(new Point(3, 3)));
        Assert.That(a.Day, Is.EqualTo(1));
        Assert.That(a.Energy, Is.EqualTo(99));
    }
}
