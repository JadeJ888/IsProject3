using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform target;

    public bool readyToSpawn = true;

    void Update(){
        if(Vector3.Distance(transform.position, target.position) < 25 && readyToSpawn){
            int buddiesToSpawn = Random.Range(3,5);
            for(int i = 0; i < buddiesToSpawn; i++) {
                Spawn();
            }
            
            readyToSpawn = false;
        }
    }

    void Spawn() {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.transform.Translate(Vector3.up);
        newEnemy.GetComponent<MoveTo>().target = this.target;
    }
    
}
