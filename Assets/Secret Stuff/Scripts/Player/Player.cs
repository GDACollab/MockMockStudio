using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int currentHealth = 3;
    public int CurrentHealth {get{return currentHealth;}}
    [SerializeField] int maxHealth = 3;
    public int MaxHealth {get{return maxHealth;}}
    
    [Header("Movement")]
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float groundAccelerationMultiplier = 2f;
    [SerializeField] float airAccelerationMultiplier = 0.5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float jumpTime = 1f;
    [SerializeField] LayerMask groundLayerMask;
    
    [Header("Interaction")]
    [SerializeField] LayerMask interactLayerMask;
    
    float jumpPower => Mathf.Sqrt(2*Physics2D.gravity.magnitude*jumpHeight);
    // float minJumpPower => Mathf.Sqrt(2*Physics2D.gravity.magnitude*2);
    
    public Action Pause, Cancel, Die;
    public string currentControlScheme => _playerInput.currentControlScheme;
    public void SwitchCurrentActionMap(string mapNameOrId) => _playerInput.SwitchCurrentActionMap(mapNameOrId);
    
    // Internal Variables
    bool _isGrounded = false;
    float _jumpTimer = 0f;
    float _extraGravity = 0f;
    
    // Internal Components
    Rigidbody2D _rb;
    SpriteRenderer _sprite;
    BoxCollider2D _collider;
    
    PlayerInput _playerInput;
    InputAction _moveAction, _jumpAction, _dashAction, _interactAction;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (currentHealth <= 0){
            KillPlayer();
        }
    }
    
    void Movement(){
        _isGrounded = Physics2D.BoxCast(transform.position, new Vector2(_collider.size.x*0.99f, 0.1f), 0, -transform.up, _collider.size.y/2, groundLayerMask);
        
        float moveValue = _moveAction.ReadValue<float>();
        
        float horizontalVelocity = _rb.velocity.x;
        
        if (_isGrounded){
            horizontalVelocity = Mathf.MoveTowards(_rb.velocity.x, moveValue*groundSpeed, Time.deltaTime*groundSpeed*groundAccelerationMultiplier);
        }
        else if(moveValue != 0){
            horizontalVelocity = Mathf.MoveTowards(_rb.velocity.x, moveValue*groundSpeed, Time.deltaTime*groundSpeed*airAccelerationMultiplier);
        }
        
        if (horizontalVelocity > 0){
            _sprite.flipX = false;
        }
        else if (horizontalVelocity < 0){
            _sprite.flipX = true;
        }
        
        float verticalVelocity = _rb.velocity.y;
        
        if (_jumpAction.triggered && _isGrounded){
            verticalVelocity = jumpPower;
            // _extraGravity = 0;
            _jumpTimer = jumpTime;
            // _rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
        }
        else if(_jumpAction.inProgress && _jumpTimer > 0 && Vector2.Dot(_rb.velocity, Vector2.up) > 0){
            // verticalVelocity = jumpPower;
            _jumpTimer -= Time.deltaTime;
        }
        else {
            // _extraGravity = Physics2D.gravity.magnitude;
            verticalVelocity -= Physics2D.gravity.magnitude*Time.deltaTime;
        }
        
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
                floorNote.DisplayFloorNote();
                return;
            }
        }
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
    
    public void KillPlayer(){
        if (Die != null){
            Die();
        }
    }
}
