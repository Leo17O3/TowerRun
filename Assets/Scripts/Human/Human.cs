using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    public Transform FixationPoint => _fixationPoint;

    public Human[] GetDownHumans()
    {
        Human[] humans = FindObjectsOfType<Human>();
        List<Human> _downHumans = new List<Human>();

        for (int i = 0; i < humans.Length; i++)
        {
            if (humans[i].transform.parent == transform.parent && humans[i].FixationPoint.position.y <= transform.position.y)
            {
                _downHumans.Add(humans[i]);
            }
        }

        return _downHumans.ToArray();
    }

    public Human[] GetUpHumans()
    {
        Human[] humans = FindObjectsOfType<Human>();
        List<Human> upHumans = new List<Human>();

        for (int i = 0; i < humans.Length; i++)
        {
            if (humans[i].transform.parent == transform.parent && humans[i].FixationPoint.position.y > transform.position.y)
            {
                upHumans.Add(humans[i]);
            }
        }

        return upHumans.ToArray();
    }
}
