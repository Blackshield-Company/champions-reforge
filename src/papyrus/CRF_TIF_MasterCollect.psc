Scriptname CRF_TIF_MasterCollect extends TopicInfo Hidden
{Champion's Reforge - dialogue fragment: collect the masterwork.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as CRF_IntroScript).CollectMasterwork()
EndFunction
