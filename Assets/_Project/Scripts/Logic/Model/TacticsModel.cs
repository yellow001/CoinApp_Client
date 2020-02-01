using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TacticsModel
{
    Dictionary<string,Tactics> V_AccountInfoDic;

    /// <summary>
    /// 更新合约信息
    /// </summary>
    /// <param name="msg"></param>
    public void UpdateTacticsList(ResTacticsListMessage msg) {
        if (V_AccountInfoDic == null) {
            InitAccountDic();
        }
        foreach (var key in V_AccountInfoDic.Keys.ToList())
        {
            V_AccountInfoDic[key].V_TacticsState = EM_TacticsState.Stop;
            V_AccountInfoDic[key].V_OrderState = EM_OrderOperation.Normal;

            foreach (var item in msg.V_AccountInfoList)
            {
                if (key.Equals(item.V_Instrument_id)) {
                    V_AccountInfoDic[key] = item;
                    continue;
                }
            }
        }

        EventMgr.Ins.InvokeDeList(EM_AccountEvent.UpdateTactics);
    }

    void InitAccountDic() {
        string[] list = AppSetting.Ins.GetValue("Run").Split(';');
        V_AccountInfoDic = new Dictionary<string, Tactics>();
        foreach (var item in list)
        {
            Tactics tactics = new Tactics();
            tactics.V_TacticsState = EM_TacticsState.Stop;
            tactics.V_OrderState = EM_OrderOperation.Normal;
            string k = string.Format("{0}-USD-SWAP", item);
            tactics.V_Instrument_id = k;
            V_AccountInfoDic[k] = tactics;
        }
    }

    public Dictionary<string, Tactics> GetAccountInfoDic() {
        if (V_AccountInfoDic == null) {
            InitAccountDic();
        }
        return V_AccountInfoDic;
    }
}
