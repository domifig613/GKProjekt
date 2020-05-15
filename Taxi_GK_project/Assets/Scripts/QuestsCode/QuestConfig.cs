using UnityEngine;

[CreateAssetMenu(fileName ="QuestConfig", menuName="QuestConfig")]
public class QuestConfig : ScriptableObject
{
    [SerializeField] private Color hardQuestColor;
    [SerializeField] private Color mediumQuestColor;
    [SerializeField] private Color easyQuestColor;
    [SerializeField] private Color campaignQuestColor;
    [SerializeField] private Color finishingQuestColor;

    [SerializeField] private int hardQuestCount;
    [SerializeField] private int mediumQuestCount;
    [SerializeField] private int easyQuestCount;
    [SerializeField] private int campaignQuestCount;

    [SerializeField] private float multiplayerTimerForHardQuest;
    [SerializeField] private float multiplayerTimerForMediumQuest;
    [SerializeField] private float multiplayerTimerForEasyQuest;
    [SerializeField] private float multiplayerTimerForCampaignQuest;

    [SerializeField] private float multiplierHardQuestCash;
    [SerializeField] private float multiplierMediumQuestCash;
    [SerializeField] private float multiplierEasyQuestCash;
    [SerializeField] private float multiplierCampaignQuestCash;
    [SerializeField] private float notReachingOnTimePenalty;

    public Color HardQuestColor { get { return hardQuestColor; } }
    public Color MediumQuestColor { get { return mediumQuestColor; } }
    public Color EasyQuestColor { get { return easyQuestColor; } }
    public Color CampaignQuestColor { get { return campaignQuestColor; } }
    public Color FinishingQuestColor { get { return finishingQuestColor; } }

    public int HardQuestCount { get { return hardQuestCount; } }
    public int MediumQuestCount { get { return mediumQuestCount; } }
    public int EasyQuestCount { get { return easyQuestCount; } }
    public int CampaignQuestCount { get { return campaignQuestCount; } }

    public float MultiplayerTimerForHardQuest { get { return multiplayerTimerForHardQuest; } }
    public float MultiplayerTimerForMediumQuest { get { return multiplayerTimerForMediumQuest; } }
    public float MultiplayerTimerForEasyQuest { get { return multiplayerTimerForEasyQuest; } }
    public float MultiplayerTimerForCampaignQuest { get { return multiplayerTimerForCampaignQuest; } }

    public float MultiplierHardQuestCash { get { return multiplierHardQuestCash; } }
    public float MultiplierMediumQuestCash { get { return multiplierMediumQuestCash; } }
    public float MultiplierEasyQuestCash { get { return multiplierEasyQuestCash; } }
    public float MultiplierCampaignQuestCash { get { return multiplierCampaignQuestCash; } }
    public float NotReachingOnTimePenalty { get { return notReachingOnTimePenalty; } }


    public Color GetStartColor(QuestType type)
    {
        switch (type)
        {
            case QuestType.campaign:
                return CampaignQuestColor;
            case QuestType.easy:
                return EasyQuestColor;
            case QuestType.medium:
                return MediumQuestColor;
            case QuestType.hard:
                return HardQuestColor;
            default:
                Debug.LogError("QuestController. GetStartColor. Unknown Quest Type");
                return new Color();
        }
    }

    public float GetMultiplierCashQuest(QuestType type)
    {
        switch (type)
        {
            case QuestType.campaign:
                return MultiplierCampaignQuestCash;
            case QuestType.easy:
                return MultiplierEasyQuestCash;
            case QuestType.medium:
                return MultiplierMediumQuestCash;
            case QuestType.hard:
                return MultiplierHardQuestCash;
            default:
                Debug.LogError("QuestController. GetStartColor. Unknown Quest Type");
                return 0f;
        }
    }
}
