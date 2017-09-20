using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public enum EnumRunMode
    {
        SingleLoop, //单账号循环
        MultiLoop,  //多账号循环
        Timing      //定时
    }

    public enum BiteStatus
    {
        NoRoom,
        IsRecovering,
        Healthy,
        NeedRecovery,
        InBlackList,
        Unknown
    }
    
    public enum CarColor
    { 
        White = 16777215,
        Silver = 13421772,
        Black = 0,
        Red = 16711680,
        Blue = 255,
        Yellow = 16776960
    }

    public enum ExchangeCar
    { 
        Expensive,
        Cheap,
        Stop
    }

    public enum MatchStatus
    {
        NotInMatch,//目前无比赛
        OriginateMatch,//发起比赛中
        InMatch, //比赛进行中     
        WithoutTeam, //你还没有组建你的车队
        UnKnown //未知
    }

    public enum CarStatus
    {
        Started, //赛车已启动
        UnKnown
    }

    public enum StayStatus
    {
        InTheStreet, //我目前露宿街头
        StayInOwn, //我目前住在自己家
        StayInOthers, //我目前住在王思懿家
        WithinOneHour, //入住还没满1小时
        UnKnown //未知
    }

    public enum HouseStatus
    {
        NoLodger, //没有住客
        //OnlyOneLodger, //只有一个住客
        CanStay,
        Full, //住满了
        UnKnown //未知
    }

    //public enum FarmStatus
    //{
    //    OutOfMoney,
    //    Success,
    //    Continue,
    //    Failed
    //}
    public enum FortuneRank
    {
        Worker,             //打工者
        Millionaire,        //百万富翁
        Multimillionaire,   //千万富翁
        Billionaire,        //亿万富翁
        SuperBillionaire    //超级富翁
    }
}
