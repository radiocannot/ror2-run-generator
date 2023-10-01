namespace RoR2RunGenerator;

public record Artifact(string ArtifactName, int Weight = 1) : WeightedRecord(Weight)
{
    public static implicit operator ArtifactUnlock(Artifact artifact) => new(artifact.ArtifactName, artifact.Weight);
}