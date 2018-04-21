using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {
    [SerializeField]
    private GameObject objToFollow;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos = new Vector3(objToFollow.transform.position.x, objToFollow.transform.position.y, pos.z);
        this.transform.position = pos;
	}
}
