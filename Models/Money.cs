namespace Program_Ksiegowy.Models;

public struct Money
{
    public int value;
    public float floatValue => (value / 100f);

    public Money(int value)
        => (this.value) = (value);

    public override string ToString()
    {
        return value.ToString("F2", new System.Globalization.CultureInfo("en-US"));
    }
}