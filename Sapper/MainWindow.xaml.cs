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

namespace Sapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MaxGridHeight = 20;
        public const int MinGridHeight = 10;
        public const int MaxGridLength = 20;
        public const int MinGridLength = 10;

        public int GridHeight { get; set; }
        public int GridLength { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SizeToContent = SizeToContent.WidthAndHeight;
            var a = new SapperInstance();
            a.RevealSquare(5, 5);
        }
    }
}
