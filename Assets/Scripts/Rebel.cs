using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    LEFT,
    RIGHT,
    JUMP,
    DUCK,
}

public class Rebel : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 30.0f)] float m_moveDistance = 3.0f;
    [SerializeField] AnimationCurve m_moveCurve = null;

    public void Move(Movement movement)
    {
        MoveFrom(movement.ToString());
    }

    void MoveFrom(string movement)
    {
        Vector2 dir = Vector2.zero;
        switch (movement)
        {
            case "LEFT":
                dir = Vector2.left;
                break;
            case "RIGHT":
                dir = Vector2.right;
                break;
            case "JUMP":
                dir = Vector2.up;
                break;
            case "DUCK":
                dir = Vector2.down;
                break;
        }

        StopAllCoroutines();
        StartCoroutine(MoveTo((Vector2)transform.position + dir * m_moveDistance));
    }

    IEnumerator MoveTo(Vector2 position)
    {
        Vector2 start = transform.position;
        for (float i = 0.0f; i < Commander.Instance.ActionTime; i += Time.deltaTime)
        {
            float t = i / Commander.Instance.ActionTime;
            transform.position = Vector2.LerpUnclamped(start, position, m_moveCurve.Evaluate(t));
            yield return null;
        }

        transform.position = position;
    }
}
