using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blockchain_Simulator_App
{
    class Blockchain
    {
        private static int _difficulty; // Number of 0 required to be at the begining of a Hash key. 
                                        // The Higher the difficulty, the harder it is to get a suitable Hash key, the slowier it is to mine a Block.
        private static List<Block> _chain = new List<Block>();


        public List<Block> Chain { get { return _chain; } set { _chain = value; } }
        public static int Difficulty { get { return _difficulty; } set { _difficulty = value; } }
        public static bool ValidBlock { get; set; }

        public Blockchain()
        {
            _chain.Add(CreateGenesisBlock(new Block(_difficulty, "Genesis block"))); // Adds by default, the very first Block called Genesis, to the Chain.
        }

        /// <summary>
        /// Creates the very first Block called the Genesis.
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="previousHash"></param>
        /// <returns></returns>
        public Block CreateGenesisBlock(Block genesisBlock)
        {
            genesisBlock.MiningBlock(0);
            return genesisBlock;
        }

        /// <summary>
        /// returns previous Block Hash Key
        /// </summary>
        /// <returns></returns>
        public static string PreviousHash()
        {
            try
            { return _chain.Last().Hash; }
            catch
            { return "n/a"; } // Because the Genesis Block is the root Block of the Chain. Previous Hash Key is n/a.
        }

        /// <summary>
        /// If previous block Has matches the New Block previous Hash. 
        /// Then Create new Block.
        /// And Add to Chain List.
        /// </summary>
        /// <param name="previousBlock"></param>
        /// <param name="newData"></param>
        public void AddBlock(Block newBlock)
        {
             newBlock.MiningBlock(_difficulty);
            newBlock.PrevHash = PreviousHash();
            _chain.Add(newBlock);
            ValidBlock = IsChainValid();
            Console.WriteLine("Is Block valid? \n" + ValidBlock);
            if (ValidBlock)
            {
                newBlock.Index = Chain.IndexOf(newBlock);
                Console.WriteLine(newBlock.ToString() + "\n");
            }
            else
            { _chain.Remove(newBlock); }
            /*
            if (_chain.Count == 3) // Testing BlockChain member method IsValid
            {
                Console.WriteLine("\n Trying to change Data of Block index : 1");
                _chain[1].Data = "Change previous Block Data"; // If you try to change the Data value of a previous Block, 
                Console.WriteLine("Is Block valid? \n" + IsChainValid()); // its new Hash will not match the one originally recorded. 
                                                                          //The Block will NOT be valid.
                Console.WriteLine("Trying to create a new Hash Key from changed Data of Block index : 1");
                _chain[1].Hash = _chain[1].CalculateHashKey_Sha256(); // As well, if you try to create a new Hash Key,
                Console.WriteLine("Is Block valid? \n" + IsChainValid()); // it will not match the next Block previous Hash in record. 
            }
            */
        }

        /// <summary>
        /// Checks if previous Block Data has been changed.
        /// Checks if a Block Hash has been changed
        /// </summary>
        /// <returns></returns>
        public bool IsChainValid()
        {
            for (int index = 1; index < _chain.Count; index++)
            {
                Block currentBlock = _chain[index];
                Block previousBlock = _chain[index - 1];
                //if (currentBlock.Hash != currentBlock.CalculateHashKey_Sha256())
                // if (currentBlock.Hash != currentBlock.MiningBlock(_difficulty))
                //{ return false; }
                if (currentBlock.PrevHash != previousBlock.Hash)
                { return false; }
            }
            return true;
        }

    }
}
