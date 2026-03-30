using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //This allows us to use Unity's event system to detect our mouse inputs

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler //These classes hold the methods required to handle UI interactions that we need
{
    //Set up "States" for the cards
    //Default state, Hover state, Drag state, Play state

    private RectTransform rectTransform;

    private Canvas canvas;

    private Vector2 originalLocalPointerPosition;

    private Vector3 originalPanelLocalPosition;

    private int currentState;

    private Quaternion originalRotation;

    private Vector3 originalPosition


    private Vector3 originalScale;

    [SerializeField] private float selectScale = 1.1f;

    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;

    [SerializeField] private GameObject highlightCard; //glowEffect

    [SerializeField] private GameObject playArrow;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();

        orginalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition
        originalRotation = rectTransform.localRotation
    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0)) //Check if mouse button is released
                    break;
            case 3:
                HandlePlayState();
                break;
        }
    }
}