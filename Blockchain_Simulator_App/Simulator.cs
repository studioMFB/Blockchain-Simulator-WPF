using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain_Simulator_App
{
    class Simulator
    {
        Blockchain blockchain = new Blockchain();

        public Simulator()
        {
            int index = 1;
            Console.WriteLine("WELCOME TO THE BLOCKCHAIN SIMULATOR");
            do
            {
                Console.WriteLine("\n Enter a message");
                blockchain.AddBlock(new Block(index, Console.ReadLine()));
                Exit();
                index++;
            } while (true);
        }

        /// <summary>
        /// Ask user if he wants to exit the program.
        /// </summary>
        public void Exit()
        {
            string answer = "";
            do
            {
                Console.WriteLine("\n Do you want to exit? Y/N");
                answer = Console.ReadLine().ToLower();
                if (answer == "y")
                { Environment.Exit(0); }
            } while (answer != "n");
        }

    }
}
