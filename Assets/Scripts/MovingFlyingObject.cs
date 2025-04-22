using UnityEngine;

public class MovingFlyingObject : FlyingObject
{
    [Header("Movement Config")]
    [SerializeField] private float minSpeed = 0.2f;
    [SerializeField] private float maxSpeed = 1.4f;
    [SerializeField] private float minAmplitude = 4f;
    [SerializeField] private float maxAmplitude = 10f;
    [SerializeField] private bool flipSprite = true;

    private float startX;
    private float lastX;
    private float speed;
    private float amplitude;
    private float phaseOffset;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        amplitude = Random.Range(minAmplitude, maxAmplitude);
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override float GetInitialX()
    {
        startX = Random.Range(-18f, 18f);
        lastX = startX;
        return startX;
    }

    void Update()
    {
        Vector3 position = transform.position;
        float offsetX = Mathf.Sin(Time.time * speed + phaseOffset) * amplitude;
        position.x = startX + offsetX;
        transform.position = position;
        float direction = position.x - lastX;
        if (flipSprite && Mathf.Abs(direction) > 0.001f)
        {
            spriteRenderer.flipX = direction >= 0;
        }
        lastX = position.x;
    }

}