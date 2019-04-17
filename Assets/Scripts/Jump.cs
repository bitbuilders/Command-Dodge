using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Command
{
    public override void Execute(Rebel rebel)
    {
        rebel.Move(Movement.JUMP);
    }

    public override void Undo(Rebel rebel)
    {
        rebel.Move(Movement.DUCK);
    }
}
