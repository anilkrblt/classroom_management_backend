using System;

namespace GeneticAlgorithmExample
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            int geneCount = 94;
            int minValue = 0;
            int maxValue = 179;
            int populationSize = 30;
            int generations = 1000;
            double mutationRate = 0.7;
            double crossoverRate = 0.75;
        
            Func<Chromosome, double> fitnessFunction = (chromosome) =>
            {
                return PenaltyFunctions.PenaltyForInstructorConflict(chromosome.Genes.ToList()) +
                PenaltyFunctions.PenaltyForOtherGrades(chromosome.Genes.ToList()) +
                PenaltyFunctions.PenaltyForFourthGrade(chromosome.Genes.ToList()) +
                PenaltyFunctions.PenaltyForInstructorPreferences(chromosome.Genes.ToList());
            };

            var ga = new GeneticAlgorithm(geneCount, minValue, maxValue, mutationRate, 
                crossoverRate, fitnessFunction, populationSize); 

            ga.DriveGA(generations);
            ga.ShowBestSolution();
        }
    }
}
