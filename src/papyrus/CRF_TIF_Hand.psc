Scriptname CRF_TIF_Hand extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: hand over notes and materials.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_MainScript).HandOver()
EndFunction
