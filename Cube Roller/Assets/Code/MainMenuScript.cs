using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public Color highlighted;
    public Color normal;
    public int selected = 0;
    public int selectedMenu = 0;
    public GameObject curMenu;
    public SoundEffectsManager soundEffects;
    public GameObject music;
    public float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        curMenu = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
		{
            soundEffects.PlaySound("Pop");
            switch (selectedMenu)
			{
                case 0:
                    switch (selected)
                    {
                        case 0:
                            PlayText();

                            break;
                        case 1:
                            SettingsText();
                            break;
                        case 2:
                            CreditsText();
                            break;
                        case 3:
                            QuitText();
                            break;
                    }
                    break;
                case 1:
                    switch (selected)
                    {
                        case 0:
                            LoadLevel(1);
                            break;
                        case 1:
                            LoadLevel(2);
                            break;
                        case 2:
                            LoadLevel(3);
                            break;
                        case 3:
                            LoadLevel(4);
                            break;

                    }
                    break;
                case 2:
                    switch (selected)
                    {

                        case 3:
                            curMenu.active = false;
                            curMenu = transform.GetChild(0).gameObject;
                            curMenu.active = true;
                            selectedMenu = 0;
                            break;
                        case 0:
                            VolumeChange();
                            break;
                        case 1:
                            FullScreen();
                            break;

                    }
                    break;
                case 3:
                    switch (selected)
                    {

                        case 3:
                            curMenu.active = false;
                            curMenu = transform.GetChild(0).gameObject;
                            curMenu.active = true;
                            selectedMenu = 0;
                            break;
                    }
                    break;


            }


        }
    }

    public void VolumeChange()
	{
        if(volume >= 100)
		{
            volume = 0;
		}
        else
		{
            
            volume += 10f;
            
		}
        
        soundEffects.gameObject.GetComponent<AudioSource>().volume = volume/100;
        music.GetComponent<AudioSource>().volume = volume/100;
        transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Volume: " + volume + "%";
	}
    public void FullScreen()
	{
        Screen.fullScreen = !Screen.fullScreen;
	}
    public void PlayText()
	{
        curMenu.active = false;
        curMenu = transform.GetChild(1).gameObject;
        curMenu.active = true;
        selectedMenu = 1;

	}
    public void LoadLevel(int level)
	{
        SceneManager.LoadScene(level);
        //whatever
	}
    public void SettingsText()
    {
        curMenu.active = false;
        curMenu = transform.GetChild(2).gameObject;
        curMenu.active = true;
        selectedMenu = 2;
    }
    public void CreditsText()
	{
        curMenu.active = false;
        curMenu = transform.GetChild(3).gameObject;
        curMenu.active = true;
        selectedMenu = 3;
    }
    public void ChangeTextColour(int option)
	{
        switch(option)
		{
            case 1:
                curMenu.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = highlighted;
                curMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                selected = 0;
                break;
            case 4:
                curMenu.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = highlighted;
                curMenu.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                selected = 1;
                break;
            case 7:
                curMenu.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = highlighted;
                curMenu.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                selected = 2;
                break;
            case 10:
                curMenu.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().color = normal;
                curMenu.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().color = highlighted;
                selected = 3;
                break;

        }
	}
    public void QuitText()
    {
        Application.Quit();
    }
}
