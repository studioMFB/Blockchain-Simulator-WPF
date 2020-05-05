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
            Block block;
            string inputData = dataText.Text;
            int inputDifficulty = int.Parse(difficultyText.Text);
            blockchain.AddBlock(block = new Block(inputDifficulty, inputData));
            indexText.Text = Block.Index++.ToString();
            timeStampText.Text = block.TimeStamp.ToString();
            nonceText.Text = Block.Nonce.ToString();
            prevHashText.Text = block.PrevHash;
            hashText.Text = block.Hash;
        }
    }
}
