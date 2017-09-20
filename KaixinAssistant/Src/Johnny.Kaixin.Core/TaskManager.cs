using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Kaixin.Helper;
using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class TaskManager : KaixinBase
    {
        private string _taskid;
        private string _taskname;

        private bool _singleloopfirst;

        //private
        private Collection<MatchInfo> _matchList;
        //house
        private Collection<int> _houseFullList;
        //garden
        private Collection<int> _hasNothingTobeFarmedList;
        private Collection<SeedInfo> _seedsList;
        private Collection<RankSeedInfo> _rankSeedsList;
        private Collection<FruitInfo> _fruitsList;
        //ranch
        private Collection<int> _hasNothingTobeHelpedList;
        private Collection<CalfInfo> _calfsList;
        private Collection<ProductInfo> _productsList;
        //fish
        private Collection<FishFryInfo> _fishFrysList;
        private Collection<FishTackleInfo> _fishTacklesList;
        private Collection<FishMaturedInfo> _fishMaturedList;

        //rich
        private Collection<AssetInfo> _assetsList;
        private Collection<AdvancedPurchaseInfo> _advancedPurchaseList;

        //cafe
        private Collection<DishInfo> _dishesList;
        private Collection<DishInfo> _transactiondDishesList;

        public TaskManager(string taskid, string taskname)
        {
            base.Caption = taskname;
            base.Key = taskid;
            this._matchList = new Collection<MatchInfo>();
            this._houseFullList = new Collection<int>();
            this._hasNothingTobeFarmedList = new Collection<int>();
            this._hasNothingTobeHelpedList = new Collection<int>();

            _singleloopfirst = true; 
            _taskid = taskid;
            _taskname = taskname;

            Task = ConfigCtrl.GetTask(taskid, taskname, false);            
        }

        #region TaskStart
        public void TaskStart()
        {
            try
            {
                _module = Constants.MSG_TASK;

                int num = 0;

                if (Task.RunMode == EnumRunMode.MultiLoop || Task.RunMode == EnumRunMode.SingleLoop)
                {
                    SetMessageLn("开始执行循环任务...");

                    if (Task.RunMode == EnumRunMode.SingleLoop)
                        _singleloopfirst = true;

                    if (Task.Forbidden)
                    {
                        num = DurationMinute(new TimeInfo(DateTime.Now.Hour, DateTime.Now.Minute));
                        if (num > 0)
                        {
                            while (num > 0)
                            {
                                _module = Constants.MSG_TASK;
                                TaskWait(string.Format("距离{0}还有{1}分钟...", AddZero(Task.ForbiddenEnd.Hour) + ":" + AddZero(Task.ForbiddenEnd.Minute), num--));
                                Thread.Sleep(0xea60);
                            }
                        }
                        TaskRun();

                        while (Task.RoundTime > 0)
                        {
                            int dur = DurationMinute(GetNextRunTime());
                            num = Task.RoundTime + dur;

                            while (num > 0)
                            {
                                _module = Constants.MSG_TASK;
                                if (dur > 0)
                                    TaskWait(string.Format("距离{0}还有{1}分钟...", AddZero(Task.ForbiddenEnd.Hour) + ":" + AddZero(Task.ForbiddenEnd.Minute), num--));
                                else
                                    TaskWait(string.Format("{0}分钟后重新启动任务...", new object[] { num-- }));
                                Thread.Sleep(0xea60);
                            }
                            TaskRun();
                        }
                    }
                    else
                    {
                        _module = Constants.MSG_TASK;
                        TaskRun();
                        while (Task.RoundTime > 0)
                        {
                            num = Task.RoundTime;
                            while (num > 0)
                            {
                                TaskWait(string.Format("{0}分钟后重新启动任务...", new object[] { num-- }));
                                Thread.Sleep(0xea60);
                            }
                            TaskRun();
                        }
                    }
                }
                else if (Task.RunMode == EnumRunMode.Timing)
                {
                    TimeInfo lastrun = null;
                    SetMessageLn("开始执行定时任务...");
                    while (true)
                    {
                        TimeInfo runtime = GetLatestRunTime(lastrun);
                        if (runtime.CompareTo(new TimeInfo(99, 99)) == 0)
                            return;

                        num = runtime.LeftMinutes(DateTime.Now);
                        if (num == 0)
                        {
                            lastrun = runtime;
                            TaskRun();
                        }
                        else
                        {
                            while (num > 0)
                            {
                                _module = Constants.MSG_TASK;
                                TaskWait(string.Format("距离{0}还有{1}分钟...", AddZero(runtime.Hour) + ":" + AddZero(runtime.Minute), num--));
                                Thread.Sleep(0xea60);
                            }
                            lastrun = runtime;
                            TaskRun();
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
                //LogHelper.Write("TaskManager.TaskStart", "ThreadAbortException", LogSeverity.Info);
                SetMessageLn("中止执行！");
                if (Task.WriteLogToFile)
                    TraceLog.WriteLogToFile(Task.TaskName, this.ExecutionLog.ToString());
            }
            catch (ThreadInterruptedException)
            {
                //LogHelper.Write("TaskManager.TaskStart", "ThreadInterruptedException", LogSeverity.Info);
                SetMessageLn("中止执行！");
                if (Task.WriteLogToFile)
                    TraceLog.WriteLogToFile(Task.TaskName, this.ExecutionLog.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.Write("TaskManager.TaskStart", ex);
                SetMessageLn("发生异常，任务已中止！错误：" + ex.Message);
                if (Task.WriteLogToFile)
                    TraceLog.WriteLogToFile(Task.TaskName, this.ExecutionLog.ToString());
            }
        }
        #endregion

        #region TaskRun
        public void TaskRun()
        {
            try
            {
                //重新读取
                Task = ConfigCtrl.GetTask(_taskid, _taskname, false);

                base.ExecutionLog = new StringBuilder();
                base.Proxy = ConfigCtrl.GetProxy();
                base.Delay = ConfigCtrl.GetDelay();

                if (Task.RunMode != EnumRunMode.SingleLoop || _singleloopfirst == true)
                    base.Initial(true);
                else
                    base.Initial(false);

                if (Task.ExecutePark)
                    this._matchList = ConfigCtrl.GetMatches();
                if (Task.ExecuteHouse)
                    this._houseFullList.Clear();
                if (Task.ExecuteGarden)
                {
                    this._hasNothingTobeFarmedList.Clear();
                    this._seedsList = ConfigCtrl.GetSeedsInShop();
                    this._rankSeedsList = ConfigCtrl.GetRankSeeds();
                    this._fruitsList = ConfigCtrl.GetFruits();
                }
                if (Task.ExecuteRanch)
                {
                    this._hasNothingTobeHelpedList.Clear();
                    this._calfsList = ConfigCtrl.GetCalvesInShop();
                    this._productsList = ConfigCtrl.GetAnimalProducts();
                }
                if (Task.ExecuteFish)
                {
                    this._fishFrysList = ConfigCtrl.GetFishFrysInShop();
                    this._fishTacklesList = ConfigCtrl.GetFishTacklesInShop();
                    this._fishMaturedList = ConfigCtrl.GetFishMaturedInMarket();
                }
                if (Task.ExecuteRich)
                {
                    this._assetsList = ConfigCtrl.GetAssetsInShop();
                    this._advancedPurchaseList = ConfigCtrl.GetAdvancedPurchaseMD();
                }
                if (Task.ExecuteCafe)
                {
                    this._dishesList = ConfigCtrl.GetDishesInMenu();
                    this._transactiondDishesList = ConfigCtrl.GetTransactionDishes();
                }

                SetMessage(Constants.COMMAND_CLEARLOG);
                //start
                SetMessage("\r\n" + "============================== 开始 ==============================");

                int num = 0;
                foreach (AccountInfo account in Task.Accounts)
                {
                    try
                    {
                        Thread.Sleep(2000);
                        //common
                        base.AllMyFriendsList.Clear();
                        //this._verifyCode = "";
                        this._module = Constants.MSG_TASK;

                        num++;
                        SetMessageLn("------ 共" + Task.Accounts.Count + "个帐户，第" + num + "个帐户：" + account.UserName + "(" + account.Email + ") ------");
                        base.Operation = ConfigCtrl.GetOperation(Task.GroupName, account);
                        base.CurrentAccount = account;

                        if (Task.RunMode != EnumRunMode.SingleLoop || _singleloopfirst == true)
                        {
                            if (!this.ValidationLogin(account))
                            {
                                continue;
                            }
                            else
                                _singleloopfirst = false;
                        }
                        //if (!this.Login(true))
                        //{
                        //    if (base.NeedValidationCode)
                        //    {
                        //        while (base.RetryLogin)
                        //        {
                        //            if (!this.Login(true))
                        //                continue;
                        //            else
                        //                break;
                        //        }
                        //        if (!base.RetryLogin)
                        //            continue;
                        //    }
                        //    else
                        //        continue;
                        //}

                        string content = RequestAllMyFriends();
                        ReadAllMyFriends(content, false);

                        if (Task.ExecutePark)
                        {
                            GamePark gPark = new GamePark();
                            gPark.Clone(this);
                            gPark.MatchesList = this._matchList;
                            gPark.RunPark();
                            this.ExecutionLog = gPark.ExecutionLog;
                        }
                        if (Task.ExecuteBite)
                        {
                            GameBite gBite = new GameBite();
                            gBite.Clone(this);
                            gBite.RunBite();
                            this.ExecutionLog = gBite.ExecutionLog;
                        }
                        if (Task.ExecuteSlave)
                        {
                            GameSlave gSlave = new GameSlave();
                            gSlave.Clone(this);
                            gSlave.RunSlave();
                            this.ExecutionLog = gSlave.ExecutionLog;
                        }
                        if (Task.ExecuteHouse)
                        {
                            GameHouse gHouse = new GameHouse();
                            gHouse.Clone(this);
                            gHouse.HouseFullList = this._houseFullList;
                            gHouse.RunHouse();
                            this.ExecutionLog = gHouse.ExecutionLog;
                            this._houseFullList = gHouse.HouseFullList;
                        }
                        if (Task.ExecuteGarden)
                        {
                            GameGarden gGarden = new GameGarden();
                            gGarden.Clone(this);
                            gGarden.SeedsList = this._seedsList;
                            gGarden.RankSeedsList = this._rankSeedsList;
                            gGarden.FruitsList = this._fruitsList;
                            gGarden.HasNothingTobeFarmedList = this._hasNothingTobeFarmedList;
                            gGarden.RunGarden();
                            this.ExecutionLog = gGarden.ExecutionLog;
                            this._hasNothingTobeFarmedList = gGarden.HasNothingTobeFarmedList;
                        }
                        if (Task.ExecuteRanch)
                        {
                            GameRanch gRanch = new GameRanch();
                            gRanch.Clone(this);
                            gRanch.CalfsList = this._calfsList;
                            gRanch.ProductsList = this._productsList;
                            //gRanch.HasNothingTobeHelpedList = this._hasNothingTobeHelpedList;
                            gRanch.RunRanch();
                            this.ExecutionLog = gRanch.ExecutionLog;
                            //this._hasNothingTobeHelpedList = gRanch.HasNothingTobeHelpedList;
                        }

                        if (Task.ExecuteFish)
                        {
                            GameFish gFish = new GameFish();
                            gFish.Clone(this);
                            gFish.FishFrysList = this._fishFrysList;
                            gFish.FishTacklesList = this._fishTacklesList;
                            gFish.FishMaturedList = this._fishMaturedList;
                            gFish.RunFish();
                            this.ExecutionLog = gFish.ExecutionLog;
                        }

                        if (Task.ExecuteRich)
                        {
                            GameRich gRich = new GameRich();
                            gRich.Clone(this);
                            gRich.AssetsList = this._assetsList;
                            gRich.AdvancedPurchaseList = this._advancedPurchaseList;
                            gRich.RunRich();
                            this.ExecutionLog = gRich.ExecutionLog;
                        }

                        if (Task.ExecuteCafe)
                        {
                            GameCafe gCafe = new GameCafe();                           
                            gCafe.Clone(this);
                            gCafe.DishesList = this._dishesList;
                            gCafe.TransactiondDishesList = this._transactiondDishesList;
                            gCafe.RunCafe();
                            this.ExecutionLog = gCafe.ExecutionLog;
                        }

                        if (Task.RunMode != EnumRunMode.SingleLoop)
                            this.LogOut(true);
                    }
                    catch (ThreadAbortException)
                    {
                        //LogHelper.Write("TaskManager.TaskRun", "ThreadAbortException" + account.UserName, LogSeverity.Info);
                    }
                    catch (ThreadInterruptedException)
                    {
                        //LogHelper.Write("TaskManager.TaskRun", "ThreadInterruptedException" + account.UserName, LogSeverity.Info);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Write("TaskManager.TaskRun", account.UserName, ex, LogSeverity.Error);
                        SetMessageLn("发生异常，此账户操作失败！错误：" + ex.Message);
                    }
                }

                SetMessage("\r\n" + "============================== 完成 ==============================");

                _module = Constants.MSG_TASK;
                if (Task.SendLog && !String.IsNullOrEmpty(Task.ReceiverEmail))
                    SendRemindMail();

                if (Task.WriteLogToFile)
                    TraceLog.WriteLogToFile(Task.TaskName, this.ExecutionLog.ToString());
            }
            catch (ThreadAbortException)
            {
                //LogHelper.Write("TaskManager.TaskRun", "AferAllAccounts-ThreadAbortException", LogSeverity.Info);
            }
            catch (ThreadInterruptedException)
            {
                //LogHelper.Write("TaskManager.TaskRun", "AferAllAccounts-ThreadInterruptedException", LogSeverity.Info);
            }
            catch (Exception ex)
            {
                LogHelper.Write("TaskManager.TaskRun", ex);
                SetMessageLn("发生异常，本次任务已中止！错误：" + ex.Message);
            }
        }

        #endregion

        #region TaskWait
        public void TaskWait(string msg)
        {
            SetMessageLn(msg);
        }
        #endregion

        #region GetNextRunTime
        private TimeInfo GetNextRunTime()
        {
            int hour = DateTime.Now.Hour + Task.RoundTime / 60;
            int minute = DateTime.Now.Minute + Task.RoundTime % 60;
            if (minute >= 60)
            {
                hour += 1;
                minute -= 60;
            }
            if (hour >= 24)
                hour -= 24;

            return new TimeInfo(hour, minute);
        }
        #endregion

        #region DurationMinute
        private int DurationMinute(TimeInfo time)
        {
            int num = 0;

            if (CompareTo(Task.ForbiddenStart, Task.ForbiddenEnd) < 0)
            {
                if (CompareTo(Task.ForbiddenStart, time) <= 0 && CompareTo(Task.ForbiddenEnd, time) >= 0)
                {
                    num = LeftMinutes(Task.ForbiddenEnd, time);
                }
            }
            else
            {
                if (CompareTo(Task.ForbiddenStart, time) <= 0 || CompareTo(Task.ForbiddenEnd, time) >= 0)
                {
                    num = LeftMinutes(Task.ForbiddenEnd, time);
                }
            }

            return num;
        }
        #endregion

        #region GetLatestRunTime
        private TimeInfo GetLatestRunTime(TimeInfo last)
        {
            if (Task.StartTimes == null || Task.StartTimes.Count == 0)
                return new TimeInfo(99, 99);

            TimeInfo min = null;
            int num = 0;

            foreach (TimeInfo time in Task.StartTimes)
            {
                if (last == null)
                {
                    if (CompareTo(time, DateTime.Now) >= 0)
                    {
                        if (min == null)
                        {
                            min = time;
                            num = min.LeftMinutes(DateTime.Now);
                        }
                        else
                        {
                            if (num > time.LeftMinutes(DateTime.Now))
                            {
                                min = time;
                                num = time.LeftMinutes(DateTime.Now);
                            }
                        }
                    }
                }
                else
                {
                    if (CompareTo(time, DateTime.Now) >= 0 && time.Minute > last.Minute)
                        if (min == null)
                        {
                            min = time;
                            num = min.LeftMinutes(DateTime.Now);
                        }
                        else
                        {
                            if (num > time.LeftMinutes(DateTime.Now))
                            {
                                min = time;
                                num = time.LeftMinutes(DateTime.Now);
                            }
                        }
                }
            }

            if (min == null)
                min = Task.StartTimes[0];

            return min;
        }
        #endregion

        #region LeftMinutes
        private int LeftMinutes(TimeInfo time, TimeInfo dt)
        {
            int cmpmin = CompareTo(time, new TimeInfo(dt.Hour, dt.Minute));
            if (cmpmin < 0)
                return (24 - dt.Hour) * 60 - dt.Minute + time.Hour * 60 + time.Minute;
            else if (cmpmin == 0)
                return 0;
            else
                return (time.Hour - dt.Hour) * 60 - dt.Minute + time.Minute;
        }
        #endregion

        #region CompareTo
        private int CompareTo(TimeInfo time, DateTime now)
        {
            if (time.Hour - now.Hour > 0)
                return 1;
            else if (time.Hour - now.Hour == 0)
            {
                if (time.Minute - now.Minute > 0)
                    return 1;
                else if (time.Minute - now.Minute == 0)
                    return 0;
                else
                    return -1;
            }
            else
                return -1;
        }
        private int CompareTo(TimeInfo time1, TimeInfo time2)
        {
            if (time1.Hour - time2.Hour > 0)
                return 1;
            else if (time1.Hour - time2.Hour == 0)
            {
                if (time1.Minute - time2.Minute > 0)
                    return 1;
                else if (time1.Minute - time2.Minute == 0)
                    return 0;
                else
                    return -1;
            }
            else
                return -1;
        }
        #endregion

        #region AddZero
        private string AddZero(int min)
        {
            if (min <= 9)
                return "0" + min.ToString();
            else
                return min.ToString();
        }
        #endregion

        #region 发送运行日志
        private void SendRemindMail()
        {
            try
            {
                //load smtp config info
                SmtpInfo smtpInfo = ConfigCtrl.GetSmtp();
                if (smtpInfo == null)
                {
                    SetMessageLn("Smtp配置信息读取失败，无法发送日志！");
                    return;
                }

                MailHelper.SendMail(smtpInfo.SmtpHost, smtpInfo.SmtpPort, smtpInfo.SenderEmail, smtpInfo.Password, Task.ReceiverEmail, "开心助手运行日志：" + DateTime.Now.ToString(), this.ExecutionLog.ToString());

                SetMessageLn("运行日志已发送到" + Task.ReceiverEmail + "！");
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (ThreadInterruptedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Write("TaskManager.SendRemindMail", ex, LogSeverity.Error);
                SetMessageLn("发送日志失败！错误：" + ex.Message);
            }
        }

        #endregion

    }
}
