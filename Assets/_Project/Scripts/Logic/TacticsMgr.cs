using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMgr
{
    public TacticsNetLogic V_NetLogic;

    public TacticsModel V_Model;


    static TacticsMgr ins;

    public static TacticsMgr GetIns()
    {
        if (ins == null)
        {
            ins = new TacticsMgr();

            ins.V_NetLogic = new TacticsNetLogic();
            ins.V_Model = new TacticsModel();

            ins.V_NetLogic.Init();
        }
        return ins;
    }

    public TacticsMgr()
    {
    }
}
