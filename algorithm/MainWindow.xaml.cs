using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace algorithm
{
    //IComparer는 비교를 위한 인터페이스다
    public class XComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (int) (((Point)x).X - ((Point)y).X);  
        }
    }

    public class YComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (int)(((Point)x).Y - ((Point)y).Y);
        }
    }


    public partial class MainWindow : Window
    {
        //점의 개수
        const int P = 100;
        //점 배열
        Point[] points = new Point[P];
        Line myline = new Line();
        Grid myGrid = new Grid();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();  
            MakePointArray();
        }

        private void MakePointArray()
        {
            Random r = new Random();

            for (int i = 0; i < P; i++)
            {
                points[i].X = r.Next(400);
                points[i].Y = r.Next(400);
            }

            foreach (var p in points)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 3;
                rect.Height = 3;
                rect.Stroke = Brushes.Black;
                Canvas.SetLeft(rect, p.X - 1); //오른쪽 좌표값
                Canvas.SetTop(rect, p.Y - 1);
                canvas1.Children.Add(rect);
            }
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            //// 초기최솟값
            //double min = double.MaxValue;

            //for (int i = 0; i < P; i++)
            //{
            //    for (int j = i + 1; j < P; j++)
            //    {
            //        Point a = points[i];
            //        Point b = points[j];

            //        //점 사이의 거리
            //        double d = Dist(a, b);

            //        // d가 min 보다 작을 때
            //        if (d < min)
            //        {
            //            // d, i, j를 저장
            //            min = d;

            //            myline.X1 = points[i].X;
            //            myline.Y1 = points[i].Y;
            //            myline.X2 = points[j].X;
            //            myline.Y2 = points[j].Y;
            //        }
            //    }

            //    }


            //myline.Stroke = Brushes.Red;
            //canvas1.Children.Add(myline);
            //MessageBox.Show("최솟값은 " + min);

            //Points[] 배열에 있는 점들을 x좌표 기준으로 정렬하여 출력하시오
            //Array.sort() Test
            //int[] a = new int[100];
            //Random r = new Random();
            //for(int i = 0; i < 100; i++)
            //{
            //    a[i] = r.Next(1000);
            //}

            //foreach (var v in a)
            //{
            //    Console.WriteLine(v);
            //}
            //Console.WriteLine("...After sort...");
            //Array.Sort(a);
            //foreach (var v in a)
            //{
            //    Console.WriteLine(v);
            //}
            IComparer xComp = new XComparer();
            Array.Sort(points, xComp);
            PrintPoints();
            //Array.Sort(points, new YComparer());
            //PrintPoints();
            ClosestPair CP = FindClosestPair(points, 0, 100 - 1);
        }

        private ClosestPair FindClosestPair(Point[] points, int left, int right)
        {
            if(right-left <= 3)
            {
                AlgorithmN2();
                int mid = left + (right - left) / 2;  //중앙점
                ClosestPair CPL = FindClosestPair(points, left, mid);
                ClosestPair CPR = FindClosestPair(points, mid+1, right);
                double d = Math.Min(CPL.dist, CPR.dist);
                ClosestPair CPC = FindMidRange(points, d);

                return MinCP(CPL, CPR, CPC);

            }
        }

        class ClosestPair
        {
            // 가장 가까운 거리, 두 점을 retuen 받을거임
            
        }
        private void PrintPoints()
        {
            foreach(var p in points)
            {
                Console.WriteLine(p.X + " " + p.Y);
            }
        }

        private double Dist(Point i, Point j)
        {
            // Math.Pow(x, 2) 사용, 또는 x*x
            // Math.Sqrt() 사용
            //점 i와 j 각각의 x,y좌표를 알아야함
            double s = Math.Sqrt(Math.Pow(i.X - j.X, 2) + Math.Pow(i.Y - j.Y, 2));
            return s;
        }
    }
}
