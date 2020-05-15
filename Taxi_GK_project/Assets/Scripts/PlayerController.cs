using System;

public static class PlayerController
{
    public static Quest currentQuest { get; private set; }
    public static int Cash { get; private set; } = 100;

    public static Action OnCashRefresh = delegate { };
    public static Action OnQuestStateChanged = delegate { };

    public static bool QuestIsActive()
    {
        return currentQuest != null;
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
        OnCashRefresh();

        if (Cash <= 0)
        {
            //game over? :P
        }
    }
}
