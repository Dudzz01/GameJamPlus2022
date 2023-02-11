using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBulletEnemy : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigBullet; //bullet
    [SerializeField] public int DirBullet{get; set;}
    [SerializeField] private SpriteRenderer spriteBullet;
    
    private void Start() {
        
    }

    private void Update()
    {
        MovimentOfBullet();
        AnimationOfBullet();
    }

    public virtual void MovimentOfBullet()
    {
        float speedOfBulletX = 8.5f;
        
        
        rigBullet.velocity = new Vector2(DirBullet*speedOfBulletX,rigBullet.velocity.y);

    }

    public void AnimationOfBullet()
    {
        if(DirBullet == 1)
        {
            spriteBullet.flipX = false;
            return;
        }

            spriteBullet.flipX = true;
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

        if(col.gameObject.tag == "Espinho")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Serra")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Placa")
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D col)
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

        if(col.gameObject.tag == "Espinho")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Serra")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "Placa")
        {
            Destroy(gameObject);
        }
    }
}
