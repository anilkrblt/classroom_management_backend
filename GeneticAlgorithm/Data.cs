namespace GeneticAlgorithmExample
{
    public static class Data
    {
        // 9 * periyot indisi + derslik indisi = ilk indisteki hocanın tercihe karşılık geliyo
        public static List<List<int>> InstructorAndPreferences = new List<List<int>>
        {
            new List<int>()
            {7},
            new List<int>()
            {16},
            new List<int>()
            {169},
            new List<int>()
            {36},
            new List<int>()
            {86},
            new List<int>()
            {45},
            new List<int>()
            {77},
            new List<int>()
            {0},
            new List<int>()
            {14},
            new List<int>()
            {9},
            new List<int>()
            {95},
            new List<int>()
            {53},
            new List<int>()
            {131},
            new List<int>()
            {62},
            new List<int>()
            {113},
            new List<int>()
            {26},
            new List<int>()
            {35},
            new List<int>()
            {79},
            new List<int>()
            {88},
            new List<int>()
            {122},
            new List<int>()
            {150},
            new List<int>()
            {159},
            new List<int>()
            {42}
        };

        public static Dictionary<string, int[]> InstructorsAndIndexes = new Dictionary<string, int[]>
        {
            {"Dr. Öğr. Üyesi Andaç Şahin Mesut" , [3, 4, 5, 6, 7, 8, 62, 63]},
            {"Dr. Öğr. Üyesi Cem Taşkın" , [2, 54, 55, 64, 65]},
            {"Dr. Öğr. Üyesi Rembiye Kandemir" , [9, 10, 58, 59, 60, 61]},
            {"Prof. Dr. Muharrem Tolga Sakallı" , [15, 16, 66, 67]},
            {"Dr. Öğr. Üyesi Aydın Carus" , [11, 12, 13, 14, 68, 69]},
            {"Dr. Öğr. Üyesi Deniz Taşkın" , [17, 18, 19, 46, 47, 70, 71]},
            {"Dr. Öğr. Üyesi Altan Mesut" , [30, 31, 32, 33, 72, 73]},
            {"Dr. Öğr. Üyesi Özlem Aydın Fidan" , [34, 35, 36, 37, 74, 75]},
            {"Dr. Öğr. Üyesi Emir Öztürk" , [52, 53, 76, 77]},
            {"Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı" , [24, 25, 26, 27, 28, 29, 78, 79]},
            {"Dr. Öğr. Üyesi Turgut Doğan" , [38, 39, 40, 41, 80, 81]},
            {"Dr. Öğr. Üyesi Özlem Uçar" , [48, 49, 50, 51, 82, 83]},
            {"Dr. Öğr. Üyesi Derya Alsancak Arda" , [42, 43, 44, 45, 84, 85]},
            {"Doç. Dr. Deniz Mertkan Gezgin" , [56, 57, 86, 87]},
            {"Dr. Öğr. Üyesi Tarık Yerlikaya" , [0, 1, 20, 21, 88, 89]},
            {"Öğr. Gör. Evla Yürükler" , [22, 23]},
            {"Arş. Gör. Dr. Işıl Çetintav",  [90, 91]},
            {"Arş. Gör. Dr. Ümit Can Kumdereli" , [92, 93]}
        };

        // şube indisi
        public static Dictionary<string, int[]> BranchAndIndexes = new Dictionary<string, int[]>
        {
            {"1A" , [3, 4]},
            {"1B" , [5, 6]},
            {"1AB" , [0,1,2]},
            {"2A" , [7, 8, 11, 12]},
            {"2B" , [9, 10, 13, 14]},
            {"2AB" , [15, 16, 17, 18, 19, 20, 21, 22]},
            {"3A" , [24, 25, 30, 31]},
            {"3B" , [26, 27, 32, 33]},
            {"3AB" , [23, 28, 29, 34, 35, 36, 37, 38, 39]},
        };
        // 1,2,3. sınıf dersi indisi
        public static Dictionary<string, int[]> GradesAndIndexes = new Dictionary<string, int[]>
        {
            {"1" , [0, 1, 2, 3, 4, 5, 6]},
            {"2" , [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22]},
            {"3" , [23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39]}
        };
        // 4. sınıf dersi indisleri
        public static List<int> FourthGradeAndIndexes = new List<int>
        {
            40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
            50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };
        // proje dersi indisleri
        public static List<int> ProjectIndexes = new List<int>
        {
            60, 61, 62, 63, 64, 65, 66, 67, 68, 69,
            70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
            80, 81, 82, 83, 84, 85, 86, 87, 88, 89,
            90, 91, 92, 93
        };


        public static Dictionary<string, string> schedule = new Dictionary<string, string>()
            {
                {"BLM112 Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 2 saat", "Pazartesi 08:30 - 10:30 S1"},
                {"BLM112 Dr. Öğr. Üyesi Tarık Yerlikaya (1AB) 1 saat", "Pazartesi 10:30 - 12:30 S1"},
                {"BLM113 Dr. Öğr. Üyesi Cem Taşkın (1AB) 1 saat", "Cuma 13:30 - 15:30 A2"},
                {"BLM111 Dr. Öğr. Üyesi Andaç Şahin Mesut Bilgisayar Mühendisliğine Giriş (1A) 2 saat Teori","Salı 15:30 - 17:30 D103" },
                {"BLM111 Dr. Öğr. Üyesi Andaç Şahin Mesut Bilgisayar Mühendisliğine Giriş (1A) 2 saat Uygulama","Çarşamba 13:30 - 15:30 D103"},
                {"BLM111 Dr. Öğr. Üyesi Andaç Şahin Mesut Bilgisayar Mühendisliğine Giriş (1B) 2 saat Teori", "Salı 13:30 - 15:30 D103"},
                {"BLM111 Dr. Öğr. Üyesi Andaç Şahin Mesut Bilgisayar Mühendisliğine Giriş (1B) 2 saat Uygulama", "Çarşamba 15:30 - 17:30 D103"},
                {"BLM212 Dr. Öğr. Üyesi Andaç Şahin Mesut Birleştirici Dil ile Programlama (2A) 2 saat Teori", "Pazartesi 10:30 - 12:30 L206"},
                {"BLM212 Dr. Öğr. Üyesi Andaç Şahin Mesut Birleştirici Dil ile Programlama (2A) 2 saat Uygulama", "Salı 10:30 - 12:30 L208"},
                {"BLM212 Dr. Öğr. Üyesi Rembiye Kandemir Birleştirici Dil ile Programlama (2B) 2 saat Teori", "Pazartesi 10:30 - 12:30 D103"},
                {"BLM212 Dr. Öğr. Üyesi Rembiye Kandemir Birleştirici Dil ile Programlama (2B) 2 saat Uygulama","Pazartesi 13:30 - 15:30 L208"},
                {"BLM211 Dr. Öğr. Üyesi Aydın Carus C ile Programlama (2A) 2 saat Teori", "Salı 13:30 - 15:30 S1"},
                {"BLM211 Dr. Öğr. Üyesi Aydın Carus C ile Programlama (2A) 2 saat Uygulama",  "Cuma 15:30 - 17:30 L208"},
                {"BLM211 Dr. Öğr. Üyesi Aydın Carus C ile Programlama (2B) 2 saat Teori", "Salı 10:30 - 12:30 L206"},
                {"BLM211 Dr. Öğr. Üyesi Aydın Carus C ile Programlama (2B) 2 saat Uygulama", "Perşembe 10:30 - 12:30 L208"},
                {"BLM216 Prof. Dr. Muharrem Tolga Sakallı (2AB) 2 saat", "Pazartesi 13:30 - 15:30 A1"},
                {"BLM216 Prof. Dr. Muharrem Tolga Sakallı (2AB) 1 saat", "Pazartesi 15:30 - 17:30 A1"},
                {"BLM213 Dr. Öğr. Üyesi Deniz Taşkın (2AB) 2 saat Teori", "Çarşamba 08:30 - 10:30 A1"},
                {"BLM213 Dr. Öğr. Üyesi Deniz Taşkın (2AB) 1 saat Teori", "Çarşamba 10:30 - 12:30 A1"},
                {"BLM214 Dr. Öğr. Üyesi Deniz Taşkın (2AB) 2 saat Uygulama", "Cuma 10:30 - 12:30 A1"},
                {"BLM215 Dr. Öğr. Üyesi Tarık Yerlikaya (2AB) 2 saat Teori", "Perşembe 13:30 - 15:30 A1"},
                {"BLM215 Dr. Öğr. Üyesi Tarık Yerlikaya (2AB) 2 saat Uygulama", "Perşembe 15:30 - 17:30 A1"},
                {"BLM217 Öğr. Gör. Evla Yürükler 2 saat (2AB)", "Salı 08:30 - 10:30 D103"},
                {"BLM317 Öğr. Gör. Evla Yürükler (3AB) 2 saat", "Salı 10:30 - 12:30 D103"},
                {"BLM316 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı Elektrik Devre Temelleri Laboratuvarı (3A) 2 saat", "Perşembe 13:30 - 15:30 L202"},
                {"BLM316 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı Elektrik Devre Temelleri Laboratuvarı (3A) 1 saat", "Perşembe 15:30 - 17:30 L202"},
                {"BLM316 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı Elektrik Devre Temelleri Laboratuvarı (3B) 2 saat", "Pazartesi 13:30 - 15:30 L202"},
                {"BLM316 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı Elektrik Devre Temelleri Laboratuvarı (3B) 1 saat", "Pazartesi 15:30 - 17:30 L202"},
                {"BLM311 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı (3AB) 2 saat", "Salı 13:30 - 15:30 L206"},
                {"BLM311 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı (3AB) 1 saat", "Salı 15:30 - 17:30 L206"},
                {"BLM315 Dr. Öğr. Üyesi Altan Mesut (3A) 2 saat", "Pazartesi 08:30 - 10:30 D103"},
                {"BLM315 Dr. Öğr. Üyesi Altan Mesut (3A) 1 saat", "Pazartesi 10:30 - 12:30 D103"},
                {"BLM315 Dr. Öğr. Üyesi Altan Mesut (3B) 2 saat", "Perşembe 13:30 - 15:30 L206"},
                {"BLM315 Dr. Öğr. Üyesi Altan Mesut (3B) 1 saat", "Perşembe 15:30 - 17:30 L206"},
                {"BLM312 Dr. Öğr. Üyesi Özlem Aydın Fidan (3AB) 2 saat", "Pazartesi 13:30 - 15:30 A2"},
                {"BLM312 Dr. Öğr. Üyesi Özlem Aydın Fidan (3AB) 1 saat", "Pazartesi 15:30 - 17:30 A2"},
                {"BLM313 Dr. Öğr. Üyesi Özlem Aydın Fidan (3AB) 2 saat", "Cuma 08:30 - 10:30 D103"},
                {"BLM313 Dr. Öğr. Üyesi Özlem Aydın Fidan (3AB) 1 saat", "Cuma 10:30 - 12:30 D103"},
                {"BLM314 Dr. Öğr. Üyesi Turgut Doğan (3AB) 2 saat", "Perşembe 08:30 - 10:30 A1"},
                {"BLM314 Dr. Öğr. Üyesi Turgut Doğan (3AB) 1 saat", "Perşembe 10:30 - 12:30 A1"},
                {"BLM4107 Dr. Öğr. Üyesi Turgut Doğan (4AB) 2 saat", "Salı 13:30 - 15:30 L208"},
                {"BLM4107 Dr. Öğr. Üyesi Turgut Doğan (4AB) 1 saat", "Salı 15:30 - 17:30 L208"},
                {"BLM4104 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 2 saat", "Salı 13:30 - 15:30 L202"},
                {"BLM4104 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 1 saat", "Salı 15:30 - 17:30 L202"},
                {"BLM4106 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 2 saat", "Çarşamba 13:30 - 15:30 S1"},
                {"BLM4106 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 1 saat", "Çarşamba 15:30 - 17:30 S1"},
                {"BLM4119 Dr. Öğr. Üyesi Deniz Taşkın (4AB) 2 saat", "Pazartesi 08:30 - 10:30 L208"},
                {"BLM4119 Dr. Öğr. Üyesi Deniz Taşkın (4AB) 1 saat", "Pazartesi 10:30 - 12:30 L208"},
                {"BLM4101 Dr. Öğr. Üyesi Özlem Uçar (4AB) 2 saat", "Perşembe 13:30 - 15:30 S1"},
                {"BLM4101 Dr. Öğr. Üyesi Özlem Uçar (4AB) 1 saat", "Perşembe 15:30 - 17:30 S1"},
                {"BLM4114 Dr. Öğr. Üyesi Özlem Uçar (4AB) 2 saat",  "Salı 08:30 - 10:30 S1"},
                {"BLM4114 Dr. Öğr. Üyesi Özlem Uçar (4AB) 1 saat",  "Salı 10:30 - 12:30 S1"},
                {"BLM4121 Dr. Öğr. Üyesi Emir Öztürk (4AB) 2 saat", "Çarşamba 13:30 - 15:30 L208"},
                {"BLM4121 Dr. Öğr. Üyesi Emir Öztürk (4AB) 1 saat", "Çarşamba 15:30 - 17:30 L208"},
                {"BLM4116 Dr. Öğr. Üyesi Cem Taşkın (4AB) 2 saat", "Pazartesi 13:30 - 15:30 L201"},
                {"BLM4116 Dr. Öğr. Üyesi Cem Taşkın (4AB) 1 saat", "Pazartesi 15:30 - 17:30 L201"},
                {"BLM4120 Doç. Dr. Deniz Mertkan Gezgin (4AB) 1 saat", "Cuma 13:30 - 15:30 S1"},
                {"BLM4120 Doç. Dr. Deniz Mertkan Gezgin (4AB) 2 saat", "Cuma 15:30 - 17:30 S1"},
                {"BLM4110 Dr. Öğr. Üyesi Rembiye Kandemir (4AB) 2 saat", "Perşembe 13:30 - 15:30 L208"},
                {"BLM4110 Dr. Öğr. Üyesi Rembiye Kandemir (4AB) 1 saat", "Perşembe 15:30 - 17:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Rembiye Kandemir (4AB) 2 saat","Çarşamba 08:30 - 10:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Rembiye Kandemir (4AB) 2 saat ","Çarşamba 10:30 - 12:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Andaç Şahin Mesut (4AB) 2 saat","Pazartesi 13:30 - 15:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Andaç Şahin Mesut (4AB) 2 saat ","Pazartesi 15:30 - 17:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Cem Taşkın (4AB) 2 saat","Cuma 08:30 - 10:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Cem Taşkın (4AB) 2 saat ","Cuma 10:30 - 12:30 L201"},
                {"BLM412 Proje 1 Prof. Dr. Muharrem Tolga Sakallı (4AB) 2 saat","Çarşamba 13:30 - 15:30 L201"},
                {"BLM412 Proje 1 Prof. Dr. Muharrem Tolga Sakallı (4AB) 2 saat ","Çarşamba 15:30 - 17:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Aydın Carus (4AB) 2 saat","Çarşamba 08:30 - 10:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Aydın Carus (4AB) 2 saat ","Çarşamba 10:30 - 12:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Deniz Taşkın (4AB) 2 saat","Perşembe 08:30 - 10:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Deniz Taşkın (4AB) 2 saat ","Perşembe 10:30 - 12:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Altan Mesut (4AB) 2 saat","Cuma 08:30 - 10:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Altan Mesut (4AB) 2 saat ","Cuma 10:30 - 12:30 L208"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Özlem Aydın Fidan (4AB) 2 saat","Salı 13:30 - 15:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Özlem Aydın Fidan (4AB) 2 saat ","Salı 15:30 - 17:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Emir Öztürk (4AB) 2 saat","Perşembe 13:30 - 15:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Emir Öztürk (4AB) 2 saat ", "Perşembe 15:30 - 17:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı (4AB) 2 saat","Çarşamba 08:30 - 10:30 D103"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Fatma Büyüksaraçoğlu Sakallı (4AB) 2 saat ","Çarşamba 10:30 - 12:30 D103"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Turgut Doğan (4AB) 2 saat","Pazartesi 08:30 - 10:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Turgut Doğan (4AB) 2 saat ","Pazartesi 10:30 - 12:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Özlem Uçar (4AB) 2 saat","Pazartesi 13:30 - 15:30 D103"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Özlem Uçar (4AB) 2 saat ","Pazartesi 15:30 - 17:30 D103"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 2 saat","Pazartesi 08:30 - 10:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Derya Alsancak Arda (4AB) 2 saat ","Pazartesi 10:30 - 12:30 L207"},
                {"BLM412 Proje 1 Doç. Dr. Deniz Mertkan Gezgin (4AB) 2 saat","Salı 08:30 - 10:30 L207"},
                {"BLM412 Proje 1 Doç. Dr. Deniz Mertkan Gezgin (4AB) 2 saat ","Salı 10:30 - 12:30 L207"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Tarık Yerlikaya (4AB) 2 saat","Salı 08:30 - 10:30 L201"},
                {"BLM412 Proje 1 Dr. Öğr. Üyesi Tarık Yerlikaya (4AB) 2 saat ","Salı 10:30 - 12:30 L201"},
                {"BLM412 Proje 1 Arş. Gör. Dr. Işıl Çetintav (4AB) 2 saat","Perşembe 08:30 - 10:30 D103"},
                {"BLM412 Proje 1 Arş. Gör. Dr. Işıl Çetintav (4AB) 2 saat ","Perşembe 10:30 - 12:30 D103"},
                {"BLM412 Proje 1 Arş. Gör. Dr. Ümit Can Kumdereli (4AB) 2 saat","Perşembe 08:30 - 10:30 L201"},
                {"BLM412 Proje 1 Arş. Gör. Dr. Ümit Can Kumdereli (4AB) 2 saat ","Perşembe 10:30 - 12:30 L201"}
            };

        public static List<string> days = new List<string>
            {
                "Pazartesi 08:30 - 10:30",
                "Pazartesi 10:30 - 12:30",
                "Pazartesi 13:30 - 15:30",
                "Pazartesi 15:30 - 17:30",
                "Salı 08:30 - 10:30",
                "Salı 10:30 - 12:30",
                "Salı 13:30 - 15:30",
                "Salı 15:30 - 17:30",
                "Çarşamba 08:30 - 10:30",
                "Çarşamba 10:30 - 12:30",
                "Çarşamba 13:30 - 15:30",
                "Çarşamba 15:30 - 17:30",
                "Perşembe 08:30 - 10:30",
                "Perşembe 10:30 - 12:30",
                "Perşembe 13:30 - 15:30",
                "Perşembe 15:30 - 17:30",
                "Cuma 08:30 - 10:30",
                "Cuma 10:30 - 12:30",
                "Cuma 13:30 - 15:30",
                "Cuma 15:30 - 17:30"
            };

        public static List<string> classrooms = new List<string>
            {
                "D103",
                "L201",
                "L202",
                "L206",
                "L207",
                "L208",
                "A1",
                "A2",
                "S1"
            };


        public static List<string> dayAndClassroom = new List<string>
            {
                "Pazartesi 08:30 - 10:30 D103",
                "Pazartesi 08:30 - 10:30 L201",
                "Pazartesi 08:30 - 10:30 L202",
                "Pazartesi 08:30 - 10:30 L206",
                "Pazartesi 08:30 - 10:30 L207",
                "Pazartesi 08:30 - 10:30 L208",
                "Pazartesi 08:30 - 10:30 A1",
                "Pazartesi 08:30 - 10:30 A2",
                "Pazartesi 08:30 - 10:30 S1",
                "Pazartesi 10:30 - 12:30 D103",
                "Pazartesi 10:30 - 12:30 L201",
                "Pazartesi 10:30 - 12:30 L202",
                "Pazartesi 10:30 - 12:30 L206",
                "Pazartesi 10:30 - 12:30 L207",
                "Pazartesi 10:30 - 12:30 L208",
                "Pazartesi 10:30 - 12:30 A1",
                "Pazartesi 10:30 - 12:30 A2",
                "Pazartesi 10:30 - 12:30 S1",
                "Pazartesi 13:30 - 15:30 D103",
                "Pazartesi 13:30 - 15:30 L201",
                "Pazartesi 13:30 - 15:30 L202",
                "Pazartesi 13:30 - 15:30 L206",
                "Pazartesi 13:30 - 15:30 L207",
                "Pazartesi 13:30 - 15:30 L208",
                "Pazartesi 13:30 - 15:30 A1",
                "Pazartesi 13:30 - 15:30 A2",
                "Pazartesi 13:30 - 15:30 S1",
                "Pazartesi 15:30 - 17:30 D103",
                "Pazartesi 15:30 - 17:30 L201",
                "Pazartesi 15:30 - 17:30 L202",
                "Pazartesi 15:30 - 17:30 L206",
                "Pazartesi 15:30 - 17:30 L207",
                "Pazartesi 15:30 - 17:30 L208",
                "Pazartesi 15:30 - 17:30 A1",
                "Pazartesi 15:30 - 17:30 A2",
                "Pazartesi 15:30 - 17:30 S1",
                "Salı 08:30 - 10:30 D103",
                "Salı 08:30 - 10:30 L201",
                "Salı 08:30 - 10:30 L202",
                "Salı 08:30 - 10:30 L206",
                "Salı 08:30 - 10:30 L207",
                "Salı 08:30 - 10:30 L208",
                "Salı 08:30 - 10:30 A1",
                "Salı 08:30 - 10:30 A2",
                "Salı 08:30 - 10:30 S1",
                "Salı 10:30 - 12:30 D103",
                "Salı 10:30 - 12:30 L201",
                "Salı 10:30 - 12:30 L202",
                "Salı 10:30 - 12:30 L206",
                "Salı 10:30 - 12:30 L207",
                "Salı 10:30 - 12:30 L208",
                "Salı 10:30 - 12:30 A1",
                "Salı 10:30 - 12:30 A2",
                "Salı 10:30 - 12:30 S1",
                "Salı 13:30 - 15:30 D103",
                "Salı 13:30 - 15:30 L201",
                "Salı 13:30 - 15:30 L202",
                "Salı 13:30 - 15:30 L206",
                "Salı 13:30 - 15:30 L207",
                "Salı 13:30 - 15:30 L208",
                "Salı 13:30 - 15:30 A1",
                "Salı 13:30 - 15:30 A2",
                "Salı 13:30 - 15:30 S1",
                "Salı 15:30 - 17:30 D103",
                "Salı 15:30 - 17:30 L201",
                "Salı 15:30 - 17:30 L202",
                "Salı 15:30 - 17:30 L206",
                "Salı 15:30 - 17:30 L207",
                "Salı 15:30 - 17:30 L208",
                "Salı 15:30 - 17:30 A1",
                "Salı 15:30 - 17:30 A2",
                "Salı 15:30 - 17:30 S1",
                "Çarşamba 08:30 - 10:30 D103",
                "Çarşamba 08:30 - 10:30 L201",
                "Çarşamba 08:30 - 10:30 L202",
                "Çarşamba 08:30 - 10:30 L206",
                "Çarşamba 08:30 - 10:30 L207",
                "Çarşamba 08:30 - 10:30 L208",
                "Çarşamba 08:30 - 10:30 A1",
                "Çarşamba 08:30 - 10:30 A2",
                "Çarşamba 08:30 - 10:30 S1",
                "Çarşamba 10:30 - 12:30 D103",
                "Çarşamba 10:30 - 12:30 L201",
                "Çarşamba 10:30 - 12:30 L202",
                "Çarşamba 10:30 - 12:30 L206",
                "Çarşamba 10:30 - 12:30 L207",
                "Çarşamba 10:30 - 12:30 L208",
                "Çarşamba 10:30 - 12:30 A1",
                "Çarşamba 10:30 - 12:30 A2",
                "Çarşamba 10:30 - 12:30 S1",
                "Çarşamba 13:30 - 15:30 D103",
                "Çarşamba 13:30 - 15:30 L201",
                "Çarşamba 13:30 - 15:30 L202",
                "Çarşamba 13:30 - 15:30 L206",
                "Çarşamba 13:30 - 15:30 L207",
                "Çarşamba 13:30 - 15:30 L208",
                "Çarşamba 13:30 - 15:30 A1",
                "Çarşamba 13:30 - 15:30 A2",
                "Çarşamba 13:30 - 15:30 S1",
                "Çarşamba 15:30 - 17:30 D103",
                "Çarşamba 15:30 - 17:30 L201",
                "Çarşamba 15:30 - 17:30 L202",
                "Çarşamba 15:30 - 17:30 L206",
                "Çarşamba 15:30 - 17:30 L207",
                "Çarşamba 15:30 - 17:30 L208",
                "Çarşamba 15:30 - 17:30 A1",
                "Çarşamba 15:30 - 17:30 A2",
                "Çarşamba 15:30 - 17:30 S1",
                "Perşembe 08:30 - 10:30 D103",
                "Perşembe 08:30 - 10:30 L201",
                "Perşembe 08:30 - 10:30 L202",
                "Perşembe 08:30 - 10:30 L206",
                "Perşembe 08:30 - 10:30 L207",
                "Perşembe 08:30 - 10:30 L208",
                "Perşembe 08:30 - 10:30 A1",
                "Perşembe 08:30 - 10:30 A2",
                "Perşembe 08:30 - 10:30 S1",
                "Perşembe 10:30 - 12:30 D103",
                "Perşembe 10:30 - 12:30 L201",
                "Perşembe 10:30 - 12:30 L202",
                "Perşembe 10:30 - 12:30 L206",
                "Perşembe 10:30 - 12:30 L207",
                "Perşembe 10:30 - 12:30 L208",
                "Perşembe 10:30 - 12:30 A1",
                "Perşembe 10:30 - 12:30 A2",
                "Perşembe 10:30 - 12:30 S1",
                "Perşembe 13:30 - 15:30 D103",
                "Perşembe 13:30 - 15:30 L201",
                "Perşembe 13:30 - 15:30 L202",
                "Perşembe 13:30 - 15:30 L206",
                "Perşembe 13:30 - 15:30 L207",
                "Perşembe 13:30 - 15:30 L208",
                "Perşembe 13:30 - 15:30 A1",
                "Perşembe 13:30 - 15:30 A2",
                "Perşembe 13:30 - 15:30 S1",
                "Perşembe 15:30 - 17:30 D103",
                "Perşembe 15:30 - 17:30 L201",
                "Perşembe 15:30 - 17:30 L202",
                "Perşembe 15:30 - 17:30 L206",
                "Perşembe 15:30 - 17:30 L207",
                "Perşembe 15:30 - 17:30 L208",
                "Perşembe 15:30 - 17:30 A1",
                "Perşembe 15:30 - 17:30 A2",
                "Perşembe 15:30 - 17:30 S1",
                "Cuma 08:30 - 10:30 D103",
                "Cuma 08:30 - 10:30 L201",
                "Cuma 08:30 - 10:30 L202",
                "Cuma 08:30 - 10:30 L206",
                "Cuma 08:30 - 10:30 L207",
                "Cuma 08:30 - 10:30 L208",
                "Cuma 08:30 - 10:30 A1",
                "Cuma 08:30 - 10:30 A2",
                "Cuma 08:30 - 10:30 S1",
                "Cuma 10:30 - 12:30 D103",
                "Cuma 10:30 - 12:30 L201",
                "Cuma 10:30 - 12:30 L202",
                "Cuma 10:30 - 12:30 L206",
                "Cuma 10:30 - 12:30 L207",
                "Cuma 10:30 - 12:30 L208",
                "Cuma 10:30 - 12:30 A1",
                "Cuma 10:30 - 12:30 A2",
                "Cuma 10:30 - 12:30 S1",
                "Cuma 13:30 - 15:30 D103",
                "Cuma 13:30 - 15:30 L201",
                "Cuma 13:30 - 15:30 L202",
                "Cuma 13:30 - 15:30 L206",
                "Cuma 13:30 - 15:30 L207",
                "Cuma 13:30 - 15:30 L208",
                "Cuma 13:30 - 15:30 A1",
                "Cuma 13:30 - 15:30 A2",
                "Cuma 13:30 - 15:30 S1",
                "Cuma 15:30 - 17:30 D103",
                "Cuma 15:30 - 17:30 L201",
                "Cuma 15:30 - 17:30 L202",
                "Cuma 15:30 - 17:30 L206",
                "Cuma 15:30 - 17:30 L207",
                "Cuma 15:30 - 17:30 L208",
                "Cuma 15:30 - 17:30 A1",
                "Cuma 15:30 - 17:30 A2",
                "Cuma 15:30 - 17:30 S1"
            };


        public static void UpdateAllData(
        List<List<int>> newInstructorAndPreferences,
        Dictionary<string, int[]> newInstructorsAndIndexes,
        Dictionary<string, int[]> newBranchAndIndexes,
        Dictionary<string, int[]> newGradesAndIndexes,
        List<int> newFourthGradeAndIndexes,
        List<int> newProjectIndexes,
        Dictionary<string, string> newSchedule,
        List<string> newrooms
    )
        {
            InstructorAndPreferences = newInstructorAndPreferences ?? InstructorAndPreferences;
            InstructorsAndIndexes = newInstructorsAndIndexes ?? InstructorsAndIndexes;
            BranchAndIndexes = newBranchAndIndexes ?? BranchAndIndexes;
            GradesAndIndexes = newGradesAndIndexes ?? GradesAndIndexes;
            FourthGradeAndIndexes = newFourthGradeAndIndexes ?? FourthGradeAndIndexes;
            ProjectIndexes = newProjectIndexes ?? ProjectIndexes;
            schedule = newSchedule ?? schedule;
            classrooms = newrooms ?? classrooms;
        }

    }
}
