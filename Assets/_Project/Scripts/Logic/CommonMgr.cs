using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMgr
{
    public CommonNetLogic V_NetLogic;

    public CommonModel V_Model;


    static CommonMgr ins;

    public static CommonMgr GetIns()
    {
        if (ins == null)
        {
            ins = new CommonMgr();

            ins.V_NetLogic = new CommonNetLogic();
            ins.V_Model = new CommonModel();

            ins.V_NetLogic.Init();
        }
        return ins;
    }

    public CommonMgr()
    {
    }
}
