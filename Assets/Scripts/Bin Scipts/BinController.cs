using UnityEngine;

public class BinController : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenBin(bool isOpen)
    {
        anim.SetBool("isOpen", isOpen);
    }
}
