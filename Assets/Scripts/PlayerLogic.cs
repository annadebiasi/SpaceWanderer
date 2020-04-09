using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
	public GameObject txt;
	public GameObject player;
    private int score = 0;

    void OnCollisionEnter2D(Collision2D star)
    {
        if(star.collider.tag == "Box")
        {
            star.gameObject.SetActive(false);
			score += 1;
			if (score == 10){
				player.SetActive(false);
	        	txt.GetComponent<UnityEngine.UI.Text>().text = "a place so full of mystery is just a puzzle to be solved.";
			}else{
			    txt.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
			}
        }
    }
}