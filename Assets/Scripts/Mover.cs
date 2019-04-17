using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    Command m_left = new MoveLeft();
    Command m_right = new MoveRight();
    Command m_jump = new Jump();
    Command m_duck = new Duck();

    Stack<Command> m_commandStack;
    Rebel m_rebel;

    public Mover(Rebel rebel)
    {
        m_commandStack = new Stack<Command>();
        m_rebel = rebel;
    }

    public void PerformAction(Movement movement)
    {
        Command cmd = GetCommand(movement);

        cmd?.Execute(m_rebel);
        m_commandStack.Push(cmd);
    }

    public void ReverseAction()
    {
        if (m_commandStack.Count > 0)
        {
            m_commandStack.Pop().Undo(m_rebel);
        }
    }

    Command GetCommand(Movement movement)
    {
        Command cmd = null;
        switch (movement)
        {
            case Movement.LEFT:
                cmd = m_left;
                break;
            case Movement.RIGHT:
                cmd = m_right;
                break;
            case Movement.JUMP:
                cmd = m_jump;
                break;
            case Movement.DUCK:
                cmd = m_duck;
                break;
        }

        return cmd;
    }
}
