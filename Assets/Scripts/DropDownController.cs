using System;
using UnityEngine;

public class DropDownController : MonoBehaviour
{
    public Action<WatchPoints> OnWatchModeSwitched;
    
    public void DropdownChanged(int index)
    {
        switch (index)
        {
            case 0:
                OnWatchModeSwitched?.Invoke(WatchPoints.FreeAspect);
                break;
            case 1:
                OnWatchModeSwitched?.Invoke(WatchPoints.Kitchen);
                break;
            case 2:
                OnWatchModeSwitched?.Invoke(WatchPoints.Bathroom);
                break;
            case 3:
                OnWatchModeSwitched?.Invoke(WatchPoints.Hall);
                break;
            case 4:
                OnWatchModeSwitched?.Invoke(WatchPoints.BedroomCabinet);
                break;
            case 5:
                OnWatchModeSwitched?.Invoke(WatchPoints.Balcony);
                break;
        }
    }
}