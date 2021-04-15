using UnityEngine;

public class CommandBase
{
    public virtual void Execute()
    {
        Debug.Log("Command not implemented");
    }
}
