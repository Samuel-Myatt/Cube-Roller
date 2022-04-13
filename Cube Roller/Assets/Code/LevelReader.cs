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
    public AudioManager audioManager;
    public TilesManager tileManager;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/" + fileName + ".txt";
        ReadFile(path);
		for (int i = 0; i < tiles.Count; i++)
		{
            Debug.Log(tiles[i]);
		}
        for (int i = 0; i < times.Count; i++)
        {
            Debug.Log(times[i]);
        }

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
            tempFloat -= audioManager.secPerBeat * 2;
            times.Add(tempFloat);
            content = fileLines[(i * 3) + 2];
            int tempInt;
            int.TryParse(content, out tempInt);
            tiles.Add(tempInt);
            //Debug.Log("RUNNING");
        }
		
    }
    // Update is called once per frame
    void Update()
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
        if ((times[count] - audioManager.songPosition) <= 0.001)
        {
            Debug.Log(Math.Round(times[count], 2) + "ROUNDED, NEXT ONE SHOULD BE " + Math.Round(times[count + 1], 2));

            tileManager.triggerTile(tiles[count] - 1, times[count+1] - times[count]);
            count++;
        }
    }
}
