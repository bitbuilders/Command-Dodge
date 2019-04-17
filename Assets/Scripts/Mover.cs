using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    Stack<Command> m_commandStack;
    Rebel m_rebel;

    public Mover(Rebel rebel)
    {
        m_commandStack = new Stack<Command>();
        m_rebel = rebel;
    }

    public void PerformAction(Action Action)
    {
        Command cmd = GetCommand(Action);

        cmd?.Execute();
        m_commandStack.Push(cmd);
    }

    public void ReverseAction()
    {
        if (m_commandStack.Count > 0)
        {
            m_commandStack.Pop().Undo();
        }
    }

    Command GetCommand(Action Action)
    {
        Command cmd = null;
        switch (Action)
        {
            case Action.LEFT:
                cmd = new MoveLeft(m_rebel);
                break;
            case Action.RIGHT:
                cmd = new MoveRight(m_rebel);
                break;
            case Action.JUMP:
                cmd = new Jump(m_rebel);
                break;
            case Action.DUCK:
                cmd = new Duck(m_rebel);
                break;
            case Action.COLOR:
                cmd = new ChangeColor(m_rebel);
                break;
        }

        return cmd;
    }
}
