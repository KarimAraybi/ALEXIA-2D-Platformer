
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseScreen.activeInHierarchy){
                PauseGame(false);
            }
            else{
                PauseGame(true);
            }
        }
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion


    public void PauseGame(bool status){
        pauseScreen.SetActive(true);

        if(status){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
            pauseScreen.SetActive(false);

        }
    }
}
