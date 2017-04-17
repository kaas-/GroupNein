using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager instance = null;

    public GameObject WhilePlaying;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject StartScreen;

    public GameObject Interact;
    public GameObject Scarabs;

    private Text InteractText;

    public enum InteractMessages { OpenDoor, TakeScarab};

    private bool InteractMessageIsActive = false;

    public GameObject Scarab1, Scarab2, Scarab3;
    private GameObject[] ScarabArray;

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

        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
        InteractText = instance.Interact.GetComponent<Text>();
        instance.WinScreen.SetActive(false);
        instance.LoseScreen.SetActive(false);
        instance.Interact.SetActive(false);
        instance.WhilePlaying.SetActive(false);

        ScarabArray = new GameObject[] { Scarab1, Scarab2, Scarab3 };

    }

    public static void ShowInteractMessage(InteractMessages message)
    {
        instance.InteractMessageIsActive = true;
        instance.Interact.SetActive(true);
        if (message == InteractMessages.OpenDoor)
        {
            instance.InteractText.text = "Open door";
        }
        if (message == InteractMessages.TakeScarab)
        {
            instance.InteractText.text = "Take scarab";
        }
    }

    public static void HideInteractMessage()
    {
        if (instance.InteractMessageIsActive)
        {
            instance.InteractMessageIsActive = false;
            instance.Interact.SetActive(false);
        }

    }

    public static void UpdateScarabs(int i)
    {
        instance.ScarabArray[i].GetComponent<RawImage>().texture = Resources.Load("scarab_yellow") as Texture2D;
        instance.ScarabArray[i].GetComponent<RawImage>().color = Color.white;
    }

    public static void ResetScarabs()
    {
        foreach (GameObject scarab in instance.ScarabArray)
        {
            scarab.GetComponent<RawImage>().texture = Resources.Load("scarab_black") as Texture2D;
            scarab.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.4f);
        }
    }
	
    public static void StartGame()
    {
        instance.StartScreen.SetActive(false);
        instance.WhilePlaying.SetActive(true);
    }

    public static void EndGame()
    {
        instance.WhilePlaying.SetActive(false);
        instance.WinScreen.SetActive(true);
    }

    public static void ResetGame()
    {
        instance.WinScreen.SetActive(false);
        instance.StartScreen.SetActive(true);
    }

}
