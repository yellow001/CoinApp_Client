using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;
using System;
using System.Data;
using System.Threading.Tasks;

/// <summary>
/// 基础策略类（永续合约）
/// </summary>
[Serializable]
[ProtoContract]
public class Tactics
{
    /// <summary>
    /// 关联合约
    /// </summary>
    [ProtoMember(1)]
    public string V_Instrument_id;

    /// <summary>
    /// 当前持仓信息
    /// </summary>
    [ProtoMember(2)]
    public AccountInfo V_AccountInfo;

    /// <summary>
    /// 交易状态
    /// </summary>
    [ProtoMember(3)]
    public EM_TacticsState V_TacticsState;

    /// <summary>
    /// 下单状态
    /// </summary>
    [ProtoMember(4)]
    public EM_OrderOperation V_OrderState;

    [ProtoMember(5)]
    public KLineCache cache;

    public Tactics() { }
}