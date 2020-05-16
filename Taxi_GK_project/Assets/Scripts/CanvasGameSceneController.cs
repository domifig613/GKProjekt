using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CanvasGameSceneController : MonoBehaviour
{
    [SerializeField] private QuestsController questsController;
    [SerializeField] private TMPro.TMP_Text cashText;
    [SerializeField] private TMPro.TMP_Text currentQuestInfo;
    [SerializeField] private GameObject map;

    [SerializeField] private List<GameObject> questBigMapTags;

    private const string NO_ACTIVE_QUEST_INFO = "No active quest";
    private bool isQuestActive = false;
    private string currentQuestFinishPlace = "";
    private TimeSpan deadlineToFinishQuest;
    private int cashForOrder = 0;

    private void Start()
    {
        CloseMap();
        RefreshCash();
        RefreshQuestInfo();
        PlayerController.OnCashRefresh += RefreshCash;
        PlayerController.OnQuestStateChanged += RefreshQuestInfo;

        foreach (var tag in questBigMapTags)
        {
            tag.SetActive(false);
        }
    }

    private void RefreshCash()
    {
        cashText.text = PlayerController.Cash.ToString() + "$";
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (isQuestActive)
        {
            currentQuestInfo.text = currentQuestFinishPlace + " time left: " + deadlineToFinishQuest.Minutes + ":" + deadlineToFinishQuest.Seconds + "\n" +
                " cash for order: " + cashForOrder;
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
            cashForOrder = currentQuest.Prize;
            currentQuestFinishPlace = currentQuest.GetQuestEndPlaceName();
            currentQuestInfo.color = questsController.QuestConfig.GetStartColor(currentQuest.questType);
        }
        else
        {
            currentQuestInfo.text = NO_ACTIVE_QUEST_INFO;
            currentQuestInfo.color = Color.white;
        }
    }

    #region map
    private void OpenMap()
    {
        map.SetActive(true);
    }

    public void CloseMap()
    {
        map.SetActive(false);
    }

    public void ChangeMapState()
    {
        map.SetActive(!map.activeSelf);
    }

    public bool MapIsOpen()
    {
        return map.activeSelf;
    }

    public void AddTagToMap(int index, Color tagColor)
    {
        questBigMapTags[index].SetActive(true);
        questBigMapTags[index].GetComponent<Image>().color = tagColor;
    }

    public void RemoveTagFromMap(int index)
    {
        questBigMapTags[index].SetActive(false);
    }

    public void ChangeTagOnQuestStart(int startIndex, Color finishColor, int finishIndex)
    {
        RemoveTagFromMap(startIndex);
        AddTagToMap(finishIndex, finishColor);
    }

    #endregion
}
