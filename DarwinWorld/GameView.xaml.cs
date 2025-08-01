using DarwinWorld.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Point = DarwinWorld.Components.Point;

namespace DarwinWorld
{
    /// <summary>
    /// Logika interakcji dla klasy GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private Map _map;
        private MapDrawer _drawer;
        private int _higherForPlantsLower;
        private int _higherForPlantsHigher;
        public GameView(Map map)
        {
            InitializeComponent();
            _map = map ?? throw new ArgumentNullException("Map cannot be null");
            _drawer = new MapDrawer(_map.Width, _map.Height, canvasArea);
            int interval = (int)(_map.Height * 0.2);
            _higherForPlantsHigher = (int)(_map.Height / 2) + interval;
            _higherForPlantsLower = (int)(_map.Height / 2) - interval;
        }

        public async void GameLoop()
        {
            _drawer.GridDraw();
            Random random = new Random();
            Animal currentAnimal;
            int plantArea;

            List<Point> points = _map.Plants.Keys.ToList();
            for (int i=0; i < points.Count; i++)
            {
                _drawer.DrawPlant(points[i].X, points[i].Y);
            }

            points = _map.Animals.Keys.ToList();
            for (int i=0;i<points.Count; i++)
            {
                List<Animal> animals = _map.Animals[points[i]];

                for (int j=0;j<animals.Count;j++)
                {
                    _drawer.DrawAnimal(points[i].X, points[i].Y, animals[j],_map.AnimalStartEnergy);
                }
            }

            await Task.Delay(1000);

            while (true)
            {
                points = _map.Animals.Keys.Select(p => p.Clone()).ToList();
                int counter;

                for (int i=0;i<points.Count;i++)
                {
                    counter = _map.Animals[points[i]].Count;
                    while (_map.Animals.ContainsKey(points[i]) && counter > 0)
                    {
                        currentAnimal = _map.Animals[points[i]][0];

                        _map.EatPlant(currentAnimal);
                        _map.Reproduct(currentAnimal);

                        if (_map.MoveAnimal(currentAnimal))
                        {
                            _drawer.DrawAnimal(currentAnimal.Position.X, currentAnimal.Position.Y, currentAnimal, _map.AnimalStartEnergy);
                        }
                        if (_map.Animals.ContainsKey(points[i]) && _map.Animals[points[i]].Count > 0)
                        {
                            _drawer.DrawAnimal(points[i].X, points[i].Y, _map.Animals[points[i]][0],_map.AnimalStartEnergy);
                        }
                        else if (_map.Plants.ContainsKey(points[i]))
                        {
                            _drawer.DrawPlant(points[i].X, points[i].Y);
                        }
                        else
                        {
                            _drawer.ClearCeil(points[i].X, points[i].Y);
                        }
                        counter--;

                        await Task.Delay(1000);
                    }

                    
                }

                plantArea = random.Next(0, 101);
                Point p;

                if (plantArea < 75)
                {
                    int x = random.Next(0, _map.Width);
                    int y = random.Next(_higherForPlantsLower, _higherForPlantsHigher + 1);

                    p = new Point(x, y);

                    if (_map.Plants.ContainsKey(p))
                    {
                        continue;
                    }
                }
                else
                {
                    int x = random.Next(0, _map.Width);
                    int y = random.Next(0, _map.Height);

                    p = new Point(x, y);

                    if (_map.Plants.ContainsKey(p))
                    {
                        continue;
                    }
                }

                _map.AddPlant(new Plant(p, _map.PlantEnergy));
                _drawer.DrawPlant(p.X, p.Y);
            }
        }
    }
}
