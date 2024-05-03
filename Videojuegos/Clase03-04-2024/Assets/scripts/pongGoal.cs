/* Pong goal for pong

Enrique Martínez de Velasco Reyna
2024-04-10

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pongGoal : MonoBehaviour
{
    [SerializeField] string side;

//Varaible to reference another script
    PongManager manager;

    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<PongManager>();
    }

    void OnCollisionEnter2D(Collision2D other){
        manager.Score(side);
        Destroy(other.gameObject);
    }
}