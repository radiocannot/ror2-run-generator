// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using RoR2RunGenerator;

//File.WriteAllText(Settings.Path, JsonConvert.SerializeObject(new Settings(), Formatting.Indented));

while (true)
{
    Console.WriteLine("To use run generator press any key");
    Console.ReadKey();

    var character = RNG.RollCharacter()!;
    var destination = RNG.RollDestination()!;
    var loopAmount =
        Random.Shared.Next(destination.LoopAmountMinimum, destination.LoopAmountMaximum);
    var artifactUnlocks = RNG.RollArtifactUnlocks(destination, loopAmount)!.Select(artifactUnlock => artifactUnlock.ArtifactName + ";").ToArray();

    var artifacts = RNG.RollArtifacts()!.Select(artifact => artifact.ArtifactName+";");
    Console.WriteLine($"Use character {character.DisplayName}");
    Console.WriteLine($"You need to {destination.Destination}, looping {loopAmount} times(s)");
    Console.WriteLine($"Unlock these artifacts: {string.Join(' ', artifactUnlocks)}");
    Console.WriteLine($"Use these artifacts: {string.Join(' ', artifacts)}");
    if (artifactUnlocks.Length != 0)
    {
        Console.WriteLine("What artifacts did you unlock? (just paste them in from your \"Unlock these artifacts\" for this run)");
        var unlockedArtifacts = Console.ReadLine()!.Split(';');
        if(string.IsNullOrWhiteSpace(unlockedArtifacts[0])) continue;
        foreach (var artifact in unlockedArtifacts)
        {
            RNG.Artifacts.Add(RNG.ArtifactUnlocks.First(unlock => artifact.Contains(unlock.ArtifactName)));
        }
        File.WriteAllText(Settings.Path, JsonConvert.SerializeObject(RNG.ExportSettings(), Formatting.Indented));
    }
}
//for (int i = 0; i < 20; i++)
//{
//    Console.WriteLine(Random.Shared.Next(0,2));
//}