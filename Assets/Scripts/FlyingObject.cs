using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public double height;

    [Header("Object Config")]
    public double mass;
    public double left;
    public double right;

    public enum HitDirection
    {
        UP,
        DOWN,
    };

    private bool isCollected = false;

    public bool CheckCollision(double xPosLeft, double xPosRight)
    {
        
        return !isCollected && (left + transform.position.x <= xPosRight && xPosLeft <= right + transform.position.x);
    }

    public virtual void Hit(HitDirection dir, PlayerScript player)
    {
        player.IncreaseMass(mass);
        FindObjectOfType<GameController>().RemoveFlyer(this);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        isCollected = true;
        AudioSource audioSource = GetComponent<AudioSource>();
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
        transform.position = new Vector3(GetInitialX(), (float)height);
    }

    protected virtual float GetInitialX()
    {
        //This will depend on the object, for now, returns a random number between -30 and 30
        return Random.Range(-18f, 18f);
    }
}
