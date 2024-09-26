using UnityEngine;
using UnityEngine.UI;

public class STBar : MonoBehaviour
{
    Slider stBar;

    private void OnEnable()
    {
        PlayerModel.OnPlayerSTChange += stChange;
    }

    private void OnDisable()
    {
        PlayerModel.OnPlayerSTChange -= stChange;
    }

    private void Start()
    {
        stBar = GetComponent<Slider>();

    }

    private void stChange(int newValue)
    {
        stBar.value = newValue;
    }
}
