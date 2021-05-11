using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rBody;
    float timer;
    public bool inPlay;
    public bool disabled;
    public Transform paddlePosition;
    public Transform explosion;
    public TutorialManager tutorialManager;
    
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        if(!inPlay){
            transform.position = paddlePosition.position;
        }

        if(Input.GetButtonDown("Jump") && !inPlay && !disabled){
            inPlay = true;
            rBody.AddForce(Vector2.up * 250);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.CompareTag("Brick")){
            Transform newExplosion = Instantiate(explosion, col.transform.position, col.transform.rotation);
            tutorialManager.UpdateBrickNumber();
            Destroy(newExplosion.gameObject, 0.8f);           
            Destroy(col.gameObject);
        }
    }
}
