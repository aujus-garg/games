using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuStartButton : MonoBehaviour, IPointerDownHandler
{
    public int floor;

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(floor);
    }
}
