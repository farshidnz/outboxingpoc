namespace Drawboard.Shopping;

public interface ITerminal
{
    public void SetPricing(string name, double price, Pack special);
    public void Scan(string item);
    public double Checkout();
}