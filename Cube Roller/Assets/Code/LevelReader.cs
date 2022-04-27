using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelReader : MonoBehaviour
{
    public string fileName;

    public int count = 0;
    public List<float> times;
    public List<int> tiles;
    public List<int> turnLengths;
    public Queue<float> playerTimes = new Queue<float>();
    public int ghostTurnsCounter = -1;
    public Queue<int> turnTimes = new Queue<int>();
    public AudioManager audioManager;
    public TilesManager tileManager;
    public rollGhost rollGhost;
    public roll player;
    public GameObject playerGameObject;
    public GameObject ghostGameObject;
    public bool GhostTurn = false;
    bool tempBool = false;
    int lastTurn = 0;
    float firstTime;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/" + fileName + ".txt";
        ReadFile(path);
		

    }

    void ReadFile(string path)
	{
        int lineCount = File.ReadLines(path).Count();
        List<string> fileLines = File.ReadAllLines(path).ToList();
        //ebug.Log(fileLines[(0 * 3) + 1]);
        for (int i = 0; i < lineCount / 3; i++)
        {
            string content;
            content = fileLines[(i * 3) + 1];
            float tempFloat;
            float.TryParse(content, out tempFloat);
            //tempFloat -= audioManager.secPerBeat * 2;
            tempFloat -= audioManager.secPerBeat / 2;
            times.Add(tempFloat);
            content = fileLines[(i * 3) + 2];
            int tempInt;
            int.TryParse(content, out tempInt);
            tiles.Add(tempInt);
            
            //Debug.Log("RUNNING");
        }
        firstTime = times[0];
		for (int i = 0; i < times.Count-1; i++)
		{
            
            if(times[i+1] - times[i] > audioManager.secPerBeat*3)
			{
                if (turnTimes.Count == 0)
                {
                    turnTimes.Enqueue(i);
                    float difference = times[turnTimes.Peek()] - times[0];
                    Debug.Log("OUR DIFFERENCE IS " + difference);
                    int beats = Mathf.RoundToInt(difference / audioManager.secPerBeat);
                    turnLengths.Add(beats);
                    lastTurn = i;
                    Debug.Log("TURN NUMBER " + turnLengths.Count + " BEATS IN TURN " + beats);
                }
                else
				{
                    float difference = times[i] - times[lastTurn+1];
                    Debug.Log("THE TIMES " + times[i] + "|| THE MINUS " + times[turnTimes.Peek() + 1]);
                    int beats = Mathf.RoundToInt(difference / audioManager.secPerBeat);
                    Debug.Log("OUR DIFFERENCE IS " + difference);
                    turnTimes.Enqueue(i);
                    turnLengths.Add(beats);
                    lastTurn = i;
                    Debug.Log("TURN NUMBER " + turnLengths.Count + " BEATS IN TURN " + beats);
                }
                    
			}
		}
		/*for (int i = 0; i < turnTimes.Count; i++)
		{
            Queue<int> turnTimesTemp = new Queue<int>;
            if(i == 0)
			{

			}
            else
			{
                times[turnTimes.Peek()] - ;
			}
		}*/
		
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("AUDIO MANAGERS POSITION:" + Math.Round((audioManager.songPosition), 2));
        //Debug.Log("LEVEL READERS POSITION:" + Math.Round(times[count], 2));
        //Debug.Log(Math.Round(audioManager.songPosition, 2) + "SONG POSITION");
        /*if ((Math.Round(times[count],2)) == Math.Round((audioManager.songPosition),2))
		{
            Debug.Log(Math.Round(times[count], 2) + "ROUNDED, NEXT ONE SHOULD BE " + Math.Round(times[count+1], 2));
            
            tileManager.triggerTile(tiles[count]-1, audioManager.secPerBeat);
            count++;
		}*/

        
        
        if ((times[count] - audioManager.songPosition) <= 0.000001 || (audioManager.songPosition - times[count]) >= 0.000001)
        {
            if(!GhostTurn)
			{
                GhostTurn = true;
                player.turn = false;
                ghostGameObject.GetComponent<Renderer>().material = rollGhost.highlightMaterial;
                playerGameObject.GetComponent<Renderer>().material = player.normalMaterial;
                tileManager.ResetQueue();
                ghostTurnsCounter++;
			}
            
            playerTimes.Enqueue(times[count] + (turnLengths[ghostTurnsCounter] * audioManager.secPerBeat) + audioManager.secPerBeat);
            rollGhost.RollActivate(tiles[count]);
            Debug.Log("PLAYER TIME LAND" + (times[count] + (turnLengths[ghostTurnsCounter] * audioManager.secPerBeat)+ audioManager.secPerBeat));
            if (count == turnTimes.Peek())
			{
				
                turnTimes.Dequeue();
                //playerTimes.Clear();
                GhostTurn = false;
                player.turn = true;
                ghostGameObject.GetComponent<Renderer>().material = rollGhost.normalMaterial;
                playerGameObject.GetComponent<Renderer>().material = player.highlightMaterial;
            }
            count++;
            tempBool = true;
        }
    }
}
