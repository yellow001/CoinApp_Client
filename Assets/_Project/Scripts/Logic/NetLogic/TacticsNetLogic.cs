using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsNetLogic
{
    public void Init()
    {
        NetCenter.Ins.AddMsgEvent<ResTacticsListMessage>(ResTacticsListMessage.V_Pid, ResTacticsListMessage_CB);
    }

    /// <summary>
    /// 账户信息返回
    /// </summary>
    /// <param name="token"></param>
    /// <param name="msg"></param>
    public void ResTacticsListMessage_CB(BaseMessage msg)
    {
        if (msg is ResTacticsListMessage)
        {
            TacticsMgr.GetIns().V_Model.UpdateTacticsList(msg as ResTacticsListMessage);
        }
    }


    #region 请求
    /// <summary>
    /// 请求策略信息
    /// </summary>
    public void ReqTacticsInfo() {
        NetCenter.Ins.Send(ReqTacticsInfoMessage.V_Pid, "");
    }

    /// <summary>
    /// 请求改变运行状态
    /// </summary>
    public void ReqChangeTacticsState(string coin,int state)
    {
        ReqChangeTacticsStateMessage msg = new ReqChangeTacticsStateMessage();
        msg.coin = coin;
        msg.type = state;
        NetCenter.Ins.Send(ReqChangeTacticsStateMessage.V_Pid, msg);
    }

    /// <summary>
    /// 请求改变下单策略
    /// </summary>
    public void ReqChangeOrderState(string coin, int state)
    {
        ReqChangeOrderStateMessage msg = new ReqChangeOrderStateMessage();
        msg.coin = coin;
        msg.state = state;
        NetCenter.Ins.Send(ReqChangeOrderStateMessage.V_Pid, msg);
    }
    #endregion
}
