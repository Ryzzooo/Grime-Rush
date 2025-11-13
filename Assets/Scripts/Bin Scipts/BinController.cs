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
            print("Benar +10 poin");
            print("Fakta: " + item.correctFact);
        }
        else
        {
            print("Salah -5 point");
            print("Koreksi: " + item.wrongBinMessage);
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
