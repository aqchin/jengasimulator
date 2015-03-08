using UnityEngine;
using System.Collections;

public class GameGenerator : MonoBehaviour {

	public GameObject jenga_blocks;
	public GameObject block;
	public Vector3 start;
	public int num_sets;

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0.0f, -9.81f, 0.0f);

		for (uint i = 0; i < num_sets; i++) {
			// Jenga size
			Vector3 j_size = block.transform.localScale;

			// Left, center, right jenga positions
			Vector3 l_pos = start + new Vector3(-j_size.x, j_size.y / 2 + i * j_size.y, 0);
			Vector3 c_pos = start + new Vector3(0, j_size.y / 2 + i * j_size.y, 0);
			Vector3 r_pos = start + new Vector3(j_size.x, j_size.y / 2 + i * j_size.y, 0);

			// Jenga objects
			GameObject l_go = (GameObject) Instantiate(block, l_pos, Quaternion.identity);
			GameObject c_go = (GameObject) Instantiate(block, c_pos, Quaternion.identity);
			GameObject r_go = (GameObject) Instantiate(block, r_pos, Quaternion.identity);

			// Names
			l_go.name = "Left Jenga";
			c_go.name = "Middle Jenga";
			r_go.name = "Right Jenga";

			// Add Jenga objects to set
			GameObject set = new GameObject();
			set.name = "Jenga Set " + (i + 1);
			l_go.transform.parent = set.transform;
			c_go.transform.parent = set.transform;
			r_go.transform.parent = set.transform;

			// Rotate every other set
			float rad = 0.01f;
			float rand = -rad + (Random.value * (2 * rad));
			if((i % 2) == 0) set.transform.Rotate(new Vector3(0.0f, 90.0f + rand, 0.0f));

			// Add to block container
			set.transform.parent = jenga_blocks.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
