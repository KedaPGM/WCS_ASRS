using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using HandyControl;


namespace WcsAsra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public uint Row = 4;
        public uint Column = 6;

        public MainWindow()
        {
            InitializeComponent();
            CreatGrid(Row, Column);   // 行 -> 列
        }

        protected override void OnContentRendered(EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        
        private void CreatGrid(uint row, uint col)
        {
            //创建行
            for (int r = 0; r < row; r++)
            {
                Login.RowDefinitions.Add(new RowDefinition());
            }

            //创建列
            for (int i = 0; i < col; i++)
            {
                Login.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //添加标题内容和红灯
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    Border border = new Border()
                    {
                        CornerRadius = new CornerRadius(10),
                        BorderThickness = new Thickness(1),
                        Width = 30,
                        Height = 30,
                    };

                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 25,
                        Height = 25,
                        Stroke = new SolidColorBrush(Colors.Blue),
                        Fill = new SolidColorBrush(Colors.Red)
                    };


                    TextBlock textrowblock = new TextBlock()
                    {
                        Text = $"第{r + 1}行,第{c + 1}列",
                        Margin = new Thickness(10, 10, 10, 10),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                    };
                    Login.Children.Add(textrowblock);
                    Grid.SetColumn(textrowblock, c);
                    Grid.SetRow(textrowblock, r);
                }
            }
        }
    }
}
