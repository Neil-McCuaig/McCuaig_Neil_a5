using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    // The _ in front are because I'm not sure if it will conflict with anything if I just call it "rigidbody" for example. Better safe then sorry.

    private Rigidbody2D _rigidbody;
    private bool _thrust;
    private float _turnDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input checks
        _thrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow)) 
        { 
            _turnDirection = -1.0f; 
        } else 
        { 
            _turnDirection = 0.0f; 
        }
    }

    private void FixedUpdate()
    {
        // .up makes it go forward, not "Up" as in the top of the screen.
        if (_thrust)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection != 0.0)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }
}
