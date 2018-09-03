using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    Animator myAnim;
    PlayerController controller;

	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Huddle animations
        if (controller.huddling)
        {
            myAnim.SetBool("huddling", true);
        }
        else
        {
            myAnim.SetBool("huddling", false);
        }

        //Charge animations
        if (controller.charging)
        {
            myAnim.SetBool("charging", true);
        }
        else
        {
            myAnim.SetBool("charging", false);
        }

        //Running animations
        if (controller.moving)
        {
            myAnim.SetBool("running", true);
        }
        else
        {
            myAnim.SetBool("running", false);
        }
	}
}
