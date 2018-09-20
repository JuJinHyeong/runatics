using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShootingControl : MonoBehaviour {

    [SerializeField] protected float shootSpeed = 50.0f;
    [SerializeField] protected float destroyTime = 5.0f;

    protected virtual void Update(){
        transform.Translate(Vector2.right * shootSpeed * Time.deltaTime * (transform.localScale.x / Mathf.Abs(transform.localScale.x) * -1));
    }
}
