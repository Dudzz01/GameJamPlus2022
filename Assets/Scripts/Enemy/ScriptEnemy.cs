using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Animator animatorEnemy;
    private bool animShoot;

    private float TimeToShoot {get; set;}

    private float TimeToShootAgain {get; set;}

    [SerializeField]private int EyeEnemy;

    private void Start()
    {
        animShoot = false;
        TimeToShoot = 0;
    }

    private void Update()
    {
        Shooting();
        AnimShootEnemy();
    }

    public void Shooting()
    {
        TimeToShoot+=Time.deltaTime;
        //TimeToShootAgain+=Time.deltaTime;
        
        if(TimeToShoot == 0)
        {
            animatorEnemy.SetInteger("ParadoToAtirar",1);
        }
        
        if(TimeToShoot >= 1.10 && TimeToShoot < 1.108)
        {
            GameObject bulletEnemy = Instantiate(bullet, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,0), Quaternion.identity);
            bulletEnemy.GetComponent<ScriptBulletEnemy>().DirBullet = EyeEnemy;
        }

        if(TimeToShoot>=1.38)
        {
            // float duracao = animatorEnemy.GetCurrentAnimatorStateInfo (0).length;
            TimeToShoot = 0;
        }
        
    }

    public void AnimShootEnemy()
    {
        if(animShoot == true)
        {
            
            //StartCoroutine(AnimShootTime());
        }
        
    }

//     private IEnumerator AnimShootTime()
//    {
//      yield return new WaitForSeconds(1f);
//      animShoot = false;
//      yield return new WaitForSeconds(0);
//    }

}
