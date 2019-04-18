using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveAction
{
    public Action Action;
    public bool Undo;
}

public class Commander : Singleton<Commander>
{
    [SerializeField] Rebel m_rebel = null;

    [Tooltip("The time/speed per action")]
    [SerializeField] [Range(0.0f, 10.0f)] float m_actionTime = 0.5f;

    [SerializeField] [Range(1, 10)] int m_maxQueueSize = 2;

    [Tooltip("When the queue is full, action time will be scaled by this value")]
    [SerializeField] [Range(0.0f, 1.0f)] float m_maxSpeedMult = 0.75f;

    [Tooltip("If the time is over this percentage, the current move will be interupted")]
    [SerializeField] [Range(0.0f, 1.0f)] float m_interuptPercentage = 0.75f;

    public float ActionTime { get { return m_actionTime; } }
    public bool Left { get { return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A); } }
    public bool Right { get { return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D); } }
    public bool Up { get { return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W); } }
    public bool Down { get { return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S); } }
    public bool Color { get { return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0); } }
    public bool LeftDown { get { return Left && !Right; } }
    public bool RightDown { get { return Right && !Left; } }
    public bool UpDown { get { return Up && !Down; } }
    public bool DownDown { get { return Down && !Up; } }
    public bool Undo { get { return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.U); } }

    Queue<MoveAction> m_actionQueue;
    Mover m_mover;
    float m_originalActionTime;
    float m_time;

    private void Start()
    {
        m_actionQueue = new Queue<MoveAction>();
        m_mover = new Mover(m_rebel);

        m_time = ActionTime;
        m_originalActionTime = ActionTime;
    }

    private void Update()
    {
        if      (LeftDown)  QueueAction(Action.LEFT);
        else if (RightDown) QueueAction(Action.RIGHT);
        else if (UpDown)    QueueAction(Action.JUMP);
        else if (DownDown)  QueueAction(Action.DUCK);
        else if (Undo)      QueueAction(Action.LEFT, true);
        else if (Color)     QueueAction(Action.COLOR);

        m_time += Time.deltaTime;
        if (m_time >= ActionTime * m_interuptPercentage)
        {
            DoAction();
        }
    }

    void QueueAction(Action Action, bool undo = false)
    {
        if (m_actionQueue.Count < m_maxQueueSize)
        {
            m_actionQueue.Enqueue(new MoveAction() { Action = Action, Undo = undo });
            SetActionTime();
        }
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
                m_mover.PerformAction(ma.Action);
            }
            m_time = 0.0f;
            SetActionTime();
        }
    }

    void SetActionTime()
    {
        float p = (float)(Mathf.Max(m_actionQueue.Count - 1, 0)) / (float)(m_maxQueueSize - 1);
        m_actionTime = m_originalActionTime * (1.0f - (p * (1.0f - m_maxSpeedMult)));
    }
}
