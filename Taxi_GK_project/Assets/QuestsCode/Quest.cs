using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Quest
{
    private QuestVisualController startPoint;
    private QuestVisualController endPoint;
    private Stopwatch timeFromStartQuest;
    private float timeToCompleteQuestInSecound;
    private int prize;
    
    public QuestType questType { get; private set; }

    public Quest(QuestType questType, float timeToCompleteQuestInSecound, QuestVisualController startPoint, QuestVisualController endPoint, int prize)
    {
        this.questType = questType;
        this.timeToCompleteQuestInSecound = timeToCompleteQuestInSecound;

        this.prize = prize;

        timeFromStartQuest.Start();
    }

    public float GetSecoundToEndQuest()
    {
        return timeToCompleteQuestInSecound - (float)timeFromStartQuest.Elapsed.TotalSeconds;
    }
}
