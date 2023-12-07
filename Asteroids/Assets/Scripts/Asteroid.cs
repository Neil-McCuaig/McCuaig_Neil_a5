using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;

    public float minSize = 0.5f;

    public float maxSize = 1.5f;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    public float speed = 50.0f;

    public float maxLifetime = 30.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //Randomizes the rotation visually
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        //Randomizes size
        this.transform.localScale = Vector3.one * this.size;
        //Ties mass to size
        _rigidbody.mass = this.size * 2.0f;
    }
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //THis works off of a tag system that you set up, not off of the fact that the bullets are named "Bullet"
        if (collision.gameObject.tag == "Bullet")
        {
            //Splits if the asteroid is large enough if half of the asteroid's size is larger then the minimum size
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
        }
    }
    private void CreateSplit()
    {
        Vector2 positions = this.transform.position;
        positions += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, positions, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
