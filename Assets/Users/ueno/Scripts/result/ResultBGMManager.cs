using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGMManager
{
    static GameObject resultBGM = null;

    static public bool setResultBGM(GameObject obj)
    {
        if (resultBGM != null)
            return false;

        Object.DontDestroyOnLoad(obj);
        resultBGM = obj;

        return true;
    }

    static public void destoryBGM()
    {
        GameObject.Destroy(resultBGM);
        resultBGM = null;
    }
}