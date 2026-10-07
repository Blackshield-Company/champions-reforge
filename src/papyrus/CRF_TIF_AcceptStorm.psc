Scriptname CRF_TIF_AcceptStorm extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: commission for the Stormcloaks.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_IntroScript).Accept(2)
EndFunction
