using System.Collections.Generic;
using UnityEngine;

public class ArcRender : MonoBehaviour
{
    public GameObject arrowPrefab; //The arrow head.

    public GameObject dotPrefab; //The dots.

    public int poolSize = 50; // the size of our dot pool
    private List<GameObject> dotPool = new List<GameObject>(); //the dot pool
    private GameObject arrowInstance; //Holds a reference to the arrow head
    
    public float spacing = 50; //the spacing of the dots
    public float arrowAngleAdjustment = 0; //angle the correction for the arrowhead
    public int dotsToSkip = 1; //number if dots to skip to give the arrowhead space.
    private Vector3 arrowDirection; //Holds the position the arrowhead needs to point from

    void Awake()
    {
        Debug.Log("ArcRender Start called on" + gameObject.name);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("ArcRender Start called");

        if (arrowPrefab == null) Debug.Log("arrowHeadPrefab is Null!");
        if (dotPrefab == null) Debug.Log("dotPrefab is null");
        arrowInstance = Instantiate(arrowPrefab, transform);
        arrowInstance.transform.localPosition = Vector3.zero; //Vector3.zero is the same as new vector3 (0,0,0). This also works in Vector2
        InitializeDotPool(poolSize);

        Debug.Log("Dot pool size: " + dotPool.Count);
    }

    // Update is called once per frame
    void Update() //calculating where the arc will be by the player mouse
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;

        Vector3 startPos = transform.position;

        Debug.Log("startPos" + startPos);
        Debug.Log("mousePos" + mousePos);

        Vector3 midpoint = CalculateMidPoint(startPos, mousePos);
        UpdateArc(startPos, midpoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    void UpdateArc(Vector3 start, Vector3 mid, Vector3 end) //shows the arrows and dots
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / spacing);
        Debug.Log("NumDots:" + numDots);
        Debug.Log("Dot pool count:" + dotPool.Count);

        for (int i=0; i < numDots && i < dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f); //enture to stays within the range [0,1]

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);
            Debug.Log("Dot" + i + "position" + position);

            if (i != numDots - dotsToSkip)
            {
                dotPool[i].GetComponent<RectTransform>().position = position;
                dotPool[i].SetActive(true);
            }
            if (i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
            {
                arrowDirection = dotPool[i].transform.position;
            }
        }

        //deactivate unused dots
        for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
        {
            if (i>0)
            {
                dotPool[i].SetActive(false);
            }
        }
    }

    void PositionAndRotateArrow(Vector3 position)
    {
        arrowInstance.GetComponent<RectTransform>().position = position;
        Vector3 direction = arrowDirection - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += arrowAngleAdjustment;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //The same as (0,0,1)
    }

    Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 midpoint = (start + end) /2;
        float arcHeight = Vector3.Distance(start, end) /3f;
        midpoint.y += arcHeight;
        return midpoint;
    }

    Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu  = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end; return point;
    }

    void InitializeDotPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }
}
