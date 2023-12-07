using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;


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
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        //This makes you shoot once per click, or at least it should.
        //This being in fixed update makes it fire strangely
        //The Input.GetMouseButton makes it fire way too fast with a touchpad.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Fire();
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

    private void Fire()
    {
        //The latter two "this" and the bullet.project make it spawn where the player is facing
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            //Turns off everything to do with the player
            this.gameObject.SetActive(false);

            FindObjectOfType<Game_Manager>().PlayerDied();
        }
    }
}
