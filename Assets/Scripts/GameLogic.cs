using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject player;
    private bool reached_bottom = false;
    private GameObject[] grnd;
    private GameObject[] stars;
    private GameObject txt;

    void Start()
    {
        grnd = GameObject.FindGameObjectsWithTag("ground");
        stars = GameObject.FindGameObjectsWithTag("Box");
        txt = GameObject.FindGameObjectsWithTag("Text")[0];

        SetLevels(false, grnd);
    }

    void Update()
    {
      if(!reached_bottom)
        {
           // Makes levels appear and restocks point stars 
           if (player.transform.position.y < -4){
               SetLevels(true, grnd);
               SetLevels(true, stars);
	           reached_bottom = true;
	           txt.GetComponent<UnityEngine.UI.Text>().text = "there's something more to this that we missed...";
	       }
	    // Player falls off screen and game ends
        }else if (player.transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).y ){
        	player.SetActive(false);
            txt.GetComponent<UnityEngine.UI.Text>().text = "there are things you should know that I can't show...";
   	    }
    }
    
    void SetLevels(bool bl, GameObject[] tag)
    {
         foreach (GameObject t in tag)
         {   
             t.SetActive(bl);
         }
    }
    
}