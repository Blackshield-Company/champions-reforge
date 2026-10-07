using Modkit.Mutagen;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using static Wave3Gen.Kit;
using static Wave3Gen.Vanilla;

namespace Wave3Gen;

/// <summary>Champion's Reforge: everything specific to this mod. Kit/Program/Vanilla/ItemData are shared.</summary>
internal static class ModInfo
{
    public const string PluginName = "ChampionsReforge.esp";
    public const string Description = "Champion's Reforge: a commission for Adrianne Avenicci, a champion's harness and a temper that grows with every matching piece (also Civil War Champions armor, if installed).";

    const int APlayer = 0, AAdrianne = 1, AFortLoc = 2, AFortMarker = 3, ABreaker = 4, ANotes = 5;

    public static void Build(Kit k)
    {
        var qIntro = k.Id("CRF_Intro");
        var qMain = k.Id("CRF_Main");

        var gState = k.Global("CRF_State", 0);         // 0 new, 1 running, 2 harness delivered, 3 masterwork in progress, 4 masterwork delivered
        var gSide = k.Global("CRF_Side", 0);           // 1 Legion (Imperial Dragon), 2 Stormcloak (Storm-Bear)
        var gTier = k.Global("CRF_TemperTier", 0);     // 0 none, 1 tempered, 2 masterwork
        var gMinLevel = k.Global("CRF_MinLevel", 10);
        var gReady = k.Global("CRF_MasterReady", 0);
        var gCC = k.Global("CRF_SeriesCC", 0);         // 1 = Civil War Champions (ccffbsse001-imperialdragon.esl) found
        var gEMB = k.Global("CRF_SeriesEMB", 0);       // 1 = Ember Smithing installed
        var gGMD = k.Global("CRF_SeriesGMD", 0);       // GMD_State + 1 when The Gray-Mane Debt is installed (3 = helped Eorlund)

        var forts = k.List("CRF_FortHolds", new[] { WhiterunHoldLocation, FalkreathHoldLocation, HaafingarHoldLocation, ReachHoldLocation, PaleHoldLocation, HjaalmarchHoldLocation });

        // --- Harnesses -------------------------------------------------------------------------------------
        var eDragon = k.ArmorEnchant("CRF_EnchDragon", "Dragon's Temper", (EnchFortifyHealthConstantSelf, 30), (EnchResistFireConstantSelf, 20));
        var eBear = k.ArmorEnchant("CRF_EnchBear", "Bear's Temper", (EnchFortifyStaminaConstantSelf, 40), (EnchResistFrostConstantSelf, 30));
        var eDragonM = k.ArmorEnchant("CRF_EnchDragonMaster", "Dragon's Masterwork",
            (EnchFortifyHealthConstantSelf, 50), (EnchResistFireConstantSelf, 30), (EnchFortifyHealRateConstantSelf, 20));
        var eBearM = k.ArmorEnchant("CRF_EnchBearMaster", "Bear's Masterwork",
            (EnchFortifyStaminaConstantSelf, 60), (EnchResistFrostConstantSelf, 40), (EnchFortifyStaminaRateConstantSelf, 20));
        var hDragon = k.ArmorLike("Steel Armor", "CRF_HarnessDragon", T.HarnessDragon, 34, 1200, eDragon);
        var hBear = k.ArmorLike("Steel Armor", "CRF_HarnessBear", T.HarnessBear, 34, 1200, eBear);
        var hDragonM = k.ArmorLike("Steel Armor", "CRF_HarnessDragonMaster", T.HarnessDragonM, 42, 3200, eDragonM);
        var hBearM = k.ArmorLike("Steel Armor", "CRF_HarnessBearMaster", T.HarnessBearM, 42, 3200, eBearM);

        // --- Champion's Temper: one ability per (tier, matching pieces worn) ---------------------------------
        var fxArmor = k.ValueEffect("CRF_TemperArmorEffect", T.TemperName, ActorValue.DamageResist, T.TemperDesc);
        var fxWard = k.ValueEffect("CRF_TemperWardEffect", T.TemperWard, ActorValue.ResistMagic, T.TemperWardDesc);
        var temperI = Enumerable.Range(1, 6).Select(n => k.Ability($"CRF_TemperI{n}", T.TemperName, T.TemperDesc, (fxArmor, 15 * n))).ToArray();
        var temperII = Enumerable.Range(1, 6).Select(n => k.Ability($"CRF_TemperII{n}", T.TemperNameII, T.TemperDescII, (fxArmor, 25 * n), (fxWard, 3 * n))).ToArray();

        var notes = k.Note("CRF_ForgingNotes", T.NotesName, T.Notes, second: true);
        var breaker = k.Enemy("CRF_Forgebreaker", T.BreakerName, T.BreakerShort, LCharBanditBossNordM, NordRace);
        var msgNoStart = k.Msg("CRF_MsgNoStart", T.NoStart);
        var msgMasterReady = k.Msg("CRF_MsgMasterReady", T.MasterReady);
        var msgCC = k.Msg("CRF_MsgCCReforged", T.CCReforged);

        // --- Intro (start game enabled): opening, masterwork, and the temper tracker on the player ------------
        var intro = k.Quest(qIntro, "CRF_Intro", T.QuestName, true, Quest.TypeEnum.None, 50);
        intro.Aliases.Add(Player(0));
        intro.NextAliasID = 1;
        intro.VirtualMachineAdapter = Vmad.QuestAdapter(Vmad.Script("CRF_IntroScript",
                Vmad.Obj("CRF_Main", qMain), Vmad.Obj("CRF_State", gState), Vmad.Obj("CRF_Side", gSide), Vmad.Obj("CRF_TemperTier", gTier),
                Vmad.Obj("CRF_MasterReady", gReady), Vmad.Obj("CRF_MsgNoStart", msgNoStart), Vmad.Obj("CRF_MsgMasterReady", msgMasterReady),
                Vmad.Obj("CRF_MsgCCReforged", msgCC), Vmad.Obj("CRF_SeriesCC", gCC),
                Vmad.Obj("CRF_HarnessDragon", hDragon.FormKey), Vmad.Obj("CRF_HarnessBear", hBear.FormKey),
                Vmad.Obj("CRF_HarnessDragonMaster", hDragonM.FormKey), Vmad.Obj("CRF_HarnessBearMaster", hBearM.FormKey),
                Vmad.Obj("IngotEbony", IngotEbony), Vmad.Obj("DragonBone", DragonBone), Vmad.Alias("PlayerAlias", qIntro, 0)))
            .WithAliasScripts(qIntro, 0,
                Vmad.Script("CRF_TemperScript", Vmad.Obj("CRF_Side", gSide), Vmad.Obj("CRF_TemperTier", gTier), Vmad.Obj("CRF_SeriesCC", gCC),
                    Vmad.ObjList("Harnesses", hDragon.FormKey, hBear.FormKey, hDragonM.FormKey, hBearM.FormKey),
                    Vmad.ObjList("TemperI", temperI), Vmad.ObjList("TemperII", temperII)),
                Vmad.Script("CRF_SeriesScript", Vmad.Obj("CRF_SeriesEMB", gEMB), Vmad.Obj("CRF_SeriesGMD", gGMD)));

        // --- Main ---------------------------------------------------------------------------------------------
        var main = k.Quest(qMain, "CRF_Main", T.QuestName, false, Quest.TypeEnum.SideQuest, 60);
        Stage(main, 0, null, flags: QuestStage.Flag.StartUpStage);
        Stage(main, 10, T.Log10);
        Stage(main, 20, T.Log20);
        Stage(main, 30, T.Log30);
        Stage(main, 40, T.Log40);
        Stage(main, 100, T.Log100, QuestLogEntry.Flag.CompleteQuest);
        Stage(main, 200, T.Log200, QuestLogEntry.Flag.FailQuest);
        Objective(main, 10, T.Obj10);
        Objective(main, 11, T.Obj11, ABreaker);
        Objective(main, 20, T.Obj20, AAdrianne);
        Objective(main, 30, T.Obj30);
        Objective(main, 40, T.Obj40, AAdrianne);
        main.Aliases.Add(Player(APlayer));
        main.Aliases.Add(Unique(AAdrianne, "Adrianne", AdrianneAvenicci));
        main.Aliases.Add(Dungeon(AFortLoc, "FortLoc", forts.FormKey, LocTypeMilitaryFort));
        main.Aliases.Add(MarkerOf(AFortMarker, "FortMarker", AFortLoc));
        main.Aliases.Add(Created(ABreaker, "Breaker", breaker.FormKey, AFortMarker, false, Level.Hard, QuestAlias.Flag.StoresText));
        main.Aliases.Add(Created(ANotes, "Notes", notes.FormKey, ABreaker, true, Level.Easy, QuestAlias.Flag.QuestObject));
        main.NextAliasID = (uint)main.Aliases.Count;
        main.VirtualMachineAdapter = Vmad.QuestAdapter(Vmad.Script("CRF_MainScript",
                Vmad.Alias("Notes", qMain, ANotes), Vmad.Obj("CRF_State", gState), Vmad.Obj("CRF_Side", gSide), Vmad.Obj("CRF_TemperTier", gTier),
                Vmad.Obj("CRF_HarnessDragon", hDragon.FormKey), Vmad.Obj("CRF_HarnessBear", hBear.FormKey),
                Vmad.Obj("IngotSteel", IngotSteel), Vmad.Obj("IngotCorundum", IngotCorundum), Vmad.Obj("Leather01", Leather01),
                Vmad.Obj("CRF_Intro", qIntro)))
            .WithAliasScripts(qMain, ANotes, Vmad.Script("CRF_NotesAliasScript"))
            .WithAliasScripts(qMain, AAdrianne, Vmad.Script("CRF_AdrianneAliasScript"));

        // --- Dialogue (Adrianne Avenicci, voice type FemaleCommander) ------------------------------------------
        var sAdr = ("FemaleCommander", "Adrianne Avenicci");
        var isAdr = Cond.IsID(AdrianneAvenicci);
        Condition State(int v) => GlobalIs(gState, CompareOperator.EqualTo, v);
        Condition St(CompareOperator op, int s) => StageIs(qMain, op, s);
        var materials = new[] { PlayerHas(IngotSteel, 4), PlayerHas(IngotCorundum, 2), PlayerHas(Leather01, 2) };

        var bIntro = k.Id("CRF_IntroBranch");
        var tStart = k.Topic("CRF_I_Start", qIntro, bIntro, T.TopicStart, sAdr);
        var tLegion = k.Topic("CRF_I_Legion", qIntro, bIntro, T.TopicLegion, sAdr);
        var tStorm = k.Topic("CRF_I_Storm", qIntro, bIntro, T.TopicStorm, sAdr);
        var tDecline = k.Topic("CRF_I_Decline", qIntro, bIntro, T.TopicDecline, sAdr);
        k.Branch(bIntro, "CRF_IntroBranch", qIntro, tStart);
        var offer = new[] { isAdr, State(0), PlayerLevelAtLeast(gMinLevel), Cond.QuestRunning(CWSiegeObj, false), Cond.QuestRunning(qMain, false) };
        var sides = new[] { tLegion, tStorm, tDecline };
        k.Info(tStart, "CRF_I_StartGMD", T.StartGMD, offer.Append(GlobalIs(gGMD, CompareOperator.EqualTo, 3)).ToArray(), links: sides);
        k.Info(tStart, "CRF_I_StartEMB", T.StartEMB, offer.Append(GlobalIs(gEMB, CompareOperator.EqualTo, 1)).ToArray(), links: sides);
        k.Info(tStart, "CRF_I_StartOffer", T.Start, offer, links: sides);
        k.Info(tLegion, "CRF_I_LegionInfo", T.Legion, new[] { isAdr }, goodbye: true, fragment: "CRF_TIF_AcceptLegion");
        k.Info(tStorm, "CRF_I_StormInfo", T.Storm, new[] { isAdr }, goodbye: true, fragment: "CRF_TIF_AcceptStorm");
        k.Info(tDecline, "CRF_I_DeclineInfo", T.Decline, new[] { isAdr }, goodbye: true);

        // Masterwork (after the harness is delivered), in the always-running quest
        var bMaster = k.Id("CRF_MasterBranch");
        var tMaster = k.Topic("CRF_I_Master", qIntro, bMaster, T.TopicMaster, sAdr);
        k.Branch(bMaster, "CRF_MasterBranch", qIntro, tMaster);
        k.Info(tMaster, "CRF_I_MasterCC", T.MasterCC, new[] { isAdr, State(2), PlayerHas(IngotEbony, 3), PlayerHas(DragonBone, 1), GlobalIs(gCC, CompareOperator.EqualTo, 1) }, goodbye: true, fragment: "CRF_TIF_Master");
        k.Info(tMaster, "CRF_I_MasterGo", T.Master, new[] { isAdr, State(2), PlayerHas(IngotEbony, 3), PlayerHas(DragonBone, 1) }, goodbye: true, fragment: "CRF_TIF_Master");
        k.Info(tMaster, "CRF_I_MasterNeed", T.MasterNeed, new[] { isAdr, State(2) });
        k.Info(tMaster, "CRF_I_MasterWait", T.MasterWait, new[] { isAdr, State(3), GlobalIs(gReady, CompareOperator.EqualTo, 0) });
        k.Info(tMaster, "CRF_I_MasterDone", T.MasterDone, new[] { isAdr, State(3), GlobalIs(gReady, CompareOperator.EqualTo, 1) }, goodbye: true, fragment: "CRF_TIF_MasterCollect");

        // Commission, in the main quest
        var tHand = k.TopTopic("CRF_A_Hand", qMain, T.TopicHand, sAdr);
        k.Info(tHand, "CRF_A_HandInfo", T.Hand, new[] { isAdr, St(CompareOperator.EqualTo, 20) }.Concat(materials).ToArray(), fragment: "CRF_TIF_Hand");
        k.Info(tHand, "CRF_A_HandShort", T.HandShort, new[] { isAdr, St(CompareOperator.EqualTo, 20) });
        var tStatus = k.TopTopic("CRF_A_Status", qMain, T.TopicStatus, sAdr);
        k.Info(tStatus, "CRF_A_Status10", T.Status10, new[] { isAdr, St(CompareOperator.EqualTo, 10) });
        k.Info(tStatus, "CRF_A_Status30", T.Status30, new[] { isAdr, St(CompareOperator.EqualTo, 30) });
        var tCollect = k.TopTopic("CRF_A_Collect", qMain, T.TopicCollect, sAdr);
        k.Info(tCollect, "CRF_A_CollectDragon", T.CollectDragon, new[] { isAdr, St(CompareOperator.EqualTo, 40), GlobalIs(gSide, CompareOperator.EqualTo, 1) }, goodbye: true, fragment: "CRF_TIF_Collect");
        k.Info(tCollect, "CRF_A_CollectBear", T.CollectBear, new[] { isAdr, St(CompareOperator.EqualTo, 40), GlobalIs(gSide, CompareOperator.EqualTo, 2) }, goodbye: true, fragment: "CRF_TIF_Collect");
    }
}

internal static class T
{
    public const string QuestName = "Champion's Reforge";
    public const string BreakerName = "Ash-Hand Forgebreaker", BreakerShort = "Forgebreaker";

    public const string TopicStart = "Any commissions worth taking?";
    public static readonly string[] Start =
    {
        "One. A champion's harness, the kind my grandfather made for duelists before the war. Whoever wins the next field wants one.",
        "Problem is, the old forging notes were stolen off a supply wagon. Bandits holed up in a fort with them, calling themselves forgebreakers.",
        "Get the notes back, bring me the steel, and I'll forge the harness for whichever side you favor. Well?",
    };
    public static readonly string[] StartGMD =
    {
        "Eorlund told me what you did for the Gray-Manes. He doesn't say that about anyone. So I'll trust you with this.",
        "A champion's harness, my grandfather's design. The forging notes were stolen and carried off to a fort by bandits.",
        "Get the notes back, bring me the steel, and I'll forge it for whichever side you favor. Well?",
    };
    public static readonly string[] StartEMB =
    {
        "You've been engraving steel, haven't you? I can see the ember-work on your gear. Then you'll appreciate this commission.",
        "A champion's harness, my grandfather's design. The forging notes were stolen and carried off to a fort by bandits.",
        "Get the notes back, bring me the steel, and I'll forge it for whichever side you favor. Well?",
    };
    public const string TopicLegion = "Forge it for the Legion. Dragon crest.";
    public static readonly string[] Legion = { "The dragon, then. Father will be pleased. Bring me four steel ingots, two of corundum, two leather, and those notes." };
    public const string TopicStorm = "Forge it for the Stormcloaks. Bear crest.";
    public static readonly string[] Storm = { "The bear? Don't tell my father. Bring me four steel ingots, two of corundum, two leather, and those notes." };
    public const string TopicDecline = "Not now.";
    public static readonly string[] Decline = { "Suit yourself. The forge isn't going anywhere." };

    public const string TopicStatus = "About the champion's harness...";
    public static readonly string[] Status10 = { "The forgebreakers are holed up at <Alias=FortLoc>. Bring me those notes, and the steel." };
    public static readonly string[] Status30 = { "It's on the anvil. Give me a day. Champion's steel can't be rushed." };

    public const string TopicHand = "I have the notes and the materials.";
    public static readonly string[] Hand =
    {
        "Grandfather's hand, right down to the coffee stains. And good steel. Give me a day at the forge.",
        "Come back tomorrow. And when you wear it, wear the rest of the champion's kit too. The temper takes from every piece.",
    };
    public static readonly string[] HandShort = { "Four steel ingots, two of corundum, two leather, and the notes. Not one short." };

    public const string TopicCollect = "Is the harness ready?";
    public static readonly string[] CollectDragon =
    {
        "Ready. The dragon crest, tempered three times. Feel how it settles?",
        "That's the champion's temper. Wear matching pieces with it, and every one adds to the guard. Bring me ebony someday and I'll make it a masterwork.",
    };
    public static readonly string[] CollectBear =
    {
        "Ready. The bear crest, tempered three times. Feel how it settles?",
        "That's the champion's temper. Wear matching pieces with it, and every one adds to the guard. Bring me ebony someday and I'll make it a masterwork.",
    };

    public const string TopicMaster = "About that masterwork tempering...";
    public static readonly string[] Master = { "Three ebony ingots and a dragon's bone. Perfect. Leave the harness with me a day." };
    public static readonly string[] MasterCC =
    {
        "Three ebony ingots and a dragon's bone. And I've seen the champions' plate you carry. I'll fold the temper into it too. Leave it with me a day.",
    };
    public static readonly string[] MasterNeed = { "For a masterwork I need three ebony ingots and a dragon's bone. Nothing less will take the temper." };
    public static readonly string[] MasterWait = { "Still on the anvil. Masterwork takes the time it takes." };
    public static readonly string[] MasterDone = { "Done. Best thing I've ever made. Don't you dare let it rust." };

    public const string Log10 = "Adrianne Avenicci will forge a champion's harness for the side I chose, if I recover her grandfather's forging notes from the bandits at <Alias=FortLoc> and bring her the steel.";
    public const string Log20 = "I recovered the forging notes. Now Adrianne needs them, along with four steel ingots, two corundum ingots and two pieces of leather.";
    public const string Log30 = "Adrianne has the notes and the materials. The harness will be ready in a day.";
    public const string Log40 = "The champion's harness should be ready.";
    public const string Log100 = "Adrianne forged my champion's harness. Every matching champion's piece I wear with it adds to its temper. She says she could make it a masterwork with three ebony ingots and a dragon bone.";
    public const string Log200 = "Adrianne Avenicci is dead. The commission died with her.";

    public const string Obj10 = "Gather 4 steel ingots, 2 corundum ingots and 2 leather";
    public const string Obj11 = "Recover the forging notes at <Alias=FortLoc>";
    public const string Obj20 = "Bring the notes and the materials to Adrianne";
    public const string Obj30 = "Wait a day for Adrianne to forge the harness";
    public const string Obj40 = "Collect the harness from Adrianne";

    public const string NotesName = "Avenicci Forging Notes";
    public const string Notes = "Champion's harness, after the Colovian pattern.\n\nFold the steel three times; quench in oil, not water. Temper the harness last, and temper it to the WEARER, not the plate: every piece of matching kit will draw on it.\n\n- R. Avenicci";
    public const string HarnessDragon = "Champion's Harness of the Dragon";
    public const string HarnessBear = "Champion's Harness of the Bear";
    public const string HarnessDragonM = "Masterwork Harness of the Dragon";
    public const string HarnessBearM = "Masterwork Harness of the Bear";
    public const string TemperName = "Champion's Temper";
    public const string TemperNameII = "Champion's Masterwork Temper";
    public const string TemperDesc = "Every matching champion's piece worn increases armor rating.";
    public const string TemperDescII = "Every matching champion's piece worn increases armor rating and magic resistance.";
    public const string TemperWard = "Champion's Ward";
    public const string TemperWardDesc = "Magic resistance from the champion's masterwork temper.";
    public const string NoStart = "(The commission could not start: Adrianne must be alive and a fort must be free for the bandits. Nothing has changed; try again later.)";
    public const string MasterReady = "Adrianne should have finished the masterwork tempering by now.";
    public const string CCReforged = "Adrianne folded the champion's temper into your Civil War Champions armor. Matching pieces now count toward the Champion's Masterwork Temper.";
}
