using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{

    public List<Gun> gunPrefabs; //4 guns for each type of element

    private bool onCooldown = false;
    
    void SpawnItem() {
        if(!onCooldown) {
            //random number to choose element
            int elementType = Random.Range(0,3);

            Gun newGun = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Count)], transform.position, transform.rotation);
            
            newGun.elType = (Gun.elements)elementType;
            newGun.Randomize();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            SpawnItem();
        }
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(2);
        onCooldown = false;
    }
}
