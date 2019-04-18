using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected Rebel m_rebel;

    public Command(Rebel rebel)
    {
        m_rebel = rebel;
    }

    virtual public void Execute()
    {
        Debug.Log("Default command executed on rebel: " + m_rebel.name);
    }

    virtual public void Undo()
    {
        Debug.Log("Default command undone on rebel: " + m_rebel.name);
    }
}

public class MoveLeft : Command
{
    public MoveLeft(Rebel rebel) : base(rebel) { }

    public override void Execute()
    {
        m_rebel.Move(Action.LEFT);
    }

    public override void Undo()
    {
        m_rebel.Move(Action.RIGHT);
    }
}

public class MoveRight : Command
{
    public MoveRight(Rebel rebel) : base(rebel) { }

    public override void Execute()
    {
        m_rebel.Move(Action.RIGHT);
    }

    public override void Undo()
    {
        m_rebel.Move(Action.LEFT);
    }
}

public class Jump : Command
{
    public Jump(Rebel rebel) : base(rebel) { }

    public override void Execute()
    {
        m_rebel.Move(Action.JUMP);
    }

    public override void Undo()
    {
        m_rebel.Move(Action.DUCK);
    }
}

public class Duck : Command
{
    public Duck(Rebel rebel) : base(rebel) { }

    public override void Execute()
    {
        m_rebel.Move(Action.DUCK);
    }

    public override void Undo()
    {
        m_rebel.Move(Action.JUMP);
    }
}

public class ChangeColor : Command
{
    Color PreviousColor;

    public ChangeColor(Rebel rebel) : base(rebel) { }

    public override void Execute()
    {
        PreviousColor = m_rebel.Color;
        m_rebel.ChangeColor(GetRandomColor());
    }

    public override void Undo()
    {
        m_rebel.ChangeColor(PreviousColor);
    }

    Color GetRandomColor()
    {
        return Random.ColorHSV(0.0f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f);
    }
}