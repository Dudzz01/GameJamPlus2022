using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float TimeToShoot {get; set;}

    [SerializeField]private int EyeEnemy;

    private void Start()
    {
        TimeToShoot = 2;
    }

    private void Update()
    {
        Shooting();
    }

    public void Shooting()
    {
        TimeToShoot-=Time.deltaTime;

        if(TimeToShoot<=0)
        {
            GameObject bulletEnemy = Instantiate(bullet, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,0), Quaternion.identity);

            bulletEnemy.GetComponent<ScriptBullet>().DirBullet = EyeEnemy;
            
            TimeToShoot = 2;
        }
    }

}
