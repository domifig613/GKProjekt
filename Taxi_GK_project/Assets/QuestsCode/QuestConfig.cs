using System.Collections;
using System.Collections.Generic;
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
}
