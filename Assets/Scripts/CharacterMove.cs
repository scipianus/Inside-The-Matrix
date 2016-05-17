using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float speed = 1f;
	public int player = 0;
	private KeyCode[,] key = new KeyCode[2, 4];

	// Use this for initialization
	void Start () {
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
        Rigidbody2D body = GetComponent<Rigidbody2D>();
		if (Input.GetKey(key[player, 0]))
            body.AddForce(Vector2.up * speed);
		if (Input.GetKey(key[player, 1]))
            body.AddForce(Vector2.down * speed);
		if (Input.GetKey(key[player, 2]))
            body.AddForce(Vector2.right * speed);
		if (Input.GetKey(key[player, 3]))
            body.AddForce(Vector2.left * speed);
    }
}
