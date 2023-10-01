namespace RoR2RunGenerator;

public record ArtifactUnlock(string ArtifactName, int Weight = 1) : WeightedRecord(Weight)
{
    public static implicit operator Artifact(ArtifactUnlock artifactUnlock) => new(artifactUnlock.ArtifactName, artifactUnlock.Weight);
}