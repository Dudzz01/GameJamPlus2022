 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private ScriptAnimatorPlayer animPlayer;
    [SerializeField] private SpriteRenderer spritePlayer;
    [SerializeField] private TrailRenderer trailRenderer;
    public float speedPlayer {get; private set;} // velocidade do player
    public bool jump {get; private set;} // permite se o player pula ou nao
    public bool canDash {get; private set;} // permite se o player pode dar dash ou nao
    public bool isDashing {get; private set;} // verifica se o player esta executando a acao dash
    public float dashPower {get; private set;} // forca do dash
    public float timeDurationDash {get; private set;} // tempo de duracao do dash
    public float timeCooldownDash {get; private set;} // tempo de cooldown do dash
    public string statePlayer{get; private set;} // verificar o state do player, só declarei, nao implementei ainda
    public float directionPlayerH{get; private set;} // direcao horizontal do player

    private void Start() {
         speedPlayer = 6;
         jump = false;
         statePlayer = "MovePlayer";
         dashPower = 10f;
         timeDurationDash = 0.6f;
         timeCooldownDash = 2f;
         canDash = true;
         isDashing = false;
         spritePlayer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        
        if(isDashing == true) // enquanto o player estiver "dashando" ele vai retornar para a funcao void e nao executara outro comando
        {
            animPlayer.AnimationPlayer("IndioDash");
            return;
        }

        directionPlayerH = Input.GetAxis("Horizontal"); // variavel local só para a direcao h do player
        PlayerAnimMoviment(statePlayer); // maquina de estados para gerenciar as animacoes do player
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

        

        if(Input.GetKey(KeyCode.W) && jump == true) // pulo
        {
            var velPlayerY = 13f;
            rig.AddForce(new Vector2(0,velPlayerY),ForceMode2D.Impulse);
            velPlayerY = 0;
            jump = false;
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
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash() 
   {
     
     canDash = false;
     isDashing = true;
     var originalGravityScale = rig.gravityScale;
     rig.gravityScale = 0;
     rig.velocity = new Vector2(directionPlayerH*dashPower,rig.velocity.y);
     trailRenderer.emitting = true;
     yield return new WaitForSeconds(timeDurationDash);
     isDashing = false;
     rig.gravityScale = originalGravityScale;
     trailRenderer.emitting = false;
     yield return new WaitForSeconds(timeCooldownDash);
     canDash = true;
     yield return null;

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

   private void OnCollisionEnter2D(Collision2D col) 
   {
     
        if(col.gameObject.tag == "Ground")
        {
            jump = true;
            
        }
        else
        {
            statePlayer = "JumpPlayer";
            jump = false;
        }
   }
   
    
}
