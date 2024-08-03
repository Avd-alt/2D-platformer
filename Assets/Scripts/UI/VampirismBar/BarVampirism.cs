using UnityEngine;

public class BarVampirism : Bar
{
    [SerializeField] private PlayerVampirism _playerVampirism;

    private void OnEnable()
    {
        _playerVampirism.ChangedValueHealth += ChangeDisplay;
    }

    private void OnDisable()
    {
        _playerVampirism.ChangedValueHealth -= ChangeDisplay;
    }

    public override void ChangeDisplay()
    {
        Slider.value = _playerVampirism.Duration;
    }
}