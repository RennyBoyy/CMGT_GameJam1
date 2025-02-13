using UnityEngine;

public class AppleCollect : MonoBehaviour
{
    public AudioClip eatSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(eatSound, Camera.main.transform.position);
            FindObjectOfType<AppleCounter>().AddApple();
            Destroy(gameObject);
        }
    }
}
