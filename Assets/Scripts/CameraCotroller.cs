using System;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    [SerializeField] private DropDownController _dropDownController;
    [SerializeField] private Camera _camera;
    [SerializeField] private LeanPinchCamera _leanPinchCamera;
    [SerializeField] private CameraMoving _cameraMoving;
    [SerializeField] private LeanPitchYaw _leanPitchYaw;
    [SerializeField] private LeanMultiUpdate _leanMultiUpdate;
    [SerializeField] private List<CameraRoomPositions> _cameraRoomPositions;
    
    private WatchPoints _actualWatchPoint;
    private SavedCameraParameters _savedCameraParameters;

    private void Start()
    {
        _actualWatchPoint = WatchPoints.FreeAspect;

        _savedCameraParameters = new SavedCameraParameters();
        Subscribe();
    }

    private void OnDestroy() => 
        UnSubscribe();

    private void UnSubscribe() => 
        _dropDownController.OnWatchModeSwitched -= OnWatchModeSwitched;

    private void Subscribe() => 
        _dropDownController.OnWatchModeSwitched += OnWatchModeSwitched;

    private void OnWatchModeSwitched(WatchPoints watchPoint)
    {
        CheckSwitchFromFreeAspect();
        CheckSwitchOnFreeAspect(watchPoint);
        _actualWatchPoint = watchPoint;
        SpawnCamera(_actualWatchPoint);
    }

    private void CheckSwitchOnFreeAspect(WatchPoints watchPoint)
    {
        if (watchPoint == WatchPoints.FreeAspect)
        {
            SwitchCameraMoveMod(true);
            SwitchCameraScaleMod(true);
            SwitchCameraFPS(false);
        }
    }
    
    private void CheckSwitchFromFreeAspect()
    {
        if (_actualWatchPoint == WatchPoints.FreeAspect)
        {
            SaveCameraParameters();
            SwitchCameraMoveMod(false);
            SwitchCameraScaleMod(false);
            SwitchCameraFPS(true);
        }
    }

    

    private void SpawnCamera(WatchPoints watchPoint)
    {
        if (watchPoint == WatchPoints.FreeAspect)
        {
            LoadCameraParameters();
            return;
        }
        _camera.transform.position = _cameraRoomPositions.Find(p => p.WatchPoint == watchPoint).CameraSpawnPos
            .position;
        _camera.transform.eulerAngles = Vector3.zero;
        _camera.fieldOfView = 80;
    }
    
    private void SaveCameraParameters()
    {
        _savedCameraParameters.Position = _camera.transform.position;
        _savedCameraParameters.Rotation = _camera.transform.rotation.eulerAngles;
        _savedCameraParameters.Scale = _camera.transform.localScale;
        _savedCameraParameters.Zoom = _camera.fieldOfView;
    }

    private void LoadCameraParameters()
    {
        _camera.transform.position = _savedCameraParameters.Position;
        _camera.transform.eulerAngles = _savedCameraParameters.Rotation;
        _camera.transform.localScale = _savedCameraParameters.Scale;
        _camera.fieldOfView = _savedCameraParameters.Zoom;
    }

    private void SwitchCameraScaleMod(bool value) =>
        _leanPinchCamera.enabled = value;

    private void SwitchCameraMoveMod(bool value) =>
        _cameraMoving.enabled = value;

    private void SwitchCameraFPS(bool value)
    {
        _leanPitchYaw.enabled = value;
        _leanMultiUpdate.enabled = value;
    }

    [Serializable]
    public class CameraRoomPositions
    {
        public WatchPoints WatchPoint;
        public Transform CameraSpawnPos;
    }
    
    private class SavedCameraParameters
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public float Zoom;
    }
}