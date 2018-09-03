using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    //Who is dying here?
    public float playerNum = 1;

    [HideInInspector]
    public bool isDead = false;

    //Game Manager
    public GameObject managerObject;
    GameController gameManager;

    public void Start()
    {
        //Game Manager
        gameManager = managerObject.GetComponent<GameController>();
        Debug.Log(playerNum);
    }

    public void Update()
    {
        if (isDead)
        {
            gameManager.WhoDied(playerNum);
        }
    }
}
