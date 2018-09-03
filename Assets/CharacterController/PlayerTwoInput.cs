using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerTwoInput : MonoBehaviour
{

    private PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("P2_Defence"))
        {
            controller.resetDrag();
        }

        if (Input.GetButtonUp("P2_Charge"))
        {
            controller.ReCharge();
        }

        if (Input.GetButtonUp("P2_Accelerate"))
        {
            controller.moving = false;
        }
    }

    void FixedUpdate()
    {
        //Accelerate
        if (Input.GetButton("P2_Accelerate"))
        {
            controller.moving = true;
        }

        //Torque
        controller.Torque("P2_Horizontal");

        //Huddle
        if (Input.GetButton("P2_Defence") && !Input.GetButton("P2_Charge"))
        {
            controller.Huddle();
        }

        //Charge
        if (Input.GetButton("P2_Charge") && !Input.GetButton("P2_Defence"))
        {
            controller.Charge();
        }
    }
}
