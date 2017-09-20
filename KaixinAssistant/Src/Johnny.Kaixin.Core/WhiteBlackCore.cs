using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

using Johnny.Library.Helper;

namespace Johnny.Kaixin.Core
{
    public class WhiteBlackCore : KaixinBase
    {
        private GamePark _gPark;
        private GameBite _gBite;
        private GameSlave _gSlave;
        private GameHouse _gHouse;
        private GameGarden _gGarden;
        private GameRanch _gRanch;
        private GameFish _gFish;
        private GameCafe _gCafe;
        
        public delegate void InitializationFailedEventHandler();
        public event InitializationFailedEventHandler InitializationFailed;

        public delegate void InitializationFinishedEventHandler();
        public event InitializationFinishedEventHandler InitializationFinished;

        public delegate void OperationFinishedEventHandler();
        public event OperationFinishedEventHandler OperationFinished;

        public WhiteBlackCore(string groupname, AccountInfo account)
        {
            base.Caption = account.UserName;
            base.Key = account.Email;
            base.CurrentAccount = account;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();

            Operation = ConfigCtrl.GetOperation(groupname, account);
            Task = ConfigCtrl.GetTask("", "", true);

            _gPark = new GamePark();
            _gBite = new GameBite();
            _gSlave = new GameSlave();
            _gHouse = new GameHouse();
            _gGarden = new GameGarden();
            _gRanch = new GameRanch();
            _gFish = new GameFish();
            _gCafe = new GameCafe();
            //不要clone validatecode 事件
            _gPark.Clone(this, true);
            _gBite.Clone(this, true);
            _gSlave.Clone(this, true);
            _gHouse.Clone(this, true);
            _gGarden.Clone(this, true);
            _gRanch.Clone(this, true);
            _gFish.Clone(this, true);
            _gCafe.Clone(this, true);
        }

        #region Initialize
        public void InitializeByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(Initialize));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        
        private void Initialize()
        {
            try
            {
                //set the current module
                _module = Constants.MSG_INITIALIZE;

                string content = string.Empty;

                //login
                if (!this.ValidationLogin(true))
                {
                    InitializationFailed();
                    return;
                }

                //all my friends
                SetMessageLn("正在初始化[我的开心网]...");
                content = base.RequestAllMyFriends();
                base.ReadAllMyFriends(content, false);
                SetMessage("[我的所有好友]信息下载成功！");

                _gPark.Module = _module;
                _gBite.Module = _module;
                _gSlave.Module = _module;
                _gHouse.Module = _module;
                _gGarden.Module = _module;
                _gRanch.Module = _module;
                _gFish.Module = _module;
                _gCafe.Module = _module;
                _gPark.Initialize();
                _gBite.Initialize();
                _gSlave.Initialize();
                _gHouse.Initialize();
                _gGarden.Initialize();
                _gRanch.Initialize();
                _gFish.Initialize();
                _gCafe.Initialize();

                SetMessageLn("初始化完成！");

                //invoke event
                if (InitializationFinished != null)
                    InitializationFinished();
            }
            catch (ThreadAbortException)
            {
                LogHelper.Write("WhiteBlackCore.Initialize", "终止", LogSeverity.Info);
                SetMessageLn("中止执行！");
                if (InitializationFailed != null)
                    InitializationFailed();
            }
            catch (ThreadInterruptedException)
            {
                LogHelper.Write("WhiteBlackCore.Initialize", "终止", LogSeverity.Info);
                SetMessageLn("中止执行！");
                if (InitializationFailed != null)
                    InitializationFailed();
            }
            catch (Exception ex)
            {
                LogHelper.Write("初始化" + base.Caption, ex);
                SetMessageLn("发生异常，初始化失败！错误：" + ex.Message);
                if (InitializationFailed != null)
                    InitializationFailed();
            }
        }
        #endregion

        #region StopThread
        public new void StopThread()
        {
            if (_threadMain != null && _threadMain.ThreadState != ThreadState.Stopped)
            {
                _threadMain.Abort();
            }
            if (_gPark != null)
                _gPark.StopThread();
            if (_gBite != null)
                _gBite.StopThread();
            if (_gSlave != null)
                _gSlave.StopThread();
            if (_gHouse != null)
                _gHouse.StopThread();
            if (_gGarden != null)
                _gGarden.StopThread();
            if (_gRanch != null)
                _gRanch.StopThread();
            if (_gFish != null)
                _gFish.StopThread();
            if (_gCafe != null)
                _gCafe.StopThread();
        }
        #endregion

        #region SingleTaskStart
        public void SingleTaskStartByThread()
        {
            _threadMain = new Thread(new System.Threading.ThreadStart(SingleTaskStart));
            _threadMain.IsBackground = true;
            _threadMain.Start();
        }
        private void SingleTaskStart()
        {
            TryCatchBlock th = new TryCatchBlock(delegate
            {
                _module = Constants.MSG_TASK;
                SetMessage("\r\n" + "============================== 开始 ==============================");
                SingleTaskRun();
                SetMessage("\r\n" + "============================== 完成 ==============================");
            });
            base.ExecuteTryCatchBlock(th, "发生异常，测试失败！");
        }
        #endregion

        #region SingleTaskRun

        public void SingleTaskRun()
        {
            if (!base.ValidationLogin(true))
            {
                return;
            }

            if (Task.ExecutePark)
                _gPark.RunPark();
            if (Task.ExecuteBite)
                _gBite.RunBite();               
            if (Task.ExecuteSlave)
                _gSlave.RunSlave();
            if (Task.ExecuteHouse)
                _gHouse.RunHouse();
            if (Task.ExecuteGarden)
                _gGarden.RunGarden();
            if (Task.ExecuteRanch)
                _gRanch.RunRanch();
            if (Task.ExecuteFish)
                _gFish.RunFish();
            if (Task.ExecuteCafe)
                _gCafe.RunCafe();

            base.LogOut(true);

            if (OperationFinished != null)
                OperationFinished();
        }
        #endregion

        #region Properties
        public GamePark Park
        {
            get { return _gPark; }
        }
        public GameBite Bite
        {
            get { return _gBite; }
        }
        public GameSlave Slave
        {
            get { return _gSlave; }
        }
        public GameHouse House
        {
            get { return _gHouse; }
        }
        public GameGarden Garden
        {
            get { return _gGarden; }
        }
        public GameRanch Ranch
        {
            get { return _gRanch; }
        }

        public GameFish Fish
        {
            get { return _gFish; }
        }

        public GameCafe Cafe
        {
            get { return _gCafe; }
        }
        #endregion

    }
}
