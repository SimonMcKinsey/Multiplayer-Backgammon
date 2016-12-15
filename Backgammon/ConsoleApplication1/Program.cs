using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backgammon;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("hen", "simon");

            string message = game.Roll();
            Console.WriteLine(message);

            Console.WriteLine("turn :" + game.Turn);
            //source
            //try
            //{

            game.ChooseSource(13,out message);
            Console.WriteLine( message);
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            ////destination
            game.ChooseDestination(26, out message);
            try
            {

            Console.WriteLine(message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
