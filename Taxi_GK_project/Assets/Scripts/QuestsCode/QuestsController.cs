using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsController : MonoBehaviour
{
    [SerializeField] private QuestConfig questConfig;
    [SerializeField] private List<QuestVisualController> questPlaces;
    [SerializeField] private CanvasGameSceneController canvasController;

    private List<Quest> quests = new List<Quest>();

    public QuestConfig QuestConfig { get { return questConfig; } }

    private void Start()
    {
        foreach (var place in questPlaces)
        {
            place.Init();
            place.gameObject.SetActive(false);
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
            if(quests[i].QuestDone)
            {
                if(quests[i].questType == QuestType.campaign)
                {
                    PlayerController.AddCampaignQuestsDone();
                }

                PlayerController.AddCash(quests[i].Prize);
                PlayerController.EndCurrentQuest();
                canvasController.RemoveTagFromMap(questPlaces.IndexOf(quests[i].EndPoint));
                quests.Remove(quests[i]);
            }
            else if(quests[i].GetSecoundToEndQuest() <= 0f)
            {
                PlayerController.RemoveCash((int)(quests[i].Prize * questConfig.NotReachingOnTimePenalty));
                PlayerController.EndCurrentQuest();
                canvasController.RemoveTagFromMap(questPlaces.IndexOf(quests[i].EndPoint));
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
                Color startColor = QuestConfig.GetStartColor(type);

                quests.Add(new Quest(type, secondsToEndQuest, startPlace, finishPlace, 
                    (int)(QuestConfig.GetMultiplierCashQuest(type) * distance), startColor, 
                    questConfig.FinishingQuestColor, questPlaces.IndexOf(startPlace), questPlaces.IndexOf(finishPlace), canvasController.ChangeTagOnQuestStart));
                canvasController.AddTagToMap(questPlaces.IndexOf(startPlace), startColor);
            }
            else
            {
                Debug.LogError("Cannot find more free quest places, add places or remove some quests");
            }
        }
    }
}
