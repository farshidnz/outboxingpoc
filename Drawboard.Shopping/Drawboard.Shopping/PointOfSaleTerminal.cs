namespace Drawboard.Shopping;

public class PointOfSaleTerminal : ITerminal
{
    private readonly List<Item> _items = new();
    
    private readonly Dictionary<Item, int> _purchasedItems = new();
    
    public void SetPricing(string name, double price, Pack special)
    {
        _items.Add(new Item {Name = name, Price = price, Pack = special});
    }

    public void Scan(string item)
    {
        var purchasedItem = _items.Find(s => s.Name.Equals(item, StringComparison.OrdinalIgnoreCase));
        if (purchasedItem != null) AddScanedItem(purchasedItem);
    }

    public double Checkout()
    {
        for
    }
    
    private void AddScanedItem(Item purchasedItem)
    {
        if (_purchasedItems.ContainsKey(purchasedItem))
        {
            _purchasedItems[purchasedItem] += 1;
        }
        _purchasedItems.Add(purchasedItem, 1);
    }
}