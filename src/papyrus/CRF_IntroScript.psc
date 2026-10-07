Scriptname CRF_IntroScript extends Quest
{Champion's Reforge - always-running quest: Adrianne's opening topic, the masterwork upgrade, and the temper.}

Quest Property CRF_Main Auto
GlobalVariable Property CRF_State Auto
{0 = not started, 1 = commission running, 2 = harness delivered, 3 = masterwork on the anvil, 4 = masterwork delivered}
GlobalVariable Property CRF_Side Auto
GlobalVariable Property CRF_TemperTier Auto
GlobalVariable Property CRF_MasterReady Auto
GlobalVariable Property CRF_SeriesCC Auto
Message Property CRF_MsgNoStart Auto
Message Property CRF_MsgMasterReady Auto
Message Property CRF_MsgCCReforged Auto
Armor Property CRF_HarnessDragon Auto
Armor Property CRF_HarnessBear Auto
Armor Property CRF_HarnessDragonMaster Auto
Armor Property CRF_HarnessBearMaster Auto
MiscObject Property IngotEbony Auto
MiscObject Property DragonBone Auto
ReferenceAlias Property PlayerAlias Auto

Function Accept(Int aiSide)
	If CRF_State.GetValueInt() != 0 || CRF_Main.IsRunning()
		Return
	EndIf
	CRF_Side.SetValue(aiSide)
	If CRF_Main.Start()
		CRF_State.SetValue(1)
		(CRF_Main as CRF_MainScript).Begin()
		Debug.Trace("CRF_ commission started, side " + aiSide)
	Else
		CRF_Side.SetValue(0)
		Debug.Trace("CRF_ CRF_Main failed to start (Adrianne dead or no free fort)")
		CRF_MsgNoStart.Show()
	EndIf
EndFunction

Function StartMasterwork()
	If CRF_State.GetValueInt() != 2
		Return
	EndIf
	Actor player = Game.GetPlayer()
	player.RemoveItem(IngotEbony, 3)
	player.RemoveItem(DragonBone, 1)
	CRF_State.SetValue(3)
	CRF_MasterReady.SetValue(0)
	RegisterForSingleUpdateGameTime(24.0)
EndFunction

Event OnUpdateGameTime()
	If CRF_State.GetValueInt() == 3
		CRF_MasterReady.SetValue(1)
		CRF_MsgMasterReady.Show()
	EndIf
EndEvent

Function CollectMasterwork()
	If CRF_State.GetValueInt() != 3 || CRF_MasterReady.GetValueInt() != 1
		Return
	EndIf
	Actor player = Game.GetPlayer()
	Armor oldHarness = CRF_HarnessDragon
	Armor newHarness = CRF_HarnessDragonMaster
	If CRF_Side.GetValueInt() == 2
		oldHarness = CRF_HarnessBear
		newHarness = CRF_HarnessBearMaster
	EndIf
	Bool wasWorn = player.IsEquipped(oldHarness)
	If player.GetItemCount(oldHarness) > 0
		player.RemoveItem(oldHarness, 1, True)
	EndIf
	player.AddItem(newHarness, 1)
	CRF_TemperTier.SetValue(2)
	CRF_State.SetValue(4)
	If wasWorn
		player.EquipItem(newHarness, False, True)
	EndIf
	(PlayerAlias as CRF_TemperScript).Refresh()
	If CRF_SeriesCC.GetValueInt() == 1
		CRF_MsgCCReforged.Show()
	EndIf
	Debug.Trace("CRF_ masterwork delivered")
EndFunction
