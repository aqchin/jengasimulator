using UnityEngine;
using System.Collections;

public class Hydra : MonoBehaviour {

	public GameObject cursor;
	public GameObject camera;
	public GameObject dock;
	public float height = 200.0f;
	public float left_center = 45.0f;
	public float left_height = 200.0f;

	bool wasDocked = false;
	bool toReset = false;
	Vector3 rest_offset;
	Vector3 rest_pos;

	// Use this for initialization
	void Start () {
		/*
		if (SixenseInput.IsBaseConnected(0)) {
			print ("Right controller connected");
		} else {
			print ("Please connect Razer Hydra");
			Application.Quit();
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		// Hydra
		for(uint i = 0; i < 2; i++) {
			if(SixenseInput.Controllers[i] != null && SixenseInput.Controllers[i].Enabled) {
				switch (i) {
				case 0: // Left controller
					if(SixenseInput.Controllers[i].GetButton(SixenseButtons.BUMPER)) {
						if(SixenseInput.Controllers[i].Position.x < left_center) {
							camera.transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
						}
						else {
							camera.transform.RotateAround(Vector3.zero, Vector3.up, -20 * Time.deltaTime);
						}
					} 
					if(SixenseInput.Controllers[i].GetButton(SixenseButtons.TRIGGER)) {
						if(SixenseInput.Controllers[i].Position.y < left_height) {
							if(camera.transform.position.y > 7)
								camera.transform.Translate(new Vector3(0, -0.5f, 0));
						}
						else {
							camera.transform.Translate(new Vector3(0, 0.5f, 0));
						}
					}
					break;
				case 1: // Right controller
					rest_pos = dock.transform.localPosition;
					rest_pos.y += 0.75f;
					//cursor.transform.rotation = SixenseInput.Controllers[i].Rotation;
					//cursor.transform.Rotate(SixenseInput.Controllers[i].Rotation.eulerAngles);
					
					if(!wasDocked) {
						if(SixenseInput.Controllers[i].Docked) {
							rest_offset = SixenseInput.Controllers[i].Position;
							//rest_pos = cursor.transform.position;
							wasDocked = true;
						}
					}
					else if(wasDocked && SixenseInput.Controllers[i].Docked) {
						if(toReset) Application.LoadLevel(0);
						cursor.transform.localPosition = rest_pos;
					}
					else {
						if(!SixenseInput.Controllers[i].Docked && wasDocked) toReset = true;
						Vector3 v = (SixenseInput.Controllers[i].Position - rest_offset);
						v.Scale(new Vector3(0.1f, 0.1f, 0.5f ));
			
						cursor.transform.localPosition = rest_pos + v;
					}
					break;
				}
			}
		}
	}
}
