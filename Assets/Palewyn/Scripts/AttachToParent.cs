using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToParent : MonoBehaviour {

    public Transform parent;
	// Use this for initialization
	void Start () {
        transform.SetParent(parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
