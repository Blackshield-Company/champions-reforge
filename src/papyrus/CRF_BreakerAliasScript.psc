Scriptname CRF_BreakerAliasScript extends ReferenceAlias
{Champion's Reforge - on the Ash-Hand Forgebreaker. 0.1.1: re-checks that he carries the forging notes.}

Event OnLoad()
	(GetOwningQuest() as CRF_MainScript).EnsureNotes()
EndEvent

Event OnDeath(Actor akKiller)
	(GetOwningQuest() as CRF_MainScript).EnsureNotes()
EndEvent
