# In-game test checklist

The build verifies the plugin, scripts, dialogue structure, voice files and SEQ automatically; none of that proves the quest plays well. Turn on **dialogue subtitles** and Papyrus logging:

```ini
; Skyrim.ini (or SkyrimCustom.ini), [Papyrus]
bEnableLogging=1
bEnableTrace=1
bLoadDebugInformation=1
```

Every log line from this mod starts with `CRF_`. On each load you should see `CRF_ series check: ...` with the detected series mods. `help CRF_ 4` lists the records (ESL-flagged, FormIDs look like `FExxx8xx`).

## Opening

- [ ] Talk to Adrianne at Warmaiden's (level 10+): "Any commissions worth taking?"; pick a side; stage 10.
- [ ] With The Gray-Mane Debt (Eorlund helped) or Ember Smithing installed, the opening changes.

## Commission

- [ ] Forgebreaker at a fort's map marker; notes on him: stage 20.
- [ ] Hand-over without full materials: she lists them; with them: items removed, stage 30; `wait` 24h: stage 40.
- [ ] Collect: harness of your side added, quest completes. Wear it: Champion's Temper in Active Effects.

## Civil War Champions (if owned)

- [ ] `player.additem FE0xxD69 1` style: use `help "Imperial Dragon" 4` to find the pieces. Equip pieces of your side: the temper ability steps up one tier per piece (check Active Effects). Pieces of the other side don't count.
- [ ] Papyrus log: no 'Civil War Champions not installed' line.

## Masterwork

- [ ] 3 ebony ingots + dragon bone: removed; after a day a message; collect: harness swapped (re-equipped if worn), temper becomes the masterwork version.

## Series integration

- [ ] Without any other series mod: the plain opening is offered, the log shows all integration values 0, nothing errors.
- [ ] With each listed mod installed, its variant appears (see README, Series integration).

## What to report

Your game version, load order, the `CRF_` lines from Papyrus.0.log, and which step went wrong.
