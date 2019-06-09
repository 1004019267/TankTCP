using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public enum MsgID
{ 
    TipError =0001,

    ReqMove = 1005,
    NotifyMove = 1006,

    ReqFire = 1007,
    NotifyFire = 1008,

    ReqHit = 1009,
    NotifyHit = 1010,

    ReqDeath = 1011,
    NotifyDeath = 1012,

    ReqReborn = 1013,
    RspReborn = 1014,

    ReqEnterRoom=1015,
    RspEnterRoom=1016,
    NotifyBattleStart=1017,
    NotifyBattleEnd=1018,
    NotifyArenaEnd=1019,
    NotifyReborn  =1020,

    ReqRegister =2001,
    RspRegister =2002,

    ReqLogin = 2003,
    RspLogin = 2004,

    NotifyOnline = 2005,
    NotifyOffline = 2006,

    ReqBuy=2011,
    RspBuy=2012,

    ReqUseTank=2013,
    RspUseTank=2014,

    ReqUseBullet=2015,
    RspUseBullet=2016,
}