using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Tubes
{
    public partial class MainWindow : Window
    { 
        private bool isWaterOpened = false;
        private const int FIELD_WIDTH = 5;
        private TubeField field;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize() 
        {
            field = new TubeField(FIELD_WIDTH);
            for (int i = 0; i < FIELD_WIDTH * FIELD_WIDTH; i++)
            {
                var btn = (Button)ButtonsGrid.Children[i];
                btn.Content = field.tubes[i].image;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            for (int i = 0; i < ButtonsGrid.Children.Count; i++) 
            {
                if (btn == ButtonsGrid.Children[i]) {

                    Rotate(i);
                    btn.Content = field.tubes[i].image;
                    break;
                }
            }
        }
        private void Rotate(int index) 
        {
            if(!isWaterOpened)
            field.Rotate(index);
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            bool isWorking = true;
            int nextIndex = 0;
            int index = FIELD_WIDTH;

            buttonOpen.IsEnabled = false;
            buttonRefresh.IsEnabled = false;
            isWaterOpened = true;

            field.tubes[nextIndex].SetWaterDirection();

            while (isWorking)
            {
                nextIndex = field.TryMoveToNextTube(nextIndex);

                if (nextIndex != -1)
                {
                    field.tubes[nextIndex].SetWater();

                    var btn = (Button)ButtonsGrid.Children[index];
                    btn.Content = field.tubes[index].image;     
                    
                    index = nextIndex;

                    if (index == (FIELD_WIDTH * FIELD_WIDTH) - 1) { MessageBox.Show("Победа! :D"); isWorking = false; }
                }
                else { MessageBox.Show("Поражение! :/"); isWorking = false; }
            }
            GroupBoxButtons.IsEnabled = false;
            buttonOpen.IsEnabled = true;
            buttonRefresh.IsEnabled = true;
            isWaterOpened = false;

        }
        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            GroupBoxButtons.IsEnabled = true;
            Initialize();
        }
    }
}
