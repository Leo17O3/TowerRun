using PathCreation;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerBuilder))]
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private int _playersCount;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _towersCount;
    private List<Player> _players;

    private void Start()
    {
        for (int i = 0; i < _towersCount; i++)
        {
            _players = _towerBuilder.Build(_playersCount, _pathCreator.path.length / _towersCount);
        }
    }
}
