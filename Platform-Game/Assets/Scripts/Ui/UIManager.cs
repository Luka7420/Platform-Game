using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false); //Not active when game starts
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //If pause screen is already active unpause and viceversa
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else 
                PauseGame(true);
        }
    }

    #region Game Over
    //Activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Game over functions

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit(); //Quit the game (works only in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exit play mode (only works in the editor)
        #endif
    }
    #endregion

    #region pause
    public void PauseGame(bool status)
    {
         //If status == true pause | if status == false unpause
         pauseScreen.SetActive(status);
         
         //When pause status is true change timescale to 0 (time stops) | when its false back to 1 (time runs normally)
         if(status)
            Time.timeScale = 0;
         else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion


}
