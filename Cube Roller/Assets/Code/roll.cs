using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
		if (!isMoving)
		{
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if((curtile%3 != 0) && (!buffer) && turn)
                {
                    curtile += 1;
                    StartCoroutine(Roll(Vector3.right));
                    
                    
                    buffer = true;
                }
                
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if((curtile % 3 != 1) && (!buffer) && turn)
                {
                    curtile -= 1;
                    StartCoroutine(Roll(Vector3.left));
                    
                    
                    buffer = true;
                }
                    
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
				if((curtile < 7) && (!buffer) && turn)
                {
                    curtile += 3;
                    StartCoroutine(Roll(Vector3.back));
                    
                    
                    buffer = true;
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if((curtile > 3) && (!buffer) && turn)
                {
                    curtile -= 3;
                    StartCoroutine(Roll(Vector3.forward));
                    
                    
                    buffer = true;
                }
            }
        }
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
            buffer = false;
		}

    }
   
	private void FixedUpdate()
	{
        if(levelReader.playerTimes.Count > 0 && turn)
		{
            if(audioManager.songPosition > (levelReader.playerTimes.Peek() + audioManager.secPerBeat * 2))
			{
                MessUp();
            }
		}
	}

    public void MessUp()
	{
        Debug.Log("DONE NOT MOVED");
        tilesManager.ResetQueue();
        levelReader.playerTimes.Clear();
        //failedMove = true;
        lives--;
        gameObject.transform.position = ghostCube.transform.position;
        curtile = ghostCube.GetComponent<rollGhost>().curtile;
        turn = false;
        UI.UpdateLives(lives);
        if (lives == 0)
        {
            Die();
        }
    }
    public void Die()
	{
        Time.timeScale = 0f;
        Destroy(this.gameObject);
	}
	IEnumerator Roll (Vector3 direction)
    {
        Debug.Log("ROLL CALL " + audioManager.songPosition);
        bool failedMove = false;
        int tempPoints;
        isMoving = true;
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
        if (levelWriter.enabled == true)
        {
            levelWriter.addToFile(audioManager.songPosition, curtile);
        }


        tilesManager.TileTurnOff(curtile);
        if (tilesManager.tileQueue.Count > 0)
        {
            if ((curtile != tilesManager.tileQueue.Peek()) || audioManager.songPosition > levelReader.playerTimes.Peek()+(audioManager.secPerBeat*goodWindow) || audioManager.songPosition < levelReader.playerTimes.Peek() - (audioManager.secPerBeat *goodWindow))
            {
                failedMove = true;
            }
            else
            {
                if(audioManager.songPosition < levelReader.playerTimes.Peek() + (audioManager.secPerBeat * perfectWindow) && audioManager.songPosition > levelReader.playerTimes.Peek() - (audioManager.secPerBeat * perfectWindow))
				{
                    tempPoints = 10;
                    Debug.Log("PERFECT");
                    UI.QualityTextUpdate("Perfect!");
				}
                else if (audioManager.songPosition < levelReader.playerTimes.Peek() + (audioManager.secPerBeat * greatWindow) && audioManager.songPosition > levelReader.playerTimes.Peek() - (audioManager.secPerBeat * greatWindow))
				{
                    Debug.Log("GREAT");
                    tempPoints = 5;
                    UI.QualityTextUpdate("Great!");
                }
                else
				{
                    Debug.Log("GOOD");
                    UI.QualityTextUpdate("Good!");
                    tempPoints = 2;
                }
                if (tilesManager.tileQueue.Count == 1)
                {
                    soundEffects.PlaySound("Land2");
                    transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                    points += tempPoints*2;
                    UI.UpdatePoints(points);
                }
                else
                {
                    soundEffects.PlaySound("Land1");
                    points += tempPoints;
                    UI.UpdatePoints(points);
                }
                Debug.Log("LANDED ON " + audioManager.songPosition);
                tilesManager.tileQueue.Dequeue();
                levelReader.playerTimes.Dequeue();
            }
        }
        while (remainingAngle > 0)
		{
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null;
		}
        Debug.Log("ROLL CALL 2: " + audioManager.songPosition);
        if (failedMove)
		{
            MessUp();
        }
        isMoving = false;
	}
}
