using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FlyingObject : MonoBehaviour
{
    public double height;

    [Header("Object Config")]
    public double left;
    public double right;
    public string objectName;
    public string description;

    public enum HitDirection
    {
        UP,
        DOWN,
    };

    protected abstract void OnCollect(PlayerScript player, GameController gc);

    protected abstract float GetInitialX();

    private bool isCollected = false;

    public bool CheckCollision(double xPosLeft, double xPosRight)
    {
        
        return !isCollected && (left + transform.position.x <= xPosRight && xPosLeft <= right + transform.position.x);
    }

    public virtual void Hit(HitDirection dir, PlayerScript player)
    {
        // Get GameController instance
        GameController gc = FindObjectOfType<GameController>();
        // Call child OnCollect implementation
        OnCollect(player, gc);
        // Remove collectible from GameController list
        gc.RemoveFlyer(this);
        // Hide sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        // Set collected to true
        isCollected = true;
        // Start audio fade out if applicable
        AudioSource audioSource = GetComponent<AudioSource>();
        FindObjectOfType<JournalCheck>().CollectObject(name);
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.transform.SetParent(null);
            audioSource.loop = false;
            StartCoroutine(FadeOutDestroy(audioSource, 0.6f));
        }
    }

    private IEnumerator FadeOutDestroy(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }
        source.Stop();
        Destroy(this.gameObject);
    }

    public void Initialize()
    {
        transform.position = new Vector3(GetInitialX(), (float) height);
    }

    // public double height;

    // public enum HitDirection
    // {
    //     UP,
    //     DOWN,
    // };

    // private bool isCollected = false;

    // public bool CheckCollision(double xPosLeft, double xPosRight)
    // {
        
    //     return !isCollected && (left + transform.position.x <= xPosRight && xPosLeft <= right + transform.position.x);
    // }

    // public virtual void Hit(HitDirection dir, PlayerScript player)
    // {
    //     player.IncreaseMass(mass);
    //     FindObjectOfType<GameController>().RemoveFlyer(this);
    //     SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //     spriteRenderer.enabled = false;
    //     isCollected = true;
    //     AudioSource audioSource = GetComponent<AudioSource>();
    //     if (audioSource != null && audioSource.isPlaying)
    //     {
    //         audioSource.transform.SetParent(null);
    //         audioSource.loop = false;
    //         StartCoroutine(FadeOutDestroy(audioSource, 0.6f));
    //     }
    // }

    // private IEnumerator FadeOutDestroy(AudioSource source, float duration)
    // {
    //     float startVolume = source.volume;
    //     float time = 0f;
    //     while (time < duration)
    //     {
    //         time += Time.deltaTime;
    //         source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
    //         yield return null;
    //     }
    //     source.Stop();
    //     Destroy(this.gameObject);
    // }

    // public void Initialize()
    // {
    //     transform.position = new Vector3(GetInitialX(), (float)height);
    // }


}
