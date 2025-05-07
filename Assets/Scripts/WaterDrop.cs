using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class WaterDrop : MonoBehaviour
{
    public float growSpeed = 0.5f;
    public float maxScale = 1f;

    private bool isReleased = false;
    private Rigidbody2D rb;

    private GameObject sprite;

    private AudioSource audioSource;
    public List<AudioResource> soundList;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
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

    private void PlayRandomSound()
    {
        int idx = Random.Range(0, soundList.Count);
        audioSource.resource = soundList[idx];
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReleased && collision.gameObject.CompareTag("Bottom"))
        {
            WaterLevelManager levelManager = FindFirstObjectByType<WaterLevelManager>();
            if (levelManager != null)
            {
                float size = transform.localScale.x;
                levelManager.AddWater(size);
                PlayRandomSound();
            }

            StartCoroutine(DestroyAfterDelay(2f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        sprite.SetActive(false);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
