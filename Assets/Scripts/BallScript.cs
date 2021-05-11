using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rBody;
    float timer;
    public bool inPlay;
    public Transform paddlePosition;
    public Transform explosion;
    public Transform powerUpLife;
    public Transform powerUpPoints;
    public Transform powerUpBall;
    public GameManager gameManager;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gameOver){
            if(!inPlay && !gameManager.runReplay){
                transform.position = paddlePosition.position;
            }else{
                timer += Time.deltaTime;
                if (timer >= 1f) {
                    timer = timer % 1f;
                    gameManager.UpdateScore(-10);
                }
            }
            if(Input.GetButtonDown("Jump") && !inPlay){
                inPlay = true;
                rBody.AddForce(Vector2.up * 250);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag ("Bottom")){        
            gameManager.LifeLost();
            inPlay = false;
            rBody.velocity = Vector2.zero;
        }        
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.CompareTag("Brick")){
            Transform newExplosion = Instantiate(explosion, col.transform.position, col.transform.rotation);
            float chance = gameManager.GetRandomValue();
            if(chance > 0.99){
                Instantiate(powerUpLife, col.transform.position, col.transform.rotation);
            }else if(chance > 0.85){
                Instantiate(powerUpPoints, col.transform.position, col.transform.rotation);
            }
            gameManager.UpdateScore(col.gameObject.GetComponent<BrickScript>().points);
            gameManager.UpdateBrickNumber();
            Destroy(newExplosion.gameObject, 0.8f);           
            Destroy(col.gameObject);
        }
    }
}
