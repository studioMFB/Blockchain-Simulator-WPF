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

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = true;
            if (!(_simulatorIndex == 0))
            { _simulatorIndex--; }

            indexText.Text = _simulatorIndex.ToString();
            timeStampText.Text = _blockchain.Chain[_simulatorIndex].TimeStamp.ToString();
            dataText.Text = _blockchain.Chain[_simulatorIndex].Data;
            difficultyText.Text = _blockchain.Chain[_simulatorIndex].Difficulty.ToString();
            nonceText.Text = _blockchain.Chain[_simulatorIndex].Nonce.ToString();
            prevHashText.Text = _blockchain.Chain[_simulatorIndex].PrevHash.ToString();
            hashText.Text = _blockchain.Chain[_simulatorIndex].Hash.ToString();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (difficultyText.Text == "")
            { difficultyText.Text = "0"; }

            if (_simulatorIndex >= _blockchain.Chain.Count - 1) // Is it a new Block?
            { _blockchain.AddBlock(_block = new Block(int.Parse(difficultyText.Text), dataText.Text)); }
            else
            {
                _block = _blockchain.Chain[int.Parse(indexText.Text)];
                _block.Data = dataText.Text; // If existing Block, assign new Data
                _block.Hash = _blockchain.Chain[int.Parse(indexText.Text)].CalculateHashKey_Sha256(); // Recalculate Hash Key with new Data
                _blockchain.IsChainValid();
            }
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
                prevButton.IsEnabled = true;
            }
            else
            {
                borderColour.Background = Brushes.IndianRed; // Not Valid colour
                submitButton.IsEnabled = false; // Locks the form to future entries
                string message = "Hash Key does not match next Block Previous Hash Key: ";
                string caption = _blockchain.Chain[int.Parse(indexText.Text) + 1].PrevHash;
                MessageBox.Show(message + caption, "Invalid Block!");
                newChainButton.IsEnabled = true;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(_simulatorIndex == _blockchain.Chain.Count - 1))
            { _simulatorIndex++; }

            indexText.Text = _simulatorIndex.ToString();
            timeStampText.Text = _blockchain.Chain[_simulatorIndex].TimeStamp.ToString();
            dataText.Text = _blockchain.Chain[_simulatorIndex].Data;
            difficultyText.Text = _blockchain.Chain[_simulatorIndex].Difficulty.ToString();
            nonceText.Text = _blockchain.Chain[_simulatorIndex].Nonce.ToString();
            prevHashText.Text = _blockchain.Chain[_simulatorIndex].PrevHash.ToString();
            hashText.Text = _blockchain.Chain[_simulatorIndex].Hash.ToString();
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
            submitButton.IsEnabled = true;

            // foreach(Text ctl in TemplateA.Children)
            // {
            //    ctl.Text = System.String.Empty;
            // }
        }

    }
}
