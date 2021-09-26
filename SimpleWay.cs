using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Program_Ksiegowy.Models;

namespace Program_Księgowy.SimpleWay
{
    public class Data
    {
        public List<Purchase> purchases;

        public int sumNetto { get; private set; }
        public string sumNettoString => (sumNetto/100f).ToString("F2", new System.Globalization.CultureInfo("en-US"));
        public int sumVAT { get; private set; }
        public string sumVATString => (sumVAT/100f).ToString("F2", new System.Globalization.CultureInfo("en-US"));

        public Data(string path)
        {
            purchases = new List<Purchase>();

            try
            {
                using StreamReader reader = new(path);
                int i = 1;
                while(!reader.EndOfStream)
                    purchases.Add(new Purchase(reader.ReadLine(), i++));

            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Wystąpił błąd podczas odczytywania pliku!");
                Console.WriteLine("Treść błędu:");
                Console.WriteLine(e);
                Console.ResetColor();
            }

            CalculateSums();
        }

        void CalculateSums()
        {
            (sumNetto, sumVAT) = (0, 0);
            foreach (var purchase in purchases)
            {
                sumNetto += purchase.Netto;
                sumVAT += purchase.VAT;
            }
        }

        public void SaveToFile(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            // settings.Async = true;  
            // settings.NewLineOnAttributes = true;
            settings.Indent = true;
            

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                int i = 1;
                writer.WriteStartElement(null, "Zakupy", null);
                foreach (Purchase purchase in purchases)
                {
                    writer.WriteStartElement(null, "ZakupWiersz", null);

                        writer.WriteStartElement(null, "LpZakupu", null);
                        writer.WriteString(i.ToString());
                        i++;
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "KodKrajuNadaniaTIN", null);
                        writer.WriteString("PL");
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "NrDostawcy", null);
                        writer.WriteString(purchase.NIP);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "NazwaDostawcy", null);
                        writer.WriteString(purchase.NazwaDostawcy);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "DowodZakupu", null);
                        writer.WriteString(purchase.DowodZakupu);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "DataZakupu", null);
                        writer.WriteString(purchase.Data);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "DataWplywu", null);
                        writer.WriteString(purchase.Data);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "K_42", null);
                        writer.WriteString(purchase.NettoString);
                        writer.WriteEndElement();
                        writer.WriteStartElement(null, "K_43", null);
                        writer.WriteString(purchase.VATString);
                        writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();  
            }  
        }
    }
}