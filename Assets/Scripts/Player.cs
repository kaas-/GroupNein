using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static int _scarabsInInventory = 0;
    public static int ScarabsInInventory
    {
        get { return _scarabsInInventory; }
        set { _scarabsInInventory = value; }
    }

    private static bool _isRunning = false;
    public static bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }


    // Use this for initialization
    void Awake()
    {
    }

    public static void Interact()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.Log("Raycast");
        if (Physics.Raycast(ray, out hit, 2))
        {
            switch(hit.collider.tag)
            {
                case "Scarab":
                    UIManager.UpdateScarabs(_scarabsInInventory);
                    _scarabsInInventory++;
                    Destroy(hit.collider.gameObject);
                    break;
                case "Door":
                    if (_scarabsInInventory == 3)
                    {
                        Debug.Log("You win!");
                        GameManager.ResetGame();
                    }
                    else
                    {
                        Debug.Log("Not enough scarabs!");
                    }
                    break;
                default:
                    break;
            }
                        
        }

       
    }

    public static void CheckInteractables()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2))
        {
            Debug.Log("Found collider: " + hit.collider.tag);
            switch (hit.collider.tag)
            {
                case "Scarab":
                    UIManager.ShowInteractMessage(UIManager.InteractMessages.TakeScarab);
                    break;
                case "Door":
                    Debug.Log("Door found");
                    UIManager.ShowInteractMessage(UIManager.InteractMessages.OpenDoor);
                    break;
                default:
                    break;
            }
        }
        else
        {
            UIManager.HideInteractMessage();    
        }
    }

    public static void resetScarabs()
    {
        _scarabsInInventory = 0;
        UIManager.ResetScarabs();
    }
}
	

