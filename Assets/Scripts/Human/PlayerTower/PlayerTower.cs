using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private List<Human> _humans;
    [SerializeField] private float _force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Human human) && IsFirstHuman(human) && human.transform.position.y <= _humans[0].transform.position.y)
        {
            Human[] downHumans = human.GetDownHumans();
            Human[] upHumans = human.GetUpHumans();

            for (int i = 0; i < downHumans.Length; i++)
            {
                _humans.Add(downHumans[i]);
                _tower.RemoveHuman(downHumans[i]);

                Destroy(downHumans[i].GetComponent<Rigidbody>());
                BoxCollider boxCollider = downHumans[i].GetComponent<BoxCollider>();
                boxCollider.isTrigger = false;

                for (int j = 0; j < _humans.Count - downHumans.Length; j++)
                {
                    Vector3 newPosition = new Vector3(_humans[j].transform.position.x, _humans[j].transform.position.y + boxCollider.size.y, _humans[j].transform.position.z);
                    _humans[j].transform.position = newPosition;
                }

                downHumans[i].transform.position = transform.position;

                downHumans[i].transform.SetParent(transform);
            }

            Vector3 direction = Vector3.forward;

            for (int i = 0; i < upHumans.Length; i++)
            {
                Rigidbody rigidbody = upHumans[i].GetComponent<Rigidbody>();

                rigidbody.isKinematic = false;
                rigidbody.WakeUp();

                _tower.RemoveHuman(upHumans[i]);

                rigidbody.AddForceAtPosition(direction * _force, upHumans[i].transform.position, ForceMode.VelocityChange);
                direction *= -1;
            }
        }
        else if (other.TryGetComponent(out Human humanDown) && IsFirstHuman(humanDown) && humanDown.transform.position.y >= _humans[0].transform.position.y)
        {
            Human[] upHumans = human.GetUpHumans();

            Vector3 direction = Vector3.forward;

            for (int i = 0; i < upHumans.Length; i++)
            {
                Rigidbody rigidbody = upHumans[i].GetComponent<Rigidbody>();

                rigidbody.isKinematic = false;
                rigidbody.WakeUp();

                _tower.RemoveHuman(upHumans[i]);

                rigidbody.AddForceAtPosition(direction * _force, upHumans[i].transform.position, ForceMode.VelocityChange);
                direction *= -1;
            }
        }
    }

    private bool IsFirstHuman(Human human)
    {
        float[] humansPositionsY = new float[_humans.Count];

        for (int i = 0; i < _humans.Count; i++)
        {
            humansPositionsY[i] = _humans[i].transform.position.y;
        }

        if (human.transform.position.y <= humansPositionsY.Min())
        {
            return true;
        }

        return false;
    }
}
