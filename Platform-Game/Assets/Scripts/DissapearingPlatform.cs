using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 3f; // Time before the platform disappears
    public Color warningColor = Color.red; // Color to flash before disappearing
    public int flashCount = 5; // Number of flashes before disappearing

    private bool isPlayerOnPlatform = false;
    private float timer = 0f;
    private Color originalColor;
    private Renderer platformRenderer;

    private void Start()
    {
        
        platformRenderer = GetComponent<Renderer>();
        originalColor = platformRenderer.material.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            timer = 0f; // Reset the timer when the player leaves the platform
            platformRenderer.material.color = originalColor; // Reset color
        }
    }

    private void Update()
    {
        if (isPlayerOnPlatform)
        {
            timer += Time.deltaTime;

            // Trigger flashing effect as the timer approaches the delay
            if (timer >= disappearDelay - (disappearDelay / flashCount))
            {
                float lerp = Mathf.PingPong(Time.time * flashCount, 1f);
                platformRenderer.material.color = Color.Lerp(originalColor, warningColor, lerp);
            }

            // Disable the platform after the delay
            if (timer >= disappearDelay)
            {
                platformRenderer.material.color = originalColor; // Reset color
                gameObject.SetActive(false);
            }
        }
    }
}
