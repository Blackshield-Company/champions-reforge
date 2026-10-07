Scriptname CRF_TIF_Collect extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: collect the harness.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_MainScript).Collect()
EndFunction
