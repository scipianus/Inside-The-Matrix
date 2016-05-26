using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public string targetName;
    GameObject target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find(targetName);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + new Vector3(0, 0, -10);
	}
}
