using System.Windows;
using System.Windows.Media;

namespace Blockchain_Simulator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Blockchain blockchain = new Blockchain();
        private static int _index;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(_index == 0))
            { _index--; }

            indexText.Text = _index.ToString();
            dataText.Text = blockchain.Chain[_index].Data;
            timeStampText.Text = blockchain.Chain[_index].TimeStamp.ToString();
            nonceText.Text = blockchain.Chain[_index].Nonce.ToString();
            prevHashText.Text = blockchain.Chain[_index].PrevHash.ToString();
            hashText.Text = blockchain.Chain[_index].Hash.ToString();
        }

        /*
        private void  PrevButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Block block in blockchain.Chain)
            {
                _index = block.Index;
                indexText.Text = _index.ToString();

                dataText.Text = blockchain.Chain[_index].Data;
                timeStampText.Text = blockchain.Chain[_index].TimeStamp.ToString();
                nonceText.Text = blockchain.Chain[_index].Nonce.ToString();
                prevHashText.Text = blockchain.Chain[_index].PrevHash.ToString();
                hashText.Text = blockchain.Chain[_index].Hash.ToString();
            }
        }
        */
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Block block;
            string inputData = dataText.Text;
            int inputDifficulty = 0;
            try
            { inputDifficulty = int.Parse(difficultyText.Text); }
            catch
            { inputDifficulty = 0; }
            blockchain.AddBlock(block = new Block(inputDifficulty, inputData));
            indexText.Text = block.Index.ToString();
            _index = block.Index;
            timeStampText.Text = block.TimeStamp.ToString();
            nonceText.Text = block.Nonce.ToString();
            prevHashText.Text = block.PrevHash;
            hashText.Text = block.Hash;
            if (Blockchain.ValidBlock)
            {
                // submitButton1.IsEnabled = true; // If Block Valid then enables next submition form
                //borderColour1.Background = Brushes.White; // And sets its Default colour
                //borderColour.Background = Brushes.GreenYellow; // And sets its Valid colour 
                historyText.Text += "\n" + block.ToString();
            }
            else
            {
                borderColour.Background = Brushes.IndianRed; // Not Valid colour
                submitButton.IsEnabled = false; // Locks the form to future entries
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(_index == blockchain.Chain.Count - 1))
            { _index++; }

            indexText.Text = _index.ToString();
            dataText.Text = blockchain.Chain[_index].Data;
            timeStampText.Text = blockchain.Chain[_index].TimeStamp.ToString();
            nonceText.Text = blockchain.Chain[_index].Nonce.ToString();
            prevHashText.Text = blockchain.Chain[_index].PrevHash.ToString();
            hashText.Text = blockchain.Chain[_index].Hash.ToString();
        }

    }
}
