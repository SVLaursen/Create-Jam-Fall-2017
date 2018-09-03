using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerOneInput : MonoBehaviour {

    private PlayerController controller;

	void Start ()
    {
        controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonUp("P1_Defence"))
        {
            controller.resetDrag();
        }

        if (Input.GetButtonUp("P1_Charge"))
        {
            controller.ReCharge();
        }

        if (Input.GetButtonUp("P1_Accelerate"))
        {
            controller.moving = false;
        }
    }

    void FixedUpdate()
    {
        //Accelerate
        if (Input.GetButton("P1_Accelerate"))
        {
            controller.moving = true;
        }

        //Torque
        controller.Torque("P1_Horizontal");

        //Huddle
        if (Input.GetButton("P1_Defence") && !Input.GetButton("P1_Charge"))
        {
            controller.Huddle();
        }

        //Charge
        if (Input.GetButton("P1_Charge") && !Input.GetButton("P1_Defence"))
        {
            controller.Charge();
        }
    }
}
