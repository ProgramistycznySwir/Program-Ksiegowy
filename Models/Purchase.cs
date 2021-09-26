using System.IO;

namespace Program_Ksiegowy.Models;

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
    public string NettoString => (Netto/100f).ToString("F2", new System.Globalization.CultureInfo("en-US"));
    public int VAT { get; private set; }
    /// <summary>
    /// K_43
    /// </summary>
    public string VATString => (VAT/100f).ToString("F2", new System.Globalization.CultureInfo("en-US"));

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