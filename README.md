# ror2-run-generator

This is a Risk of Rain 2 run generator. It can generate you a character to play, a run destination, artifacts to choose and unlock. Almost everything is customizable through settings.json. 

## Setup
Download the latest release, install .Net 8 runtime and setup your settings.json:
  - Set Weight for characters that you have not yet unlocked to 0 in "Characters" (they won't be chosen);
  - Delete artifacts that you have not yet unlocked from "UnlockedArtifacts";
  - Edit any other values following the desciptions and basic knowledge of JSON.
> [!NOTE]
> "ChanceRollingArtifactUnlock" means "ChanceRollingArtifactUnlock_OneIn", meaning the bigger the number is the smaller the chance becomes.

## About
I made this literally in 2 days, don't expect good quality now. I will add some more variables to affect your run (for example force you into Void Fields). Maybe will add reading savefile and using the data to force doing some unlocks. I will also make a ui version after i'm satisfied with the core code. 



![image](https://github.com/radiocannot/ror2-run-generator/assets/123190217/0bfc2b03-c49b-4e1a-9604-e41b37df4ec6)\
~~if this pops up and you died without unlocking just press enter~~
