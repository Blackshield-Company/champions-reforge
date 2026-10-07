Scriptname CRF_SeriesScript extends ReferenceAlias
{Champion's Reforge - soft integration with the rest of the series, on the player alias of CRF_Intro.
Plugins are looked up at runtime (vanilla Papyrus, no SKSE); when one is missing the call returns None.}

GlobalVariable Property CRF_SeriesEMB Auto
GlobalVariable Property CRF_SeriesGMD Auto
{0 = The Gray-Mane Debt not installed, otherwise GMD_State + 1 (3 = helped Eorlund)}

Event OnInit()
	Refresh()
EndEvent

Event OnPlayerLoadGame()
	Refresh()
EndEvent

Function Refresh()
	Form embProbe = Game.GetFormFromFile(0x800, "EmberSmithing.esp") ; soft: EMB_HeatTemperedIronSword
	If embProbe
		CRF_SeriesEMB.SetValue(1.0)
	Else
		CRF_SeriesEMB.SetValue(0.0)
	EndIf
	GlobalVariable gmdState = Game.GetFormFromFile(0x802, "GrayManeDebt.esp") as GlobalVariable ; soft: GMD_State
	If gmdState
		CRF_SeriesGMD.SetValue(gmdState.GetValue() + 1.0)
	Else
		CRF_SeriesGMD.SetValue(0.0)
	EndIf
	Debug.Trace("CRF_ series check: EMB " + CRF_SeriesEMB.GetValueInt() + ", GMD " + CRF_SeriesGMD.GetValueInt())
EndFunction
