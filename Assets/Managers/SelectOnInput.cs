using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

	void Start () {

	}
	
	void Update () {
		if(Input.GetAxisRaw("P1_Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
            Cursor.visible = false;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
