using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;
    private float transition = 0.0f;
    private float animationDuration = 8.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);
    
    private Rect comboPanelRect;
    private Rect[] btnRects;

	// Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;

        setButtonPositions();
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + startOffset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 5, 7);
        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime + 1.0f / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
	}

    private void OnGUI()
    {

        GUI.Button(comboPanelRect, "COMBO PANEL");
        for (int i=0; i<btnRects.Length; ++i)
        {
            bool btn = GUI.Button(btnRects[i], i.ToString());
            if (btn)
                print(i.ToString());
        }
    }

    private void setButtonPositions()
    {
        btnRects = new Rect[4];
        float screenHeight = Screen.height,
            screenWidth = Screen.width;

        float btnAlign = Screen.width / 50f;//2%
        float btnSize = (screenWidth - btnAlign * 5) / 4f; //   |_[]_[]_[]_[]_|   - панель кнопок

        float btnCoordY = screenHeight - btnAlign - btnSize;

        for (int i=0; i<btnRects.Length; ++i)
        {
            float btnCoordX = btnAlign + btnAlign * i + btnSize * i;
            btnRects[i] = new Rect(new Vector2(btnCoordX, btnCoordY), new Vector2(btnSize,btnSize));
        }

        float comboPanelHeight = screenHeight / 10f;
        float comboPanelAlign = screenWidth / 25f;
        float comboPanelWidth = screenWidth - 2 * comboPanelAlign;
        comboPanelRect = new Rect(comboPanelAlign,comboPanelAlign,comboPanelWidth,comboPanelHeight);

    }
}
