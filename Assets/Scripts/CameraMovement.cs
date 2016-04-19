using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    GameObject target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + new Vector3(0, 0, -10);
	}
}
