using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray myRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(myRay.origin, myRay.direction * 1000.0f);
        RaycastObject lastRaycastObject = null; ///

        RaycastHit hitObject;

        if (Physics.Raycast(myRay, out hitObject, Mathf.Infinity))
        {
            //Debug.LogFormat("Raycast hit {0}", hitObject.collider.gameObject.name);
            RaycastObject raycastHitObject = hitObject.collider.GetComponent<RaycastObject>();
            
            // If the hit object actually had the script, handle it
            if (raycastHitObject != null)
            {
                // If this is a NEW object, call Exit on the old object, and Enter on the new one
                if (raycastHitObject != lastRaycastObject)
                {
                    if (lastRaycastObject != null)
                    {
                        lastRaycastObject.OnRaycastExit();
                    }

                    raycastHitObject.OnRaycastEnter(hitObject);
                    lastRaycastObject = raycastHitObject;
                }
                // If this isn't a new object, just call OnRaycast on the same object
                else
                {
                    raycastHitObject.OnRaycast(hitObject);
                }
            }

            // If the object didn't have the script on it and there is a last raycast object, deactivate it
            else if (lastRaycastObject != null)
            {
                lastRaycastObject.OnRaycastExit();
                lastRaycastObject = null;
            }
        }
        // If there's no object being looked at
        else if (lastRaycastObject != null)
        {
            lastRaycastObject.OnRaycastExit();
            lastRaycastObject = null;
        }


	}
}
