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
    [SerializeField] AnimationCurve m_colorCurve = null;

    public Color Color { get; private set; }

    SpriteRenderer m_spriteRenderer;
    Vector2 m_targetPosition;
    Color m_targetColor;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Color = m_spriteRenderer.material.color;
        m_targetColor = Color;
        m_targetPosition = transform.position;
    }

    public void Move(Action Action)
    {
        MoveFrom(Action);
    }

    public void ChangeColor(Color color)
    {
        Color = color;
        ResetValues();
        StartCoroutine(Colorify(color));
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
        ResetValues();
        StartCoroutine(MoveTo((Vector2)transform.position + dir * m_moveDistance));
    }

    private void ResetValues()
    {
        StopAllCoroutines();
        transform.position = m_targetPosition;
        m_spriteRenderer.material.SetColor("_Color", m_targetColor);
    }

    IEnumerator MoveTo(Vector2 position)
    {
        Vector2 start = transform.position;
        m_targetPosition = position;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * 1.0f / Commander.Instance.ActionTime)
        {
            float t = i / Commander.Instance.ActionTime;
            transform.position = Vector2.LerpUnclamped(start, m_targetPosition, m_moveCurve.Evaluate(t));
            yield return null;
        }

        transform.position = m_targetPosition;
    }

    IEnumerator Colorify(Color color)
    {
        Color start = m_spriteRenderer.material.color;
        m_targetColor = color;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * 1.0f / Commander.Instance.ActionTime)
        {
            float t = i / Commander.Instance.ActionTime;
            Color c = Color.LerpUnclamped(start, m_targetColor, m_colorCurve.Evaluate(t));
            m_spriteRenderer.material.SetColor("_Color", c);
            yield return null;
        }
        
        m_spriteRenderer.material.SetColor("_Color", m_targetColor);
    }
}
