using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController{

    int stopJumpCnt = 0;
    float h = 0.0f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool isLitching = false;
    public bool pisLitching{
        get{
            return isLitching;
        }
        set{
            isLitching = value;
        }
    }
    private bool isLitchJumping = false;
    public float attackTime = 0.4f;
    public float dashTime = 1.0f;
    private float gravityOrigin = 0.0f;
    private int litchCnt = 0;
    public GameObject swordAura;
    private Transform attackPos;
    public Transform spawnTr;
    [SerializeField] public Character player;

	#region override_Methods

	protected override void Start(){
        base.Start();
        attackPos = transform.Find("attackPos");
        gravityOrigin = rigid.gravityScale;
	}

	protected override void CharacterUpdate(){
        h = Input.GetAxisRaw("Horizontal");
        if(!isGrounded && isContactWall && h * transform.localScale.x < 0.0f){
            isLitching = true;
            if(isLitchJumping){
                rigid.gravityScale = gravityOrigin;
            }
            else{
                rigid.gravityScale = gravityOrigin * 0.4f;
                rigid.velocity = Vector2.zero;
            }
        }
        else{
            isLitching = false;
            litchCnt = 0;
            rigid.gravityScale = gravityOrigin;
        }
        anim.SetBool(AnimHashing.LITCHING, isLitching);
	}

	protected override void CharacterFixedUpdate(){
        base.CharacterFixedUpdate();
        if(isGrounded){
            stopJumpCnt = 0;
        }
	}

    public override void Move(float h, float moveSpeed){
        if (!isAttacking){
            base.Move(h, moveSpeed);
        }
	}

    public IEnumerator Dash(Vector2 dir){
        anim.SetTrigger(AnimHashing.DASH);
        rigid.AddForce(dir * player.dashSpeed);
        yield return StartCoroutine(switchBoolVar((value) => isDashing = value, true, false, dashTime));
        rigid.velocity = rigid.velocity / 2;
    }

    public IEnumerator Flash(Vector3 dir){
        if(!(isDashing || isLitching || isLitchJumping)){
            anim.SetTrigger(AnimHashing.DASH);
            if (Physics2D.OverlapPoint(transform.position + dir * 7.0f) == null){
                transform.position = transform.position + (dir * 7.0f);
                yield return StartCoroutine(switchBoolVar((value) => isDashing = value, true, false, dashTime));
            }
        }
    }

    public void WallLatch(){
        if(isLitchJumping){
            return;
        }
        rigid.velocity = new Vector2(0, 40.0f);
        StartCoroutine(switchBoolVar((value) => isLitchJumping = value, true, false, 0.3f));
    }

    public IEnumerator StopJump(){
        yield return new WaitForSeconds(0.1f);
        if (stopJumpCnt == 0){
            stopJumpCnt++;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y > 0.0f ?  0.0f : rigid.velocity.y);
        }
    }

	public override void Attack(){
        if(isGrounded){
            base.Attack();
            StartCoroutine(switchBoolVar((value) => isAttacking = value, true, false, attackTime));
        }
        else{
            anim.SetTrigger(AnimHashing.ATTACK_JUMP);
        }
	}

    public void MagicAttack(){
        if(!isGrounded){
            return;
        }
        swordAura.SetActive(false);
        anim.SetTrigger(AnimHashing.ATTACK_B);
        StartCoroutine(SwordAura());
        StartCoroutine(switchBoolVar((value) => isAttacking = value, true, false, attackTime));
    }

    private IEnumerator SwordAura(){
        yield return new WaitForSeconds(0.1f);
        int dir = transform.localScale.x > 0.0f ? 1 : -1;
        swordAura.transform.position = attackPos.position;
        swordAura.transform.localScale = new Vector3(Mathf.Abs(swordAura.transform.localScale.x) * dir, swordAura.transform.localScale.y, swordAura.transform.localScale.z);
        swordAura.SetActive(true);
    }

	public override void Damage(float damage, ref float hp){
        base.Damage(damage, ref hp);
	}

	public override void Dead(){
        transform.position = spawnTr.position;
        player.hp = player.maxHp;
	}

	#endregion

	#region Static_Methods
	public static Transform getPlayerTr(){
        return GameObject.FindWithTag("PLAYER").transform;
    }
    #endregion
}
