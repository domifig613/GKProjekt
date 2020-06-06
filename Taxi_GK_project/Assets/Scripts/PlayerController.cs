using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerController
{
    public static Quest currentQuest { get; private set; }
    public static int Cash { get; private set; } = 10000;

    public static Action OnCashRefresh = delegate { };
    public static Action OnQuestStateChanged = delegate { };

    public static bool QuestIsActive()
    {
        return currentQuest != null;
    }

    public static int CampaingQuestsDone { get; private set; } = 0;

    public static void AddCampaignQuestsDone()
    {
        CampaingQuestsDone++;
    }

    public static bool TrySetActiveQuest(Quest potentialNewQuest)
    {
        if (currentQuest == null)
        {
            currentQuest = potentialNewQuest;
            OnQuestStateChanged();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void EndCurrentQuest()
    {
        currentQuest = null;
        OnQuestStateChanged();
    }

    public static void AddCash(int additionalAmountCash)
    {
        Cash += additionalAmountCash;
        OnCashRefresh();
    }

    public static void RemoveCash(int amountOfCashToRemove)
    {
        Cash -= amountOfCashToRemove;

        if (Cash <= 0)
        {
            //game over?
            Cash = 0;
        }

        OnCashRefresh();
    }

    public static bool IsPlayerWin()
    {
        return CampaingQuestsDone >= 5;
    }

    public static void RestartGame()
    {
        Cash = 100;
        currentQuest = null;
        CampaingQuestsDone = 0;
        OnCashRefresh = delegate { };
        OnQuestStateChanged = delegate { };
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
