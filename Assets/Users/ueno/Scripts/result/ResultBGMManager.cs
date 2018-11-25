using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGMManager
{
    static GameObject resultBGM;

    static public void setResultBGM(GameObject obj)
    {
        Object.DontDestroyOnLoad(obj);
        resultBGM = obj;
    }

    static public void destoryBGM()
    {
        GameObject.Destroy(resultBGM);
    }
}