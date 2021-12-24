using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using HandyControl;
using WcsAsra.Model;
using WcsAsra.PubMaster;

namespace WcsAsra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public uint Row = 4;
        public uint Column = 6;
        public static bool isleft = false;
        public static List<PageView> PageViewList = new List<PageView>();
        public PageView pageview = new PageView();
        public bool ishave = false;
        public MainWindow()
        {
            InitializeComponent();
            CreatGrid(isleft, Row, Column);   // 行 -> 列 , 左 -> 右
            CreatGrid(!isleft, Row, Column);
            CheckIsHave(pageview);
        }

        protected override void OnContentRendered(EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        /// <summary>
        /// 后台创建显示容器
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void CreatGrid(bool isleft, uint row, uint col)
        {
            List<PageView> PageViewList = new List<PageView>();
            PageView pageview = new PageView();
            Grid creat = new Grid();

            if (isleft)
            {
                creat = this.LeftView;
            }
            else
            {
                creat = this.RightView;
            }

            //创建行
            for (int r = 0; r < row; r++)
            {
                creat.RowDefinitions.Add(new RowDefinition());
            }

            //创建列
            for (int i = 0; i < col; i++)
            {
                creat.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // 添加标题内容和红灯
            // 1.红灯常亮，2.绿灯
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    PageViewList.Add(new PageView() { Row = row, Column = col, IsHave = false });

                    //红灯
                    Border borderred = new Border()
                    {
                        //CornerRadius = new CornerRadius(15),
                        //BorderThickness = new Thickness(1),
                        Width = 30,
                        Height = 30,
                        BorderBrush = new SolidColorBrush(Colors.Transparent),
                        Background = new SolidColorBrush(Colors.Red),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    //绿灯
                    Border bordergreen = new Border()
                    {
                        //CornerRadius = new CornerRadius(15),
                        //BorderThickness = new Thickness(1),
                        Width = 30,
                        Height = 30,
                        BorderBrush = new SolidColorBrush(Colors.Transparent),
                        Background = new SolidColorBrush(Colors.Green),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Visibility = PageViewList.Find(c => c.Row == row && c.Column == col).IsHave ? Visibility.Visible : Visibility.Hidden,
                    };

                    TextBlock textrowblock = new TextBlock()
                    {
                        Text = $"第{r + 1}层, 第{c + 1}位",
                        Margin = new Thickness(10, 10, 10, 10),
                        FontSize = 20,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                    };

                    creat.Children.Add(textrowblock);
                    creat.Children.Add(borderred);
                    creat.Children.Add(bordergreen);

                    Grid.SetColumn(textrowblock, c);
                    Grid.SetColumn(borderred, c);
                    Grid.SetColumn(bordergreen, c);

                    Grid.SetRow(textrowblock, r);
                    Grid.SetRow(borderred, r);
                    Grid.SetRow(bordergreen, r);
                }
            }
        }

        private void CheckIsHave(PageView pageview)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PubMaster.StartMaster.Start();
        }
    }
}
