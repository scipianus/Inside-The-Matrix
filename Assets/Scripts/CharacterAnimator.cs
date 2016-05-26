using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

	public int player = 0;
    private Animator animator;
	private KeyCode[,] key = new KeyCode[2, 4];

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		key [0, 0] = KeyCode.UpArrow;
		key [0, 1] = KeyCode.DownArrow;
		key [0, 2] = KeyCode.RightArrow;
		key [0, 3] = KeyCode.LeftArrow;
		key [1, 0] = KeyCode.W;
		key [1, 1] = KeyCode.S;
		key [1, 2] = KeyCode.D;
		key [1, 3] = KeyCode.A;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(key[player, 0])) {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
		if (Input.GetKey(key[player, 1])) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
		if (Input.GetKey(key[player, 2])) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
		if (Input.GetKey(key[player, 3])) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }
		if (Input.GetKey(key[player, 0]))
            animator.SetBool("WalkUp", true);
        else
            animator.SetBool("WalkUp", false);
		if (Input.GetKey(key[player, 1]))
            animator.SetBool("WalkDown", true);
        else
            animator.SetBool("WalkDown", false);
		if (Input.GetKey(key[player, 2]))
            animator.SetBool("WalkRight", true);
        else
            animator.SetBool("WalkRight", false);
		if (Input.GetKey(key[player, 3]))
            animator.SetBool("WalkLeft", true);
        else
            animator.SetBool("WalkLeft", false);
    }
}
