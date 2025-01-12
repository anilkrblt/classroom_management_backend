namespace GeneticAlgorithmExample
{
    public class Population
    {
        public List<Chromosome> Chromosomes { get;  set; }

        public Population(int size, int geneCount, int minValue, int maxValue)
        {
            Chromosomes = new List<Chromosome>();

            for (int i = 0; i < size; i++)
            {
                Chromosomes.Add(new Chromosome(geneCount, minValue, maxValue));
            }
        }
    }
}
