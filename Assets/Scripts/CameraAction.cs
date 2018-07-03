using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;
    
    private Rect comboPanelRect;
    private Rect[] btnRects;


    public float comboResetSec;
    private float comboResetTimer;
    
    /* Нужно написать для комбо класс контейнер.
     * 1. Использовать строку для определения комбо или массив?
     * 
     */
    private List<int> combo;
    
	void Start () {
        combo = new List<int>();
        comboResetTimer = comboResetSec;
        setCameraOffset();
        setButtonPositions();
	}
	
	void FixedUpdate () {

        calcCombo();

        moveCamera();

        printCombo();

    }

    private void moveCamera()
    {
        moveVector = lookAt.position + startOffset;
        moveVector.x = 0;
        transform.position = moveVector;
    }

    private void calcCombo()
    {
        if (combo.Count > 0)
        {
            comboResetTimer -= Time.deltaTime;

            if (comboResetTimer < 0)
            {
                combo.Clear();
                comboResetTimer = comboResetSec;
            }

            if (combo.Count > 4)
                combo.RemoveRange(4, combo.Count - 4);
        }
    }

    private void printCombo()
    {
        string res = "";

        foreach (int item in combo)
        {
            res += item.ToString() + ' ';
        }

        print(res);
    }

    private void OnGUI()
    {

        GUI.Button(comboPanelRect, "COMBO PANEL");
        for (int i=0; i<btnRects.Length; ++i)
        {
            bool btn = GUI.Button(btnRects[i], i.ToString());
            if (btn)
                combo.Insert(0, i);
        }

        foreach (int i in combo)
        {

        }
    }

    /*
     * 
     */
    private void setCameraOffset()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    /* 
     * Отступ от краев 4%
     * Размер комбо панели 92х10 в %
     * Размер кнопок 20х20 % от ширины экрана
     */
    private void setButtonPositions()
    {
        btnRects = new Rect[4];
        float screenHeight = Screen.height,
            screenWidth = Screen.width;

        float btnAlign = Screen.width / 25f;
        float btnSize = (screenWidth - btnAlign * 5) / 4f; //   |_[]_[]_[]_[]_|   - панель кнопок

        float btnCoordY = screenHeight - btnAlign - btnSize;

        for (int i=0; i<btnRects.Length; ++i)
        {
            float btnCoordX = btnAlign + btnAlign * i + btnSize * i;
            btnRects[i] = new Rect(new Vector2(btnCoordX, btnCoordY), new Vector2(btnSize,btnSize));
        }

        float comboPanelHeight = screenHeight / 10f;
        float comboPanelWidth = screenWidth - 2 * btnAlign;
        comboPanelRect = new Rect(btnAlign, btnAlign,comboPanelWidth,comboPanelHeight);

    }
}
