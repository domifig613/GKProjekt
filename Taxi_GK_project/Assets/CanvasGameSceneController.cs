using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CanvasGameSceneController : MonoBehaviour
{
    [SerializeField] private QuestsController questsController;
    [SerializeField] private TMPro.TMP_Text cashText;
    [SerializeField] private TMPro.TMP_Text currentQuestInfo;

    private const string NO_ACTIVE_QUEST_INFO = "No active quest";
    private bool isQuestActive = false;
    private string currentQuestFinishPlace = "";
    private TimeSpan deadlineToFinishQuest;

    private void Start()
    {
        RefreshCash();
        RefreshQuestInfo();
        PlayerController.OnCashRefresh += RefreshCash;
        PlayerController.OnQuestStateChanged += RefreshQuestInfo;
    }

    private void RefreshCash()
    {
        cashText.text = PlayerController.Cash.ToString() + "$";
    }

    private void Update()
    {
        if(isQuestActive)
        {
            currentQuestInfo.text = currentQuestFinishPlace + " time left: " + deadlineToFinishQuest.Minutes + ":" + deadlineToFinishQuest.Seconds;
            deadlineToFinishQuest -= TimeSpan.FromSeconds(Time.deltaTime);
        }
    }

    private void RefreshQuestInfo()
    {
        isQuestActive = PlayerController.QuestIsActive();

        if(isQuestActive)
        {
            Quest currentQuest = PlayerController.currentQuest;
            deadlineToFinishQuest = new TimeSpan();
            deadlineToFinishQuest += TimeSpan.FromSeconds(currentQuest.GetSecoundToEndQuest());
            currentQuestFinishPlace = currentQuest.GetQuestEndPlaceName();
            currentQuestInfo.color = questsController.QuestConfig.GetStartColor(currentQuest.questType);
        }
        else
        {
            currentQuestInfo.text = NO_ACTIVE_QUEST_INFO;
            currentQuestInfo.color = Color.white;
        }
    }
}
