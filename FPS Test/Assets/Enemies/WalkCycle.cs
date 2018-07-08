using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkCycle : MonoBehaviour
{

    int walkTimer = 0;

    private SpriteRenderer sr;

    private NavMeshAgent ag;

    public Sprite walk1;
    public Sprite walk2;
    public Sprite walk3;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ag = GetComponentInParent<NavMeshAgent>();
    }

    private void Start()
    {
        sr.sprite = walk2;
    }

    private void Update()
    {

        if(!ag.isStopped)
            walkTimer++;

        if (walkTimer <= 20)
            sr.sprite = walk1;
        else if (walkTimer > 20 && walkTimer <= 40)
            sr.sprite = walk2;
        else if (walkTimer > 40 && walkTimer <= 60)
            sr.sprite = walk3;
        else if (walkTimer > 60 && walkTimer <= 80)
            sr.sprite = walk2;
        else if (walkTimer > 80)
        {
            walkTimer = 0;
        }

    }
}
