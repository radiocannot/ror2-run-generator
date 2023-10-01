# ror2-run-generator

This is a Risk of Rain 2 run generator. It can generate you a character to play, a run destination, artifacts to choose and unlock. Almost everything is customizable through settings.json. 

## Setup
Download the latest release, install .Net 8 runtime and setup your settings.json:
  - Set Weight for characters that you have not yet unlocked to 0 in "Characters" (they won't be chosen);
  - Delete artifacts that you have not yet unlocked from "UnlockedArtifacts";
  - Edit any other values following the desciptions and basic knowledge of JSON.
> [!NOTE]
> "ChanceRollingArtifactUnlock" means "ChanceRollingArtifactUnlock_OneIn", meaning the bigger the number is the smaller the chance becomes.
