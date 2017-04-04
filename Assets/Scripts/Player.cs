using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static int _hp;
    private static bool _hasKey = false;
    public static bool HasKey
    {
        get { return _hasKey; }
        set { _hasKey = value; }
    }


    // Use this for initialization
    void Awake()
    {
        _hp = 3;
    }

    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
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
                case "Key":
                    _hasKey = true;
                    break;
                case "Door":
                    if (_hasKey)
                    {
                        Debug.Log("You win!");
                        GameManager.ResetGame();
                    }
                    else
                    {
                        Debug.Log("No Key!");
                    }
                    break;
                default:
                    break;
            }
                        
        }
    }
}
	

