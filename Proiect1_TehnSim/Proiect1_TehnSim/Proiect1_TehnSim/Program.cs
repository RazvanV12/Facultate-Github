using System;

namespace Proiect1_TehnSim
{
    internal class Program
    {

        private static float Conquer_4Nobles(float nrTests)
        {
            float conqueredVillages = 0;
            var random = new Random();
            for (var i = 0; i < nrTests; i++)
            {
                int n1 = random.Next(20, 36), n2 = random.Next(20, 36), n3 = random.Next(20, 36), n4 = random.Next(20, 36);
                if(n1 + n2 + n3 + n4 >= 100)
                    conqueredVillages++;
            }
            return conqueredVillages / nrTests;
        }
        
        private static float Conquer_3Nobles(float nrTests)
        {
            float conqueredVillages = 0;
            var random = new Random();
            for (var i = 0; i < nrTests; i++)
            {
                int n1 = random.Next(20, 36), n2 = random.Next(20, 36), n3 = random.Next(20, 36);
                if(n1 + n2 + n3 >= 100)
                    conqueredVillages++;
            }
            return conqueredVillages / nrTests;
        }
        public static void Main(string[] args)
        {
            // print the result of the simulation
            Console.WriteLine("The probability of conquering a village with 3 nobles is: " + Conquer_3Nobles(1000000));
            Console.WriteLine("The probability of conquering a village with 4 nobles is: " + Conquer_4Nobles(1000000));
        }
    }
}