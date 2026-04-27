using UnityEngine;
using UnityEngine.EventSystems; //This allows us to use Unity's event system to detect our mouse inputs
using UnityEngine.UI;

public class CardMovement: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Set up "States" for the cards
    //Default state, Hover state, Drag state, Play state

    private RectTransform rectTransform;

    private Canvas canvas;

    private Vector2 originalLocalPointerPosition;

    private Vector3 originalPanelLocalPosition;

    private int currentState;

    private Quaternion originalRotation;

    private Vector3 originalPosition;


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

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:               
             if (!Input.GetMouseButton(0)) //Check if mouse button is released
                HandleDragState();

                    break;
            case 3:
                HandlePlayState();
                break;
        }
    }


    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale; //reset scale
        rectTransform.localRotation = originalRotation; //reset rotation
        rectTransform.localPosition = originalPosition; //reset rotation
        highlightCard.SetActive(false); //disable glow effect
        playArrow.SetActive(false); //disable arrow 
    }


    public void OnPointerEnter (PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState ==1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState ==2)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition))
            {
                localPointerPosition /= canvas.scaleFactor;

                Vector3 offsetToOriginal = localPointerPosition = originalLocalPointerPosition;
                rectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;

                if(rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = playPosition;
                }
            }

        }
    }
    private void HandleHoverState()
    {
        highlightCard.SetActive(true);
        rectTransform.localScale =  originalScale * selectScale;
    }

    private void HandleDragState() 
    {
        //set the cards rotation to zero
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}