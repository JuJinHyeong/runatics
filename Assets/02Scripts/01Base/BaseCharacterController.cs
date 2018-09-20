using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHashing{
    public static readonly int ANISTS_LANDING = Animator.StringToHash("Base Layer.Jumping.Landing");
    public static readonly int ANISTS_ATTACK_A = Animator.StringToHash("Base Layer.Attack.Attack_A");
    public static readonly int ANISTS_ATTACK_B = Animator.StringToHash("Base Layer.Attack.Attack_B");

    public static readonly int JUMP = Animator.StringToHash("Jump");
    public static readonly int MOVESPEED = Animator.StringToHash("MoveSpeed");
    public static readonly int ATTACK = Animator.StringToHash("Attack");
    public static readonly int DISTANCE2GROUND = Animator.StringToHash("Distance2Ground");
    public static readonly int Y_VELOCITY = Animator.StringToHash("yVelocity");
    public static readonly int LANDING = Animator.StringToHash("Landing");
    public static readonly int ATTACK_A = Animator.StringToHash("Attack_A");
    public static readonly int ATTACK_B = Animator.StringToHash("Attack_B");
    public static readonly int ATTACK_JUMP = Animator.StringToHash("Attack_Jump");
    public static readonly int DASH = Animator.StringToHash("Dash");
    public static readonly int LITCH_JUMP = Animator.StringToHash("Litch_Jump");
    public static readonly int LITCHING = Animator.StringToHash("Litching");
}

[System.Serializable]
public class Character{
    public float hp = 10.0f;
    public float mp = 10.0f;
    public float maxHp = 10.0f;
    public float maxMp = 10.0f;
    public float moveSpeed = 25.0f;
    public float jumpSpeed = 25.0f;
    public float dashSpeed = 30.0f;
    public float power = 1.0f;
    public float guard = 1.0f;
}

public class BaseCharacterController : MonoBehaviour
{

    protected bool isGrounded = true;
    [SerializeField] protected bool isContactWall = false;
    [SerializeField] protected float distance2Ground = 0.0f;
    [SerializeField]protected RaycastHit2D[] ray2Grounds;
    [SerializeField]protected Transform[] groundRayOrigins;
    protected Transform wallCheck_U;
    protected Transform wallCheck_D;
    protected Animator anim;
    protected Rigidbody2D rigid;
    [SerializeField] protected float jTime = 0.0f;
    public float jPTime{
        set{
            jTime = value;
        }
    }

    #region MonoBehaviour_Methods

    protected virtual void Awake(){
        groundRayOrigins = new Transform[3];
        ray2Grounds = new RaycastHit2D[3];
        groundRayOrigins[0] = transform.Find("groundRayOrigin_L");
        groundRayOrigins[1] = transform.Find("groundRayOrigin_M");
        groundRayOrigins[2] = transform.Find("groundRayOrigin_R");
        wallCheck_U = transform.Find("wallCheck_U");
        wallCheck_D = transform.Find("wallCheck_D");
    }

    protected virtual void Start(){
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update(){
        anim.SetFloat(AnimHashing.DISTANCE2GROUND, distance2Ground);
        anim.SetFloat(AnimHashing.Y_VELOCITY, rigid.velocity.y);
        CharacterUpdate();
    }

    protected virtual void FixedUpdate(){
        distance2Ground = 100.0f;
        for (int i = 0; i < 3; i++){
            ray2Grounds[i] = Physics2D.Raycast(groundRayOrigins[i].position, Vector2.down, 1000.0f);
            distance2Ground = Mathf.Min(distance2Ground, ray2Grounds[i].distance < 1000.0f ? ray2Grounds[i].distance : 100.0f);
        }
        isGrounded = distance2Ground < 0.1f ? true : false;
        isContactWall = isContact("WALL") ? true : false;
        CharacterFixedUpdate();
    }

    protected virtual void CharacterUpdate(){
        
    }

    protected virtual void CharacterFixedUpdate(){

    }

    #endregion

    #region Moving_Methods

    public virtual void Move(float h, float moveSpeed)
    {
        int dir = transform.localScale.x > 0.0f ? 1 : -1;
        if (h > 0.0f) { dir = -1; }
        else if (h < 0.0f) { dir = 1; }

        Flip(dir);

        if (!isContactWall){
            transform.Translate(h * moveSpeed * Time.deltaTime, 0, 0);
        }
        anim.SetFloat(AnimHashing.MOVESPEED, Mathf.Abs(h));
    }

    public virtual void Flip(int dir){
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y, transform.localScale.z);
    }

    public virtual void Jump(float jumpSpeed){
        if(isGrounded){
            rigid.velocity = new Vector2(0, jumpSpeed);
        }
    }

    public virtual void Attack(){
        AnimatorStateInfo animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(animatorStateInfo.fullPathHash == AnimHashing.ANISTS_ATTACK_A ||
           animatorStateInfo.fullPathHash == AnimHashing.ANISTS_ATTACK_B){
            return;
        }
        int randNum = Random.Range(0, 100000);
        randNum %= 2;
        if(randNum == 0) anim.SetTrigger(AnimHashing.ATTACK_A);
        else anim.SetTrigger(AnimHashing.ATTACK_B);
    }

    public virtual void Damage(float damage, ref float hp){
        hp -= damage;
        if(hp <= 0.0f){
            Dead();
        }
    }

    public virtual void Dead(){
        
    }

    #endregion

    #region Judge_Methods

    public IEnumerator switchBoolVar(System.Action<bool> variable, bool initValue, bool value2Change, float timer){
        variable(initValue);
        yield return new WaitForSeconds(timer);
        variable(value2Change);
    }

    public virtual bool isContact(string tagName){
        Collider2D coll = Physics2D.OverlapArea(wallCheck_U.position, wallCheck_D.position);
        if(coll != null && coll.CompareTag(tagName)){
            return true;
        }
        return false;
    }

    #endregion
}
