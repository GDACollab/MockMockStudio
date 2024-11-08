using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("General UI")]
    [SerializeField] GameObject darkBackground;
    [SerializeField] GameObject gameOver;
    [SerializeField] Button restartButton;
    [Header("Pause UI")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button resumeButton;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject gamepadControls;
    [Header("Floor Note UI")]
    [SerializeField] GameObject floorNoteDisplayParent;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI body;
    [SerializeField] TextMeshProUGUI author;
    [SerializeField] Button closeButton;
    [Header("Journal")]
    public Journal journal;
    
    // Internal Variables
    bool _isPaused = false;
    bool _isReading = false;
    Player _player;
    
    private void Awake() {
        if (!journal){Resources.Load<Journal>("Journal");}
        journal.CreateFloorNotes();
        journal.CreateFloorNotes();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _player.SwitchCurrentActionMap("Main");
        _player.Pause += Pause;
        _player.Cancel += Back;
        _player.Die += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.currentControlScheme == "Keyboard"){
            keyboardControls.SetActive(true);
            gamepadControls.SetActive(false);
        }
        else {
            keyboardControls.SetActive(false);
            gamepadControls.SetActive(true);
        }
    }
    
    void Pause(){
        if (_isReading){
            CloseNote();
            return;
        }
        _isPaused = !_isPaused;
        if (_isPaused){
            resumeButton.Select();
            EnableUI();
        }
        else {
            DisableUI();
        }
        pauseMenu.SetActive(_isPaused);
    }
    
    void EnableUI(){
        Time.timeScale = 0f;
        _player.SwitchCurrentActionMap("UI");
        darkBackground.SetActive(true);
    }
    
    void DisableUI(){
        Time.timeScale = 1f;
        _player.SwitchCurrentActionMap("Main");
        darkBackground.SetActive(false);
    }
    
    void Back(){
        if (_isPaused){
            Resume();
        }
        else if (_isReading){
            CloseNote();
        }
    }
    
    void OnDeath(){
        EnableUI();
        gameOver.SetActive(true);
        restartButton.Select();
    }
    
    public void Resume(){
        _isPaused = false;
        pauseMenu.SetActive(false);
        DisableUI();
    }
    
    public void DisplayNote(string t, string b, string a){
        EnableUI();
        _isReading = true;
        floorNoteDisplayParent.SetActive(true);
        title.text = t;
        body.text = b;
        author.text = "Made By "+a;
        closeButton.Select();
    }
    
    public void CloseNote(){
        _isReading = false;
        floorNoteDisplayParent.SetActive(false);
        DisableUI();
    }
    
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ReturnToTitleScreen(){
        SceneManager.LoadScene(0);
    }
}
