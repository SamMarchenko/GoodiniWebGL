using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform target;
    private Vector3 _previousPosition;
    private Camera _camera;
    private int _previousTouchCount = 0;

    void Start()
    {
        _camera = Camera.main;
    }

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

        _camera.transform.position = target.position;

        _camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        _camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        _camera.transform.Translate(new Vector3(0, 0, -10));

        _previousPosition = _camera.ScreenToViewportPoint(touchPos);
    }
}