using Newtonsoft.Json;
using RoR2RunGenerator.Characters;

namespace RoR2RunGenerator;

[JsonObject]
public class Settings
{
    public const string Path = "settings.json";
    public string Description = "Don't change InternalName. You're free to change everything else";
    public Character[] Characters = 
    { 
        new Commando(),
        new Huntress(),
        new Bandit(),
        new MUL_T(),
        new Engineer(),
        new Artificer(),
        new Mercenary(),
        new REX(),
        new Loader(),
        new Acrid(),
        new Captain(),
        new Railgunner(),
        new VoidFiend()
    };
    public bool PreventRollingPreviousCharacter = true;
    public bool RollCharacters = true;

    public RunDestination[] RunDestinations =
    {
        new("Kill Mithrix", 0,2, 4, 4),
        new("Escape Planetarium", 1,3,3, 3),
        new("Kill a Twisted Scavanger/Obliterate", 1,5, 2, 1)
    };
    public bool PreventRollingPreviousDestination = true;
    public bool RollDestinations = true;

    public string ArtifactsDescription = """Don't delete or change "No Artifacts"(other than Weight, you can change it), you're free to change everything else""";
    public Artifact[] UnlockedArtifacts =
    {
        new("No Artifacts"),
        new("Artifact of Chaos"),
        new("Artifact of Command"),
        new("Artifact of Death"),
        new("Artifact of Dissonance"),
        new("Artifact of Enigma"),
        new("Artifact of Evolution"),
        new("Artifact of Frailty"),
        new("Artifact of Glass"),
        new("Artifact of Honor"),
        new("Artifact of Kin"),
        new("Artifact of Metamorphosis"),
        new("Artifact of Sacrifice"),
        new("Artifact of Soul"),
        new("Artifact of Spite"),
        new("Artifact of Swarms"),
        new("Artifact of Vengeance")
    };
    public bool RollMultipleArtifacts = true;
    public bool RollArtifacts = true;
    public bool RollArtifactUnlocks = true;
}