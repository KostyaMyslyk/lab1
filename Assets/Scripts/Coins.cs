using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BirdMovement>())
        {
            ScoreManager.Instance.AddScore(value);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Rotate(0f, 180f, 0f * Time.deltaTime);
    }
}
