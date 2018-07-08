using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyTypeA : MonoBehaviour
{

    public int hp;
    public bool canMelee;
    public bool canShoot;
    public float meleeDam;
    public float shotDam;
    public float personalSpace = 5f;
    public NavMeshAgent agent;

    public GameObject projPrefab;

    public Transform projSpawn;
    public float projSpeed;

    private bool isAggro;

    public void Start()
    {
        agent.isStopped = true;
        StartCoroutine(AICycle());
    }

    void Update()
    {
        agent.SetDestination(PlayerMovement.cc.transform.position);

        if (hp <= 0)
        {
            Die();
        }

    }

    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.cc.transform.position) <= personalSpace)
            agent.isStopped = true;
    }

    protected virtual IEnumerator AICycle()
    {
        while (true)
        {
            if (CanSeePlayer())
            {
                isAggro = true;
                yield return StartCoroutine(ShootRoutine());
            }
            else
            {
                if (isAggro)
                    agent.isStopped = false;

            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected virtual IEnumerator ShootRoutine()
    {
        agent.isStopped = true;
        Shoot();

        if (Vector3.Distance(transform.position, PlayerMovement.cc.transform.position) > personalSpace)
        {
            yield return new WaitForSeconds(0.5f);
            agent.isStopped = false;
            yield return new WaitForSeconds(2f);
        }
        else
            yield return new WaitForSeconds(1f);
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;

        //Debug.DrawRay(transform.position, PlayerMovement.cc.transform.position - transform.position, Color.red);

        if (Physics.Raycast(transform.position, PlayerMovement.cc.transform.position - transform.position, out hit))
        {
            //Debug.DrawLine(transform.position, hit.point, Color.green);
            if (hit.transform.CompareTag("Player"))
                return true;
        }

        return false;
    }

    private void Shoot()
    {
        Debug.Log("Skeleton casts Fireball");
        var projectile = (GameObject)Instantiate(projPrefab, projSpawn.position, projSpawn.rotation);
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projSpeed;
    }

    public void PistolHit(int damage)
    {
        Debug.Log("I got hit for " + damage + " damage! Oh no!");
        hp -= damage;
        Debug.Log("Now I have " + hp + " hit points.");
    }

    void Die()
    {
        Debug.Log("Mein Leben!");
        Destroy(gameObject);
    }
}
