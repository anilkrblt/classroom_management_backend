namespace GeneticAlgorithmExample
{
    public class Chromosome
    {
        public int[] Genes { get; private set; }
        public double Fitness { get; set; }

        private static Random random = new Random();

        public Chromosome(int geneCount, 
            int minValue, 
            int maxValue)
        {
            Genes = GenerateUniqueRandomNumbers(geneCount, minValue, maxValue);
        }

        private int[] GenerateUniqueRandomNumbers(int geneCount, int minValue, int maxValue)
        {
            var result = new List<int>();

            while (result.Count != geneCount)
            {
                var number = random.Next(minValue, maxValue);

                bool control = result
                    .Any(x => x == number);

                if (control == false)
                    result.Add(number);
            }

            return result.ToArray();
        }

        public Chromosome(int[] genes)
        {
            Genes = genes;
        }

        public Chromosome Clone()
        {
            var result = new Chromosome((int[])Genes.Clone());
            result.Fitness = Fitness;

            return result;
        }
    }
}
