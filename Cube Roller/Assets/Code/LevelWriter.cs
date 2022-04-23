using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelWriter : MonoBehaviour
{
    public string fileName;

    public int count = 0;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/" + fileName + ".txt";
        File.AppendAllText(path, newFile());
    }

    public void addToFile(float timeInSong, int tile)
	{
        timeInSong = timeInSong / (audioManager.secPerBeat / 4);
        timeInSong = Mathf.RoundToInt(timeInSong);
        timeInSong = timeInSong * (audioManager.secPerBeat / 4);
        string content;
        content = "Tile:" + count.ToString() + "\n";
        content += timeInSong + "\n";
        content += tile.ToString() + "\n";
        count++;
        string path = Application.dataPath + "/" + fileName + ".txt";
        File.AppendAllText(path, content);
        
    }
    public void SaveFile()
	{

	}
    string newFile()
	{
        string content = "";
        return content;
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
