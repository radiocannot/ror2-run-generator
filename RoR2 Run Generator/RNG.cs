using System.Reflection;
using Newtonsoft.Json;
using RoR2RunGenerator.Characters;
using Weighted_Randomizer;

namespace RoR2RunGenerator;

public static class RNG
{
    static RNG()
    {
        if (!File.Exists(Settings.Path))
        {
            File.WriteAllText(Settings.Path, JsonConvert.SerializeObject(new Settings(), Formatting.Indented));
        }
        var jsonString = File.ReadAllText(Settings.Path);
        ImportSettings(JsonConvert.DeserializeObject<Settings>(jsonString)!);
    }

    private static bool _rollCharacters;
    private static readonly StaticWeightedRandomizer<Character> characters = new();
    private static bool _preventRollingPreviousCharacter;
    private static Character? _previousCharacter;

    private static bool _rollDestinations;
    private static readonly StaticWeightedRandomizer<RunDestination> destinations = new();
    private static bool _preventRollingPreviousDestination;
    private static RunDestination? _previousDestination;

    private static bool _rollArtifacts;
    public static readonly StaticWeightedRandomizer<Artifact> Artifacts = new()
    {
        new Artifact("No Artifacts"),
        new Artifact("Artifact of Chaos"),
        new Artifact("Artifact of Command"),
        new Artifact("Artifact of Death"),
        new Artifact("Artifact of Dissonance"),
        new Artifact("Artifact of Enigma"),
        new Artifact("Artifact of Evolution"),
        new Artifact("Artifact of Frailty"),
        new Artifact("Artifact of Glass"),
        new Artifact("Artifact of Honor"),
        new Artifact("Artifact of Kin"),
        new Artifact("Artifact of Metamorphosis"),
        new Artifact("Artifact of Sacrifice"),
        new Artifact("Artifact of Soul"),
        new Artifact("Artifact of Spite"),
        new Artifact("Artifact of Swarms"),
        new Artifact("Artifact of Vengeance")
    };
    private static bool _rollMultipleArtifacts;

    private static bool _rollArtifactUnlocks;
    public static readonly StaticWeightedRandomizer<ArtifactUnlock> ArtifactUnlocks = new();

    private static void ImportSettings(Settings settings)
    {
        #region Characters

        foreach (var character in settings.Characters)
        {
            if (!(_rollCharacters = settings.RollCharacters)) break;
            var assembly = Assembly.GetExecutingAssembly();
            var instance = (Character?)assembly.CreateInstance($"RoR2RunGenerator.Characters.{character.InternalName}");
            if (instance is null)
            {
                Console.WriteLine(
                    $"ERROR: The character internal name specified in settings.json in \"Characters\" is invalid ({character.InternalName})");
                Console.ReadKey();
                Environment.Exit(0);
            }

            character.RemapChildInstance(instance);
            characters.Add(instance, instance.Weight);
        }

        _preventRollingPreviousCharacter = settings.PreventRollingPreviousCharacter;

        #endregion

        #region Destinations

        foreach (var destination in settings.RunDestinations)
        {
            if (!(_rollDestinations = settings.RollDestinations)) break;
            destinations.Add(destination);
        }

        _preventRollingPreviousDestination = settings.PreventRollingPreviousDestination;

        #endregion

        #region Artifact unlocks

        var lockedArtifacts = Artifacts.Except(settings.UnlockedArtifacts);
        foreach (var artifact in lockedArtifacts)
        {
            if (!(_rollArtifactUnlocks = settings.RollArtifactUnlocks)) break;
            ArtifactUnlocks.Add(artifact);
        }

        #endregion

        #region Artifacts

        Artifacts.Clear();
        Artifacts.AddRange(settings.UnlockedArtifacts);
        _rollMultipleArtifacts = settings.RollMultipleArtifacts;
        _rollArtifacts = settings.RollArtifacts;

        #endregion
    }

    public static Settings ExportSettings()
    {
        var settings = new Settings();
        
        #region Characters

        settings.Characters = characters.ToArray();
        settings.PreventRollingPreviousCharacter = _preventRollingPreviousCharacter;
        settings.RollCharacters = _rollCharacters;
        
        #endregion

        #region Destinations

        settings.RunDestinations = destinations.ToArray();
        settings.PreventRollingPreviousDestination = _preventRollingPreviousDestination;
        settings.RollDestinations = _rollDestinations;
        
        #endregion

        #region Artifact unlocks

        settings.UnlockedArtifacts = Artifacts.ToArray();
        settings.RollArtifactUnlocks = _rollArtifactUnlocks;

        #endregion

        #region Artifacts

        settings.RollMultipleArtifacts = _rollMultipleArtifacts;
        settings.RollArtifacts = _rollArtifacts;

        #endregion

        return settings;
    }

    public static Character? RollCharacter()
    {
        if (!_rollCharacters) return null;

        var character = characters.NextWithReplacement();

        // ReSharper disable once InvertIf
        if (_preventRollingPreviousCharacter)
        {
            while (character == _previousCharacter)
            {
                character = characters.NextWithReplacement();
            }

            _previousCharacter = character;
        }

        return character;
    }

    public static RunDestination? RollDestination()
    {
        if (!_rollDestinations) return null;

        var destination = destinations.NextWithReplacement();

        // ReSharper disable once InvertIf
        if (_preventRollingPreviousDestination)
        {
            while (destination == _previousDestination)
            {
                destination = destinations.NextWithReplacement();
            }

            _previousDestination = destination;
        }
        
        

        return destination;
    }

    public static IEnumerable<ArtifactUnlock> RollArtifactUnlocks(RunDestination destination, int loopAmount)
    {
        if (!_rollArtifactUnlocks) return Enumerable.Empty<ArtifactUnlock>();

        List<ArtifactUnlock>? returnValue = new();
        var artifactUnlocksCopy = new StaticWeightedRandomizer<ArtifactUnlock>();
        artifactUnlocksCopy.AddRange(ArtifactUnlocks);
        for (int i = 0; i < loopAmount; i++)
        {
            if (Random.Shared.Next(0, destination.ChanceRollingArtifactUnlock) != 0) continue;
        
            returnValue.Add(artifactUnlocksCopy.NextWithRemoval());
        }
        
        return returnValue;
    }

    public static IEnumerable<Artifact> RollArtifacts()
    {
        if (!_rollArtifacts) return Enumerable.Empty<Artifact>();

        var singleArtifact = Artifacts.NextWithReplacement();
        var returnArtifacts = new List<Artifact> { singleArtifact };
        
        if (!_rollMultipleArtifacts) return returnArtifacts;
        if (singleArtifact.ArtifactName == "No Artifacts") return returnArtifacts;

        var artifactsCopy = new StaticWeightedRandomizer<Artifact>();
        artifactsCopy.AddRange(Artifacts);
        artifactsCopy.Remove(Artifacts.Single(artifact => artifact.ArtifactName == "No Artifacts"));
        artifactsCopy.Remove(singleArtifact);
        
        int artifactAmount;
        if (ArtifactAmount() != 4) return returnArtifacts;
        if (ArtifactAmount() != 4) return returnArtifacts;
        if (ArtifactAmount() != 4) return returnArtifacts;
        ArtifactAmount(3);
        return returnArtifacts;

        int ArtifactAmount(int max = 4)
        {
            max = artifactsCopy.Count<max ? artifactsCopy.Count : max;
            artifactAmount = Random.Shared.Next(0, max);
            for (int i = 0; i < artifactAmount; i++)
            {
                returnArtifacts.Add(artifactsCopy.NextWithRemoval());
            }

            return artifactAmount+1;
        }
    }
}