using PathCreation;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerBuilder))]
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _towersCount;
    private List<Human> _humans;

    private void Start()
    {
        float distance = _pathCreator.path.length / _towersCount;

        for (int i = 0; i < _towersCount; i++)
        {
            _humans = _towerBuilder.Build(distance);
        }
    }

    public void RemoveHuman(Human human)
    {
        _humans.Remove(human);
    }
}
