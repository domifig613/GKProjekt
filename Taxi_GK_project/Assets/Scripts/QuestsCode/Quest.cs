using System;
using System.Diagnostics;
using UnityEngine;

public class Quest
{
    private QuestVisualController startPoint;
    private Stopwatch timeFromStartQuest = new Stopwatch();
    private float timeToCompleteQuestInSecound;
    private Color startColor;
    private Color endColor;
    private Action<int, Color, int> onQuestStartAction;

    public string GetQuestEndPlaceName()
    {
        return EndPoint.gameObject.name;
    }

    public int StartIndex { get; private set; }
    public int EndIndex { get; private set; }
    public bool QuestDone { get; private set; } = false;
    public int Prize { get; private set; }
    public QuestVisualController EndPoint { get; private set; }

    public QuestType questType { get; private set; }

    public Quest(QuestType questType, float timeToCompleteQuestInSecound, QuestVisualController startPoint, 
        QuestVisualController endPoint, int prize, Color startColor, Color endColor, int visualStartIndex, 
        int visualEndIndex, Action<int, Color, int> onQuestStartAction)
    {
        this.questType = questType;
        this.timeToCompleteQuestInSecound = timeToCompleteQuestInSecound;
        this.Prize = prize;
        this.startColor = startColor;
        this.endColor = endColor;
        this.startPoint = startPoint;
        this.EndPoint = endPoint;
        this.StartIndex = visualStartIndex;
        this.EndIndex = visualEndIndex;
        this.onQuestStartAction = onQuestStartAction;
        startPoint.StartQuest(startColor, StartQuest);
    }

    public void StartQuest()
    {
        if (!PlayerController.QuestIsActive())
        {
            PlayerController.TrySetActiveQuest(this);
            onQuestStartAction(StartIndex, endColor, EndIndex);
            startPoint.DisableQuestVisual();
            timeFromStartQuest.Start();
            EndPoint.StartQuest(endColor, EndQuest);
        }
    }

    public void EndQuest()
    {
        EndPoint.DisableQuestVisual();
        QuestDone = true;
    }

    public float GetSecoundToEndQuest()
    {
        return timeToCompleteQuestInSecound - (float)timeFromStartQuest.Elapsed.TotalSeconds;
    }
}
