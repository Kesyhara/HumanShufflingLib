using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kesyhara.HumanShuffling.UtilityFunctions;
using MyUtils = Kesyhara.HumanShuffling.UtilityFunctions.Utils;

namespace GaussianDebugging
{
    class Program
    {
        static Random ranGen = new Random();
        static int multiplicand = 30;
        static void Main(string[] args)
        {
            for (int numToGenerate = 20; numToGenerate > 0; numToGenerate--)
                OutputCalculationsForDebugging();

            Pause();
        }

        static void OutputCalculationsForDebugging()
        {
            double gaussianVariable = ranGen.NextGaussian();
            Console.WriteLine("Gaussian Variable: " + gaussianVariable);
            Console.WriteLine("Product: " + (gaussianVariable * multiplicand));
            Console.WriteLine();
        }

        static void Pause()
        {
            Console.ReadLine();
        }
    }
}
