namespace RoR2RunGenerator.Characters;

public record Character(string InternalName, string DisplayName) : WeightedRecord
{
    public string DisplayName { get; private set; } = DisplayName;

    public void RemapChildInstance(Character child)
    {
        child.DisplayName = DisplayName;
        child.Weight = Weight;
    }
}