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

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //Randomizes the rotation visually
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        //Randomizes size
        this.transform.localScale = Vector3.one * this.size;
        //Ties mass to size
        _rigidbody.mass = this.size;
    }
}
