using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class IsometricSorter : MonoBehaviour
{
    [Tooltip("Tinggi objek dalam unit dunia")]
    public float objectHeight = 0f;
    
    [Tooltip("Offset untuk penyesuaian manual")]
    public int sortingOrderOffset = 0;
    
    private SpriteRenderer spriteRenderer;
    private static readonly int precision = 100;
    private float lastYPosition;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void OnEnable()
    {
        UpdateSortingOrder();
    }

    void Update()
    {
        if (transform.position.y != lastYPosition)
        {
            UpdateSortingOrder();
            lastYPosition = transform.position.y;
        }
    }
    
    public void UpdateSortingOrder()
    {
        if (spriteRenderer == null) 
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;
        }
        
        float calculatedPosition = transform.position.y - (objectHeight / 2f);
        spriteRenderer.sortingOrder = -(int)(calculatedPosition * precision) + sortingOrderOffset;
    }
    
    void OnValidate()
    {
        UpdateSortingOrder();
    }
}