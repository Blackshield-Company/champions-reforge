Scriptname CRF_AdrianneAliasScript extends ReferenceAlias
{Champion's Reforge - on Adrianne (unique actor fill; her record is not edited).}

Event OnDeath(Actor akKiller)
	(GetOwningQuest() as CRF_MainScript).AdrianneDied()
EndEvent
