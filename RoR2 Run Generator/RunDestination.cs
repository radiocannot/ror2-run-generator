using Newtonsoft.Json;

namespace RoR2RunGenerator;

public record RunDestination(string Destination, int LoopAmountMinimum, int LoopAmountMaximum, int ChanceRollingArtifactUnlock, int Weight = 1) : WeightedRecord(Weight);