using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform holdingSpot;
    private GameObject heldTrash;
    private GameObject trashInRange;

    // Update is called once per frame
    void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash == null && trashInRange != null)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sampah"))
        {
            trashInRange = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sampah") && trashInRange != null)
        {
            trashInRange = null;
        }
    }
}
