using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;  // StreamWriter için gerekli

namespace GeneticAlgorithmExample
{
    public class GeneticAlgorithm
    {
        private int _geneCount;
        private int _minValue;
        private int _maxValue;
        private double _mutationRate;
        private double _crossoverRate;
        private Func<Chromosome, double> _fitnessFunction;
        private static Random random = new Random();
        private Chromosome _bestSolution;
        private Population _population;
        private int _populationSize;
        private List<int> _availableValues;

        public GeneticAlgorithm(int geneCount,
            int minValue,
            int maxValue,
            double mutationRate,
            double crossoverRate,
            Func<Chromosome, double> fitnessFunction,
            int populationSize)
        {
            _geneCount = geneCount;
            _minValue = minValue;
            _maxValue = maxValue;
            _mutationRate = mutationRate;
            _crossoverRate = crossoverRate;
            _fitnessFunction = fitnessFunction;
            _bestSolution = new Chromosome([]);
            _bestSolution.Fitness = int.MinValue;
            _populationSize = populationSize;
            _population = new Population(_populationSize, geneCount, minValue, maxValue);

            _availableValues = new List<int>();

            for (int i = 0; i <= _maxValue; i++)
            {
                _availableValues.Add(i);
            }
        }

        private void EvaluateFitness(Population population)
        {
            foreach (var chromosome in population.Chromosomes)
            {
                chromosome.Fitness = 100 - _fitnessFunction(chromosome);
            }

            var maxFitness = population.Chromosomes
                .Max(c => c.Fitness);

            if (maxFitness > _bestSolution.Fitness)
            {
                var bestSolution = population.Chromosomes
                    .Where(c => c.Fitness == maxFitness)
                    .FirstOrDefault();
                _bestSolution = bestSolution.Clone();
            }
        }

        private List<Chromosome> SelectMatingPool(Population population)
        {
            var rankedPopulation = population
                .Chromosomes
                .OrderByDescending(x => x.Fitness)
                .ToList();

            double totalRank = _populationSize * (_populationSize + 1) / 2.0;

            List<double> selectionProbabilities = rankedPopulation
                .Select((x, rank) => (_populationSize - rank) / totalRank)
                .ToList();

            List<double> cumulativeProbabilities = new List<double>();
            double cumulativeSum = 0;
            foreach (var prob in selectionProbabilities)
            {
                cumulativeSum += prob;
                cumulativeProbabilities.Add(cumulativeSum);
            }

            var matingPool = new List<Chromosome>();

            for (int i = 0; i < _populationSize; i++)
            {
                double randomValue = random.NextDouble();

                for (int j = 0; j < cumulativeProbabilities.Count; j++)
                {
                    if (randomValue <= cumulativeProbabilities[j])
                    {
                        matingPool.Add(rankedPopulation[j]);
                        break;
                    }
                }
            }

            return matingPool;
        }

        private void Crossover(List<Chromosome> offspring)
        {
            int index1, index2, start, end;

            for (int i = 0; i < offspring.Count; i++)
            {
                if (random.NextDouble() < _crossoverRate)
                {
                    index1 = random.Next(0, _geneCount);
                    index2 = random.Next(0, _geneCount);

                    start = Math.Min(index1, index2);
                    end = Math.Max(index1, index2);

                    Array.Reverse(offspring[i].Genes, start, end - start + 1);
                }
            }
        }

        private void Mutate(List<Chromosome> offspring)
        {
            double randomNumber;
            List<int> unusedValidValues;
            int index;

            foreach (var chromosome in offspring)
            {
                for (int i = 0; i < _geneCount; i++)
                {
                    randomNumber = random.NextDouble();
                    if (randomNumber < _mutationRate)
                    {
                        unusedValidValues = ValidIntersectionValues(chromosome, i);
                        if (unusedValidValues.Count != 0)
                        {
                            if (i < Data.InstructorAndPreferences.Count)
                            {
                                var instructorPreference = ValidInstructorPreferences(Data.InstructorAndPreferences[i], unusedValidValues);

                                if (instructorPreference != -1)
                                    chromosome.Genes[i] = instructorPreference;
                            }
                            else
                            {
                                index = random.Next(unusedValidValues.Count);
                                chromosome.Genes[i] = unusedValidValues[index];
                            }
                        }
                    }
                }
            }
        }

        private List<Chromosome> CrateNextGeneration(List<Chromosome> populationChromosomes,
            List<Chromosome> matingPool)
        {
            Crossover(matingPool);

            Mutate(matingPool);

            populationChromosomes.AddRange(matingPool);

            var nextGenerationChromosomes = populationChromosomes
                .Distinct()
                .OrderByDescending(c => c.Fitness)
                .Take(_populationSize)
                .ToList();

            return nextGenerationChromosomes;
        }

        private List<int> ValidIntersectionValues(Chromosome chromosome, int index)
        {
            var validInstructorValues = ValidValues(chromosome, index, Data.InstructorsAndIndexes).indexes;

            List<int> validGradeValues;
            var maxIndex = Data
                .GradesAndIndexes
                .Where(kv => kv.Key == "3")
                .Select(kv => kv.Value)
                .First()
                .Max();

            if (index <= maxIndex)
            {
                var branchValidValuesAndKey = ValidValues(chromosome, index, Data.BranchAndIndexes);

                var key = branchValidValuesAndKey.key;
                var branchValidValues = branchValidValuesAndKey.indexes;

                var notConflictValidValues = ValidNotConflictValues(chromosome, index, key);

                validGradeValues = branchValidValues
                    .Intersect(notConflictValidValues)
                    .ToList();
            }
            else
                validGradeValues = ValidValuesForFourthGrade(chromosome, index, Data.FourthGradeAndIndexes);

            return validInstructorValues
                .Intersect(validGradeValues)
                .Except(chromosome.Genes)
                .ToList();
        }

        private List<int> ValidValuesForFourthGrade(Chromosome chromosome, int index, List<int> fourthGradeAndIndexes)
        {
            var unavailableQuotients = new List<int>();

            var lessonQuotients = QuotientCounter(chromosome, Data.FourthGradeAndIndexes);
            var projectQuotients = QuotientCounter(chromosome, Data.ProjectIndexes);

            var intersectionKeys = projectQuotients
                .Keys
                .Intersect(lessonQuotients.Keys)
                .ToList();

            foreach (var key in intersectionKeys)
            {
                if (lessonQuotients[key] + projectQuotients[key] >= 3)
                    unavailableQuotients.Add(key);
            }

            var lessonKeys = lessonQuotients
                 .Keys
                 .Except(projectQuotients.Keys)
                 .ToList();

            foreach (var key in lessonKeys)
            {
                if (lessonQuotients[key] >= 2)
                    unavailableQuotients.Add(key);
            }

            var projectKeys = projectQuotients
               .Keys
               .Except(lessonQuotients.Keys)
               .ToList();

            foreach (var key in projectKeys)
            {
                if (projectQuotients[key] >= 3)
                    unavailableQuotients.Add(key);
            }

            var validValues = _availableValues
                .Where(v => !unavailableQuotients.Contains(v / 9))
                .ToList();

            return validValues;
        }

        private static Dictionary<int, int> QuotientCounter(Chromosome chromosome, List<int> indexes)
        {
            var quotientCount = new Dictionary<int, int>();

            foreach (var indexInChromosome in indexes)
            {
                var gen = chromosome.Genes[indexInChromosome];
                int quotient = gen / 9;

                if (quotientCount.ContainsKey(quotient))
                    quotientCount[quotient]++;
                else
                    quotientCount.Add(quotient, 1);
            }

            return quotientCount;
        }

        private (string key, List<int> indexes) ValidValues(Chromosome chromosome, int index, Dictionary<string, int[]> keyValuePairs)
        {
            var indexesAndKey = keyValuePairs
                .Where(kv => kv.Value.Contains(index))
                .First();

            var key = indexesAndKey.Key;
            var indexes = indexesAndKey.Value.ToList();

            var quotients = new List<int>();

            foreach (var listIndex in indexes)
            {
                var indexValue = chromosome.Genes[listIndex];
                var quotient = indexValue / 9;

                if (!quotients.Contains(quotient))
                    quotients.Add(quotient);
            }

            var validValues = _availableValues
                .Where(v => !quotients.Contains(v / 9))
                .ToList();

            return (key, validValues);
        }

        private List<int> ValidNotConflictValues(Chromosome chromosome, int index, string key)
        {
            List<int> indexes = new List<int>();

            var whichClass = Data
                .GradesAndIndexes
                .Where(kv => kv.Value.Contains(index))
                .Select(kv => kv.Key)
                .First();

            if (key.Contains("AB"))
            {
                var aClass = whichClass + 'A';
                var aIndexes = Data
                    .BranchAndIndexes
                    .Where(kv => kv.Key == aClass)
                    .Select(kv => kv.Value)
                    .First()
                    .ToList();

                indexes.AddRange(aIndexes);

                var bClass = whichClass + 'B';
                var bIndexes = Data
                    .BranchAndIndexes
                    .Where(kv => kv.Key == bClass)
                    .Select(kv => kv.Value)
                    .First()
                    .ToList();

                indexes.AddRange(bIndexes);
            }
            else
            {
                whichClass += "AB";

                indexes = Data
                    .BranchAndIndexes
                    .Where(kv => kv.Key == whichClass)
                    .Select(kv => kv.Value)
                    .First()
                    .ToList();
            }

            var quotients = new List<int>();

            foreach (var listIndex in indexes)
            {
                var indexValue = chromosome.Genes[listIndex];
                var quotient = indexValue / 9;

                if (!quotients.Contains(quotient))
                    quotients.Add(quotient);
            }

            var validValues = _availableValues
                .Where(v => !quotients.Contains(v / 9))
                .ToList();

            return validValues;
        }

        private int ValidInstructorPreferences(List<int> instructorPreferences, List<int> unusedValidValues)
        {
            foreach (var preference in instructorPreferences)
            {
                if (unusedValidValues.Contains(preference))
                    return preference;
            }
            return -1;
        }

        public void DriveGA(int generations)
        {

            double maxMutationRate = 0.8;
            double minMutationRate = 0.6;

            double maxCrossoverRate = 0.75;
            double minCrossoverRate = 0.1;


            for (int generation = 0; generation < generations; generation++)
            {
                EvaluateFitness(_population);/*
                foreach (var chromosome in _population.Chromosomes)
                {
                    Console.WriteLine($"Fitness = {chromosome.Fitness}, Genes = [{string.Join(", ", chromosome.Genes)}]");
                }*/
                var bestChromosome = _population.Chromosomes.OrderByDescending(c => c.Fitness).First();
                Console.WriteLine($"Generation {generation + 1}: Best Fitness = {bestChromosome.Fitness}, Genes = [{string.Join(", ", bestChromosome.Genes)}]");

                var meanFitness = _population
                    .Chromosomes
                    .Select(c => c.Fitness)
                    .Average();

                Console.WriteLine($"Generation {generation + 1}: Mean Fitness = {meanFitness}");

                var matingPool = SelectMatingPool(_population);
                _population.Chromosomes = CrateNextGeneration(_population.Chromosomes, matingPool);

                _mutationRate = minMutationRate + (maxMutationRate - minMutationRate) * (1 - (double)generation / generations);
                _crossoverRate = maxCrossoverRate - (maxCrossoverRate - minCrossoverRate) * (1 - (double)generation / generations);

            }

            Console.WriteLine($"Best Fitness of All Generations = {_bestSolution.Fitness}, Genes = [{string.Join(", ", _bestSolution.Genes)}]");
            Console.WriteLine("Genetic Algorithm Completed.");
        }



public void ShowBestSolution()
    {
        // 1) "scheduleValues" sözlüğü: 
        //    hangi "gün + sınıf" kombinasyonunun hangi gene değeri ile eşlendiğini tutuyor.
        Dictionary<string, int> scheduleValues = new Dictionary<string, int>();

        for (int day = 0; day < Data.days.Count; day++)
        {
            for (int classroom = 0; classroom < Data.classrooms.Count; classroom++)
            {
                // Örnek key: "Pazartesi A1" -> day * #classrooms + classroom
                scheduleValues[$"{Data.days[day]} {Data.classrooms[classroom]}"]
                    = day * Data.classrooms.Count + classroom;
            }
        }

        // 2) Genetik algoritmanın ürettiği en iyi çözümdeki genlerin 
        //    takvim üzerinde hangi "gün + sınıf" değerine karşılık geldiğini buluyoruz.
        var sessions = new List<string>();
        foreach (var geneValue in _bestSolution.Genes)
        {
            // geneValue’yu, sözlükte hangi "gün + sınıf" eşlemiş bul
            // (Key = "Pazartesi A1", Value = integer).
            var session = scheduleValues
                .Where(kv => kv.Value == geneValue)
                .Select(kv => kv.Key)
                .First();

            sessions.Add(session);
        }

        // 3) Çıktıyı CSV formatında bir dosyaya yazdıralım.
        //    Örneğin "DersProgrami.csv" adında bir dosya oluşturuyoruz.
        string filePath = "DersProgrami.csv";
        using (var writer = new StreamWriter(filePath))
        {
            // -------------------------------------------------------------
            // CSV başlık satırını yazalım.
            // Burada 6 sütun kullanacağız: 
            // (1) Ders Kodu 
            // (2) Öğretim Üyesi 
            // (3) Grup 
            // (4) Süre 
            // (5) Ek Bilgi 
            // (6) Atanmış Zaman-Mekan
            // -------------------------------------------------------------
            writer.WriteLine("Ders Kodu;Öğretim Üyesi;Grup;Süre;Ek Bilgi;Atanmış Zaman-Mekan");

            // 4) Her bir ders (Data.schedule.Keys) için o derse denk gelen
            //    atama bilgisini (sessions[i]) CSV formatında yazacağız.
            int i = 0;
            foreach (var schedule in Data.schedule.Keys)
            {
                // Örnek schedule:
                // "BLM112 Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 2 saat -> Pazartesi 08:30 - 10:30 A2"
                // Bu metni Regex veya String.Split gibi yöntemlerle parçalara ayıracağız.
                // Aşağıdaki Regex ve Split örnek amaçlıdır; kendi formatınıza göre düzenlemeniz gerekebilir.

                // 1) Ders kodu (örneğin BLM112) -> genelde ilk boşluk öncesi
                // 2) Öğretim Üyesi (örneğin "Dr. Öğr. Üyesi Tarık Yerlikaya")
                // 3) Grup (örneğin "(1AB)")
                // 4) Süre (örneğin "2 saat")
                // 5) Ek bilgi (örneğin "-> Pazartesi 08:30 - 10:30 A2")

                // NOT: Bu kısım verinizin yapısına göre özelleştirilmeli!
                // Basit bir Regex örneği kurgulayalım:
                //  ^(\S+)                      -> Ders kodu: İlk boşluğa dek (\S+)
                //  \s+(Dr.*?)(\(\d.*?\))?      -> "Öğretim Üyesi" ve parantez içi grup(isteğe bağlı)
                //  (.*?)                       -> Kalan her şey
                //  ->\s*(.+)$                  -> "->" den sonraki kısım
                // Bu, tamamen örnek bir yaklaşımdır. Verinizde ufak tefek farklılıklar varsa Regex değişmeli.

                // Kolaylık olsun diye şimdilik bir "yaygın format" kabul edelim.
                // Arama yaparken ufak bir string manipülasyonu deneyelim.
                // Belli parçaları bulmak için approach: "Split" + el ile ayıklama.

                // Örnek yaklaşım:
                // 1) "->" işaretine böl
                // 2) Sol taraf -> "BLM112 Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 2 saat"
                // 3) Sağ taraf -> "Pazartesi 08:30 - 10:30 A2"

                var parts = schedule.Split(new string[] { "->" }, StringSplitOptions.None);
                string leftSide = parts.Length > 0 ? parts[0].Trim() : "";
                string rightSide = parts.Length > 1 ? parts[1].Trim() : "";

                // leftSide: "BLM112 Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 2 saat"
                // rightSide: "Pazartesi 08:30 - 10:30 A2"

                // leftSide'daki "Ders kodu", "Öğretim üyesi", "Grup" ve "Süre" gibi bilgileri parçalıyoruz.
                // Basit bir yaklaşımla, ders kodunun ilk boşluğa kadar olan kısmı olduğunu varsayalım:
                string[] leftParts = leftSide.Split(' ');
                string dersKodu = leftParts[0]; // Örn. "BLM112"

                // Şimdi geriye kalan kısımda "Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 2 saat" var.
                // Bunu biraz daha detaylı parse edebiliriz. Örneğin:
                // - Grup bilgisi -> genellikle parantez içinde ise Regex ile alabiliriz.
                // - Süre -> genelde "1 saat" veya "2 saat" gibi bir string
                // - Öğretim üyesi -> geri kalan kısım

                // Bir Regex yapalım:
                // ^(.*?)             -> "Öğretim üyesi" alanı (her şeyi yakala, ancak aç parantezden önce durdurabiliriz)
                // \((.*?)\)          -> parantez içi "grup"
                // \s+(.*?)\s*saat    -> "x saat"
                Regex pattern = new Regex(@"^(.*?)\s*\((.*?)\)\s+(\d+)\s*saat");
                // Group1: Öğretim üyesi
                // Group2: Grup
                // Group3: Süre (rakam)

                // Not: Tabii ki bu yine verinin aynı formatta olduğu varsayımıyla çalışır.
                // Elde ettiğimiz match ile parçalara ayırabiliriz.
                var match = pattern.Match(leftSide);

                string ogretimUyesi = "";
                string grup = "";
                string sure = "";
                if (match.Success)
                {
                    ogretimUyesi = match.Groups[1].Value.Trim();
                    grup = match.Groups[2].Value.Trim();
                    sure = match.Groups[3].Value + " saat"; // match.Groups[3] => "2", ekliyoruz " saat"
                }
                else
                {
                    // Eşleşme yoksa, fallback: 
                    ogretimUyesi = "Bilinmiyor";
                    grup = "Bilinmiyor";
                    sure = "Bilinmiyor";
                }

                // Ek bilgi => leftSide içinde "->" öncesi her şeyi, 
                // ama biz zaten "->" ile sağ tarafa böldük.
                // Dolayısıyla leftSide'da yazarak karışmasın diye 
                // "Ek Bilgi" => "leftSide" ın geri kalanı demeyelim. 
                // Burada "Ek Bilgi" için isterseniz rightSide'ı tabloya eklemek yerine,
                // "Atanmış Zaman-Mekan" sütununa koyalım.
                // Fakat örneğin "Ek Bilgi" diye 2 saat/1 saat vb. isterseniz 
                // yeniden tasarlayabilirsiniz.

                // Bizim "Atanmış Zaman-Mekan" ise, GA sonucunda elde ettiğimiz 
                // sessions[i] (örn. "Pazartesi A1") 
                // + orijinal "rightSide" (örn. "Pazartesi 08:30 - 10:30 A2").
                // Hangisini tabloya koymak istediğinize siz karar verebilirsiniz.
                // Şimdilik, GA'nın bulduğu atama ("sessions[i]") ile 
                // asıl "rightSide" metnini birleştirip yazabiliriz:
                string gaZamanMekan = $"{sessions[i]} ({rightSide})";

                // Son olarak, CSV formatında yazarız:
                writer.WriteLine(
                    $"{dersKodu};{ogretimUyesi};{grup};{sure};{rightSide};{gaZamanMekan}"
                );

                i++;
            }
        }

        // 5) Artık "DersProgrami.csv" dosyasını, Excel'de açabilirsiniz.
        Console.WriteLine($"Ders programı '{filePath}' dosyasına kaydedildi.");
    }


}
}
