using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveAction
{
    public Movement Movement;
    public bool Undo;
}

public class Commander : Singleton<Commander>
{
    [SerializeField] Rebel m_rebel = null;
    [SerializeField] [Range(0.0f, 10.0f)] float m_actionTime = 0.75f;

    public float ActionTime { get { return m_actionTime; } }
    public bool Left { get { return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A); } }
    public bool Right { get { return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D); } }
    public bool Up { get { return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W); } }
    public bool Down { get { return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S); } }
    public bool Undo { get { return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.U); } }

    Queue<MoveAction> m_actionQueue;
    Mover m_mover;
    float m_time;

    private void Start()
    {
        m_actionQueue = new Queue<MoveAction>();
        m_mover = new Mover(m_rebel);

        m_time = ActionTime;
    }

    private void Update()
    {
        if (Left)       QueueAction(Movement.LEFT);
        else if (Right) QueueAction(Movement.RIGHT);
        else if (Up)    QueueAction(Movement.JUMP);
        else if (Down)  QueueAction(Movement.DUCK);
        else if (Undo)  QueueAction(Movement.LEFT, true);

        m_time += Time.deltaTime;
        if (m_time >= ActionTime)
        {
            DoAction();
        }
    }

    void QueueAction(Movement movement, bool undo = false)
    {
        m_actionQueue.Enqueue(new MoveAction() { Movement = movement, Undo = undo });
    }

    void DoAction()
    {
        if (m_actionQueue.Count > 0)
        {
            MoveAction ma = m_actionQueue.Dequeue();
            if (ma.Undo)
            {
                m_mover.ReverseAction();
            }
            else
            {
                m_mover.PerformAction(ma.Movement);
            }
            m_time = 0.0f;
        }
    }
}
