namespace Drawboard.Shopping;

public record Item
{
    public string Name { get; init; }
    public double Price { get; init; }
    public Pack? Pack { get; init; }
}

public record Pack
{
    public int Volume { get; init; }
    public double Price { get; init; }
}