using UnityEngine;

public class Bincon : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenBin(bool isOpen1)
    {
        anim.SetBool("isOpen1", isOpen1);
    }
}
