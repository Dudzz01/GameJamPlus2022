using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigBullet;
    [SerializeField] public int DirBullet{get; set;}
    
    private void Start() {
        
    }

    private void Update()
    {
        MovimentOfBullet();
    }

    public void MovimentOfBullet()
    {
        float speedOfBulletX = 5;

        rigBullet.velocity = new Vector2(DirBullet*speedOfBulletX,rigBullet.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            VerifyScenes.gameOverActive = true;
        }

        if(col.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }
    }
}
