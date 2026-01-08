using System.Collections.Generic;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public static readonly List<string> LimitBreakList =
        [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, NeitherSideHoldingBack, 
            BattleElectrifying, MjGameChanger, MjBoldMove, MjHeatingUp, AbsoluteBrutality, SuchFerocity, SomethingsComing];

    public const string NoRespectText = "This man has absolutely no respect for the rules!";
    public const string UpOnThePostText = "He's up on the post! You know what that means...";
    public const string BombarianPressText = "It's the Bombarian press!";
    public const string OhMercyText = "Oh mercy is she doing what I think she's doing?";
    public const string BackTailText = "Yes! You've got the serpent on the back tail! Press the advantage and finish this!";
    public const string MjTextGameChanger = "This could be a real game changer.";
    public const string MjTextBoldMove = "A bold move from our challenger, but will it pay off?";
    public const string MjTextChallengerRecover = "How will the challenger recover from this I wonder?";
    public const string MjTextRivalVying = "Whats this? A rival vying for victory?";
    public const string MjTextHeatingUp = "Things are really heating up. I can scarcely look away.";
    public const string MjTextHandDecided = "There it is. The hand is decided.";
    public const string MjTextKissLadyLuck = "And with a kiss from lady luck, we have our winner.";
    public const string MjTextGoodnessGracious = "Oh goodness gracious me.";
    public const string MjTextOurTile = "That should have been our tile, the scoundrel.";
    public const string MjTextPainfulToWatch = "Oh the horror! It was too painful to watch.";
    public const string MjTextClobberedTable = "Ha! You all but clobbered them with a table that round."; //NOTE: Metem actually says "the table" instead of "a table" but it sounds similar enough that it should pass i think 
    public const string MjTextBeautifullyPlayed = "Beautifully played my friend, beautifully played.";
    public const string MjTextMadeYourMark = "You certainly made your mark this round. Keep it up!";
    public const string MjTextDontStandAChance = "Mmm, letting them think they stand a chance. I like it.";
    public const string MjTextChallengerDownHard = "Oh my, our challenger went down hard.";
    public const string MjTextStillInIt = "Our challenger is still in it! But for how long?";
    public const string MjTextStillStanding = "Ooh even I felt that one, but our challenger is still standing.";
    public const string MjTextDownNotOut = "Our challenger is down, but not out.";
    public const string MjTextTitanOfTable = "The titan of the table, tactician of the tiles! Brilliantly Played.";
    public const string MjTextCommendableEffort = "A commendable effort! You should be proud.";
    public const string MjTextReportingLive = "This was metem, reporting live from the Mahjong table. Thank you and good night.";
    public const string MjTextCompetitionTooMuch = "Was the competition simply too much for our challenger? I should hope not.";
    public const string MjTextUtterlyHumiliated = "Oh dear our challenger has been utterly humiliated! I fear this will haunt them...";
    
    public const string TextLindwurmsHeart = "Huh? Is it? The Lindwurm’s heart!?";
    public const string TextBadFeeling = "It’s pulsating… I’ve got a bad feeling about this!";
    public const string TextTransforming = "It’s contorted…transforming!";
    public const string TextItsAliveLindwurm = "It’s alive! It’s the birth of a new Lindwurm!";
    public const string TextBattleNotOverDontDespair = "Alas, the battle isn’t over yet, but don’t despair Champion!";
    public const string TextCreatingMore = "The Lindwurm is creating more of itself!";
    public const string TextContinuesToMultiply = "The Lindwurm continues to multiply! We’ve gone well beyond mortal limits now!";
    public const string TextCuriousProps = "Some curious props have appeared… What would they possibly be?";
    public const string TextTakenOnChampionForm = "Egads! They’ve taken on the Champion’s form! This bodes ill!";
    public const string TextDejaVu = "Ah-I’ve got a serious case of deja vu here.";
    public const string TextRingRendInTwo = "What destructiveness! The ring has been rend in two!";
    public const string TextMutationCorrupting = "The mutation is…corrupting the champion! Let’s hope they keep it together!";
    public const string TextSomethingsComing = "Something’s coming! Stay on your toes!";
    public const string TextWarpedFabric = "Woah! What is this place? Did the Lindwurm warp the very fabric of space and time?";
    public const string TextAnotherDimension = "Is this another dimension? I must confess, I am utterly bewildered!";
    public const string TextHardPressed = "A relentless assault! The Champion is hard-pressed!";
    public const string TextFeverPitch = "The battle has reached a fever pitch, but the Champion still stands!";
    public const string TextStayStrong = "There’s so many of them! Stay strong, I beg you!";
    public const string TextNotSureIUnderstand = "I’m not sure I understand what’s going on anymore, folks!";
    public const string TextLeftRealmOfLogic = "We appear to have completely left the realm of logic behind!";
    public const string TextDefiesComprehension = "Something that defies all comprehension is unfolding before our very eyes!";
}