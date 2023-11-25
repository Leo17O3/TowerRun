using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private Human[] _humanTemplates;
    private List<Human> _humans = new List<Human>();
    [SerializeField] private float _force;

    private void Awake()
    {
        Spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == transform)
        {
            return;
        }


        if (other.TryGetComponent(out Human human))
        {
            Human[] downHumans = human.GetDownHumans();
            Human[] upHumans = human.GetUpHumans();

            for (int i = 0; i < downHumans.Length; i++)
            {
                _tower.RemoveHuman(downHumans[i]);
                _humans.Add(downHumans[i]);

                Destroy(downHumans[i].GetComponent<Rigidbody>());
                BoxCollider boxCollider = downHumans[i].GetComponent<BoxCollider>();
                Destroy(boxCollider);

                for (int j = 0; j < _humans.Count - downHumans.Length; j++)
                {
                    Vector3 newPosition = new Vector3(_humans[j].transform.position.x, _humans[j].FixationPoint.position.y, _humans[j].transform.position.z);
                    _humans[j].transform.position = newPosition;
                }

                downHumans[i].transform.position = transform.position;

                downHumans[i].transform.SetParent(transform);
            }

            if (upHumans.Length > 0)
            {
                Destroy(upHumans[0].transform.parent.gameObject);
            }

            Vector3 direction = Vector3.forward;


            for (int i = 0; i < upHumans.Length; i++)
            {
                Rigidbody rigidbody = upHumans[i].GetComponent<Rigidbody>();

                rigidbody.WakeUp();
                rigidbody.isKinematic = false;

                _tower.RemoveHuman(upHumans[i]);
                upHumans[i].transform.SetParent(null);
                upHumans[i].GetComponent<Animator>().applyRootMotion = false;
                upHumans[i].GetComponent<BoxCollider>().isTrigger = false;

                rigidbody.AddForceAtPosition(direction * _force, upHumans[i].transform.position, ForceMode.VelocityChange);
                direction *= -1f;

                Destroy(upHumans[i].gameObject, 5f);
            }
        }
        else if (other.TryGetComponent(out Human humanDown) && humanDown.transform.position.y >= _humans[0].transform.position.y)
        {
            Human[] upHumans = human.GetUpHumans();

            if (upHumans.Length > 0)
            {
                Destroy(upHumans[0].transform.parent.gameObject);
            }

            Vector3 direction = Vector3.forward;


            for (int i = 0; i < upHumans.Length; i++)
            {
                Rigidbody rigidbody = upHumans[i].GetComponent<Rigidbody>();

                rigidbody.WakeUp();
                rigidbody.isKinematic = false;
                upHumans[i].GetComponent<Animator>().applyRootMotion = false;
                upHumans[i].GetComponent<BoxCollider>().isTrigger = false;

                _tower.RemoveHuman(upHumans[i]);
                upHumans[i].transform.SetParent(null);

                rigidbody.AddForceAtPosition(direction * _force, upHumans[i].transform.position, ForceMode.VelocityChange);
                direction *= -1f;

                Destroy(upHumans[i].gameObject, 5f);
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

    private void Spawn()
    {
        Human human = Instantiate(_humanTemplates[Random.Range(0, _humanTemplates.Length)], transform.position, Quaternion.identity, transform);
        _humans.Add(human);
        DisableCollider(human.GetComponent<BoxCollider>());
        EnableKinematicRigidbody(human.GetComponent<Rigidbody>());
    }

    private void DisableCollider(BoxCollider collider)
    {
        Destroy(collider);
    }

    private void EnableKinematicRigidbody(Rigidbody rigidbody)
    {
        rigidbody.isKinematic = true;
    }

    public Human[] GetHumans(int humansCount)
    {
        Human[] humans = _humans.GetRange(0, humansCount).ToArray();

        return humans;
    }
}
