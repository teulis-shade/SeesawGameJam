using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;


public class ConsumableDisplay : MonoBehaviour
{
    [Header("References")]
    public ConsumableData ConsumableData;
    private ConsumableManager consumableManager;
    private GameController gameController;
    public TextMeshProUGUI consumableCountText;
    public TextMeshProUGUI inputPromptText;
    [Header("Pop text effect config")]
    public float popSpeed = 2f;
    public float maxScale = 2.0f;
    private Vector3 originalScale;

    void Start()
    {
        consumableManager = FindObjectOfType<ConsumableManager>();
        gameController = FindObjectOfType<GameController>();
        gameController.OnConsumableCollected += OnConsumableCollected;
        originalScale = consumableCountText.rectTransform.localScale;
    }

    public void OnConsumableCollected(ConsumableData consumable)
    {
        StopAllCoroutines();
        StartCoroutine(PopEffect());
    }

    private IEnumerator PopEffect()
    {
        float t = 0f;
        bool effectInProgress = true;
        while (effectInProgress)
        {
            t += Time.deltaTime * popSpeed;
            t = Mathf.Clamp01(t);
            float sineValue = Mathf.Sin(t * Mathf.PI);
            float scaleFactor = Mathf.Lerp(1, maxScale, (sineValue + 1f) / 2f);
            consumableCountText.rectTransform.localScale = originalScale * scaleFactor;
            if (t >= 1f)
            {   
                effectInProgress = false;
            }
            yield return null; 
        }
        consumableCountText.rectTransform.localScale = originalScale;
    }

    void Update()
    {
        int count = consumableManager.GetConsumableCount(ConsumableData);
        consumableCountText.text = count.ToString("#,0");
        inputPromptText.text = count <= 0 ? "" : $"<sprite=\"spritesheet_keyboard\" name=\"{ConsumableData.activationKey.ToString()}\">";
    }
}
