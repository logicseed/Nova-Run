using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorController : MonoBehaviour {

	//// Use this for initialization
	//void Start () {
	//	Cursor.lockState = CursorLockMode.Confined;
	//	Cursor.visible = false;
	//	DontDestroyOnLoad(gameObject);
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	var aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	//	aimPosition.z = 0.0f;
	//	transform.position = aimPosition;
	//}

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //cursorTexture.Resize(cursorTexture.width / 2, cursorTexture.height / 2);
        hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
