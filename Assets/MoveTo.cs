using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    float stoppingDistance = 10f;
    private NavMeshAgent agent;

    [SerializeField]
    private Rigidbody bulletprefab;
    public Transform bulletSpawn;
    private float bulletSpeed = 70f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance > stoppingDistance) {
            agent.destination = target.position;
            agent.updateRotation = true;
        } else {
            agent.destination = transform.position;
            agent.updateRotation = false;

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360);

            Fire();
        }
        
    }

    void Fire() {
        if(canFire) {
            Rigidbody copy = Instantiate(bulletprefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            copy.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);

            Debug.Log("Pow!");
            StartCoroutine(WaitToFire());
        }
    }

    bool canFire = true;
    IEnumerator WaitToFire() {
        canFire = false;
        yield return new WaitForSeconds(1);
        canFire = true;

    }
}
