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
    private List<Humanoid> _players;

    private void Start()
    {
        float distance = _pathCreator.path.length / _towersCount;

        for (int i = 0; i < _towersCount; i++)
        {
            _players = _towerBuilder.Build(_playersCount, distance);
        }
    }
}
