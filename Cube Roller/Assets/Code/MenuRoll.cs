using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRoll : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 600;
    bool isMoving = false;

    bool buffer = false;
    public int bufferlength = 100;
    int curtile = 1;
    public bool turn = false;
    public int lives = 5;
    public int points = 0;
    public float goodWindow;
    public float greatWindow;
    public float perfectWindow;
    public LevelWriter levelWriter;
    public LevelReader levelReader;
    public AudioManager audioManager;
    public TilesManager tilesManager;
    public SoundEffectsManager soundEffects;
    public GameObject ghostCube;
    public UIScript UI;
    public Material highlightMaterial;
    public Material normalMaterial;
    public MainMenuScript menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if ((curtile < 10) && (!buffer) && turn)
                {
                    curtile += 3;
                    StartCoroutine(Roll(Vector3.back));
                    menu.ChangeTextColour(curtile);
                    soundEffects.PlaySound("Pop");
                    buffer = true;
                }

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if ((curtile > 3) && (!buffer) && turn)
                {
                    curtile -= 3;
                    StartCoroutine(Roll(Vector3.forward));
                    menu.ChangeTextColour(curtile);
                    soundEffects.PlaySound("Pop");
                    buffer = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            buffer = false;
        }
    }
    IEnumerator Roll(Vector3 direction)
    {
        
        isMoving = true;
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
       


       
        while (remainingAngle > 0)
        {
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null;
        }
        
        isMoving = false;
    }
}
