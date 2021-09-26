using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Program_Księgowy
{

    public class Document
    {
        public const uint TotalCollumnsNumber = 67;
        public const string FirstLine = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
        "<JPK xmlns:etd=\"http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2020/03/11/eD/DefinicjeTypy/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://crd.gov.pl/wzor/2020/05/08/9393/ http://crd.gov.pl/wzor/2020/05/08/9393/schemat.xsd\" xmlns=\"http://crd.gov.pl/wzor/2020/05/08/9393/\">";
        public const string LastLine = "</JPK>";
        public Nagłówek nagłówek;
        public Podatnik podatnik;
        //public List<>
    }

    public class Nagłówek
    {
        public string kodFormularza = "JPK_VAT";
        public string kodSystemowy = "JPK_VAT (3)";
        public string wersjaSchemy = "1-1";
        public string wariantFormularza = "3";
        public string celZlozenia = "0";
        public string dataWytworzeniaJPK; // Changed
        public Date dataOd; // Changed
        public Date dataDo; // Changed
        public string nazwaSystemu = "OpenOffice Calc";

        public Nagłówek()
        {

        }
    }

    public class Podatnik
    {
        public string NIP;
        public string PelnaNazwa;
        public string Email;
    }

    public class Kupno
    {
        //public
    }

    public struct Date
    {
        byte day;
        public byte Day { get { return day; } set { if (value > 31) throw new Exception($"Nie właściwa data.\n Day = {day}"); day = value; } }
        byte month;
        public byte Month { get { return month; } set { if (value > 12) throw new Exception($"Nie właściwa data.\n Month = {month}"); month = value; } }
        public short year;

        public Date(byte d, byte m, short y)
        { day = d; month = m; year = y; }

        public override string ToString()
        {
            return $"{year}-{month}-{day}";
        }
    }

    public struct DateAndTime
    {
        //2020-07-25T21:51:33
        public Date date;
        public byte hours;
        public byte minutes;
        public byte seconds;

    }

    public interface ISection
    {
        // string Name;
        public StringBuilder Render();
    }
}
