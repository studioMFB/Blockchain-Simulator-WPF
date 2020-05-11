using System.Windows;
using System.Windows.Media;

namespace Blockchain_Simulator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Blockchain _blockchain = new Blockchain();
        private Block _block = new Block();
        private static int _simulatorIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (difficultyText.Text == "")
            { difficultyText.Text = "0"; }
            { _blockchain.AddBlock(_block = new Block(int.Parse(difficultyText.Text), dataText.Text)); }
            // Output Block's values
            indexText.Text = _block.Index.ToString();
            _simulatorIndex = _block.Index;
            timeStampText.Text = _block.TimeStamp.ToString();
            nonceText.Text = _block.Nonce.ToString();
            prevHashText.Text = _block.PrevHash;
            hashText.Text = _block.Hash;

            if (Blockchain.IsValidBlock)
            {
                historyText.Text += "\n" + _block.ToString();
                if (_blockchain.Chain.Count > 1)
                { prevButton.IsEnabled = true; }
            }
        }

        private void TamperButton_Click(object sender, RoutedEventArgs e)
        {
            _block = _blockchain.Chain[int.Parse(indexText.Text)];

            if (_block.Data != dataText.Text)
            {
                _block.Data = dataText.Text;
                string recordedHash = hashText.Text;
                _blockchain.Chain[int.Parse(indexText.Text)].MiningBlock(_blockchain.Chain[int.Parse(indexText.Text)].Difficulty);
               // _block.Hash = _blockchain.Chain[int.Parse(indexText.Text)].CalculateHashKey_Sha256(); // Recalculate Hash Key with new Data
                _blockchain.IsChainValid();

                // Output Block's values
                indexText.Text = _block.Index.ToString();
                _simulatorIndex = _block.Index;
                timeStampText.Text = _block.TimeStamp.ToString();
                nonceText.Text = _block.Nonce.ToString();
                prevHashText.Text = _block.PrevHash;
                hashText.Text = _block.Hash;
                if (!Blockchain.IsValidBlock || _block.Hash != recordedHash)
                {
                    borderColour.Background = Brushes.IndianRed; // Not Valid colour
                                                                 // Locks the form to future entries
                    submitButton.IsEnabled = false;
                    tamperButton.IsEnabled = false;
                    prevButton.IsEnabled = false;
                    nextButton.IsEnabled = false;
                    string message = "Hash Key has been tampered and does not match the Hash Key previously recorded: ";
                    MessageBox.Show(message + recordedHash, "Invalid Block!");
                    newChainButton.IsEnabled = true;
                }
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            difficultyText.IsEnabled = false;
            submitButton.IsEnabled = false;
            tamperButton.IsEnabled = true;
            if (!(_simulatorIndex == 0))
            {
                _simulatorIndex--;
                dataText.IsEnabled = true;
                nextButton.IsEnabled = true;
            }
            if (_simulatorIndex == 0)
            {
                dataText.IsEnabled = false;
                prevButton.IsEnabled = false;
            }
            indexText.Text = _simulatorIndex.ToString();
            timeStampText.Text = _blockchain.Chain[_simulatorIndex].TimeStamp.ToString();
            dataText.Text = _blockchain.Chain[_simulatorIndex].Data;
            difficultyText.Text = _blockchain.Chain[_simulatorIndex].Difficulty.ToString();
            nonceText.Text = _blockchain.Chain[_simulatorIndex].Nonce.ToString();
            prevHashText.Text = _blockchain.Chain[_simulatorIndex].PrevHash.ToString();
            hashText.Text = _blockchain.Chain[_simulatorIndex].Hash.ToString();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_simulatorIndex < _blockchain.Chain.Count - 1)
            {
                _simulatorIndex++;
                dataText.IsEnabled = true;
                prevButton.IsEnabled = true;

                indexText.Text = _simulatorIndex.ToString();
                timeStampText.Text = _blockchain.Chain[_simulatorIndex].TimeStamp.ToString();
                dataText.Text = _blockchain.Chain[_simulatorIndex].Data;
                difficultyText.Text = _blockchain.Chain[_simulatorIndex].Difficulty.ToString();
                nonceText.Text = _blockchain.Chain[_simulatorIndex].Nonce.ToString();
                prevHashText.Text = _blockchain.Chain[_simulatorIndex].PrevHash.ToString();
                hashText.Text = _blockchain.Chain[_simulatorIndex].Hash.ToString();
            }
            else
            {
                indexText.Text = System.String.Empty;
                timeStampText.Text = System.String.Empty;
                dataText.Text = System.String.Empty;
                difficultyText.Text = System.String.Empty;
                nonceText.Text = System.String.Empty;
                prevHashText.Text = System.String.Empty;
                hashText.Text = System.String.Empty;
                difficultyText.IsEnabled = true;
                submitButton.IsEnabled = true;
                tamperButton.IsEnabled = false;
                nextButton.IsEnabled = false;
            }
        }

        private void NewChainButton_Click(object sender, RoutedEventArgs e)
        {
            Blockchain _blockchain = new Blockchain();
            _simulatorIndex = 0;
            indexText.Text = System.String.Empty;
            timeStampText.Text = System.String.Empty;
            dataText.Text = System.String.Empty;
            difficultyText.Text = System.String.Empty;
            nonceText.Text = System.String.Empty;
            prevHashText.Text = System.String.Empty;
            hashText.Text = System.String.Empty;
            historyText.Text = System.String.Empty;
            borderColour.Background = Brushes.White; // Default colour
            prevButton.IsEnabled = false;
            submitButton.IsEnabled = true;
            tamperButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            newChainButton.IsEnabled = false;
        }

    }
}
