﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace listarendezes
{
    internal class TesztAdat
    {
        public List<string> Szavak = new List<string>();
        public string Abc = "";
        string Kisbetuk = "aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyz";
        string Nagybetuk = "AÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ";
        string Szamjegyek = "0123456789";
        string Jelek = ",.- §+!%/=()?:_~ˇ^˘°˛`˙´˝¨¸|Ä€÷×¤äđĐ[]łŁ$ß>#&@{}<;>*";
        public TesztAdat()
        {
            EgybetusSzavak(true, true, false, false);
        }
        public TesztAdat(int szoszam, int maxbetuszam)
        {
            EgybetusSzavak(true, true, false, false);
            SzoGenerator(szoszam, maxbetuszam);
        }
        public TesztAdat(bool szamjegyek, bool jelek)
        {
            EgybetusSzavak(true, true, szamjegyek, jelek);
        }
        public void EgybetusSzavak(bool kisbetuk, bool nagybetuk, bool szamjegyek, bool jelek)
        {
            Abc = kisbetuk ? Abc + Kisbetuk : Abc;
            Abc = nagybetuk ? Abc + Nagybetuk : Abc;
            Abc = szamjegyek ? Abc + Szamjegyek : Abc;
            Abc = jelek ? Abc + Jelek : Abc;
            char[] karakterek = Abc.ToCharArray();
            Random vg = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int hely1 = vg.Next(karakterek.Length);
                int hely2 = vg.Next(karakterek.Length);
                char karakter = karakterek[hely1];
                karakterek[hely1] = karakterek[hely2];
                karakterek[hely2] = karakter;
            }
            karakterek.ToList().ForEach(k => Szavak.Add(k.ToString()));
        }

        public void SzoGenerator(int szoszam, int maxbetuszam)
        {
            Random vg = new Random();
            for (int i = 0; i < szoszam; i++)
            {
                string szo = "";
                for (int j = 0; j <= vg.Next(maxbetuszam); j++)
                {
                    szo += Abc[vg.Next(Abc.Length)];
                }
                Szavak.Add(szo);
            }
        }
    }


    //-----------------------------------------------------------------------------------------------------

    public class RndString
    {
        public List<string> Szavak = new List<string>();
        string charabc = "aAáÁbBcCdDeEéÉfFgGhHiIíÍjJkKlLmMnNoOóÓöÖőŐpPqQrRsStTuUúÚüÜűŰvVwWxXyYzZ0123456789";
        string charstock = "aAáÁbBcCdDeEéÉfFgGhHiIíÍjJkKlLmMnNoOóÓöÖőŐpPqQrRsStTuUúÚüÜűŰvVwWxXyYzZ0123456789,.- §+!%/=()?:_~ˇ^˘°˛`˙´˝¨¸|Ä€÷×¤äđĐ[]łŁ$ß>#&@{}<;>*";
        string word = "";
        string inchecked = "";
        Random rnd = new Random();
        public RndString(int wordnumber, int vocablenumber, int type)
        {
            Generator(wordnumber, vocablenumber, type);
        }
        public void Generator(int wordnumber, int vocablenumber, int type)
        {
            switch (type)
            {
                case 0:
                    inchecked = charabc;
                    break;
                case 1:
                    inchecked = charstock;
                    break;
                default:
                    inchecked = charstock;
                    break;
            }
            for (int i = 0; i < vocablenumber; i++)
            {
                for (int j = 0; j < wordnumber; j++) { word += inchecked[rnd.Next(0, inchecked.Length)]; }
                Szavak.Add(word);
                word = "";
            }
        }
    }



    //-----------------------------------------------------------------------------------------------------
    class Rendezo : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return x.CompareTo(y);
        }
    }
    class RendezoAbc : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string abc = "aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyzAÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ";
            //string abc = "aAáÁbBcCdDeEéÉfFgGhHiIíÍjJkKlLmMnNoOóÓöÖőŐpPqQrRsStTuUúÚüÜűŰvVwWxXyYzZ";
            //Ez az 1. feladat: Írjanak egy rendezőt ami a megadott abc alapján rendezi a listában lévő szavakat.
            //A nem felsorolt karaktereket a végére vagy az elejére teszi (pl.: a számjegyek a végére kerülnek).
            int return_value = 0;
            int maxlength;
            if (x == y) { return_value = 0; }
            if (y.Length < x.Length) { maxlength = x.Length; } else { maxlength = y.Length; }
            for (int i = 0; i < maxlength; i++)
            {
                //Az comment alatti két sor akkor kell hogyha a betűvel kezdődő de számot tartalmazó szó az abc szerinti helyére kell h kerüljön. Ha a két sort alul aktiváljuk akkor ez  ne maradjon bent.
                if (!(abc.Contains(x[i]))) { return_value = 1; break; }
                else if (!(abc.Contains(y[i]))) { return_value = -1; break; }
                else if (abc.IndexOf(x[i]) < abc.IndexOf(y[i])) { return_value = -1; break; }
                else if (abc.IndexOf(x[i]) == abc.IndexOf(y[i])) { return_value = 0; break; }
                else if (abc.IndexOf(x[i]) > abc.IndexOf(y[i])) { return_value = 1; break; }
                else { Console.WriteLine("Valami baj történt, mert egyik se futott le."); }
            }
            //Az alábbi két sor akkor kell hogyha a betűvel kezdődő de számot tartalmazó szó a végére kell h kerüljön.
            for (int i = 0; i < x.Length; i++) { if (!(abc.Contains(x[i]))) { return_value = 1; break; }}
            for (int i = 0; i < y.Length; i++) { if (!(abc.Contains(y[i]))) { return_value = -1; break; }}

            return return_value;
        }
    }

    class RendezoAbcEgyezok : IComparer<string>
    {
        private int maxlength;
        private int return_value = 0;

        private Vector2 IndexOfMatrix(char[,] array, char x)
        {
            Vector2 return_value = new Vector2(999, 999);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < array.Length / 4; i++){ if (array[j, i] == x) { return_value =  new Vector2(i, j); }} //{return_value = (array[j, i] == x ? new Vector2(i, j) : new Vector2(999, 999));}
            }
            return return_value;
        }
        private bool ContainsOfMatrix(char[,] array, char x) { return IndexOfMatrix(array, x).X == 999 ? false : true; }

        public int Compare(string x, string y)
        {
            char[,] abc =
            {
                {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','ö','p','q','r','s','t','u','ü','v','w','x','y','z'},
                {'á','b','c','d','é','f','g','h','í','j','k','l','m','n','ó','ő','p','q','r','s','t','ú','ű','v','w','x','y','z'},
                {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','Ö','P','Q','R','S','T','U','Ü','V','W','X','Y','Z'},
                {'Á','B','C','D','É','F','G','H','Í','J','K','L','M','N','Ó','Ő','P','Q','R','S','T','Ú','Ű','V','W','X','Y','Z'}
            };
            //Ez az 2. feladat: Írjanak egy rendezőt ami a megadott abc alapján rendezi a listában lévő szavakat,
            //és a megadott (egymás fölött lévő) azonos betűket nem különbözteti meg.
            //A nem felsorolt karaktereket kihagyja a rendezésből (pl.: a szóköz nem számít).
            //Mukodes megfejtese console.writeline("list"("sorban", "oszlopban"))
            if (y.Length < x.Length) { maxlength = x.Length; } else { maxlength = y.Length; }
            for (int i = 0; i < maxlength; i++)
            {
                Vector2 xpos = IndexOfMatrix(abc, x[i]);
                Vector2 ypos = IndexOfMatrix(abc, y[i]);
                if (xpos.X < ypos.X){ return_value = -1; break; }
                else if (xpos.X > ypos.X){ return_value = 1; break; }
                else if (xpos.X == ypos.X){ return_value = 0; break; }
                else {System.Console.WriteLine("Nem ment bele egyikbe se!");}
            }
            for (int i = 0; i < x.Length; i++) { if (!ContainsOfMatrix(abc, x[i])) { return_value = 1; break; }}
            for (int i = 0; i < y.Length; i++) { if (!ContainsOfMatrix(abc, y[i])) { return_value = -1; break; }}
            return return_value;
        }
    }
    class Tanulmany
    {
        private int szoszam = 10;
        public Tanulmany()
        {
            System.Console.WriteLine("Kérlek add meg, hogy a beépített, az első saját megoldással vagy a második saját megoldással írja ki. (0 / 1 / 2)");
            string comparetype = Console.ReadLine();
            System.Console.WriteLine("Kérlek add meg, hogy az alap listát használja, a tanárúr álltal írt lista generálót, vagy a saját lista generálót. (0 / 1 / 2)");
            string listtype = Console.ReadLine();
            RndString generatedlist = new RndString(10, 20, 0);
            TesztAdat generatedlistt = new TesztAdat(10, 20);
            List<string> szavak = new List<string>()
                {"farok", "Fanni", "zebra", "Zita", "álom", "alom", "köcsög", "12asd", "asd123asdf", "kő", "olló", "elvarázsolt",
                 "éles", "Éva", "Edina", "Elemér"};
            switch (listtype)
            {
                case "1":
                    szavak = generatedlistt.Szavak;
                    break;
                case "2":
                    szavak = generatedlist.Szavak;
                    break;
                default:
                    break;
            }
            //szavak.ForEach(szo => Console.Write($"{szo} "));
            Console.WriteLine(string.Join(" ", szavak));
            //generatedlist.Szavak.ForEach(i => Console.Write("{0}\n", i));

            //szavak.Sort();
            //Console.WriteLine(string.Join(" ", szavak));

            //szavak.Sort((a, b) => a.CompareTo(b));
            //szavak.Sort((a, b) => a.Length.CompareTo(b.Length));
            //szavak.Sort((a, b) => -1);
            switch (comparetype)
            {
                case "0":
                    szavak.Sort(new Rendezo());
                    break;
                case "1":
                    szavak.Sort(new RendezoAbc());
                    break;
                case "2":
                    szavak.Sort(new RendezoAbcEgyezok());
                    break;
                default:
                    szavak.Sort(new RendezoAbcEgyezok());
                    break;
            }
            System.Console.WriteLine('\n' + "A formázott szöveg: ");
            Console.WriteLine(string.Join(" ", szavak));
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            new Tanulmany();
            Console.ReadKey();
        }
    }
}