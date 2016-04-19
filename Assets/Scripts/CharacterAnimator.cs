using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow)) {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }
        if (Input.GetKey(KeyCode.UpArrow))
            animator.SetBool("WalkUp", true);
        else
            animator.SetBool("WalkUp", false);
        if (Input.GetKey(KeyCode.DownArrow))
            animator.SetBool("WalkDown", true);
        else
            animator.SetBool("WalkDown", false);
        if (Input.GetKey(KeyCode.RightArrow))
            animator.SetBool("WalkRight", true);
        else
            animator.SetBool("WalkRight", false);
        if (Input.GetKey(KeyCode.LeftArrow))
            animator.SetBool("WalkLeft", true);
        else
            animator.SetBool("WalkLeft", false);
    }
}
