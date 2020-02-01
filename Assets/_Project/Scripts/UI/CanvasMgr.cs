using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgr : MonoBehaviour
{

    [SerializeField]
    WinTip tip;


    bool canUpdate = false;
    // Start is called before the first frame update
    void Start()
    {
        EventMgr.Ins.AddEventFun(EM_LoginEvent.BeginConnect, (args) => { canUpdate = true; });
    }

    // Update is called once per frame
    void Update()
    {
        if (!canUpdate) { return; }

        List<ResTipsMessage> msgList = CommonMgr.GetIns().V_Model.GetTipList();
        if (msgList != null && msgList.Count > 0) {
            ResTipsMessage msg = msgList[0];
            tip.Refresh(msg.tips);
            msgList.Remove(msg);
        }
    }
}
