using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace GeneticAlgorithmExample
{
    public static class PenaltyFunctions
    {
        public static int PenaltyForFourthGrade(List<int> genes)
        {
            var lessonQuotients = new Dictionary<int, int>();
            var projectQuotients = new Dictionary<int, int>();

            int penalty = 0;

            foreach (var indexInChromosome in Data.FourthGradeAndIndexes)
            {
                var gen = genes[indexInChromosome];
                int quotient = gen / 9;

                if (lessonQuotients.ContainsKey(quotient))
                    lessonQuotients[quotient]++;
                else
                    lessonQuotients.Add(quotient, 1);
            }

            foreach (var indexInChromosome in Data.ProjectIndexes)
            {
                var gen = genes[indexInChromosome];
                int quotient = gen / 9;

                if (projectQuotients.ContainsKey(quotient))
                    projectQuotients[quotient]++;
                else
                    projectQuotients.Add(quotient, 1);
            }

            var intersectionKeys = projectQuotients
                .Keys
                .Intersect(lessonQuotients.Keys)
                .ToList();

            foreach (var key in intersectionKeys)
            {
                if (lessonQuotients[key] + projectQuotients[key] > 3)
                    penalty += 5;
            }

            var lessonKeys = lessonQuotients
                 .Keys
                 .Except(projectQuotients.Keys)
                 .ToList();

            foreach (var key in lessonKeys)
            {
                if (lessonQuotients[key] > 2)
                    penalty += 5;
            }

            var projectKeys = projectQuotients
                   .Keys
                   .Except(lessonQuotients.Keys)
                   .ToList();

            foreach (var key in projectKeys)
            {
                if (projectQuotients[key] > 3)
                    penalty += 5;
            }

            return penalty;
        }

        public static int PenaltyForInstructorConflict(List<int> genes)
        {
            int penalty = 0;

            foreach (var indexes in Data.InstructorsAndIndexes.Values)
            {
                penalty += CalculatePenalty(indexes.ToList(), genes);
            }

            return penalty;
        }

        public static int PenaltyForOtherGrades(List<int> genes)
        {
            var penalty = 0;

            foreach (var keyAndIndexes in Data.BranchAndIndexes)
            {
                var key = keyAndIndexes.Key;
                var indexes = keyAndIndexes.Value.ToList();

                penalty += CalculatePenalty(indexes, genes);

                var whichClass = Data
                    .GradesAndIndexes
                    .Where(kv => kv.Value.Contains(indexes[0]))
                    .Select(kv => kv.Key)
                    .First();

                if (key.Contains("AB"))
                {
                    var AKey = whichClass + "A";
                    var indexesA = Data
                        .BranchAndIndexes
                        .Where(kv => kv.Key == AKey)
                        .Select(kv => kv.Value)
                        .First()
                        .ToList();

                    var BKey = whichClass + "B";
                    var indexesB = Data
                        .BranchAndIndexes
                        .Where(kv => kv.Key == BKey)
                        .Select(kv => kv.Value)
                        .First()
                        .ToList();
                    
                    penalty += CalculatePenalty(indexesA, genes) + CalculatePenalty(indexesB, genes);
                }
                else
                {
                    var ABkey = whichClass + "AB";
                    var indexesAB = Data
                        .BranchAndIndexes
                        .Where(kv => kv.Key == ABkey)
                        .Select(kv => kv.Value)
                        .First()
                        .ToList();

                    penalty += CalculatePenalty(indexesAB, genes);
                }
            }

            return penalty;
        }

        public static int PenaltyForInstructorPreferences(List<int> genes)
        {
            int penalty = 0;
            for (int i = 0; i < Data.InstructorAndPreferences.Count; i++)
            {
                if (!Data.InstructorAndPreferences[i].Contains(genes[i]))
                    penalty += 5;
            }
            return penalty;
        }

        private static int CalculatePenalty(List<int> indexes, List<int> genes)
        {
            var quotients = new List<int>();
            var penalty = 0;

            foreach (var indexInChromosome in indexes)
            {
                var gen = genes[indexInChromosome];
                int quotient = gen / 9;

                if (quotients.Contains(quotient))
                    penalty += 5; //penalty
                else
                    quotients.Add(quotient);
            }

            return penalty;
        }
    }
}
