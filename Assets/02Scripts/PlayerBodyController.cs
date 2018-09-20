using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour {

    PlayerController playerCtrl;

	private void Start(){
        playerCtrl = GetComponentInParent<PlayerController>();
	}

	private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("DEAD_TRAP")){
            playerCtrl.Damage(10000.0f, ref playerCtrl.player.hp);
        }
	}
}
