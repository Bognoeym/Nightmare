using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransform;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private int index = 0;
    private bool canMove;

    public float waitTime;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = movePositionTransform[0];
    }

    private void Start() {
        
    }

    private void Update() {
        if(canMove){
            navMeshAgent.destination = target.position;
            TargetCheck();
        }
        else{
            StartCoroutine(WaitForIt());
        }
    }

    void TargetCheck(){
        //Debug.Log((target.position - gameObject.transform.position).sqrMagnitude);
        if((target.position - gameObject.transform.position).sqrMagnitude < 1f && canMove){
            canMove = false;
            index++;
            if(index >= movePositionTransform.Length){
                index = 0;
            }
            target = movePositionTransform[index];
            //Debug.Log("Target: " + target.gameObject.name);
        }
    }

    IEnumerator WaitForIt(){
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
