using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

    public Transform portalOut;
    [SerializeField]private bool portalOpen = true;
    private WaitForSeconds oneDotFiveSecond;

	private void Start()
    {
        oneDotFiveSecond = new WaitForSeconds(0.5f);
    }

	private void OnTriggerStay2D(Collider2D coll)
	{
        if(coll.gameObject.CompareTag("PLAYER")){
            if(Input.GetKeyDown(KeyCode.UpArrow) && portalOpen){
                StartCoroutine(portal(coll.transform.root.transform));
            }
        }
	}

    private IEnumerator portal(Transform target){
        portalOpen = false;
        target.position = portalOut.position;
        yield return oneDotFiveSecond;
        portalOpen = true;
    }
}
