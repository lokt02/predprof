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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BulgingShell
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> points = new List<Point>();
        List<Point> ShellPointsList = new List<Point>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_OnClick(object sender, MouseButtonEventArgs e)
        {
            Canvas.Children.Clear();


            Point p = e.GetPosition(Canvas);
            points.Add(p);
            AddPoint(p.X, p.Y);

            foreach(Point point in points)
            {
                AddPoint(point.X, point.Y);
            }

            if(points.Count > 2)
            {
                ShellPointsList = BuildingAlg(ShellPointsList);
                ShellBuilding(ShellPointsList);
            }
        }

        private void ShellBuilding(List<Point> points)
        {
            
            for (int i = 1; i < points.Count; i++)
            {
                ShellLines(points.ElementAt(i-1), points.ElementAt(i));
                if (i == points.Count - 1)
                {
                    ShellLines(points.ElementAt(0), points.ElementAt(i));
                }
            }
        }

        private List<Point> BuildingAlg(List<Point> ShellPointsList)
        {
            ShellPointsList = new List<Point>();
            Point MinP = new Point(-1, -1);
            foreach(Point point in points)
            {
                if (point.X > MinP.X) MinP = point;
                else if(point.X == MinP.X)
                {
                    if(point.Y > MinP.Y)
                    {
                        MinP = point;
                    }
                }
            }

            int l = 0;
            l = points.IndexOf(MinP);

            int p = l, q;
            do
            {
                ShellPointsList.Add(points[p]);

                q = (p + 1) % points.Count;
                for (int i = 0; i < points.Count; i++)
                {
                    if (VectorProduct(points[p], points[i], points[q]) < 0)
                        q = i;
                    else if (VectorProduct(points[p], points[i], points[q]) == 0)
                    {
                        if (IsInside(points[p], points[i], points[q]))
                        {
                            q = i;
                        }
                    }
                }
                p = q;
            }
            while (p != l);

            return ShellPointsList;
        }
        
        private double VectorProduct(Point a, Point b, Point c)
        {
            return (b.Y - a.Y) * (c.X - b.X) - (b.X - a.X) * (c.Y - b.Y);
        }

        private bool IsInside(Point A, Point B, Point C)
        {
            if ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y) > (C.X - A.X) * (C.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y))
            {
                return true;
            }
            else return false;
        }

        private void AddPoint(double x, double y)
        {
            Rectangle rect = new Rectangle
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Black
            };
            Canvas.Children.Add(rect);
            Canvas.SetLeft(rect, x - 2.5);
            Canvas.SetTop(rect, y - 2.5);
        }

        private void ShellLines(Point p1, Point p2)
        {
            Line line = new Line
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = Brushes.Black
            };
            Canvas.Children.Add(line);
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            points.Clear();
            Canvas.Children.Clear();
        }
    }
}
