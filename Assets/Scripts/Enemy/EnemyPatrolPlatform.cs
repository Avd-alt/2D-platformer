using UnityEngine;

public class EnemyPatrolPlatform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundDetection;

    private float _distanceDetection = 1;
    private bool _isMovingLeft = true;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _distanceDetection);

        if(groundInfo.collider == false)
        {
            if(_isMovingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _isMovingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _isMovingLeft = true;
            }
        }
    }
}