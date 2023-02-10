using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Animator animatorEnemy;
    private bool animShoot;

    private float TimeToShoot {get; set;}

    [SerializeField]private int EyeEnemy;

    private void Start()
    {
        animShoot = false;
        TimeToShoot = 1.5f;
    }

    private void Update()
    {
        Shooting();
        AnimShootEnemy();
    }

    public void Shooting()
    {
        TimeToShoot+=Time.deltaTime;

        if(TimeToShoot>=1.5 && animShoot == false)
        {
            animShoot = true;
            GameObject bulletEnemy = Instantiate(bullet, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,0), Quaternion.identity);

            bulletEnemy.GetComponent<ScriptBulletEnemy>().DirBullet = EyeEnemy;
            
            TimeToShoot = 0;
        }
        
    }

    public void AnimShootEnemy()
    {
        if(animShoot == true)
        {
            animatorEnemy.Play("EnemyAtirando");
            StartCoroutine(AnimShootTime());
        }
        else
        {
             animatorEnemy.Play("EnemyCarregando");
        }
    }

    private IEnumerator AnimShootTime()
   {
     yield return new WaitForSeconds(1.5f);
     animShoot = false;
     yield return new WaitForSeconds(0);
   }

}
