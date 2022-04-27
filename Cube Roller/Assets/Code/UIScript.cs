using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIScript : MonoBehaviour
{
    public roll player;
    public GameObject qualityTextPrefab;
    public GameObject lossMenu;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ("LIVES: " + player.lives);
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ("POINTS: " + player.points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int lives)
	{
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = ("LIVES: " + lives);
	}
    public void UpdatePoints(int points)
    {
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ("POINTS: " + points);
    }

    public void QualityTextUpdate(string quality)
	{
        GameObject textInstance = Instantiate(qualityTextPrefab,this.transform);
        textInstance.GetComponent<TextMeshProUGUI>().text = quality;
	}


    public void RestartScene()
	{
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ActivateLoss()
	{
        lossMenu.active = true;
        lossMenu.GetComponent<Animator>().Play("LossMenu");
        lossMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "You Scored: " + player.points.ToString() + "!";
	}
    public void MainMenu()
	{
        // go to main menu
        SceneManager.LoadScene(0);
	}
}
