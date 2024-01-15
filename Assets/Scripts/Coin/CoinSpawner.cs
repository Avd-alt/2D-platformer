using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private CoinSpawnPoints[] _points;

    private void Start()
    {
        foreach (var _point in _points)
        {
            Instantiate(_prefab, _point.transform.position, Quaternion.identity);
        }
    }
}