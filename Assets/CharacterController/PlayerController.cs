using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    RaycastHit2D hit;
    Death death;
    public GameObject caster;

    public float speedForce = 10;
    public float addTorque = -1;
    public float slowdown = 100;
    public float huddleTime = 10;
    public float chargeForce = 20;
    public float chargeForceTime = 5;

    [HideInInspector]
    public bool huddling = false;
    [HideInInspector]
    public bool charging = false;
    [HideInInspector]
    public bool moving = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        death = GetComponent<Death>();
    }

    public void Acceleration()
    {
        if (moving)
        {
            rb2d.AddForce(transform.up * speedForce);
        }        
    }

    public void Torque(string playerturn)
    {
        rb2d.AddTorque(Input.GetAxis(playerturn) * addTorque);
    }

    //Defence
    public void Huddle()
    {
        if (huddleTime > 0)
        {
            huddling = true;
            rb2d.drag = slowdown;
            rb2d.angularVelocity = 0; //Turning speed is set to zero
            huddleTime -= 10 * Time.deltaTime;
        }
        
    }

    public void resetDrag()
    {
        rb2d.drag = 1; //Resets drag
        huddling = false;
    }

    //Attack
    public void Charge()
    {
        

        if (chargeForceTime > 0)
        {
            charging = true;
            rb2d.AddForce(transform.up * chargeForce);
            chargeForceTime -= 10 * Time.deltaTime;

            if (hit.collider.tag == "Player")
            {
                hit.rigidbody.AddForce(transform.up * chargeForce);
            }

        }
        
    }

    public void ReCharge()
    {
        charging = false;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(caster.transform.position, transform.up, 0.2f);
        Debug.DrawRay(caster.transform.position, transform.up, Color.red);

        Acceleration();

        if (chargeForceTime < 5 && !charging)
        {
            chargeForceTime += 2 * Time.deltaTime;
        }

        if (huddleTime < 10 && !huddling)
        {
            huddleTime += 1 * Time.deltaTime;
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Out")
        {
            death.isDead = true;
        }
    }

    public void ResetPlacement()
    {

    }
}
