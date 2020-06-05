using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuCallButton;
    [SerializeField] private GameObject menuImage;
    [SerializeField] private Image soundIcon;
    [SerializeField] private Image musicIcon;
    [SerializeField] private Image enable;
    [SerializeField] private Image disable;
    private bool _soundEnabled;
    private bool _musicEnabled;

    void Start()
    {
        soundIcon = disable;
        musicIcon = disable;
        menuImage.SetActive(false);
    }

    public void CallMenu()
    {
        menuImage.SetActive(true);
        menuCallButton.SetActive(false);
    }

    public void Sound()
    {
        if (_soundEnabled)
        {
            soundIcon = disable;
            _soundEnabled = false;
        }
        else
        {
            soundIcon = enable;
            _soundEnabled = true;
        }
    }

    public void Music()
    {
        if (_musicEnabled)
        {
            musicIcon = disable;
            _musicEnabled = false;
        }
        else
        {
            musicIcon = enable;
            _musicEnabled = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void Quit()
    {
        SceneManager.LoadScene("HomeScene");
        Resume();
    }

    public void Resume()
    {
        menuCallButton.SetActive(true);
        menuImage.SetActive(false);
    }
}