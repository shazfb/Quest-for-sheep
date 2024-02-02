using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Animator questgiverAnimator;
    public Player player;
    public bool QuestGiven;
    public bool QuestDone;
    

    public void Start()
    {
        questgiverAnimator = gameObject.GetComponent<Animator>();
        if (questgiverAnimator == null)
        {
            Debug.LogError("Animator component not found");
        }
    }

    public void Update()
    {
        if (player.questGiven)
        {
            QuestGiven = true;
            questgiverAnimator.SetBool("QuestGiven", true);
        }
        if (player.questSubject.IsQuestComplete())
        {
            QuestDone = true;
            questgiverAnimator.SetBool("QuestDone", true);
        }
    }


}

