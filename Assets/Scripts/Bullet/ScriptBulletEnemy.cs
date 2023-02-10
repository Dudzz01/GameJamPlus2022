using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBulletEnemy : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigBullet; //bullet
    [SerializeField] public int DirBullet{get; set;}
    
    private void Start() {
        
    }

    private void Update()
    {
        MovimentOfBullet();
    }

    public virtual void MovimentOfBullet()
    {
        float speedOfBulletX = 5;

        rigBullet.velocity = new Vector2(DirBullet*speedOfBulletX,rigBullet.velocity.y);

    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            VerifyScenes.gameOverActive = true;
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject); 
        }
    }
}
