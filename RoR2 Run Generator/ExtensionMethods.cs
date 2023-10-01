using Weighted_Randomizer;

namespace RoR2RunGenerator;

public static class ExtensionMethods
{
    public static void AddRange<T>(this StaticWeightedRandomizer<T> randomizer, IEnumerable<T> data) where T : WeightedRecord
    {
        foreach (var item in data)
        {
            randomizer.Add(item, item.Weight);
        }
    }
}