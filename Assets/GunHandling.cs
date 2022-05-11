using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunHandling : MonoBehaviour
{
    public Transform hand; 
    Gun heldGun = null;

    void Update() {
        if(heldGun != null) {
            var mouse = Mouse.current;
            if(mouse == null) return;

            if(mouse.leftButton.isPressed) {
                heldGun.Fire();
            }

            var keyboard = Keyboard.current;
            if(keyboard == null) return;

            if(keyboard.eKey.isPressed) {
                heldGun.Drop();
                heldGun = null;
            }
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Gun")) {
            if(heldGun == null) {
                heldGun = other.gameObject.GetComponent<Gun>();
                heldGun.Pickup(hand);
            }
        }
    }
}
