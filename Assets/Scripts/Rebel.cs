using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    LEFT,
    RIGHT,
    JUMP,
    DUCK,
    COLOR
}

public class Rebel : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 30.0f)] float m_moveDistance = 3.0f;
    [SerializeField] AnimationCurve m_moveCurve = null;

    public Color Color { get; private set; }

    SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Color = m_spriteRenderer.color;
    }

    public void Move(Action Action)
    {
        MoveFrom(Action);
    }

    public void ChangeColor(Color color)
    {
        m_spriteRenderer.color = color;
        Color = m_spriteRenderer.color;
    }

    void MoveFrom(Action action)
    {
        switch (action)
        {
            case Action.LEFT:
                CreateMovement(Vector2.left);
                break;
            case Action.RIGHT:
                CreateMovement(Vector2.right);
                break;
            case Action.JUMP:
                CreateMovement(Vector2.up);
                break;
            case Action.DUCK:
                CreateMovement(Vector2.down);
                break;
        }
    }
    
    void CreateMovement(Vector2 dir)
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo((Vector2)transform.position + dir * m_moveDistance));
    }

    IEnumerator MoveTo(Vector2 position)
    {
        Vector2 start = transform.position;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * 1.0f / Commander.Instance.ActionTime)
        {
            float t = i / Commander.Instance.ActionTime;
            transform.position = Vector2.LerpUnclamped(start, position, m_moveCurve.Evaluate(t));
            yield return null;
        }

        transform.position = position;
    }
}
