using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Program_Księgowy.SimpleWay
{
    public class Data
    {
        public List<Purchase> purchases;

        public int sumNetto { get; private set; }
        public string sumNettoString => (sumNetto/100f).ToString().Replace(',', '.');
        public int sumVAT { get; private set; }
        public string sumVATString => (sumVAT/100f).ToString().Replace(',', '.');

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

    public class Purchase
    {
        /// <summary>
        /// NrDostawcy
        /// </summary>
        public string NIP { get; private set; }
        public string NazwaDostawcy { get; private set; }
        public string DowodZakupu { get; private set; }
        /// <summary>
        /// Data zakupu i data wpływu.
        /// </summary>
        public string Data { get; private set; }
        public int Netto { get; private set; }
        /// <summary>
        /// K_42
        /// </summary>
        public string NettoString => (Netto/100f).ToString().Replace(',', '.');
        public int VAT { get; private set; }
        /// <summary>
        /// K_43
        /// </summary>
        public string VATString => (VAT/100f).ToString().Replace(',', '.');

        public Purchase(string dataLineString, int lp)
        {
            string[] splitedLine = dataLineString.Split(';');

            NIP = splitedLine[0];
            if(NIP.Length != 10)
                // TODO: Zaimplementować odpowiednie weryfikowanie nipu z pomocą zewnętrznego API.
                throw new InvalidDataException($"Rekord {lp}: Nieprawidłowy NIP");

            NazwaDostawcy = splitedLine[1] + "  " + splitedLine[2];
            DowodZakupu = splitedLine[3];
            Data = string.Join('-', splitedLine[4].Split('.').Reverse());

            Console.WriteLine($"{lp}: Netto: {splitedLine[5]}, VAT: {splitedLine[6]}");
            try
            {
                Netto = (int)(Convert.ToSingle(splitedLine[5].Replace('.', ',')) * 100);
                VAT   = (int)(Convert.ToSingle(splitedLine[6].Replace('.', ',')) * 100);
            }
            catch(Exception _)
            {
                throw new InvalidDataException($"Rekord {lp}: Nieprawidłowe Netto lub/i VAT");
            }

            // dataLineString.Substring
        }
    }
}