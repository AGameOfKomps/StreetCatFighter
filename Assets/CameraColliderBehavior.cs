using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderBehavior : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            GetComponentInParent<CameraBehavior>().triggerChase();
        }
    }
}
