using UnityEngine;
using UnityEngine.UI;

public class BarVampirism : MonoBehaviour
{
    [SerializeField] private PlayerVampirism _vampirism;
    [SerializeField] private Slider _slider;

    private void Update()
    {
        float targetValue = _vampirism.Duration;

        transform.rotation = Quaternion.identity;

        _slider.value = targetValue;
    }
}