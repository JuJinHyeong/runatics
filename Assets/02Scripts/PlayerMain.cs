using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour {

    private PlayerController playerCtrl;

	private void Start(){
        playerCtrl = GetComponent<PlayerController>();
	}

	private void Update(){
        float h = Input.GetAxis("Horizontal");
        float rh = Input.GetAxisRaw("Horizontal");
        float rv = Input.GetAxisRaw("Vertical");

        playerCtrl.Move(h, playerCtrl.player.moveSpeed);

        if(Input.GetKeyDown(KeyCode.Z)){
            Vector2 dir = new Vector3(rh, rv, 0).normalized;
            if(rh == 0.0f && rv == 0.0f){
                return;
            }
            StartCoroutine(playerCtrl.Flash(dir));
        }

        if(Input.GetKeyDown(KeyCode.X)){
            if (playerCtrl.pisLitching){
                playerCtrl.WallLatch();
            }
            else{
                playerCtrl.Jump(playerCtrl.player.jumpSpeed);
            }
        }
        if(Input.GetKeyUp(KeyCode.X)){
            StartCoroutine(playerCtrl.StopJump());
        }

        if(Input.GetKeyDown(KeyCode.V)){
            playerCtrl.MagicAttack();
        }

        if(Input.GetKeyDown(KeyCode.C)){
            playerCtrl.Attack();
        }
	}
}
