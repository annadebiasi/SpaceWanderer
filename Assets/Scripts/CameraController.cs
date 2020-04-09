using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject backwall;
    public GameObject player;
    private bool reached_bottom = false;

    public float min_speed = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {	

        //Vector3 movement = new Vector3(min_speed, 0, 0) * Time.deltaTime;
        Vector3 movement = new Vector3(0, 0, 0) * Time.deltaTime;

        // Adapt to the Y position of the player
        float y_diff = transform.position.y - player.transform.position.y;
		
        if (System.Math.Abs(y_diff) > Camera.main.orthographicSize / 5) { movement.y -= (float) System.Math.Pow(y_diff, 2) * System.Math.Sign(y_diff) * Time.deltaTime; }
        
		// Stops camera from going too far down	when at base
		if (movement.y + transform.position.y < -1){
		    reached_bottom = true;
			return;
		}

		if(!reached_bottom && movement.y < 100)
        {
		    // Stops camera from going up on descend
			if (movement.y > 0){return;}
        }
	
   		// Stops camera from going up on ascend
		else if (movement.y < 0 ){ return;}

		transform.position += movement;
    }
}
