using UnityEngine;

public class BinController : MonoBehaviour
{
    private Animator anim;
    public TrashType acceptsType;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenBin(bool isOpen)
    {
        anim.SetBool("isOpen", isOpen);
    }

    public void ReceiveTrash(TrashItem item)
    {
        if (item.trashType == this.acceptsType)
        {
            string correctMessage = "+10 Poin!\n\n" + item.correctFact;
            NotificationManager.instance.ShowNotification(correctMessage);
            ScoreManager.instance.AddScore(10);
        }
        else
        {
            string wrongMessage = "-5 Poin!\n\n" + item.wrongBinMessage;
            NotificationManager.instance.ShowNotification(wrongMessage);
            ScoreManager.instance.AddScore(-5);
        }
        Destroy(item.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenBin(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenBin(false);
        }
    }
}
