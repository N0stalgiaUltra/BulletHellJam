using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    //controla as características básicas das balas/tiro/munição
    //velocidade, rate of fire, colisor com outros objetos, etc
    Rigidbody2D rb;  
    private void Awake() {
         rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
            
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Limite")
            Destroy(this.gameObject);
    }
}
