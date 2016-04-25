using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public float speed = 1f;
    public int finalLine, finalColumn;

	// Use this for initialization
	void Start () {
        MazeController script = GameObject.Find("Canvas").GetComponent<MazeController>();
        finalLine = script.finalLine;
        finalColumn = script.finalColumn;
    }
	
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
