Scriptname CRF_MainScript extends Quest
{Champion's Reforge - the commission.
Stages: 10 gather + recover notes, 20 notes recovered, 30 forging (one day), 40 ready, 100 delivered, 200 failed.}

ReferenceAlias Property Notes Auto
GlobalVariable Property CRF_State Auto
GlobalVariable Property CRF_Side Auto
GlobalVariable Property CRF_TemperTier Auto
Armor Property CRF_HarnessDragon Auto
Armor Property CRF_HarnessBear Auto
MiscObject Property IngotSteel Auto
MiscObject Property IngotCorundum Auto
MiscObject Property Leather01 Auto
Quest Property CRF_Intro Auto

Bool PendingStop = False

Function Begin()
	SetStage(10)
	SetObjectiveDisplayed(10)
	SetObjectiveDisplayed(11)
EndFunction

Function NotesTaken()
	If GetStage() != 10
		Return
	EndIf
	SetObjectiveCompleted(11)
	SetStage(20)
	SetObjectiveDisplayed(20)
EndFunction

Function HandOver()
	If GetStage() != 20
		Return
	EndIf
	Actor player = Game.GetPlayer()
	ObjectReference n = Notes.GetReference()
	If n
		player.RemoveItem(n.GetBaseObject(), 1, True)
	EndIf
	player.RemoveItem(IngotSteel, 4)
	player.RemoveItem(IngotCorundum, 2)
	player.RemoveItem(Leather01, 2)
	SetObjectiveCompleted(10)
	SetObjectiveCompleted(20)
	SetStage(30)
	SetObjectiveDisplayed(30)
	RegisterForSingleUpdateGameTime(24.0)
EndFunction

Event OnUpdateGameTime()
	If GetStage() == 30
		SetObjectiveCompleted(30)
		SetStage(40)
		SetObjectiveDisplayed(40)
	EndIf
EndEvent

Function Collect()
	If GetStage() != 40
		Return
	EndIf
	If CRF_Side.GetValueInt() == 2
		Game.GetPlayer().AddItem(CRF_HarnessBear, 1)
	Else
		Game.GetPlayer().AddItem(CRF_HarnessDragon, 1)
	EndIf
	CRF_TemperTier.SetValue(1)
	CRF_State.SetValue(2)
	SetObjectiveCompleted(40)
	SetStage(100)
	Finish()
EndFunction

; Adrianne alias OnDeath.
Function AdrianneDied()
	If GetStage() >= 100
		Return
	EndIf
	Debug.Trace("CRF_ Adrianne died at stage " + GetStage())
	FailAllObjectives()
	CRF_State.SetValue(5)
	SetStage(200)
	Finish()
EndFunction

Function Finish()
	PendingStop = True
	RegisterForSingleUpdate(4.0)
EndFunction

Event OnUpdate()
	If PendingStop
		PendingStop = False
		Stop()
	EndIf
EndEvent
