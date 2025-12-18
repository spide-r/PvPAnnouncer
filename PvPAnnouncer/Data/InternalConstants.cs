using System.Collections.Generic;

namespace PvPAnnouncer.Data;
using static AnnouncerLines;
public abstract class InternalConstants
{
    public const string MessageTag = "PvPAnnouncer";
    public static readonly List<string> LimitBreakList =
        [WhatPower, PotentMagicks, WhatAClash, ThrillingBattle, BattleElectrifying];

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
    public const string MjTextClobberedTable = "Ha! You all but clobbered them with the table that round.";
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
}