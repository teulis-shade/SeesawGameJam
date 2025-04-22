using UnityEngine;

public class ShakingFlyingObject : FlyingObject
{
    [Header("Shake Config")]
    [SerializeField] private float minModulatorFreq = 1.2f;
    [SerializeField] private float maxModulatorFreq = 1.8f;
    [SerializeField] private float shakeAmount = 17f;
    [SerializeField] private int biasPower = 3;
    [SerializeField] private float maxAngle = 25f;

    [Header("Pulse Config")]
    [SerializeField] private float minScale = 1.0f;
    [SerializeField] private float maxScale = 1.15f;


    private float phaseOffset;
    private float modulatorFreq;
    private Vector3 baseScale;

    void Start()
    {
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
        modulatorFreq = Random.Range(minModulatorFreq, maxModulatorFreq);
        baseScale = transform.localScale;
    }

    void Update()
    {
        float time = Time.time + phaseOffset;
        float modulator = Mathf.Sin(time * modulatorFreq);
        float modulatorNorm = (modulator + 1f) / 2f;
        float modulatorBiased = Mathf.Pow(modulatorNorm, biasPower); 
        float shakeAngle = Mathf.Sin(shakeAmount * modulatorBiased) * maxAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, shakeAngle);
        float scaleAmount = Mathf.Lerp(minScale, maxScale, modulatorNorm);
        transform.localScale = baseScale * scaleAmount;
    }

}