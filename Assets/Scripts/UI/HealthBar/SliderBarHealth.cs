using System.Collections;
using UnityEngine;

public class SliderBarHealth : Bar
{
    [SerializeField] private Health _health;

    private float _changeSpeed = 30f;
    private Coroutine _coroutineChange;

    private void OnEnable()
    {
        _health.ChangedHealth += ChangeDisplay;
    }

    private void OnDisable()
    {
        _health.ChangedHealth -= ChangeDisplay;
    }

    public override void ChangeDisplay()
    {
        if(_coroutineChange != null)
        {
            StopCoroutine(_coroutineChange);
        }

        _coroutineChange = StartCoroutine(ChangingValue());
    }

    private IEnumerator ChangingValue()
    {
        while(Slider.value != _health.CurrentHealth)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, _health.CurrentHealth, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}