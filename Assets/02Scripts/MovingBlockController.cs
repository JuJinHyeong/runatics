using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockController : MonoBehaviour {

    public float moveSpeed=10.0f;
    public bool startMove = false;
    private Vector2 dir = Vector2.zero;

    public IEnumerator Move(){
        while(true){
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

	public void OnTriggerEnter2D(Collider2D coll){
        if(!startMove){
            StartCoroutine(Move());
            startMove = true;
        }
        if(coll.gameObject.CompareTag("ATTACK")){
            if(Input.GetKey(KeyCode.RightArrow)){
                dir = Vector2.right;
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                dir = Vector2.left;
            }
            else if(Input.GetKey(KeyCode.DownArrow)){
                dir = Vector2.down;
            }
            else if(Input.GetKey(KeyCode.UpArrow)){
                dir = Vector2.up;
            }
        }
	}

	public void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("PLAYER")){
            return;
        }
        gameObject.SetActive(false);
	}
}
