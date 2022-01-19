using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WcsAsra.Model;
using WcsAsra.PubMaster;
using WcsAsra.ViewModel;

namespace WcsAsra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //实例化静态变量
            MainViewModel.PageViewList = new List<PageView>();

            //创建界面
            CreatGrid(1, 4, 6);   // 左 -> 右，行 -> 列 
            CreatGrid(2, 4, 6);
        }

        #region[前端界面创建]

        /// <summary>
        /// 后台创建显示容器
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void CreatGrid(int isleft, int row, int col)
        {

            //是否左边
            bool Left = isleft == 1;

            //Binding使用变量(使界面从下到上显示)
            int binrow = row;
            //Binding使用变量(使界面从右到左显示)
            int bincol = col;

            //Border边距使用变量
            int BorLeft = 0, BorRight = 0, BorTop = 0, BorBottom = 2;

            //Border边距使用变量
            int BorRadiu = 0, BorAvoid = 10;

            //关联前端布局
            Grid creat = Left ? LeftView : RightView;
            Grid horarrow = Left ? LeftArroView : RightArrowView;
            Grid verarrow = VerArrowView;


            //创建行
            for (int r = 0; r < row; r++)
            {
                //界面创建
                creat.RowDefinitions.Add(new RowDefinition());

                if (Left)
                {
                    //垂直界面创建(只需一次，随左侧)
                    verarrow.RowDefinitions.Add(new RowDefinition());
                }

            }

            //创建列
            for (int i = 0; i < col; i++)
            {
                //界面创建
                creat.ColumnDefinitions.Add(new ColumnDefinition());
                
                //水平标识创建
                horarrow.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // 循环创建行类及Binding数据
            for (int r = 0; r < row; r++)
            {
                //根据左右界面反转显示（从左到右）
                bincol = Left ? col : 1;

                for (int c = 0; c < col; c++)
                {
                    //构造边框需要复位
                    int left = BorLeft, right = BorRight, top = BorTop, bottom = BorBottom, leftradiu = BorRadiu, rightradiu = BorRadiu;

                    //第一行每一列要上边框
                    if (r == 0)
                    {
                        top = 2;
                    }
                    if (r == 0 && c == 0)
                    {
                        leftradiu = 8;
                    }
                    if (r == 0 && c == col - 1)
                    {
                        rightradiu = 8;
                    }

                    //if (r != row - 1)
                    //{
                    //    bottom = 2;
                    //}

                    //每一行的第一列要左边框
                    if (c == 0)
                    {
                        left = 6;
                    }

                    //每一行的最后一列要右边框
                    if (c == col - 1)
                    {
                        right = 6;
                    }

                    //无货状态显示容器（仅用来设置边框，停用预留显示）
                    Border borderred = new Border()
                    {
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        BorderThickness = new Thickness(left, top, right, bottom),
                        CornerRadius = new CornerRadius(leftradiu, rightradiu, 0, 0),

                        //Width = '*',
                        //Height = '*',
                        Background = new SolidColorBrush(Colors.WhiteSmoke),
                        //HorizontalAlignment = HorizontalAlignment.Center,
                        //VerticalAlignment = VerticalAlignment.Center
                    };

                    //有货状态显示容器
                    Border bordergreen = new Border()
                    {
                        BorderBrush = new SolidColorBrush(Colors.Transparent),

                        //货架边框加粗，相同会影响重叠显示
                        //Width = borderred.Width - BorAvoid,
                        //Height = borderred.Height - BorAvoid,
                        Margin = new Thickness(BorAvoid, BorAvoid, BorAvoid, BorAvoid)

                        //BorderThickness = new Thickness(1),
                        //Width = '*',
                        //Height = '*',
                        //Background = new SolidColorBrush(Colors.Black),
                        //HorizontalAlignment = HorizontalAlignment.Center,
                        //VerticalAlignment = VerticalAlignment.Center
                    };

                    //有货状态Bingding静态List中对应变量
                    Binding binding = new Binding
                    {
                        Mode = BindingMode.TwoWay,
                        Path = new PropertyPath("IsHave"),
                        Source = MainViewModel.PageViewList.Find(a => a.Direction == isleft && a.Row == binrow && a.Column == bincol),
                        Converter = new BooleanToVisibilityConverter()
                    };

                    //添加Binding关系
                    BindingOperations.SetBinding(bordergreen, VisibilityProperty, binding);


                    //添加有货状态显示样式
                    Image image = new Image()
                    {
                        //根据货架方向显示相反图标
                        Source = Left ? new BitmapImage(new Uri("/Resources/LeftHuo.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri("/Resources/RightHuo.png", UriKind.RelativeOrAbsolute))
                    };

                    //图片需StackPanel包裹
                    StackPanel stackPanel = new StackPanel();

                    //添加容器图片绑定关系
                    stackPanel.Children.Add(image);
                    bordergreen.Child = stackPanel;

                    //测试用
                    //TextBlock textrowblock = new TextBlock()
                    //{
                    //    Text = $"第{r + 1}层, 第{c + 1}位",
                    //    Margin = new Thickness(10, 10, 10, 10),
                    //    FontSize = 20,
                    //    HorizontalAlignment = HorizontalAlignment.Center,
                    //    VerticalAlignment = VerticalAlignment.Bottom,
                    //};

                    //其他添加方法--停用
                    //creat.Children.Add(textrowblock);
                    //Grid.SetColumn(textrowblock, c);
                    //Grid.SetRow(textrowblock, r);

                    //状态显示容器绑定大容器所在横列
                    creat.Children.Add(borderred);
                    creat.Children.Add(bordergreen);
    
                    Grid.SetColumn(borderred, c);
                    Grid.SetColumn(bordergreen, c);

                    Grid.SetRow(borderred, r);
                    Grid.SetRow(bordergreen, r);


                    if (r == 0)
                    {
                        TextBlock texthorblock = new TextBlock()
                        {
                            Text = $"{bincol}",
                            Margin = new Thickness(10, 10, 10, 10),
                            FontSize = 20,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                        };

                        horarrow.Children.Add(texthorblock);

                        Grid.SetColumn(texthorblock, c);

                    }

                    //计算界面对应List变量
                    bincol = Left ? --bincol : ++bincol;

                }

                if (Left)
                {
                    TextBlock textverblock = new TextBlock()
                    {
                        Text = $"{binrow}",
                        FontSize = 20,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,

                        //Background = new SolidColorBrush(Colors.HotPink)
                        //Margin = new Thickness(0, 0, 0, 0),
                    };

                    verarrow.Children.Add(textverblock);

                    Grid.SetRow(textverblock, r);
                };

                //计算界面对应List变量
                binrow--;
                
            }

        }

        #endregion

        #region[界面触发方法]

        /// <summary>
        /// 打开窗口时的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartMaster.Start();
        }

        /// <summary>
        /// 关闭窗口前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    e.Cancel = true;
                    StartMaster.Stop();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Environment.Exit(0);
        }

        #endregion

    }

}
