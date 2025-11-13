using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashType trashType;
    public string correctFact;
    public string wrongBinMessage;
    public GameObject sparkleEffect;
    private GameObject curSparkle;

    void OnDisable()
    {
        if (curSparkle != null)
        {
            Destroy(curSparkle);
        }
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (isHighlighted)
        {
            if (curSparkle == null && sparkleEffect != null)
            {
                curSparkle = Instantiate(sparkleEffect, transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            if (curSparkle != null)
            {
                Destroy(curSparkle);
                curSparkle = null;
            }
        }
    }
}
