Scriptname CRF_TIF_Master extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: start the masterwork.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_IntroScript).StartMasterwork()
EndFunction
