using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public enum EnumRunMode
    {
        SingleLoop, //���˺�ѭ��
        MultiLoop,  //���˺�ѭ��
        Timing      //��ʱ
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
        NotInMatch,//Ŀǰ�ޱ���
        OriginateMatch,//���������
        InMatch, //����������     
        WithoutTeam, //�㻹û���齨��ĳ���
        UnKnown //δ֪
    }

    public enum CarStatus
    {
        Started, //����������
        UnKnown
    }

    public enum StayStatus
    {
        InTheStreet, //��Ŀǰ¶�޽�ͷ
        StayInOwn, //��Ŀǰס���Լ���
        StayInOthers, //��Ŀǰס����˼ܲ��
        WithinOneHour, //��ס��û��1Сʱ
        UnKnown //δ֪
    }

    public enum HouseStatus
    {
        NoLodger, //û��ס��
        //OnlyOneLodger, //ֻ��һ��ס��
        CanStay,
        Full, //ס����
        UnKnown //δ֪
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
        Worker,             //����
        Millionaire,        //������
        Multimillionaire,   //ǧ����
        Billionaire,        //������
        SuperBillionaire    //��������
    }
}
