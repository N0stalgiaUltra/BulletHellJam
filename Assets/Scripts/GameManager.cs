using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Controlador do jogo

    //int limiteSpawn;
    //[SerializeField] float tempo = 5f;
    //float t  = 5f;
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake() {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    float slowTimeAllowed;
    float fastTimeAllowed;
    float currentSlowTimer;
    float currentFastTimer;

    /* Animação dos ícones*/

    public GameObject pauseMenu;
    public GameObject deathScreen;
    public GameObject victoryScreen;
    public bool estaPausado;

    public int count;
    public int index;
    public Scene currentScene;

    

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        deathScreen = GameObject.Find("DeathScreen");
        victoryScreen = GameObject.Find("VictoryScreen");
       
        fastTimeAllowed = 10f;
        slowTimeAllowed = 10f;
        currentSlowTimer = 0f;
        currentFastTimer = 0f;

        count = 0;
        //limiteSpawn = 10;
        pauseMenu.SetActive(false);
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
        estaPausado = false;
        
        Time.timeScale = 1f;

        currentScene = SceneManager.GetActiveScene();
        index = currentScene.buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //tempo -= Time.deltaTime;
        #region  Aumentar o Tempo
        if(Time.timeScale == 1.75f)
        {
            currentFastTimer += Time.deltaTime;
            
        }
        if(currentFastTimer > fastTimeAllowed)
        {
            currentFastTimer = 0f;
            Time.timeScale = 1f;
        }
        #endregion
        #region  Diminuir o Tempo
        if(Time.timeScale == 0.5f)
        {
            currentSlowTimer += Time.deltaTime;
            
        }
        if(currentSlowTimer > slowTimeAllowed)
        {
            currentSlowTimer = 0f;
            Time.timeScale = 1f;
        }
        #endregion    
        #region  Instancia com Pooling
        /*     
        if(tempo <= 0f && limiteSpawn >= 1)
        {
            EnemyPooling.Instance.SpawnFromPool("enemy");
            limiteSpawn--;
            tempo = t;
        }*/
        #endregion
        #region  Menu de Pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(estaPausado == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                estaPausado = true;
            }
            else
            {
                estaPausado = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        #endregion
    
        LimitScene(index);
    }
    #region  Botões, telas e menus
    public void LimitScene(int i)
    {
        switch (i)
         {
            case 0:
                break;
            case 1:
                if(count >= 2)
                    SceneManager.LoadScene("02");
                break;
            case 2:
                if(count >= 3)
                    SceneManager.LoadScene("03");
                break;
            case 3:
                if(count >= 4)
                    SceneManager.LoadScene("04");
                break;
            case 4:
                if(count >= 3)
                    SceneManager.LoadScene("05");
                break;
            case 5:
                if(count >= 5)
                    VictoryScreen();
                break;
            
            
         }
    }

    public void Retry()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene(index);
    }

    public void DStoMM()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene("menu");
    }    
    public void PMtoMM()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("menu");
    }
    public void DeathScreen(){
        
        deathScreen.SetActive(true);
    }

    public void VictoryScreen()
    {
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
    }

    public void VStoMM()
    {
        victoryScreen.SetActive(false);
        SceneManager.LoadScene("menu");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        estaPausado = !estaPausado;
        pauseMenu.SetActive(false);
    }
    #endregion
    /* *** Power Ups *** */
    //acelera o tempo do jogo por um periodo de tempo
    void AccelerateTime()
    {
        if(Time.timeScale == 1f)
            Time.timeScale = 1.75f;
        else
            Time.timeScale = 1f;
    }
    
    //deixa o tempo lento por um periodo de tempo
    void SlowTime()
    {
        if(Time.timeScale == 1f)
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1f;
    }
    
    //dá uma sobrevida para o player
    void GainHealth(){}
    
    //não é necessariamente um powerup/down, mas reflete disparos
    //Grandes chances de ser cancelado
    void Reflection(){}
    
    //dá para o player um boost de velocidade
    void MoreSpeed(){} 
    
    //dá para o player um decréscimo de velocidade
    void SlowSpeed(){}
    
    //aumenta o tempo do FireRate dos inimigos
    void SlowBullets(){} 

}
