using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmovetest : MonoBehaviour
{

    public Transform dest;

    private void Awake()
    {
        Debug.LogError(dest.position);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<NavMeshAgent>().destination = dest.position;
        }
    }
}
