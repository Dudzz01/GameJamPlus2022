   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Profiling;
public class ScriptPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ManageSpawnPoints ManageSpawn;
    [SerializeField] private GameObject textFinal;
    [SerializeField] private GameObject bulletPlayer;
    public static int QuantidadeErvasColetadas {get; set;}
    #region SoundsPlayerVariables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audiosPlayer = new List<AudioClip>();
    #endregion
    #region AnimationPlayerVariables
    [SerializeField] private ScriptAnimatorPlayer animPlayer;
    [SerializeField] private SpriteRenderer spritePlayer;
    public string statePlayer{get; private set;} // verificar o state do player, só declarei, nao implementei ainda
    #endregion
    #region WallJump/Jump/Collisions/Actions Variables
    public bool IsGround {get; private set;} // verifica se o player está colidindo com o chao ou nao
    public float IsGroundTimerJumpCoyote{get; private set;}
    public bool IsWallRight {get; private set;} // verifica se o player está colidindo com a parede ou nao
    public bool IsWallLeft{get; private set;}
    public bool IsSliding {get; private set;}
    public bool IsWallJump {get; private set;}
    public bool IsWallJumping {get; private set;}
    public bool CanMove{get; private set;}
    private bool doubleJump;
    [SerializeField]private LayerMask groundMask;
    [SerializeField]private LayerMask objectsGroundMask;
    [SerializeField]private Transform transformFeet;
    [SerializeField]private Transform transformArm;
    [SerializeField]private float powerJump;
    private Vector2 rightOffSetArm;
    private Vector2 leftOffSetArm;
    private bool jump;

    private float shootTime;
    #endregion
    #region DashVariables
    
    public bool canDash {get; private set;} // permite se o player pode dar dash ou nao
    public bool isDashing {get; private set;} // verifica se o player esta executando a acao dash
    public float dashPower {get; private set;} // forca do dash
    public float timeDurationDash {get; private set;} // tempo de duracao do dash
    public float timeCooldownDash {get; private set;} // tempo de cooldown do dash
    #endregion
    #region MovimentPlayerVariables

    public float directionPlayerH{get; private set;} // direcao horizontal do player
    public float directionPlayerY{get; private set;} // direcao horizontal do player
    #endregion
    #region ActionPermissionVariables
    [SerializeField] private bool[] arrayOfActionPermissionOfPlayer = new bool[4];
    private bool shootPlayerAnim;
    
    #endregion
    private void Awake() 
    { 
        //Inicializando Váriaveis
        
        rightOffSetArm = new Vector2(0.13f,0);// Offset da posicao do colisor do braco direito do player
        leftOffSetArm = new Vector2(-0.03f,0);// Offset da posicao do colisor do braco esquerdo do player
        spritePlayer = GetComponent<SpriteRenderer>();
        trailRenderer.sortingLayerName = "Player";
        statePlayer = "MovePlayer"; // Stateplayer é o estado de animacao do player
        CanMove = true;
        dashPower = 12f;
        timeDurationDash = 0.6f;
        timeCooldownDash = 0.5f;
        canDash = true;
        jump = false;
        isDashing = false;
        shootTime = 0.8f;
        
    }

    

    private void Update() {

       

        if(isDashing == true) // enquanto o player estiver "dashando" ele vai retornar para a funcao void e nao executara outro comando
        {
            animPlayer.AnimationPlayer("IndioDash");
            return;
        }

        
        
        PlayerAnimMoviment(statePlayer); // maquina de estados para gerenciar as animacoes do player
        #region DirectionsPlayer
        directionPlayerH = Input.GetAxisRaw("Horizontal"); // variavel para saber a direcao h do player
        directionPlayerY = Input.GetAxisRaw("Vertical"); // variavel para saber a direcao y do player
        #endregion
        #region CollidersPlayer
        IsGround = Physics2D.OverlapBox((Vector2)transformFeet.position ,new Vector2(0.25f,0.20f),0,groundMask) || Physics2D.OverlapBox(transformFeet.position,new Vector2(0.25f,0.24f),0,objectsGroundMask); // verifica se o pé do player está colidindo com o chao
        IsWallRight = Physics2D.OverlapCircle((Vector2)transformArm.position+rightOffSetArm,0.19f,groundMask); // retornará true se o colisor do braco direito do player estiver colidindo na parede
        IsWallLeft = Physics2D.OverlapCircle((Vector2)transformArm.position+leftOffSetArm,0.19f,groundMask); // retornará true se o colisor do braco esquerdo do player estiver colidindo na parede
        #endregion
        #region ActionPlayer
        // Walk(); 
        // JumpInput();
        // JumpMovimentEffects(); //double jump = 0;
        
        // ShootAction();// shooting = 3;
        // SlidingWall();// wall jump = 1;
        // WallJump();//wall jump = 1;
        
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     DashAction(); // dash = 2;
        // }
        
        ManageAllActionsOfPlayer();
        #endregion
        #region ConfigActionsPlayer
        
        SoundSettings();
        #endregion
        
       
    }

    private void SlidingWall()
    {
        if((IsWallRight && directionPlayerH == 1 || IsWallLeft && directionPlayerH == -1) && !IsGround) // se estiver colidindo com a parede e pressionando o botao de movimento em direcao a parede e se não estiver colidindo com o chao, ele fara o sliding
        {

             statePlayer = "SlidingPlayer";
             IsSliding = true;
             rig.velocity = new Vector2(rig.velocity.x, -2); // Deslizando
        }
        else
        {
            IsSliding = false;
        }
       
    }

    private void WallJump() // Método que faz o player pular quando estiver deslizando na parede
    {   
        
        
        if(IsWallJump == true)
        {
            
            if(IsWallRight)
            {
               rig.velocity = new Vector2(-10,10);
               spritePlayer.flipX = true; // Váriavel que basicamente inverte a sprite para condizer com o movimento do player
            }
            if(IsWallLeft)
            {
                rig.velocity = new Vector2(10,10);
                spritePlayer.flipX = false; 
                
            }

            if(shootPlayerAnim == false)
            {
            statePlayer = "JumpPlayer";
        
            }
        }
        
    }

    
    private void Walk()
    {
        if(shootPlayerAnim == false)
        {
            if(directionPlayerH == 0 && IsWallJumping == false && IsGround) // Condicoes para que a animacao do player seja parado
            {
            statePlayer = "ParadoPlayer";
            }
            else if(directionPlayerH!=0 && IsSliding == false && IsWallJumping == false && IsGround) // Condicoes para que a animacao do player seja andando
            {
             statePlayer = "MovimentoPlayer";
            }
        }
        
        if(CanMove)
        {   //movimento de walk do player otimizado, para funcionar de forma mais fluida
            float horizontalSpeedPlayerH = rig.velocity.x;
            horizontalSpeedPlayerH += directionPlayerH;
            horizontalSpeedPlayerH *= Mathf.Pow(0.1f,Time.deltaTime*10);
            rig.velocity = new Vector2(Mathf.Clamp(horizontalSpeedPlayerH,-8f,8f),rig.velocity.y);
        }  
    }

    private void JumpAction()
    {      //first jump
           audioSource.PlayOneShot(audiosPlayer[4]); // som do pulo
           rig.velocity = new Vector2(rig.velocity.x,powerJump);
           jump = false;

           if(arrayOfActionPermissionOfPlayer[0] == true) // se o double jump estiver habilitado
           {
                doubleJump = !doubleJump;
           }
           
    }

    private void ShootAction()
    {
        float duracao = animPlayer.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        if(shootPlayerAnim == true)
        {
            animPlayer.GetComponent<Animator>().SetInteger("AllToShoot",1);
            statePlayer = "PlayerAttackAnim";
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            shootPlayerAnim = true;
            if(shootTime>=0.8 && statePlayer == "PlayerAttackAnim")
            {
                    GameObject bulletP = Instantiate(bulletPlayer, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,0), Quaternion.identity);
                    shootTime = 0; 
                    shootPlayerAnim = false;
                    animPlayer.GetComponent<Animator>().SetInteger("AllToShoot",0);
                    if(spritePlayer.flipX == true)
                    {
                        bulletP.GetComponent<ScriptBulletPlayer>().DirBullet = -1; // caso o player esteja olhando pra esquerda, a direcao do tiro será para a esquerda
                        return;
                    }

                    bulletP.GetComponent<ScriptBulletPlayer>().DirBullet = 1; // caso o player esteja olhando pra direita, a direcao do tiro será para a direita 
            }
                   
        }
            
    }

    private bool JumpInput()
    {

        if( Input.GetKeyDown(KeyCode.W) && IsGroundTimerJumpCoyote>0) // Se apertar W e colidir com o chao... 
        {
            if(shootPlayerAnim == false)
            {
                statePlayer = "JumpPlayer";
            }
            jump = true;
            IsGroundTimerJumpCoyote = 0;
            doubleJump = false;
        }

        
        
        if(jump || doubleJump == true && Input.GetKeyDown(KeyCode.W)) //Se cumprir essa condicao, a acao do pulo é executada
        {
                JumpAction();
        }

        if(Input.GetKeyDown(KeyCode.W) && IsSliding) // Se estiver deslizando na parede e usa o W para pular, o Wall Jump funcionará
        {
            IsWallJump = true;
            CanMove = false;
            IsWallJumping = true;
            Invoke("StopWallJump",0.2f);  //Em 0.2 segundos, o metodo StopWallJump é invocado
        }

        return jump;
    }

    private void StopWallJump()
    {
        IsWallJump = false;
        CanMove = true;
    }
    


    private void JumpMovimentEffects()
    {
        float fallMultiplier = 5f; //variavel que faz o player cair mais rapido
        float lowJumpMultiplier = 4.5f; //variavel que faz o player subir mais "lento"
        const float valueTimerJumpCoyote = 0.15f; // efeito coyote
        
        

        if(rig.velocity.y < 0)
        {
            rig.velocity+= Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; // se o player estiver caindo, será somado a velocidade dele um valor através dessa formula aritmetica para cair mais rapido
        }
        else if(rig.velocity.y>0 && !Input.GetKey(KeyCode.W))
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; // se o player estiver subindo, será somado a velocidade dele um valor através dessa formula aritmetica para subir mais "devagar"
        } 

         if(IsGround!=true && IsSliding == false && IsWallJumping == false )
         {
            
            if(shootPlayerAnim == false)
            {
                statePlayer = "JumpPlayer";
            }
            
         }
         
         if(doubleJump == true && Input.GetKeyDown(KeyCode.W))
         {
            if(shootPlayerAnim == false)
            {
                statePlayer = "DoubleJumpPlayer";
            }
         }
        
        #region Coyote/Responsive Jump
        //Coyote Jump 

        
        if(IsGround)
        {
            
            IsWallJumping = false;
            IsGroundTimerJumpCoyote = valueTimerJumpCoyote; // se o player estiver colidindo com o chao, a variavel recebe 0.2f
            
        }
        else
        {
            IsGroundTimerJumpCoyote-=Time.deltaTime; // se o player nao estiver colidindo com o chao, a variavel ficará diminuindo até 0, logo o player consegue pular mesmo no ar com a condicao dessa variavel ser maior que 0 (Efeito Coyote)
           
        }
        #endregion 
    }

    public void DashAction()// controla o input do dash
    {
        if(canDash == true)
        {
            audioSource.PlayOneShot(audiosPlayer[5]); // som do dash
            StartCoroutine(Dash()); //inicializando coroutine do dash
        }
    }

    public void ConfigDashVelocity()
    {
     if(directionPlayerH!=0)
     {
        rig.velocity = new Vector2(directionPlayerH*dashPower,0);
        return;
     }
     //caso o player esteja parado, ou seja, directionPlayerH = 0...
     if(spritePlayer.flipX == false)
     {
        rig.velocity = new Vector2(1*dashPower,0);
     }
     else if(spritePlayer.flipX == true)
     {
        rig.velocity = new Vector2(-1*dashPower,0);
     }
      
        return;
    }

    public void ManageAllActionsOfPlayer()
    {
        Walk(); 

        JumpMovimentEffects();// condicao será feita dentro do método
        JumpInput();
        

        if(arrayOfActionPermissionOfPlayer[1] == true)
        {
            SlidingWall();
            WallJump();
        }
        
        if(arrayOfActionPermissionOfPlayer[2] == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                DashAction(); // dash = 2;
            }
        }
        
        if(arrayOfActionPermissionOfPlayer[3] == true)
        {
            if(shootTime<=0.8)
            {
                shootTime+=Time.deltaTime;
            }
            
            ShootAction();// shooting = 3;

        }

        
    }

    public bool[] GetArrayOfActionPermissionPlayer()
    {
        return arrayOfActionPermissionOfPlayer;
    }


    public void SoundSettings()
    {
        if(Input.GetKeyDown(KeyCode.W) && IsGround == true) // pulo
        {
           audioSource.PlayOneShot(audiosPlayer[4]);
           
        }

         if(Input.GetKeyDown(KeyCode.A ) || Input.GetKeyDown(KeyCode.D) && IsGroundTimerJumpCoyote > 0 && IsGround)
         {
            audioSource.clip = audiosPlayer[0];
            audioSource.loop = true;
            audioSource.Play();
            
         }
         if(directionPlayerH == 0 && !Input.GetKeyDown(KeyCode.A ) && !Input.GetKeyDown(KeyCode.D) || IsGroundTimerJumpCoyote <= 0 && !IsGround)
         {
            audioSource.loop = false;
         }
        
    }



   public void PlayerAnimMoviment(string stateAnim)
   {    
      if(CanMove)
      {
        if(directionPlayerH > 0)
        {
                spritePlayer.flipX = false;
        }
        else if(directionPlayerH < 0)
        {
                spritePlayer.flipX = true;
        }
      }
       switch(stateAnim)
       {
            case "MovimentoPlayer":

                animPlayer.AnimationPlayer("IndioAndando");
            
            break;

            case "ParadoPlayer":
                animPlayer.AnimationPlayer("IndioParadoAnim");
            break;

            case "DashPlayer":
                animPlayer.AnimationPlayer("IndioDash");
                
            break;

            case "JumpPlayer":
                
                animPlayer.AnimationPlayer("IndioPulandoo");
            break;

            case "SlidingPlayer":
                
                animPlayer.AnimationPlayer("IndioSliding");
                
            break;

            case "DoubleJumpPlayer":
                
                animPlayer.AnimationPlayer("DoubleJumpPlayer");
            break;

            case "PlayerAttackAnim":
                
                animPlayer.AnimationPlayer("PlayerAttackAnim");
            break;

            default:

            animPlayer.AnimationPlayer("IndioParadoAnim");

            break;
       }   
   }

   private void OnCollisionEnter2D(Collision2D col) {

        if(col.gameObject.tag == "PulaPula")
        {
            rig.velocity = Vector2.up * 35;
            
        }

        // if(col.gameObject.tag == "Erva")
        // {
        //     QuantidadeErvasColetadas+=1;
        //     Destroy(col.gameObject);
            
            
        // }

        if(col.gameObject.tag == "Espinho")
        {
            VerifyScenes.gameOverActive = true;
            audioSource.PlayOneShot(audiosPlayer[2]);
            StartCoroutine(TrailRendActive());
        }

        if(col.gameObject.tag == "Enemy")
        {
            VerifyScenes.gameOverActive = true;
            
        }

        if(col.gameObject.tag == "Serra")
        {
            VerifyScenes.gameOverActive = true;
            audioSource.PlayOneShot(audiosPlayer[2]);
            StartCoroutine(TrailRendActive());
        }

        if(col.gameObject.tag == "Paje")
        {
            Text texto = textFinal.gameObject.GetComponent<Text>();
            texto.enabled = true;
            //texto.text+=ScriptContador.ContadorTempoDuranteJogo.ToString("F2");
            Destroy(col.gameObject);
            StartCoroutine(GoMenu());
        }
   }

   public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Erva")
        {
            QuantidadeErvasColetadas+=1;
            Destroy(col.gameObject);
        }

        
    }

   private void OnCollisionStay2D(Collision2D col) {
    
        if(col.gameObject.tag == "PulaPula")
        {
            rig.velocity = Vector2.up * 35;
            
        }
   }

   private IEnumerator Dash() 
   {
     canDash = false;
     isDashing = true;
     var originalGravityScale = rig.gravityScale;
     rig.gravityScale = 0;
     ConfigDashVelocity();
     trailRenderer.emitting = true;
     yield return new WaitForSeconds(timeDurationDash);
     isDashing = false;
     rig.gravityScale = originalGravityScale;
     trailRenderer.emitting = false;
     yield return new WaitForSeconds(timeCooldownDash);
     canDash = true;
     yield return null;
   }
    private IEnumerator TrailRendActive() 
   {
     trailRenderer.enabled = false;
     yield return new WaitForSeconds(1.5f);
     trailRenderer.enabled = true;
     yield return new WaitForSeconds(0);
   }

   private IEnumerator GoMenu()
   {
     yield return new WaitForSeconds(3.5f);
     SceneManager.LoadScene("Menu Original");
     yield return new WaitForSeconds(0);
   }

  
   

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere((Vector2)transformArm.position+rightOffSetArm,0.19f);
        Gizmos.DrawWireSphere((Vector2)transformArm.position+leftOffSetArm,0.19f);
        Gizmos.DrawWireCube((Vector2)transformFeet.position,new Vector2(0.25f,0.20f));
    }
   
    
}
