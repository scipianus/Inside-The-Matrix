﻿using UnityEngine;
using System.Collections;

public class HowToPlayPopUp : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        gameObject.active = false;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void switchActive() {
        gameObject.active = !gameObject.active;
    }
}
