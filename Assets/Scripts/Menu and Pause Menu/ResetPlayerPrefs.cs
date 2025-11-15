using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    // [ContextMenu("Nama Tombol")] akan membuat tombol
    // yang bisa kamu klik kanan di Inspector.
    
    [ContextMenu("!!! RESET 'HasPlayedFirstTime' (Hapus Ingatan) !!!")]
    public void ResetFirstTimePlayKey()
    {
        // Ganti "HasPlayedKey" dengan nama key-mu
        // (Saya tidak bisa lihat nama key di script-mu,
        //  tapi di contoh saya namanya "HasPlayedFirstTime")
        PlayerPrefs.DeleteKey("HasPlayedFirstTime"); 
        PlayerPrefs.Save();
        
        // Hati-hati, pakai ini jika kamu ingin menghapus SEMUA ingatan
        // PlayerPrefs.DeleteAll(); 
        
        Debug.LogWarning("INGATAN FIRST TIME TELAH DIRESET! (PlayerPrefs Dihapus)");
    }
}