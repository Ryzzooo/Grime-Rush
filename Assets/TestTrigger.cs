using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debugging paling jujur
        Debug.Log("### TES FISIKA MURNI BERHASIL ### Nama yang masuk: " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("### TES FISIKA MURNI KELUAR ### Nama yang keluar: " + other.name);
    }
}