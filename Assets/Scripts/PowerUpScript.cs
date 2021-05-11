using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7f){
            Destroy(gameObject);
        }
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * fallSpeed);
    }
}
