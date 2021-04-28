using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject menu, credits, howToPlay;
    void Start()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }
    void Update()
    {
        
    }

    public void CreditsToMenu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }
    public void MenuToGame()
    {
        menu.SetActive(false);
        SceneManager.LoadScene("01");
    }
    public void MenuToCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MenuToHTP()
    {
        menu.SetActive(false);
        howToPlay.SetActive(true);
    }
    public void HTPtoMenu() 
    {
        menu.SetActive(true);
        howToPlay.SetActive(false);
    }   
}
