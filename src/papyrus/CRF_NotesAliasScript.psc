Scriptname CRF_NotesAliasScript extends ReferenceAlias
{Champion's Reforge - on the forging notes.}

Event OnContainerChanged(ObjectReference akNewContainer, ObjectReference akOldContainer)
	If akNewContainer == Game.GetPlayer()
		(GetOwningQuest() as CRF_MainScript).NotesTaken()
	EndIf
EndEvent
