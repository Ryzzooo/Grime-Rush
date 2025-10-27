using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform holdingSpot;
    private GameObject heldTrash;
    private GameObject trashInRange = null;
    private BinController trashcanInRange = null;
    private Animator binAnimator;

    // Update is called once per frame
    void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash != null && trashcanInRange != null)
            {
                DropTrash();
            }
            else if (heldTrash == null && trashInRange != null)
            {
                PickUpTrash(trashInRange);
            }
        }
    }

    void PickUpTrash(GameObject trashToPickUp)
    {
        heldTrash = trashToPickUp;
        heldTrash.transform.SetParent(holdingSpot);
        heldTrash.transform.localPosition = Vector3.zero;

        Collider2D trashCollider = heldTrash.GetComponent<Collider2D>();
        if (trashCollider != null)
        {
            trashCollider.enabled = false;
        }
    }

    void DropTrash()
    {
        if (heldTrash != null)
        {
            Destroy(heldTrash);
            heldTrash = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sampah"))
        {
            trashInRange = other.gameObject;
        }
        if (other.CompareTag("TrashCan"))
        {
            trashcanInRange = other.GetComponent<BinController>();
            trashcanInRange.OpenBin(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sampah") && trashInRange != null)
        {
            trashInRange = null;
        }

        if (other.CompareTag("TrashCan") && trashcanInRange != null)
        {
            trashcanInRange.OpenBin(false);
            trashcanInRange = null;
        }
    }
}
