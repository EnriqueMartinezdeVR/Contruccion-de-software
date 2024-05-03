/* Game Manager for the pong Demo 

Enrique Martínez de Velasco Reyna 

2024-04-10

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Necesary to display text in the UI
using TMPro;

public class PongManager : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float speed;
    [SerializeField] TMP_Text scoreLeft;
    [SerializeField] TMP_Text scoreRight;

    public int pointsLeft;
     public int pointsRight;

    void Start()
    {
        InitGame();
        
    }

//Start a new game
    void InitGame(){
        StartCoroutine(ServeBall());

    }

    IEnumerator ServeBall(){

        yield return new WaitForSeconds(1.0f);
        ball=Instantiate(ballPrefab);
        ball.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
    }

//Incerase the score of the specified  player

public void Score(string side){
    if (side == "left"){
        pointsLeft++;
        scoreLeft.text = pointsLeft.ToString();
        InitGame();

    }
    else if (side=="right"){
        pointsRight++;
        scoreRight.text = pointsRight.ToString();
        InitGame();
    }
}

    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
    }

    public void reset(){

        //Check that there is a ball
        if(ball!=null){
            Destroy(ball);
            InitGame();
        }
    }
}