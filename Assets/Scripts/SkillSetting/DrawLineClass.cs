using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLineClass : MonoBehaviour {
    [SerializeField]
    Canvas canvas;
    int lineWidth = 5;
	
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start () {
        DrawLine(new Vector2(0, 0), new Vector2(100,100));
    }

    Vector2 setPoint(Vector2 point) {
        point.x = (int)point.x;
        point.y = Screen.height - (int)point.y;
        return point;
    }

    void DrawLine(Vector2 pointA, Vector2 pointB) {
        Vector3 differenceVector = pointB - pointA;

        GameObject line = (GameObject)Instantiate(Resources.Load("LineObject"), new Vector3(0, 0, 0), Quaternion.identity);
        line.transform.SetParent(canvas.transform, false);
        RectTransform imageRectTransform = line.transform as RectTransform;
        
        imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
        imageRectTransform.pivot = new Vector2(0, 0.5f);
        imageRectTransform.position = pointA;
        float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
        imageRectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
