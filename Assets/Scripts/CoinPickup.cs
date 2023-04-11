using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsValue = 100;

    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().UpdateScore(pointsValue);
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);            
            Destroy(gameObject);
        }
    }
}
