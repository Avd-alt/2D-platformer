using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public abstract void ChangeDisplay();
}