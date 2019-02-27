using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour {

    GameObject gameController;

	// Use this for initialization
	void Start () {
        //ゲーム開始時にGameControllerをFindにしておく
        gameController = GameObject.FindWithTag("GameController");
	}

    //トリガーからExitしたらクリアとみなす
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameController.SendMessage("IncreaseScore");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
