using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonNetLogic
{
    public void Init()
    {
        NetCenter.Ins.AddMsgEvent<ResTipsMessage>(ResTipsMessage.V_Pid, ResTipsMessage_CB);
    }

    /// <summary>
    /// 账户信息返回
    /// </summary>
    /// <param name="token"></param>
    /// <param name="msg"></param>
    public void ResTipsMessage_CB(BaseMessage msg)
    {
        if (msg is ResTipsMessage)
        {
            CommonMgr.GetIns().V_Model.AddTip(msg as ResTipsMessage);
        }
    }
}
