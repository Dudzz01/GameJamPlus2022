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
    #region WallJump/Jump/Collisions Variables
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
    private Vector2 rightOffSetArm;
    private Vector2 leftOffSetArm;
    private bool jump;
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
    private void Awake() 
    { 
        rightOffSetArm = new Vector2(0.13f,0);
        leftOffSetArm = new Vector2(-0.03f,0);
        spritePlayer = GetComponent<SpriteRenderer>();
        trailRenderer.sortingLayerName = "Player";
        statePlayer = "MovePlayer";
        CanMove = true;
        dashPower = 12f;
        timeDurationDash = 0.6f;
        timeCooldownDash = 0.5f;
        canDash = true;
        jump = false;
        isDashing = false;
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
        IsGround = Physics2D.OverlapBox(transformFeet.position,new Vector2(0.25f,0.20f),0,groundMask) || Physics2D.OverlapBox(transformFeet.position,new Vector2(0.25f,0.24f),0,objectsGroundMask);
        IsWallRight = Physics2D.OverlapCircle((Vector2)transformArm.position+rightOffSetArm,0.19f,groundMask);
        IsWallLeft = Physics2D.OverlapCircle((Vector2)transformArm.position+leftOffSetArm,0.19f,groundMask);
        #endregion
        #region ActionPlayer
        Profiler.BeginSample("ACOES PLAYER");
        Walk(); 
        JumpInput();
        JumpMovimentEffects();
        
        
        SlidingWall();
        WallJump();
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DashAction();
        }
        
        #endregion
        #region ConfigActionsPlayer
        
        SoundSettings();
        #endregion
        Debug.Log(doubleJump);
        Profiler.EndSample();
       
    }

    private void SlidingWall()
    {
        if((IsWallRight || IsWallLeft) && directionPlayerH !=0 && !IsGround)
        {
             statePlayer = "SlidingPlayer";
             IsSliding = true;
             rig.velocity = new Vector2(rig.velocity.x, -2);
        }
        else
        {
            IsSliding = false;
        }
       
    }

    private void WallJump()
    {   
        
        
        if(IsWallJump == true)
        {
            
            if(IsWallRight)
            {
               rig.velocity = new Vector2(-10,10);
               spritePlayer.flipX = true;
            }
            if(IsWallLeft)
            {
                rig.velocity = new Vector2(10,10);
                spritePlayer.flipX = false;
                
            }
            statePlayer = "JumpPlayer";
        
        }
        
    }

    
    private void Walk()
    {
        if(directionPlayerH == 0 && IsWallJumping == false && IsGround)
        {
            statePlayer = "ParadoPlayer";
        }
        else if(directionPlayerH!=0 && IsSliding == false && IsWallJumping == false && IsGround)
        {
             statePlayer = "MovimentoPlayer";
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
           rig.velocity = new Vector2(rig.velocity.x,10f);
           jump = false;
           doubleJump = !doubleJump;
    }

    private bool JumpInput()
    {

        if( Input.GetKeyDown(KeyCode.W) && IsGroundTimerJumpCoyote>0)
        {
            statePlayer = "JumpPlayer";
            jump = true;
            IsGroundTimerJumpCoyote = 0;
            doubleJump = false;
        }
        
        if(jump || doubleJump == true && Input.GetKeyDown(KeyCode.W))
        {
                JumpAction();
        }

        if(Input.GetKeyDown(KeyCode.W) && IsSliding)
        {
            IsWallJump = true;
            CanMove = false;
            IsWallJumping = true;
            Invoke("StopWallJump",0.2f);  
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
        float fallMultiplier = 5f;
        float lowJumpMultiplier = 4.5f;
        const float valueTimerJumpCoyote = 0.2f; // efeito coyote
        
        

        if(rig.velocity.y < 0)
        {
            rig.velocity+= Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rig.velocity.y>0 && !Input.GetKey(KeyCode.W))
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        } 

         if(IsGround!=true && IsSliding == false && IsWallJumping == false )
         {
            
            statePlayer = "JumpPlayer";
            
         }
         
         if(doubleJump == true && Input.GetKeyDown(KeyCode.W))
         {
            statePlayer = "DoubleJumpPlayer"; 
         }
        
        #region Coyote/Responsive Jump
        //Coyote Jump 
        if(IsGround)
        {
            IsWallJumping = false;
            IsGroundTimerJumpCoyote = valueTimerJumpCoyote;
        }
        else
        {
            IsGroundTimerJumpCoyote-=Time.deltaTime;
        }
        #endregion 
    }

    public void DashAction()// controla o input do dash
    {
        if(canDash == true)
        {
            audioSource.PlayOneShot(audiosPlayer[5]);
            StartCoroutine(Dash());
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

        if(col.gameObject.tag == "Erva")
        {
            QuantidadeErvasColetadas+=1;
            Destroy(col.gameObject);
            
            
        }

        if(col.gameObject.tag == "Espinho")
        {
            this.transform.position = ManageSpawn.SpawnPointAtual().transform.position;
            audioSource.PlayOneShot(audiosPlayer[2]);
            StartCoroutine(TrailRendActive());
        }

        if(col.gameObject.tag == "Serra")
        {
            this.transform.position = ManageSpawn.SpawnPointAtual().transform.position;
            audioSource.PlayOneShot(audiosPlayer[2]);
            StartCoroutine(TrailRendActive());
        }

        if(col.gameObject.tag == "Paje")
        {
            Text texto = textFinal.gameObject.GetComponent<Text>();
            texto.enabled = true;
            texto.text+=ScriptContador.ContadorTempoJogo.ToString("F2");
            Destroy(col.gameObject);
            StartCoroutine(GoMenu());
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
     SceneManager.LoadScene("Menu");
     yield return new WaitForSeconds(0);
   }

  
   

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere((Vector2)transformArm.position+rightOffSetArm,0.19f);
        Gizmos.DrawWireSphere((Vector2)transformArm.position+leftOffSetArm,0.19f);
        Gizmos.DrawWireCube(transformFeet.position,new Vector2(0.25f,0.20f));
    }
   
    
}
