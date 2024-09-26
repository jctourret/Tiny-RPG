using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        InventoryUI.OnWealthUpdate += updateWealth;
    }

    void updateWealth(int newWealth)
    {
        if (text != null)
        {
            text.text = newWealth.ToString();
        }
    }
}
