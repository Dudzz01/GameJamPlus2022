 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private ScriptAnimatorPlayer animPlayer;
    [SerializeField] private SpriteRenderer spritePlayer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ManageSpawnPoints ManageSpawn;
    [SerializeField] private GameObject textFinal;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audiosPlayer = new List<AudioClip>();
    public static int quantidadeErvasColetadas {get; set;}
    public float speedPlayer {get; private set;} // velocidade do player
    public bool isGround {get; private set;} // permite se o player pula ou nao
    [SerializeField]private LayerMask groundMask;
    [SerializeField]private Transform positionPe;
    private bool jump;
    public bool canDash {get; private set;} // permite se o player pode dar dash ou nao
    public bool isDashing {get; private set;} // verifica se o player esta executando a acao dash
    public float dashPower {get; private set;} // forca do dash
    public float timeDurationDash {get; private set;} // tempo de duracao do dash
    public float timeCooldownDash {get; private set;} // tempo de cooldown do dash
    public string statePlayer{get; private set;} // verificar o state do player, só declarei, nao implementei ainda
    public float directionPlayerH{get; private set;} // direcao horizontal do player

    private void Start() {
         trailRenderer.sortingLayerName = "Player";
         speedPlayer = 6;
         statePlayer = "MovePlayer";
         dashPower = 10f;
         timeDurationDash = 0.6f;
         timeCooldownDash = 1f;
         canDash = true;
         jump = false;
         isDashing = false;
         spritePlayer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        
        if(isDashing == true) // enquanto o player estiver "dashando" ele vai retornar para a funcao void e nao executara outro comando
        {
             animPlayer.AnimationPlayer("IndioDash");
            return;
        }
        ConfigSound();
        //Debug.Log(ScriptContador.ContadorTempoJogo);
        
        directionPlayerH = Input.GetAxisRaw("Horizontal"); // variavel local só para a direcao h do player

        PlayerAnimMoviment(statePlayer); // maquina de estados para gerenciar as animacoes do player

        isGround = Physics2D.OverlapCircle(positionPe.position,0.3f,groundMask); // colisao com o chao

        if(Input.GetKeyDown(KeyCode.W) && isGround == true) // pulo
        {
           audioSource.PlayOneShot(audiosPlayer[4]);
           rig.velocity = Vector2.up * 11;
           jump = false;
        }

        InputDash();
    }

    private void FixedUpdate() {

        if(isDashing == true)
        {
            statePlayer = "DashPlayer";
            return;
        }

        MovePlayer();
        
    }

    public void MovePlayer()// controla o movimento horizontal e pulo do player
    {   
        if(directionPlayerH == 0)
        {
            statePlayer = "ParadoPlayer";
        }
        else
        {
             statePlayer = "MovimentoPlayer";
        }

        rig.velocity = new Vector2(directionPlayerH*speedPlayer,rig.velocity.y); // movimento horizontal

       if(jump == true)
       {

         print("pulando");
       }
       else if(jump == false)
       {
         statePlayer = "JumpPlayer";
       }
            
        
    }

    public void InputDash()// controla o input do dash
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash == true)
        {
            audioSource.PlayOneShot(audiosPlayer[5]);
            StartCoroutine(Dash());
        }
    }


    public void ConfigSound()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGround == true) // pulo
        {
           audioSource.PlayOneShot(audiosPlayer[4]);
           
        }

         if(Input.GetKeyDown(KeyCode.A ) || Input.GetKeyDown(KeyCode.D) && isGround == true  )
         {
            audioSource.clip = audiosPlayer[0];
            audioSource.loop = true;
            audioSource.Play();
            
         }
         if(directionPlayerH == 0 && !Input.GetKeyDown(KeyCode.A ) && !Input.GetKeyDown(KeyCode.D) || isGround == false )
         {
            audioSource.loop = false;
         }
        
    }

   public void PlayerAnimMoviment(string stateAnim)
   {  
        if(directionPlayerH > 0)
        {
                spritePlayer.flipX = false;
        }
        else if(directionPlayerH < 0)
        {
                spritePlayer.flipX = true;
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

            default:

            animPlayer.AnimationPlayer("IndioParadoAnim");

            break;
       }   
   }

   private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Ground")
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        if(col.gameObject.tag == "PulaPula")
        {
            rig.velocity = Vector2.up * 22;
            
        }

        if(col.gameObject.tag == "Erva")
        {
            quantidadeErvasColetadas+=1;
            Destroy(col.gameObject);
            Debug.Log(quantidadeErvasColetadas);
            
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

  
  

   private IEnumerator Dash() 
   {
     
     canDash = false;
     isDashing = true;
     var originalGravityScale = rig.gravityScale;
     rig.gravityScale = 0;
     rig.velocity = new Vector2(directionPlayerH*dashPower,0);
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

   

   
    
}
