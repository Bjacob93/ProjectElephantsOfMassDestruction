using UnityEngine;
using System.Collections;

[System.Serializable]
public class Variable {

    public string variableId;

    public Variable()
    {

    }

    public Variable(string id)
    {
        variableId = id;
    }
}
