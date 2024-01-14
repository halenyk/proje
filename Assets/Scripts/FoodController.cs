using UnityEngine;

public class FoodController : MonoBehaviour
{
    public float fallSpeed;  // D��me h�z�n� buradan ayarlayabilirsiniz
    Rigidbody2D rb;
    PlayerController player;
    public AudioClip zbesises;

    private float destroyTime; // Yiyece�in yok olma s�resi (saniye cinsinden)

    void Start()
    {
        fallSpeed = 1f;
        destroyTime = 10f;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime); // 10 saniye sonra yok et
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.skor >= 35)
        {
            fallSpeed = 2.15f;
        }
        else if (player.skor >= 25)
        {
            fallSpeed = 1.90f;
        }
        else if (player.skor >= 20)
        {
            fallSpeed = 1.65f;
        }
        else if (player.skor >= 10)
        {
            fallSpeed = 1.45f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * fallSpeed * Time.fixedDeltaTime * 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kase") && this.gameObject.CompareTag("Healthy"))
        {
            Destroy(this.gameObject);
            player.skor += 1;

        }
        else if (collision.gameObject.CompareTag("Kase") && this.gameObject.CompareTag("Junk"))
        {
            Destroy(this.gameObject);
            player.can -= 1;
            audio.PlayOneShot(zbesises);
        }
        else if (collision.gameObject.CompareTag("Zemin") && this.gameObject.CompareTag("Healthy"))
        {
            player.can -= 1;
        }
        else if (collision.gameObject.CompareTag("Zemin") && this.gameObject.CompareTag("Junk"))
        {
            player.skor += 1;
        }
    }
}
