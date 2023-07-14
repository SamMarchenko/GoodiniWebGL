using System;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    private Vector3 _previousPosition;
    
    private int _previousTouchCount = 0;
    private float _maxCameraYPos;
    private float _minCameraYPos = 0f;

    void Start() => 
        _maxCameraYPos = _camera.transform.position.y;

    void LateUpdate()
    {
        #region PC

        if (Input.GetMouseButtonDown(0))
            _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);

        #endregion

        #region Mobile

        if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
            _previousPosition = _camera.ScreenToViewportPoint(Input.GetTouch(0).position);

        if (Input.GetMouseButton(0))
            MoveCamera(Input.mousePosition);

        else if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved)
        {
            if (_previousTouchCount < 2)
                MoveCamera(Input.touches[0].position);
            else
                _previousPosition = _camera.ScreenToViewportPoint(Input.GetTouch(0).position);
        }

        #endregion

        _previousTouchCount = Input.touchCount;
    }

    private void MoveCamera(Vector3 touchPos)
    {
        Vector3 direction = _previousPosition - _camera.ScreenToViewportPoint(touchPos);
        
        var canCameraMoveY = CheckCameraMoveVerticalLock(direction.y);
        
        _camera.transform.position = _target.position;

        if (canCameraMoveY) 
            _camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);

        _camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);


        _camera.transform.Translate(new Vector3(0, 0, -10));

        _previousPosition = _camera.ScreenToViewportPoint(touchPos);
    }

    private bool CheckCameraMoveVerticalLock(float directionY)
    {
        if (directionY > 0 && Math.Abs(_camera.transform.position.y - _maxCameraYPos) < 0.01f)
            return false;

        if (directionY < 0 && _camera.transform.position.y <= _minCameraYPos)
            return false;

        return true;
    }
}