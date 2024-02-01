using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private QuestSubject questSubject = new QuestSubject();
    private QuestLogObserver questLogObserver;
    public bool isSheepCollected = false;
    public bool questGiven = false;

    public Vector3 QuestGiver;

    public GameObject questTextObject;
    public GameObject questCountertext;
    public GameObject interactText;
    public TMP_Text questText;

    public TextMeshProUGUI sheepCollectedText;
    public GameObject idleModel;
    public GameObject holdingSheepModel;

    private void Start()
    {
        interactText.SetActive(false);
        questTextObject.SetActive(false);
        questCountertext.SetActive(false);
        sheepCollectedText.text = questSubject.itemsCollected.ToString();
        idleModel.SetActive(true);
        holdingSheepModel.SetActive(false);
    }

    private void Update()
    {
        idleModel.SetActive(!isSheepCollected);
        holdingSheepModel.SetActive(isSheepCollected);


        if (IsQuestGiverInRange() | (IsTargetInRange() && questGiven) && !questSubject.IsQuestComplete())
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && IsQuestGiverInRange())
        {
            TriggerQuest();
            questCountertext.SetActive(true);
            questGiven = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && IsQuestGiverInRange() && isSheepCollected)
        {
            CollectItem();         
        }

        if (IsTargetInRange() && Input.GetKeyDown(KeyCode.E) && !isSheepCollected && questGiven)
        {
            isSheepCollected = true; 
            DeactivateSheep();

            if (isSheepCollected)
            {
                questText.SetText("Sheep Collected - Return to quest giver!");
            }
            
        }

        if (Input.GetKeyDown(KeyCode.E) && IsQuestGiverInRange() && questSubject.IsQuestComplete())
        {
            questText.SetText("Quest Complete!");
            questCountertext.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsQuestGiverInRange() && !questSubject.IsQuestComplete())
        {
            if (!isSheepCollected)
            {
                questText.SetText("Gather the 5 runaway sheep");
            }
        }
    }

    private void TriggerQuest()
    {
        questLogObserver = new QuestLogObserver(questSubject);
        questSubject.AddObserver(questLogObserver);
        questTextObject.SetActive(true);
    }

    private bool IsQuestGiverInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("QuestGiver"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsTargetInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Sheep"))
            {
                return true;
            }
        }

        return false;
    }

    private void DeactivateSheep()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Sheep"))
            {
                GameObject sheepObject = collider.gameObject;                
                sheepObject.SetActive(false);
                break;
            }
        }
    }

    private void CollectItem()
    {
        questSubject.CollectItem();
        sheepCollectedText.text = questSubject.itemsCollected.ToString();
        isSheepCollected = false;
    }
}
