using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    private int type = 0;
    public Transform cameraPos;
    public Transform target;
    private Vector3 vel;
    //private float arrowTime = 0.0f;
    private Vector3 curPos;
    public float smoothTime = 0.1f;

	private void Update(){
        //if(Input.GetKeyDown(KeyCode.DownArrow)){
        //    type = -1;
        //    arrowTime = 0.0f;
        //    curPos = transform.position;
        //}
        //if(Input.GetKey(KeyCode.DownArrow)){
        //    arrowTime += Time.deltaTime;
        //    if(arrowTime >= 1.0f){
        //        Vector3 temp = new Vector3(curPos.x, curPos.y - 10.0f, curPos.z);
        //        smoothTime = 0.1f;
        //        baseFollow(transform.position, temp);
        //    }
        //}
        //if(Input.GetKeyUp(KeyCode.DownArrow)){
        //    baseFollow(transform.position, curPos);
        //}

	}

	private void LateUpdate(){
        switch(type){
            case 0:
                follow();
                break;

            case 1:
                followX();
                break;

            default:
                break;
        }
	}

    private void baseFollow(Vector3 origin, Vector3 want2Follow){
        Vector3 temp = Vector3.SmoothDamp(origin, want2Follow, ref vel, smoothTime);
        temp.z = -10.0f;
        transform.position = temp;
    }

    private void follow(){
        smoothTime = 0.1f;
        baseFollow(transform.position, target.position);
    }

    private void followX(){
        smoothTime = 0.1f;
        Vector3 xTemp = new Vector3(target.position.x, cameraPos.position.y, target.position.z);
        baseFollow(transform.position, xTemp);
    }
}
