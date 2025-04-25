using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ConsumableDisplay : MonoBehaviour
{
    public ConsumableData ConsumableData;
    private ConsumableManager consumableManager;
    private TextMeshProUGUI consumableCountText;
    public TextMeshProUGUI inputPromptText;

    void Start()
    {
        consumableManager = FindObjectOfType<ConsumableManager>();
        consumableCountText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int count = consumableManager.GetConsumableCount(ConsumableData);
        consumableCountText.text = count.ToString("#,0");
        inputPromptText.text = count <= 0 ? "" : $"<sprite=\"spritesheet_keyboard\" name=\"{ConsumableData.activationKey.ToString()}\">";
    }
}
