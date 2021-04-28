using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Andar e atirar
    //alguns ficam apenas parados e atiram

    //private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPos;

    private Vector3 roamingPos;

    public static Vector3 GetRandomDirection(){
        return new Vector3 (UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    private void Start() {
        startingPos = transform.position;
        roamingPos = GetRoamingPosition();
    }
    private void Update() {

    }
    
    private Vector3 GetRoamingPosition(){
        return startingPos + GetRandomDirection() * Random.Range(5f, 5f);
    }
    

}
