using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float yPosFollowRatio;
    
    float _yExtent, _xExtent, _minX, _maxX, _minY;
    
    Transform _player;
    BoxCollider2D _deathPlane;
    Collider2D _groundCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _deathPlane = GameObject.FindWithTag("Death Plane").GetComponent<BoxCollider2D>();
        _groundCollider = GameObject.FindWithTag("Ground").GetComponent<Collider2D>();
        
        _yExtent = Camera.main.orthographicSize;
        _xExtent = _yExtent * Screen.width / Screen.height;
        _minX = _groundCollider.bounds.min.x + _xExtent;
        _maxX = _groundCollider.bounds.max.x - _xExtent;
        _minY = _deathPlane.bounds.max.y + _yExtent;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = CalculatePosition();
    }
    
    Vector3 CalculatePosition(){
        Vector3 newPos = new Vector3();
        newPos.x = Mathf.Clamp(_player.position.x, _minX, _maxX);
        newPos.y = Mathf.Max(_player.position.y-_yExtent*yPosFollowRatio, _minY);
        newPos.z = transform.position.z;
        
        return newPos;
    }
}
