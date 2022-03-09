using System;
using System.Collections.Generic;
using System.Xml;

namespace IS_Lab1_XML
{
    internal class XMLReadWithSAXApproach
    {
        internal static void Read(string xmlpath)
        {
            // konfiguracja początkowa dla XmlReadera
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            // odczyt zawartości dokumentu
            XmlReader reader = XmlReader.Create(xmlpath, settings);
            // zmienne pomocnicze
            int count = 0;
            string postac = "";
            string nPS = "";
            string pO = "";

            int countSameName = 0;
            string maxTabsName = "";
            string maxCreamName = "";

            var drugsAppears = new Dictionary<String, int>();
            var tabsPodmiots = new Dictionary<String, int>();
            var creamPodmiots = new Dictionary<String, int>();


            reader.MoveToContent();

            // analiza każdego z węzłów dokumentu
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
                {
                    postac = reader.GetAttribute("postac");
                    nPS = reader.GetAttribute("nazwaPowszechnieStosowana");
                    pO = reader.GetAttribute("podmiotOdpowiedzialny");


                    maxPodmiot(pO, postac, tabsPodmiots, "Tabletki");
                    maxPodmiot(pO, postac, creamPodmiots, "Krem");
                    sameNameDiffTypeCount(nPS, drugsAppears);
                    count = creamSubCount(postac, nPS, count);
                }
            }


            List<Tuple<string, int>> listOfTabs = toSortedListConverter(tabsPodmiots);
            List<Tuple<string, int>> listOfCreams = toSortedListConverter(creamPodmiots);
            countSameName = drugsAppearsCounter(countSameName, drugsAppears);



            Console.WriteLine("\nLiczba produktów leczniczych w postacikremu, których jedyną substancją czynną jest Mometasoni furoas wynosi: {0}", count);
            Console.WriteLine("Liczba produktów leczniczych w różnych postaciach, które mają taką samą nazwę wynosi:  {0}", countSameName);
            Console.WriteLine("Podmiot z największą produkcją tabletek to:  {0}", getMaxProducer(listOfTabs));
            Console.WriteLine("Podmiot z największą produkcją kremów to:  {0}", getMaxProducer(listOfCreams));

            Console.WriteLine("\nNajwięksi producenci kremów to: ");
            printToPositionOfList(listOfCreams, 3);
        }

        private static void maxPodmiot(string pO, string postac, Dictionary<string, int> tabsPodmiots, string type)
        {
            if (tabsPodmiots.ContainsKey(pO))
            {
                if (type == postac)
                    tabsPodmiots[pO] += 1;
            }
            else
            {
                tabsPodmiots.Add(pO, 1);
            }
        }

        private static void sameNameDiffTypeCount(string nPS, Dictionary<string, int> drugsAppears)
        {
            if (drugsAppears.ContainsKey(nPS))
            {
                drugsAppears[nPS] += 1;
            }
            else
            {
                drugsAppears.Add(nPS, 1);
            }
        }

        private static int creamSubCount(string postac, string sc, int count)
        {
            if (postac == "Krem" && sc == "Mometasoni furoas")
                count++;
            return count;
        }

        private static List<Tuple<string, int>> toSortedListConverter(Dictionary<string, int> drugsAppears)
        {

            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            foreach (var entry in drugsAppears)
            {
                if (entry.Key != "")
                    list.Add(Tuple.Create(entry.Key, entry.Value));
            }


            list.Sort((e1, e2) =>
            {
                return e2.Item2.CompareTo(e1.Item2);
            });

            // list.ForEach(Console.WriteLine);
            return list;
        }
        private static int drugsAppearsCounter(int countSameName, Dictionary<string, int> drugsAppears)
        {
            foreach (var entry in drugsAppears)
            {
                if (entry.Value > 1 && entry.Key != "")
                {
                    //   Console.WriteLine(entry.Key+"   "+entry.Value);
                    countSameName++;
                }
            }

            return countSameName;
        }

        private static string getMaxProducer(List<Tuple<string, int>> list)
        {
            return list[0].Item1;
        }

        private static void printToPositionOfList(List<Tuple<string, int>> list, int positions)
        {

            for (int i = 0; i < positions; i++)
            {
                Console.WriteLine(list[i].Item1 + " których produkcja wynosi: " + list[i].Item2);
            }
        }
    }

}