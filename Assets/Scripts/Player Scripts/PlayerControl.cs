using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public Transform holdingSpot;
    private GameObject heldTrash;
    private BinController trashcanInRange = null;
    private PlayerMove moveScript;
    Animator anim;
    private GameObject curSparkle = null;
    private TrashItem trashInRange = null;


    void Awake()
    {
        moveScript = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
    }

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
                StartCoroutine(DropTrash());
            }
            else if (heldTrash == null && trashInRange != null)
            {
                StartCoroutine(PickUpTrash(trashInRange));
            }
        }
    }

    IEnumerator PickUpTrash(TrashItem trashToPickUp)
    {
        moveScript.setInteracting(true);

        if (trashToPickUp != null)
        {
            trashToPickUp.SetHighlight(false);
        }

        trashInRange = null;
        moveScript.PlayInteractionAnimation("ambil_depan");
        yield return new WaitForSeconds(0.5f);

        heldTrash = trashToPickUp.gameObject;
        heldTrash.transform.SetParent(holdingSpot);
        heldTrash.transform.localPosition = Vector3.zero;

        Collider2D trashCollider = heldTrash.GetComponent<Collider2D>();
        if (trashCollider != null)
        {
            trashCollider.enabled = false;
        }
        moveScript.setInteracting(false);
    }

    IEnumerator DropTrash()
    {
        moveScript.setInteracting(true);

        Vector2 playerPos = transform.position;
        Vector2 binPos = trashcanInRange.transform.position;
        Vector2 diff = playerPos - binPos;

        if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
        {
            if (diff.y > 0) 
            {
                anim.SetTrigger("buang_depan");
            }
            else 
            {
                anim.SetTrigger("buang_belakang");
            }
        }
        else
        {
            if (diff.x > 0)
            {
                moveScript.SetFlipX(false); // Menghadap Kiri
                moveScript.PlayInteractionAnimation("buang_samping");
            }
            else
            {
                moveScript.SetFlipX(true); // Menghadap Kanan
                moveScript.PlayInteractionAnimation("buang_samping");
            }
        }

        yield return new WaitForSeconds(0.7f);

        TrashItem item = heldTrash.GetComponent<TrashItem>();

        if (trashcanInRange != null && item != null)
        {
            trashcanInRange.ReceiveTrash(item);
            heldTrash = null;
        }
        else
        {
            if (heldTrash != null)
            {
                Destroy(heldTrash);
                heldTrash = null;
            }
        }
        moveScript.setInteracting(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sampah"))
        {
            if(trashInRange == null)
            {
                TrashItem item = other.GetComponent<TrashItem>();
                if (item != null)
                {
                    trashInRange = item;

                    if (heldTrash == null)
                    {
                        trashInRange.SetHighlight(true);
                    }
                }
            } 
        }
        if (other.CompareTag("TrashCan"))
        {
            trashcanInRange = other.GetComponent<BinController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sampah") && trashInRange != null && other.gameObject == trashInRange.gameObject)
        {
            trashInRange.SetHighlight(false);
            trashInRange = null;
        }

        if (other.CompareTag("TrashCan") && trashcanInRange != null)
        {
            BinController exitingBin = other.GetComponent<BinController>();
            if (exitingBin != null && exitingBin == trashcanInRange)
            {
                trashcanInRange = null;
            }
        }
    }
}
