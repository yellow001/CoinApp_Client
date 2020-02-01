using Newtonsoft.Json.Linq;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Serializable]
[ProtoContract]
/// <summary>
/// 永续账号信息
/// </summary>
public class AccountInfo
{
    /// <summary>
    /// 所属合约
    /// </summary>
    [ProtoMember(1)]
    public string V_Instrument_id;
    /// <summary>
    /// 动态权益（可用+已用+挂单冻结 资金）
    /// </summary>
    [ProtoMember(2)]
    public float V_Equity;
    /// <summary>
    /// 已用保证金
    /// </summary>
    [ProtoMember(3)]
    public float V_Margin;
    /// <summary>
    /// 开仓冻结保证金
    /// </summary>
    [ProtoMember(4)]
    public float V_Margin_frozen;
    /// <summary>
    /// 更新时间戳
    /// </summary>
    [ProtoMember(5)]
    public DateTime V_TimeStamp;
    /// <summary>
    /// 当前价格
    /// </summary>
    [ProtoMember(6)]
    public float V_CurPrice;

    /// <summary>
    /// 持仓信息（多 空）
    /// </summary>
    [ProtoMember(7)]
    public List<Position> V_Positions;

    /// <summary>
    /// 杠杆倍数
    /// </summary>
    [ProtoMember(8)]
    public float V_Leverage;

    public AccountInfo() { }

    public AccountInfo(string json)
    {
        RefreshData(json);
    }

    public void RefreshData(string json) {
        JObject obj = JObject.Parse(json);
        V_Instrument_id = obj["instrument_id"].ToString();
        V_Equity = float.Parse(obj["equity"].ToString());
        V_Margin = float.Parse(obj["margin"].ToString());
        V_Margin_frozen = float.Parse(obj["margin_frozen"].ToString());
        V_TimeStamp = DateTime.Parse(obj["timestamp"].ToString());
        if (V_Positions == null)
        {
            V_Positions = new List<Position>();
        }
        V_Positions.Clear();
    }

    /// <summary>
    /// 设置持仓信息
    /// </summary>
    /// <param name="positionList"></param>
    public void RefreshPositions(List<Position> positionList) {
        V_Positions.Clear();
        V_Positions.AddRange(positionList);
    }

    public static AccountInfo GetAccount(string json)
    {
        return new AccountInfo(json);
    }
}
