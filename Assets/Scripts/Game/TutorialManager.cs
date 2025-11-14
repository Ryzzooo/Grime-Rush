using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public GameObject panelMove;
    public GameObject panelPickUp;
    public GameObject panelDrop;
    public GameObject panelGuideBook;
    public GameObject panelDone;

    private PlayerMove playerMoveScript;

    private int curStep = 0;
    public int totalTrash = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        panelDone.SetActive(false);
        panelGuideBook.SetActive(false);
        panelDrop.SetActive(false);
        panelMove.SetActive(false);
        panelPickUp.SetActive(false);

        playerMoveScript = FindFirstObjectByType<PlayerMove>();

        StartTutorialStep(0);
    }

    public bool UpdateTrash(int curDropped)
    {
        if (curDropped >= totalTrash)
        {
            if (playerMoveScript != null)
            {
                playerMoveScript.setInteracting(true);
            }
            panelDone.SetActive(true);
            return true;
        }
        return false;
    }

    void StartTutorialStep(int step)
    {
        curStep = step;

        if (playerMoveScript != null)
        {
            playerMoveScript.setInteracting(true);
        }

        switch (step)
        {
            case 0: // Step 0: Move
                panelMove.SetActive(true);
                break;

            case 1: // Step 1: Pick Up
                panelMove.SetActive(false);
                panelPickUp.SetActive(true);
                break;
            
            case 2: // Step 2: Drop
                panelPickUp.SetActive(false);
                panelDrop.SetActive(true);
                break;
            
            case 3: // Step 3: Guide Book (PANEL BARU)
                panelDrop.SetActive(false);
                panelGuideBook.SetActive(true);
                break;
        }
    }

    // Dipanggil oleh tombol di panelMove
    public void MovePanel()
    {
        if (curStep == 0)
        {
            StartTutorialStep(1);
        }
    }

    // Dipanggil oleh PlayerControl setelah mengambil sampah
    public void PickUpPanel()
    {
        if (curStep == 1)
        {
            StartTutorialStep(2);
        }
    }

    // Dipanggil oleh PlayerControl setelah membuang sampah
    public void DropPanel()
    {
        if (curStep == 2)
        {
            // Sekarang lanjut ke step 3 (GuideBook), bukan 3 (Done)
            StartTutorialStep(3); 
        }
    }

    // Dipanggil oleh tombol di panelGuideBook (BARU)
    public void GuideBookPanel()
    {
        if (curStep == 3)
        {
            panelGuideBook.SetActive(false);

            if (playerMoveScript != null)
        {
            playerMoveScript.setInteracting(false);
        }
        }
    }

    // Dipanggil oleh tombol di panelDone
    public void DonePanel()
    {
        SceneManager.LoadScene("Stage 1");
    }
}