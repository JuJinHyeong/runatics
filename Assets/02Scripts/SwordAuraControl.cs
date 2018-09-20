using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAuraControl : BaseShootingControl {

	private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("PLAYER")){
            return;
        }
        gameObject.SetActive(false);
	}
}
