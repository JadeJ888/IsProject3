using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthButBetter : MonoBehaviour
{
    enum healthType {Player, Enemy, Object};
    [SerializeField]
    healthType hType = healthType.Object;
    
    public int health = 10;

    public Gun.elements elType = Gun.elements.Fire;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject); //delete bullet
            
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if(bullet.elType == this.elType) {
                health -= bullet.damage / 2; //half dmg
            } else if(((int)bullet.elType + 2) % 4 == (int)this.elType) {
                health -= bullet.damage * 2; //double dmg 
            }
            else {
                health -= bullet.damage; //normal dmg
            }

            if(health <= 0) {
                Destroy(this.gameObject);
            } 
            else if(hType == healthType.Player) {
                Application.LoadLevel(0); //restart the scene if player health goes to 0
            }
        }
    }
}
