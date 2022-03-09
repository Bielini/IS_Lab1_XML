using System;
using System.IO;

namespace IS_Lab1_XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlpath = Path.Combine("Assets", "dataOLD.xml");

            // odczyt danych z wykorzystaniem DOM
            Console.WriteLine("\nXML loaded by DOM Approach");
            XMLReadWithDOMApproach.Read(xmlpath);

            // odczyt danych z wykorzystaniem SAX
            Console.WriteLine("\nXML loaded by SAX Approach");
            XMLReadWithSAXApproach.Read(xmlpath);


            // odczyt danych z wykorzystaniem XPath i DOM
            Console.WriteLine("\nXML loaded with XPath");
            XMLReadWithXLSTDOM.Read(xmlpath);

            Console.ReadLine();




        }
    }
}
