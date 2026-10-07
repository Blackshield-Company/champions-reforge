Scriptname CRF_TIF_AcceptLegion extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: commission for the Legion.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_IntroScript).Accept(1)
EndFunction
