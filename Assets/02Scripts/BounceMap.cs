using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMap : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rigid;
	private void Start(){
        player = GameObject.Find("player");
        rigid = player.GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.CompareTag("ATTACK")){
            int dir = player.transform.localScale.x > 0.0f ? 1 : -1;
            rigid.velocity = new Vector2(10.0f * dir, 30.0f);
        }
	}
}
