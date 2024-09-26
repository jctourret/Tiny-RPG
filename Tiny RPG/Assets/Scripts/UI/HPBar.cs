using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Slider hpBar;

    private void OnEnable()
    {
        PlayerModel.OnPlayerHPChange += hpChange;
    }

    private void OnDisable()
    {
        PlayerModel.OnPlayerHPChange -= hpChange;
    }

    private void Start()
    {
        hpBar = GetComponent<Slider>();

    }

    private void hpChange(int newValue)
    {
        hpBar.value = newValue;
    }
}
