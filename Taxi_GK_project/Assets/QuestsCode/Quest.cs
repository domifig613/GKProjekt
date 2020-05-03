using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Quest
{
    private QuestVisualController startPoint;
    private QuestVisualController endPoint;
    private Stopwatch timeFromStartQuest = new Stopwatch();
    private float timeToCompleteQuestInSecound;
    private Color startColor;
    private Color endColor;

    public bool questDone { get; private set; } = false;
    public int prize { get; private set; }
    
    public QuestType questType { get; private set; }

    public Quest(QuestType questType, float timeToCompleteQuestInSecound, QuestVisualController startPoint, QuestVisualController endPoint, int prize, Color startColor, Color endColor)
    {
        this.questType = questType;
        this.timeToCompleteQuestInSecound = timeToCompleteQuestInSecound;
        this.prize = prize;
        this.startColor = startColor;
        this.endColor = endColor;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        startPoint.StartQuest(startColor, StartQuest);
    }

    public void StartQuest()
    {
        startPoint.DisableQuestVisual();
        timeFromStartQuest.Start();
        endPoint.StartQuest(endColor, EndQuest);
    }

    public void EndQuest()
    {
        endPoint.DisableQuestVisual();
        questDone = true;
    }

    public float GetSecoundToEndQuest()
    {
        return timeToCompleteQuestInSecound - (float)timeFromStartQuest.Elapsed.TotalSeconds;
    }
}
