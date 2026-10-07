# Nexus Mods page: Champion's Reforge

Early beta: v0.1.0 passed every automated build check but hasn't been playtested in-game. Exact upload-form values are in [NEXUS_FIELDS.md](NEXUS_FIELDS.md).

## Page settings

- **Game:** Skyrim Special Edition
- **Mod name:** Champion's Reforge
- **Version:** 0.1.0 (early beta)
- **Category:** Quests and Adventures
- **Suggested tags:** Quests, Lore-Friendly, Whiterun, Dialogue, Armour, Crafting, Civil War, ESL, No SKSE, Scripted
- **Requirements:** none. (Civil War Champions optional, runtime only.)
- **Permissions (suggested):** source on GitHub under MIT; allow modification with credit, no re-uploads of the archive.
- **File to upload:** `ChampionsReforge-0.1.0.zip` (FOMOD), main file, named "Champion's Reforge".

## Description (paste into the BBCode editor)

Same text as `nexus/crf-description.txt` in the workspace:

```bbcode
[center][color=#e06c3c][b]EARLY RELEASE (v0.1.0)[/b]: new mod, not yet widely playtested. Please report bugs in the Bugs tab with your Papyrus log lines (grep CRF_). Source: [url=https://github.com/synthalorian/champions-reforge]GitHub[/url][/color][/center]

[center][size=6][b]CHAMPION'S REFORGE[/b][/size]
[size=3][i]Adrianne Avenicci has her grandfather's design for a champion's harness. Somebody stole the notes.[/i][/size][/center]

[line]

[size=4][b]Summary[/b][/size]
[b]Status: Early beta (v0.1.0).[/b] Every automated build check passes, but it hasn't had wide in-game playtesting yet. It's playable now. Bug reports help shape 0.1.x.
A masterwork-smithing quest with a vanilla smith. [b]Adrianne Avenicci[/b] at Warmaiden's in Whiterun has her grandfather's design for a champion's harness, but the forging notes were stolen. Choose the side the harness is forged for, recover the notes, bring the steel, and wear a harness whose temper grows with every matching champion's piece. If you own the Anniversary Edition [b]Civil War Champions[/b] Creation, its armor of your chosen side counts too.

[size=4][b]The quest[/b][/size]
[list]
[*][b]"Any commissions worth taking?"[/b] Ask Adrianne (level 10+). Choose [b]the Legion (dragon crest)[/b] or [b]the Stormcloaks (bear crest)[/b].
[*][b]The forging notes[/b] are with the Ash-Hand Forgebreaker at a military fort in Whiterun, Falkreath, Haafingar, the Reach, the Pale or Hjaalmarch.
[*][b]The materials[/b]: 4 steel ingots, 2 corundum ingots, 2 leather.
[*][b]Hand it all over[/b], wait a day, collect the [b]Champion's Harness of the Dragon[/b] (health, fire resistance) or [b]of the Bear[/b] (stamina, frost resistance), and the [b]Champion's Temper[/b].
[*][b]Masterwork (optional, any time later)[/b]: bring 3 ebony ingots and a dragon bone. A day later: the [b]Masterwork Harness[/b] and the [b]Champion's Masterwork Temper[/b].
[/list]

[size=4][b]Choices and rewards[/b][/size]
[list]
[*][b]Legion commission[/b] ("Forge it for the Legion. Dragon crest."): Champion's Harness of the Dragon; with Civil War Champions, the [b]Imperial Dragon[/b] set counts toward the temper
[*][b]Stormcloak commission[/b] ("Forge it for the Stormcloaks. Bear crest."): Champion's Harness of the Bear; with Civil War Champions, the [b]Storm-Bear[/b] set counts toward the temper
[*][b]Masterwork[/b] ("About that masterwork tempering..." with 3 ebony ingots and a dragon bone): Masterwork Harness (higher rating, stronger enchantment) and the stronger temper
[/list]

[size=4][b]Enhanced items[/b][/size]
[list]
[*]Champion's Harness of the Dragon / of the Bear, upgradeable to Masterwork
[*]Champion's Temper: +15 armor per matching piece worn (masterwork: +25 armor and +3% magic resistance per piece), up to 6 pieces
[*]Avenicci Forging Notes
[/list]

[size=4][b]Part of the series[/b][/size]
Notices the other synthalorian mods when they're installed and never requires them. Each one is looked up at runtime with vanilla Papyrus (no SKSE, no masters):
[list]
[*][b]Civil War Champions (Anniversary Edition, ccffbsse001-imperialdragon.esl)[/b]: Every worn piece of the set matching your side counts toward the Champion's Temper: up to 6 pieces with the harness (Legion + full Imperial Dragon kit worn = +90 armor, masterwork +150 armor and +18% magic resistance). No CC record is edited, no CC asset is copied, and the CC plugin is [b]not[/b] a master; without it, only the harness counts.
[*][b]The Gray-Mane Debt[/b]: If you helped Eorlund, Adrianne has heard about it (different opening).
[*][b]Ember Smithing[/b]: Adrianne notices your ember-work (different opening).
[/list]

[size=4][b]Unvoiced dialogue that you can actually read[/b][/size]
The new lines are unvoiced and shown as subtitles. Every line ships with a silent voice file sized to its length, so subtitles stay on screen. [b]No Fuz Ro D'oh needed[/b]. Turn on dialogue subtitles.

[size=4][b]Requirements[/b][/size]
[list]
[*]Skyrim Special Edition or Anniversary Edition (1.5.97 or 1.6.x). Works on Steam under Proton on Linux.
[*]Nothing else. No SKSE, no SkyUI, no DLC, no Creation Club content required (Civil War Champions is optional).
[/list]

[size=4][b]Installation[/b][/size]
Install with [b]Mod Organizer 2[/b] or [b]Vortex[/b] (FOMOD installer) and enable [b]ChampionsReforge.esp[/b]. It's ESL-flagged. Safe to add mid-playthrough.

[size=4][b]Compatibility[/b][/size]
[list]
[*][b]No edits to vanilla records[/b], no cells, no navmesh. Skyrim.esm is the only master.
[*]Adrianne Avenicci is used through a quest alias and dialogue conditions; her record and vanilla dialogue are untouched. Mods that change her voice type move her lines away from the silent voice files.
[*]Compatible with all fifteen mods of the series (same author), checked by loading every plugin together: unique FormIDs, EditorIDs, scripts, globals and files, zero overrides.
[/list]

[size=4][b]What got scaled down / known issues[/b][/size]
[list]
[*]The Civil War Champions upgrade is a scripted ability on the player (armor/magic resistance per worn piece), not new upgraded copies of the CC armor: making real upgraded CC armor records would need the CC plugin as a hard master, and its meshes can't be redistributed. Adrianne does not hand out CC pieces; you get them through the Creation's own quest.
[*]The harness reuses the vanilla steel cuirass mesh.
[*]No forging animation or scene: Adrianne takes the materials and you come back a day later.
[*]Not playtested in-game, including the Civil War Champions path: the CC FormIDs come from UESP and were not checked against the actual .esl (I have no copy). If one is wrong, that piece just doesn't count; the Papyrus log says so.
[*]If Civil War Champions is not installed, Papyrus logs one 'file not found' style warning from Game.GetFormFromFile on each load. That's harmless.
[*]Mods that change Adrianne's voice type move her lines away from the silent voice files (FemaleCommander, per UESP).
[*]Adrianne is used through a unique-actor alias; her record and dialogue are not edited. If she dies mid-commission the quest fails cleanly.
[*]Settings are console globals: [code]set CRF_MinLevel to 10[/code]
[/list]

[size=4][b]Uninstalling[/b][/size]
As with any scripted mod, finish the quest before removing it.

[size=4][b]Source[/b][/size]
Open source (MIT): [url=https://github.com/synthalorian/champions-reforge]github.com/synthalorian/champions-reforge[/url]

[size=4][b]Credits[/b][/size]
[list]
[*][url=https://github.com/Mutagen-Modding/Mutagen]Mutagen[/url] by Noggog and contributors.
[*][url=https://github.com/russo-2025/papyrus-compiler]papyrus-compiler[/url] by russo-2025.
[*][url=https://github.com/GandaG/fomod-schema]FOMOD schema[/url] by GandaG.
[*]Bethesda Game Studios for Skyrim.
[/list]

```
