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

namespace Blockchain_Simulator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Blockchain blockchain = new Blockchain();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 1;
            //do
            //  {
            Block block;
            string inputData = dataText.Text;
            blockchain.AddBlock(block = new Block(index, inputData));
            indexText.Text = block.Index.ToString();
            timeStampText.Text = block.TimeStamp.ToString();
            prevHashText.Text = block.PrevHash;
            hashText.Text = block.Hash;
            // Exit();
            //  index++;
            //  } while (true);
        }
    }
}
