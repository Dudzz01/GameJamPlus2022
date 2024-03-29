using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBulletPlayer : ScriptBulletEnemy
{
    // Utilizei a heranca para reutilizar o código do script bullet enemy, deixando o codigo mais limpo, organizado, refatorado e mais eficaz
    public override void MovimentOfBullet()
    {
        float speedOfBulletX = 12;

        rigBullet.velocity = new Vector2(DirBullet*speedOfBulletX,rigBullet.velocity.y);

    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "BulletEnemy")
        {
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

    

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "BulletEnemy")
        {
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

        if(col.gameObject.tag == "Erva")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "PulaPula")
        {
            Destroy(gameObject);
        }
    }

    
}
