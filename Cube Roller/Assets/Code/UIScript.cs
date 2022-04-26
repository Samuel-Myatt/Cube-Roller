using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
public class UIScript : MonoBehaviour
{
    public roll player;
    public GameObject qualityTextPrefab;
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
}
