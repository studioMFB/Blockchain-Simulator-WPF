using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blockchain_Simulator_App
{
    /// <summary>
    /// (Simply Explained, 2020)
    /// </summary>
    class Blockchain
    {
        private static List<Block> _chain;

        public List<Block> Chain { get { return _chain; } set { _chain = value; } }
        public static bool IsValidBlock { get; set; }

        public Blockchain()
        {
            _chain = new List<Block>();
            CreateGenesisBlock(); // Adds by default, the very first Block called Genesis, to the Chain.
        }

        /// <summary>
        /// Creates the very first Block called the Genesis.
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="previousHash"></param>
        /// <returns></returns>
        public void CreateGenesisBlock()
        {
            Block genesisBlock = new Block(0, "Genensis Block");
            genesisBlock.MiningBlock(0);
            _chain.Add(genesisBlock); // Adds by default, the very first Block called Genesis, to the Chain.
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
             newBlock.MiningBlock(newBlock.Difficulty);
            newBlock.PrevHash = PreviousHash();
            _chain.Add(newBlock);
            if (IsChainValid())
            { newBlock.Index = Chain.IndexOf(newBlock); }
            else
            { _chain.Remove(newBlock); }
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
                if (currentBlock.PrevHash != previousBlock.Hash)
                {
                    IsValidBlock = false;
                    break;
                }
                else
                {  IsValidBlock = true;  }
            }
            return IsValidBlock;
        }

    }
}
