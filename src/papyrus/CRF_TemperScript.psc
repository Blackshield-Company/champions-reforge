Scriptname CRF_TemperScript extends ReferenceAlias
{Champion's Reforge - the Champion's Temper, on the player alias of CRF_Intro (always running).
Counts the matching champion's pieces the player wears: the harness, plus the five pieces of the Civil War
Champions set of the chosen side (Imperial Dragon for the Legion, Storm-Bear for the Stormcloaks) when the
Anniversary Edition plugin ccffbsse001-imperialdragon.esl is installed. Nothing is a master: the CC armor is
looked up at runtime with Game.GetFormFromFile (vanilla Papyrus, no SKSE) and simply not counted when absent.
No CC record is edited; the bonus is an ability on the player.}

GlobalVariable Property CRF_Side Auto
GlobalVariable Property CRF_TemperTier Auto
GlobalVariable Property CRF_SeriesCC Auto
Armor[] Property Harnesses Auto
Spell[] Property TemperI Auto
Spell[] Property TemperII Auto

Form[] Pieces
Int ResolvedSide = -1

Event OnInit()
	Refresh()
EndEvent

Event OnPlayerLoadGame()
	ResolvedSide = -1
	Refresh()
EndEvent

Event OnObjectEquipped(Form akBaseObject, ObjectReference akReference)
	If akBaseObject as Armor
		Recount()
	EndIf
EndEvent

Event OnObjectUnequipped(Form akBaseObject, ObjectReference akReference)
	If akBaseObject as Armor
		Recount()
	EndIf
EndEvent

Function Refresh()
	Resolve()
	Recount()
EndFunction

; Looks up the Civil War Champions pieces of the chosen side. FormIDs from UESP (Skyrim:Civil War Champions Items).
Function Resolve()
	Int side = CRF_Side.GetValueInt()
	If side == ResolvedSide
		Return
	EndIf
	ResolvedSide = side
	Pieces = new Form[5]
	Form probe = Game.GetFormFromFile(0xD69, "ccffbsse001-imperialdragon.esl")
	If probe
		CRF_SeriesCC.SetValue(1.0)
	Else
		CRF_SeriesCC.SetValue(0.0)
		Debug.Trace("CRF_ Civil War Champions not installed; only the harness counts toward the temper")
		Return
	EndIf
	If side == 1
		Pieces[0] = probe
		Form idBoots = Game.GetFormFromFile(0xD68, "ccffbsse001-imperialdragon.esl")
		If idBoots
			Pieces[1] = idBoots
		EndIf
		Form idGauntlets = Game.GetFormFromFile(0x84D, "ccffbsse001-imperialdragon.esl")
		If idGauntlets
			Pieces[2] = idGauntlets
		EndIf
		Form idHelmet = Game.GetFormFromFile(0x848, "ccffbsse001-imperialdragon.esl")
		If idHelmet
			Pieces[3] = idHelmet
		EndIf
		Form idShield = Game.GetFormFromFile(0x84A, "ccffbsse001-imperialdragon.esl")
		If idShield
			Pieces[4] = idShield
		EndIf
	ElseIf side == 2
		Form sbArmor = Game.GetFormFromFile(0x84C, "ccffbsse001-imperialdragon.esl")
		If sbArmor
			Pieces[0] = sbArmor
		EndIf
		Form sbBoots = Game.GetFormFromFile(0x849, "ccffbsse001-imperialdragon.esl")
		If sbBoots
			Pieces[1] = sbBoots
		EndIf
		Form sbGauntlets = Game.GetFormFromFile(0xD6A, "ccffbsse001-imperialdragon.esl")
		If sbGauntlets
			Pieces[2] = sbGauntlets
		EndIf
		Form sbHelmet = Game.GetFormFromFile(0xD6B, "ccffbsse001-imperialdragon.esl")
		If sbHelmet
			Pieces[3] = sbHelmet
		EndIf
		Form sbShield = Game.GetFormFromFile(0xD6C, "ccffbsse001-imperialdragon.esl")
		If sbShield
			Pieces[4] = sbShield
		EndIf
	EndIf
	Int i = 0
	While i < 5
		If Pieces[i] && !(Pieces[i] as Armor)
			Debug.Trace("CRF_ Civil War Champions form " + i + " is not armor; ignored")
			Pieces[i] = None
		EndIf
		i += 1
	EndWhile
EndFunction

Function Recount()
	Int tier = CRF_TemperTier.GetValueInt()
	Actor player = Game.GetPlayer()
	Int worn = 0
	If tier > 0
		Resolve()
		Int i = 0
		While i < Harnesses.Length
			If Harnesses[i] && player.IsEquipped(Harnesses[i])
				worn = 1
			EndIf
			i += 1
		EndWhile
		If Pieces
			i = 0
			While i < Pieces.Length
				If Pieces[i] && player.IsEquipped(Pieces[i])
					worn += 1
				EndIf
				i += 1
			EndWhile
		EndIf
	EndIf
	Int n = 0
	While n < 6
		If tier != 1 || worn != n + 1
			player.RemoveSpell(TemperI[n])
		EndIf
		If tier != 2 || worn != n + 1
			player.RemoveSpell(TemperII[n])
		EndIf
		n += 1
	EndWhile
	If worn > 0 && worn <= 6
		If tier == 1
			player.AddSpell(TemperI[worn - 1], False)
		ElseIf tier >= 2
			player.AddSpell(TemperII[worn - 1], False)
		EndIf
	EndIf
EndFunction
