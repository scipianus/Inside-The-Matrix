using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float speed = 1f;
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.UpArrow))
            body.AddForce(Vector2.up * speed);
        if (Input.GetKey(KeyCode.DownArrow))
            body.AddForce(Vector2.down * speed);
        if (Input.GetKey(KeyCode.RightArrow))
            body.AddForce(Vector2.right * speed);
        if (Input.GetKey(KeyCode.LeftArrow))
            body.AddForce(Vector2.left * speed);
    }
}
