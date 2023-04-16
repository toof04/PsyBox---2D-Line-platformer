using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private EdgeCollider2D edgecol;
    private LineRenderer lr;
    List<Vector2> points;
    //Rigidbody2D rb;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        edgecol = GetComponent<EdgeCollider2D>();
       // rb = GetComponent<Rigidbody2D>();
    }
    public void UpdateLine(Vector2 Pos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(Pos);
            return;
        }
        if (Vector2.Distance(points.Last(), Pos) > 0.1f)
            SetPoint(Pos);
    }
    void SetPoint(Vector2 point)
    {
        points.Add(point);
        lr.positionCount = points.Count;
        lr.SetPosition(points.Count - 1, point);
        if (points.Count > 1)
        {
            edgecol.points = points.ToArray();
           
        }
    }

}
