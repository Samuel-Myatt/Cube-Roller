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
    public bool safe = true;
    public float health = 100f;

    public LevelWriter levelWriter;
    public AudioManager audioManager;
    public TilesManager tilesManager;
    public SoundEffectsManager soundEffects;
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
                if((curtile%3 != 0) && (!buffer))
				{
                    curtile += 1;
                    StartCoroutine(Roll(Vector3.right));
                    
                    GreenCheck();
                    buffer = true;
                }
                
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if((curtile % 3 != 1) && (!buffer))
                {
                    curtile -= 1;
                    StartCoroutine(Roll(Vector3.left));
                    
                    GreenCheck();
                    buffer = true;
                }
                    
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
				if((curtile < 7) && (!buffer))
                {
                    curtile += 3;
                    StartCoroutine(Roll(Vector3.back));
                    
                    GreenCheck();
                    buffer = true;
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if((curtile > 3) && (!buffer))
                {
                    curtile -= 3;
                    StartCoroutine(Roll(Vector3.forward));
                    
                    GreenCheck();
                    buffer = true;
                }
            }
        }
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
            buffer = false;
		}

    }
    public void GreenCheck()
	{
        if (tilesManager.tiles[curtile - 1].GetComponent<FloorTile>().green == true)
        {
            tilesManager.tiles[curtile].GetComponent<FloorTile>().Advance(4, 0f);
            health += 5f;
        }
    }
	private void FixedUpdate()
	{
        health -= 0.5f * Time.deltaTime;
	}
	IEnumerator Roll (Vector3 direction)
    { 
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
            if (curtile != tilesManager.tileQueue.Peek())
            {
                tilesManager.ResetQueue();
            }
            else
            {
                if (tilesManager.tileQueue.Count == 1)
                {
                    soundEffects.PlaySound("Land2");
                    transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    soundEffects.PlaySound("Land1");
                }
                tilesManager.tileQueue.Dequeue();
            }
        }
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
