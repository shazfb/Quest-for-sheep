using System.Collections.Generic;

public class QuestSubject
{
    private List<IObserver> observers = new List<IObserver>();
    public int itemsCollected = 0;
    public int questTarget = 5;
    
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void CollectItem()
    { 
        itemsCollected++;
        NotifyObservers();
        
    
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify();
        }
    }

    public bool IsQuestComplete()
    {
        return itemsCollected >= questTarget;
    }
}
