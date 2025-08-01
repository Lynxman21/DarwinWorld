using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DarwinWorld.Components
{
    internal class MapDrawer
    {
        private double _ceilWidth;
        private double _ceilHeight;
        private Canvas _canvas;
        private int _x;
        private int _y;

        public MapDrawer(int x, int y, Canvas canvas) 
        {
            _x = x;
            _y = y;
            _ceilWidth = canvas.Width / x;
            _ceilHeight = canvas.Height / y;
            _canvas = canvas;
        }

        public void GridDraw()
        {
            for (int i=0;i<=_y;i++)
            {
                Line line = new Line
                {
                    X1 = i * _ceilWidth,
                    Y1 = 0,
                    X2 = i * _ceilWidth,
                    Y2 = _canvas.Height,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                _canvas.Children.Add(line);
            }

            for (int i=0;i<=_x;i++)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = i*_ceilHeight,
                    X2 = _canvas.Width,
                    Y2 = i * _ceilHeight,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                _canvas.Children.Add(line);
            }
        }

        //Grid cords
        private void DrawRect(Brush color, int gridX, int gridY)
        {
            Rectangle rect = new Rectangle
            {
                Width = _ceilWidth,
                Height = _ceilHeight,
                Fill = color,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(rect,_ceilWidth * gridX);
            Canvas.SetTop(rect,_ceilHeight * gridY);
            _canvas.Children.Add(rect);
        }

        public void DrawPlant(int gridX, int gridY)
        {
            DrawRect(Brushes.Brown,gridX,gridY);    
        }

        public void DrawAnimal(int gridX, int gridY, Animal animal, double maxEnergy)
        {
            double factor = animal.Energy / maxEnergy;

            if (factor > 0.75)
            {
                DrawRect(Brushes.Green,gridX,gridY);
            }
            else if (factor > 0.25 && factor <= 0.75)
            {
                DrawRect(Brushes.Orange, gridX, gridY);
            }
            else
            {
                DrawRect(Brushes.Red, gridX, gridY);
            }
        }

        public void ClearCeil(int gridX, int gridY)
        {
            DrawRect(Brushes.White, gridX, gridY);
        }
    }
}
