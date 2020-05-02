using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField] private QuestConfig questConfig;
    [SerializeField] private List<QuestVisualController> questPlaces;

    private List<Quest> quests = new List<Quest>();

    public QuestConfig QuestConfig { get { return questConfig; } }

    private void Update()
    {
        TryStartNewQuest(questConfig.HardQuestCount,    QuestType.hard,     questConfig.MultiplayerTimerForHardQuest);
        TryStartNewQuest(questConfig.MediumQuestCount,  QuestType.medium,   questConfig.MultiplayerTimerForMediumQuest);
        TryStartNewQuest(questConfig.EasyQuestCount,    QuestType.easy,     questConfig.MultiplayerTimerForEasyQuest);
        TryStartNewQuest(questConfig.CampaignQuestCount,QuestType.campaign, questConfig.MultiplayerTimerForCampaignQuest);
    }

    private void TryStartNewQuest(int questCount, QuestType type, float timeMultiplier)
    {
        if (quests.Count(x => x.questType == type) < questCount)
        {
            if (questPlaces.Count(x => !x.IsUseNow) >= 2)
            {
                List<QuestVisualController> freeQuestPlaces = questPlaces.FindAll(x => !x.IsUseNow);
                QuestVisualController startPlace = freeQuestPlaces[Random.Range(0, freeQuestPlaces.Count)];
                freeQuestPlaces.Remove(startPlace);
                QuestVisualController finishPlace = freeQuestPlaces[Random.Range(0, freeQuestPlaces.Count)];

                startPlace.IsUseNow = true;
                finishPlace.IsUseNow = true;

                float secondsToEndQuest = Vector3.Distance(startPlace.GetVisualPosition(), finishPlace.GetVisualPosition()) * timeMultiplier;

                quests.Add(new Quest(type, secondsToEndQuest, startPlace, finishPlace, 100));
            }
            else
            {
                Debug.LogError("Cannot find more free quest places, add places or remove some quests");
            }
        }
    }
}
