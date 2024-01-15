using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private UnityEvent _run;
    [SerializeField] private UnityEvent _stopRun;
    [SerializeField] private UnityEvent _jump;
    [SerializeField] private UnityEvent _prohibitJump;

    private const string Horizontal = "Horizontal";
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton(Horizontal))
        {
            Move();

            _run?.Invoke();
        }
        else
        {
            _stopRun?.Invoke();
        }

        if (_isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

            _jump?.Invoke();
        }
        else
        {
            _prohibitJump?.Invoke();
        }
    }

    private void Move()
    {
        Vector3 direction = transform.right * Input.GetAxis(Horizontal);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction , _speed * Time.deltaTime);

        _spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            _isGrounded = true;
        }
    }
}