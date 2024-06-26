using Sample4.View;
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

using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Sample4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long offset = 0x10000000; // 256 megabytes
        long length = 0x20000000; // 512 megabytes

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 탭 클릭 시 해당 설비의 공유메모리 뷰어로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTab_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Content = new CNV();
            string tabName = ((Button)sender).Name.Substring(Name.Length);
            switch(tabName)
            {
                case "btnWHS":
                    MainFrame.Content = new CNV();
                    break;
                case "btnAGV":
                    MainFrame.Content = new AGV();
                    break;
                case "btnRTV":
                    break;
                case "btnSTC":
                    break;
                default:
                    break;
            }
        }


        private void InitializeEquipValue()
        {

            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile(@"c:\ExtremelyLargeImage.dat", FileMode.Open, "ImgA"))
            {
                // Create a random access view, from the 256th megabyte (the offset)
                // to the 768th megabyte (the offset plus length).
                using (var accessor = mmf.CreateViewAccessor(offset, length))
                {
                    //int colorSize = Marshal.SizeOf(typeof(MyColor));
                    //MyColor color;

                    //// Make changes to the view.
                    //for (long i = 0; i < length; i += colorSize)
                    //{
                    //    accessor.Read(i, out color);
                    //    color.Brighten(10);
                    //    accessor.Write(i, ref color);
                    //}
                }
            }

            Def.CNVInfo  = new stConveyorInfo[10000];
            
        }
    }
}