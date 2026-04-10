using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    public float slowDownTime = 1.2f;   
    public float stopGravity = 0.5f;    

    void OnTriggerEnter2D(Collider2D other)
    {
        BirdMovement bird = other.GetComponent<BirdMovement>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (bird && rb)
        {
            bird.enabled = false; 
            StartCoroutine(SoftStop(rb));
        }
    }

    IEnumerator SoftStop(Rigidbody2D rb)
    {
        Vector2 startVelocity = rb.linearVelocity;
        float startGravity = rb.gravityScale;

        rb.gravityScale = stopGravity;

        float t = 0f;
        while (t < slowDownTime)
        {
            t += Time.deltaTime;
            float k = 1f - (t / slowDownTime);

            rb.linearVelocity = new Vector2(
                startVelocity.x * k,
                startVelocity.y * k
            );

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
    }
}
