using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private float initialScale;
    Vector2 movement;
    private Animator playerAnim;

    private void Start()
    {
        this.initialScale = this.transform.localScale.x;
        this.playerAnim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.y = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        this.scaleSprite();
    }

    void scaleSprite()
    {
        float scalingFactor = this.initialScale - ((Mathf.Abs((this.transform.position.y + 4) / 13)) * 0.5f);

        if (this.transform.position.y < -3)
        {
            scalingFactor = this.initialScale;
        }

        this.transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);
    }
}
