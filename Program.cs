using System;
using System.Xml;
using System.Xml.Linq;
using static Program_Ksiegowy.UsefulUtilities;

namespace Program_Księgowy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((510/100f).ToString().Replace(',', '.'));
            string filename = AskUser("Podaj nazwę deklaracji: ");
            SimpleWay.Data data = new($"{filename}.csv");
            // Convert("2021_04.csv");
            // Console.WriteLine("1;;;1".Split(';')[1] is "");
            Console.WriteLine($"Suma Netto: {data.sumNettoString}");
            Console.WriteLine($"Suma VAT: {data.sumVATString}");
            data.SaveToFile($"{filename}.xml");

            Console.WriteLine("Naciśnij klawisz by zamknąć program...");
            // if(Console.)
            Console.ReadKey();
        }


        static void Convert(string path)
        {
            string outputPath = System.IO.Path.ChangeExtension(path, ".xml");

            XmlWriterSettings settings = new XmlWriterSettings();
            // settings.Async = true;  
            // settings.NewLineOnAttributes = true;
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(outputPath, settings)) 
            {  
                writer.WriteStartElement(null, "root", "http://ns");  
                writer.WriteStartElement(null, "sub", null);  
                writer.WriteAttributeString(null, "att", null, "val");  
                writer.WriteString("text");  
                writer.WriteEndElement();  
                writer.WriteProcessingInstruction("pName", "pValue");  
                writer.WriteComment("cValue");  
                writer.WriteCData("cdata value");  
                writer.WriteEndElement();  
                writer.Flush();  
            }  


            // var writer = new System.Xml.XmlWriter.Create(System.IO.Path.ChangeExtension(path, ".xml"));
            // Console.WriteLine(node.ToString());
            // document.Save(System.IO.Path.ChangeExtension(path, ".xml"));
        }
    }
}
