using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float sideBound;
    public float position;
    private AudioSource hitSound;
    public bool disabledLeft;
    public bool disabledRight;
    public Transform ball;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
        disabledLeft = false;
        disabledRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.runReplay){
            float horizontal = Input.GetAxis("Horizontal");
            if((horizontal < 0 && disabledLeft) || (horizontal > 0 && disabledRight) ){
                return;
            }
            transform.Translate(Vector2.right * horizontal* Time.deltaTime* speed);
            if(transform.position.x > sideBound)
            {
                transform.position = new Vector2(sideBound, transform.position.y);
            }
            else if(transform.position.x < sideBound*-1) 
            {
                transform.position = new Vector2(sideBound*-1, transform.position.y);
            }
            position = transform.position.x;
        }

    }

    public void resetPosition(){
        transform.position = new Vector2(0, transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.CompareTag("Ball")){
            hitSound.Play();
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        switch (col.transform.tag)
        {
            case "PowerUpLife":
                hitSound.Play();
                gameManager.LifeGain();
                Destroy(col.gameObject);
                break;
            case "PowerUpPoints":
                hitSound.Play();
                gameManager.UpdateScore(500);
                Destroy(col.gameObject);
                break;
        }
    }
}
