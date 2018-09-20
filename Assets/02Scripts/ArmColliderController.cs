using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColliderController : MonoBehaviour {

    private PlayerController playerCtrl;

	private void Start(){
        playerCtrl = GetComponentInParent<PlayerController>();
	}

	private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.CompareTag("WALL")){
            
        }
	}
}
