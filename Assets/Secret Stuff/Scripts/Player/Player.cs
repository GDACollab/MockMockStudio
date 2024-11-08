using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float jumpTime = 1f;
    [SerializeField] float calcJumpTime = 0f;
    
    [Header("Interaction")]
    [SerializeField] LayerMask interactLayerMask;
    
    float jumpPower => Mathf.Sqrt(2*Physics2D.gravity.magnitude*jumpHeight);
    
    public Action Pause, Cancel;

    
    // Internal Variables
    float _jumpTimer = 0f;
    float _extraGravity = 0f;
    
    // Internal Components
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    BoxCollider2D _collider;
    
    public PlayerInput _playerInput;
    InputAction _moveAction, _jumpAction, _dashAction, _interactAction;

    AudioClip walkAudio, jumpAudio, hitAudio, paperAudio;
    AudioSource mySource;
    bool walkSoundReady = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _dashAction = _playerInput.actions.FindAction("Dash");
        _interactAction = _playerInput.actions.FindAction("Interact");

        StartCoroutine("pairAudio");
        StartCoroutine("walkSoundQueue");
    }

    IEnumerator walkSoundQueue()
    {
        walkSoundReady = false;
        yield return new WaitForSeconds(0.25f);
        walkSoundReady = true;
    }
    IEnumerator pairAudio()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject core = GameObject.Find("Core");
        walkAudio = core.GetComponent<CoreAssetLoader>().walkSound.sound;
        jumpAudio = core.GetComponent<CoreAssetLoader>().jumpSound.sound;
        hitAudio = core.GetComponent<CoreAssetLoader>().takeDamageSound.sound;
        paperAudio = core.GetComponent<CoreAssetLoader>().paperPickupSound.sound;

        mySource = this.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        calcJumpTime = jumpPower/Physics2D.gravity.magnitude;
    }
    
    void Movement(){
        float moveValue = _moveAction.ReadValue<float>();
        
        float horizontalVelocity = Mathf.MoveTowards(_rb.velocity.x, moveValue*groundSpeed, Time.deltaTime*groundSpeed);
        
        if (horizontalVelocity > 0){
            _sprite.flipX = false;
        }
        else if (horizontalVelocity < 0){
            _sprite.flipX = true;
        }

        if(moveValue > 0.02f || moveValue < -0.02f)
        {
            if (mySource)
            {
                if (walkSoundReady)
                {
                    mySource.PlayOneShot(walkAudio);
                    StartCoroutine("walkSoundQueue");
                }
            }
        }
        
        float verticalVelocity = _rb.velocity.y;
        
        if (_jumpAction.triggered){
            if (mySource) mySource.PlayOneShot(jumpAudio);
            verticalVelocity = jumpPower;
            _extraGravity = 0;
            _jumpTimer = jumpTime;
            // _rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
        }
        else if(_jumpAction.inProgress && _jumpTimer > 0 && Vector2.Dot(_rb.velocity, Vector2.up) > 0){
            // verticalVelocity = jumpPower;
            _jumpTimer -= Time.deltaTime;
        }
        else {
            _extraGravity = Physics2D.gravity.magnitude;
        }
        // else if (!_jumpAction.inProgress){
        //     verticalVelocity = jumpPower;
        // }
        
        verticalVelocity -= _extraGravity*Time.deltaTime;
        
        // Horizontal Movement
        _rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
    
    void OnInteract(){
        Vector3 end = _sprite.flipX ? transform.position+new Vector3(_collider.size.x*-1.5f, -_collider.size.y/2, 0) : transform.position+new Vector3(_collider.size.x*1.5f, -_collider.size.y/2, 0);
        
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.layerMask = interactLayerMask;
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = true;
        
        int temp = Physics2D.OverlapArea(transform.position+_collider.size.y/2*Vector3.up, end, contactFilter2D, colliders);
        
        foreach (Collider2D col in colliders){
            FloorNote floorNote;
            if (col.TryGetComponent(out floorNote)){
                if (mySource) mySource.PlayOneShot(paperAudio);
                floorNote.DisplayFloorNote();
                return;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyRunner>())
        {

            if (mySource) mySource.PlayOneShot(hitAudio);

            _rb.AddForce(new Vector2(0, 400));
            _rb.AddTorque(400, ForceMode2D.Force);
            _collider.enabled = false;
            GameObject.Find("Main Camera").GetComponent<CameraManager>().enabled = false;
            StartCoroutine("endGame");
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnPause(){
        // Pause Game
        if (Pause != null){
            Pause();
        }
    }
    
    void OnCancel(){
        // Go Back
        if (Cancel != null){
            Cancel();
        }
    }
}
