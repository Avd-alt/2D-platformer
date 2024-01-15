using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _run = "Run";
    private string _jump = "Jump";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnRunAnimation()
    {
        _animator.SetBool(_run, true);
    }

    public void OffRunAnimation()
    {
        _animator.SetBool(_run, false);
    }

    public void OnJumpAnimation()
    {
        _animator.SetBool(_jump, true);
    }

    public void OffJumpAnimation()
    {
        _animator.SetBool(_jump, false);
    }
}