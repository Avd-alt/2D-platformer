using UnityEngine;

public class EnemyPatrolGround : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private bool _isMovingLeft = true;
    private int _randomIndexPoint;

    private void Start()
    {
        _randomIndexPoint = GetRandomIndex();
    }

    private void Update()
    {
        Move();
    }

    private void Turn()
    {
        if(_isMovingLeft)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Move()
    {
        float distanceChangePoint = 0.2f;

        Turn();

        transform.position = Vector2.MoveTowards(transform.position, _points[_randomIndexPoint].position, _speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, _points[_randomIndexPoint].position) < distanceChangePoint)
        {
            _randomIndexPoint = GetRandomIndex();
        }

        if (_points[_randomIndexPoint].position.x > transform.position.x)
        {
            _isMovingLeft = true;
        }
        else
        {
            _isMovingLeft = false;
        }
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, _points.Length);
    }
}