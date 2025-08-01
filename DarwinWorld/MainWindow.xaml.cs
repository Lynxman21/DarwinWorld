using DarwinWorld.Components;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DarwinWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapFactory _mapFactory;
        public MainWindow()
        {
            InitializeComponent();
            _mapFactory = new MapFactory();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int isReady = 0;
            int widthMap;
            int heightMap;
            int startEnergyMap;
            int plantEnergyMap;
            int genomLenMap;
            int reproductionCostMap;
            int animalCountMap;
            int plantsCountMap;

            if (int.TryParse(width.Text, out widthMap))
            {
                isReady++;
            }
            if (int.TryParse(height.Text, out heightMap))
            {
                isReady++;
            }
            if (int.TryParse(startEnergy.Text, out startEnergyMap))
            {
                isReady++;
            }
            if (int.TryParse(plantEnergy.Text, out plantEnergyMap))
            {
                isReady++;
            }
            if (int.TryParse(genomLen.Text, out genomLenMap))
            {
                isReady++;
            }
            if (int.TryParse(reproductionCost.Text, out reproductionCostMap))
            {
                isReady++;
            }
            if (int.TryParse(animalCount.Text, out animalCountMap))
            {
                isReady++;
            }
            if (int.TryParse(plantsCount.Text, out plantsCountMap))
            {
                isReady++;
            }

            if (isReady == 8)
            {
                Map gameMap = _mapFactory.createMap(widthMap,heightMap,genomLenMap,startEnergyMap,plantEnergyMap,reproductionCostMap,animalCountMap);
                _mapFactory.InitMapeEnviroment(gameMap,animalCountMap,plantsCountMap);
                GameView win = new GameView(gameMap);
                win.Show();
                win.GameLoop();
            }
        }
    }
}