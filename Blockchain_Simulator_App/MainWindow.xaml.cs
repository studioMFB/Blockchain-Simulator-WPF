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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            submitButton1.IsEnabled = true;
            Block block;
            string inputData = dataText.Text;
            int inputDifficulty = 0;
            try
            { inputDifficulty = int.Parse(difficultyText1.Text); }
            catch
            { inputDifficulty = 0; }
            blockchain.AddBlock(block = new Block(inputDifficulty, inputData));
            indexText.Text = Block.Index++.ToString();
            timeStampText.Text = block.TimeStamp.ToString();
            nonceText.Text = Block.Nonce.ToString();
            prevHashText.Text = block.PrevHash;
            hashText.Text = block.Hash;
            if (!Blockchain.ValidBlock)
            { TemplateA.Background = Brushes.IndianRed; }
            submitButton.IsEnabled = false;
        }

        private void SubmitButton1_Click(object sender, RoutedEventArgs e)
        {
            submitButton.IsEnabled = true;
            Block block;
            string inputData = dataText1.Text;
            int inputDifficulty = 0;
            try
            { inputDifficulty = int.Parse(difficultyText1.Text); }
            catch
            { inputDifficulty = 0; }
            blockchain.AddBlock(block = new Block(inputDifficulty, inputData));
            indexText1.Text = Block.Index++.ToString();
            timeStampText1.Text = block.TimeStamp.ToString();
            nonceText1.Text = Block.Nonce.ToString();
            prevHashText1.Text = block.PrevHash;
            hashText1.Text = block.Hash;
            if (!Blockchain.ValidBlock)
            { TemplateA.Background = Brushes.IndianRed; }
            submitButton1.IsEnabled = false;
        }
     
    }
}
