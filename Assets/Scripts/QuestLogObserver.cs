using UnityEngine;

public class QuestLogObserver : IObserver
{
    private QuestSubject questSubject;

    public QuestLogObserver(QuestSubject questSubject)
    {
        this.questSubject = questSubject;
    }

    public void OnNotify()
    {
        if (questSubject.IsQuestComplete())
        {
            Debug.Log("Quest Complete! You've collected all items.");
        }
        else
        {
            Debug.Log("Item collected. Quest progress:" + questSubject.IsQuestComplete() + "/" + questSubject.questTarget);
        }
    }
}
