using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float growSpeed = 0.5f;
    public float maxScale = 2.5f;

    private bool isReleased = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void FollowMouse(Vector2 position)
    {
        if (!isReleased)
            transform.position = position;
    }

    public void Grow()
    {
        if (isReleased) return;

        float newScale = Mathf.Min(transform.localScale.x + growSpeed * Time.deltaTime, maxScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void Release()
    {
        isReleased = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isReleased && collision.gameObject.CompareTag("Bottom"))
        {
            WaterLevelManager levelManager = FindFirstObjectByType<WaterLevelManager>();
            if (levelManager != null)
            {
                float size = transform.localScale.x;
                levelManager.AddWater(size);
            }

            Destroy(gameObject);
        }
    }

}
