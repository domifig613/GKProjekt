using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsController : MonoBehaviour
{
    [SerializeField] private QuestConfig questConfig;
    [SerializeField] private List<QuestVisualController> questPlaces;


    private List<Quest> quests = new List<Quest>();

    public QuestConfig QuestConfig { get { return questConfig; } }

    private void Start()
    {
        foreach (var place in questPlaces)
        {
            place.Init();
        }
    }

    private void Update()
    {
        TryStartNewQuest(questConfig.HardQuestCount,    QuestType.hard,     questConfig.MultiplayerTimerForHardQuest);
        TryStartNewQuest(questConfig.MediumQuestCount,  QuestType.medium,   questConfig.MultiplayerTimerForMediumQuest);
        TryStartNewQuest(questConfig.EasyQuestCount,    QuestType.easy,     questConfig.MultiplayerTimerForEasyQuest);
        TryStartNewQuest(questConfig.CampaignQuestCount,QuestType.campaign, questConfig.MultiplayerTimerForCampaignQuest);

        TryEndQuests();
    }

    private void TryEndQuests()
    {
        for (int i = quests.Count -1; i >= 0; i--)
        {
            if(quests[i].questDone)
            {
                PlayerController.AddCash(quests[i].prize);
                PlayerController.EndCurrentQuest();
                quests.Remove(quests[i]);
            }
            else if(quests[i].GetSecoundToEndQuest() <= 0f)
            {
                PlayerController.RemoveCash((int)(quests[i].prize * questConfig.NotReachingOnTimePenalty));
                PlayerController.EndCurrentQuest();
                quests[i].EndQuest();
                quests.Remove(quests[i]);
            }
        }
    }

    private void TryStartNewQuest(int questCount, QuestType type, float timeMultiplier)
    {
        if (quests.Count(x => x.questType == type) < questCount)
        {
            if (questPlaces.Count(x => !x.IsUseNow) >= 2)
            {
                List<QuestVisualController> freeQuestPlaces = questPlaces.FindAll(x => !x.IsUseNow);
                QuestVisualController startPlace = freeQuestPlaces[UnityEngine.Random.Range(0, freeQuestPlaces.Count)];
                freeQuestPlaces.Remove(startPlace);
                QuestVisualController finishPlace = freeQuestPlaces[UnityEngine.Random.Range(0, freeQuestPlaces.Count)];

                startPlace.IsUseNow = true;
                finishPlace.IsUseNow = true;
                float distance = Vector3.Distance(startPlace.GetVisualPosition(), finishPlace.GetVisualPosition());
                float secondsToEndQuest = distance * timeMultiplier;

                quests.Add(new Quest(type, secondsToEndQuest, startPlace, finishPlace, (int)(QuestConfig.GetMultiplierCashQuest(type) * distance), QuestConfig.GetStartColor(type), questConfig.FinishingQuestColor));
            }
            else
            {
                Debug.LogError("Cannot find more free quest places, add places or remove some quests");
            }
        }
    }
}
