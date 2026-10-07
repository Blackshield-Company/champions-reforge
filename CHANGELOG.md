# Changelog

All notable changes to this project are documented here. Versions follow [Semantic Versioning](https://semver.org/).

## [0.1.0] - 2026-10-07

Early beta. First public release. It passes every automated check in the build (Papyrus compile, plugin and dialogue validation, round trip, voice file and SEQ checks, FOMOD schema, 15-plugin series compatibility check) but has not been playtested in-game yet.

### Added
- "Any commissions worth taking?" Ask Adrianne (level 10+). Choose the Legion (dragon crest) or the Stormcloaks (bear crest).
- The forging notes are with the Ash-Hand Forgebreaker at a military fort in Whiterun, Falkreath, Haafingar, the Reach, the Pale or Hjaalmarch.
- The materials: 4 steel ingots, 2 corundum ingots, 2 leather.
- Hand it all over, wait a day, collect the Champion's Harness of the Dragon (health, fire resistance) or of the Bear (stamina, frost resistance), and the Champion's Temper.
- Masterwork (optional, any time later): bring 3 ebony ingots and a dragon bone. A day later: the Masterwork Harness and the Champion's Masterwork Temper.
- Choice: Legion commission: Champion's Harness of the Dragon; with Civil War Champions, the Imperial Dragon set counts toward the temper.
- Choice: Stormcloak commission: Champion's Harness of the Bear; with Civil War Champions, the Storm-Bear set counts toward the temper.
- Choice: Masterwork: Masterwork Harness (higher rating, stronger enchantment) and the stronger temper.
- Soft integration with Civil War Champions (Anniversary Edition, ccffbsse001-imperialdragon.esl) (runtime lookup, not a master).
- Soft integration with The Gray-Mane Debt (runtime lookup, not a master).
- Soft integration with Ember Smithing (runtime lookup, not a master).
- Unvoiced dialogue with 26 silent voice files; `SEQ/ChampionsReforge.seq` for the start-game-enabled dialogue quest.
- FOMOD installer with optional Papyrus sources.
