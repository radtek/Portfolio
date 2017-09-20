using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;
using System.Data;

using Johnny.Kaixin.Helper;

namespace Johnny.Kaixin.Core
{
    public class ToolUpdateData : KaixinBase
    {
        private GamePark _gPark;
        private GameGarden _gGarden;
        private GameRanch _gRanch;
        private GameFish _gFish;
        private GameCafe _gCafe;

        public AccountInfo _account;

        public delegate void AllCarsInMarketFetchedEventHandler(Collection<NewCarInfo> carsinmarket);
        public event AllCarsInMarketFetchedEventHandler AllCarsInMarketFetched;

        public delegate void SeedsInShopFetchedEventHandler(Collection<SeedInfo> seeds);
        public event SeedsInShopFetchedEventHandler SeedsInShopFetched;

        public delegate void CalvesInShopFetchedEventHandler(Collection<CalfInfo> calves);
        public event CalvesInShopFetchedEventHandler CalvesInShopFetched;

        public delegate void FishFrysInShopFetchedEventHandler(Collection<FishFryInfo> fishfrys);
        public event FishFrysInShopFetchedEventHandler FishFrysFetched;

        public delegate void FishTacklesInShopFetchedEventHandler(Collection<FishTackleInfo> fishtackles);
        public event FishTacklesInShopFetchedEventHandler FishTacklesFetched;

        public delegate void DishesInMenuFetchedEventHandler(Collection<DishInfo> dishes);
        public event DishesInMenuFetchedEventHandler DishesFetched;

        public delegate void TransactionDishesFetchedEventHandler(Collection<DishInfo> transactiondishes);
        public event TransactionDishesFetchedEventHandler TransactionDishesFetched;

        public ToolUpdateData()
        {
            base.Caption = Constants.TOOL_UPDATEDATA;
            base.Key = Constants.TOOL_UPDATEDATA;
            base.Proxy = ConfigCtrl.GetProxy();
            base.Delay = ConfigCtrl.GetDelay();
            base.Initial();
        }

        //汽车
        public void RefreshCars()
        {
            _gPark = new GamePark();
            _gPark.AllCarsInMarketFetched += new GamePark.AllCarsInMarketFetchedEventHandler(gPark_AllCarsInMarketFetched);
            _gPark.Clone(this);
            _gPark.CurrentAccount = _account;
            _gPark.GetAllCarsInMarketByThread();
        }

        private void gPark_AllCarsInMarketFetched(Collection<NewCarInfo> carsinmarket)
        {
            if (AllCarsInMarketFetched != null)
                AllCarsInMarketFetched(carsinmarket);
        }

        //种子
        public void RefreshSeeds()
        {
            _gGarden = new GameGarden();
            _gGarden.SeedsInShopFetched += new GameGarden.SeedsInShopFetchedEventHandler(gGarden_SeedsInShopFetched);
            _gGarden.Clone(this);
            _gGarden.CurrentAccount = _account;
            _gGarden.GetSeedsInShopByThread();
        }

        private void gGarden_SeedsInShopFetched(Collection<SeedInfo> seeds)
        {
            if (SeedsInShopFetched != null)
                SeedsInShopFetched(seeds);
        }

        //动物幼仔
        public void RefreshCalves()
        {
            _gRanch = new GameRanch();
            _gRanch.CalvesInShopFetched += new GameRanch.CalvesInShopFetchedEventHandler(_gRanch_CalvesInShopFetched);
            _gRanch.Clone(this);
            _gRanch.CurrentAccount = _account;
            _gRanch.GetCalvesInShopByThread();
        }

        private void _gRanch_CalvesInShopFetched(Collection<CalfInfo> calves)
        {
            if (CalvesInShopFetched != null)
                CalvesInShopFetched(calves);
        }

        //鱼苗
        public void RefreshFishFrys()
        {
            _gFish = new GameFish();
            _gFish.FishFrysFetched += new GameFish.FishFrysInShopFetchedEventHandler(_gFish_FishFrysFetched);
            _gFish.Clone(this);
            _gFish.CurrentAccount = _account;
            _gFish.GetFishFrysInShopByThread();
        }

        private void _gFish_FishFrysFetched(Collection<FishFryInfo> fishfrys)
        {
            if (FishFrysFetched != null)
                FishFrysFetched(fishfrys);
        }

        //鱼竿
        public void RefreshFishTackles()
        {
            _gFish = new GameFish();
            _gFish.FishTacklesFetched += new GameFish.FishTacklesInShopFetchedEventHandler(_gFish_FishTacklesFetched);
            _gFish.Clone(this);
            _gFish.CurrentAccount = _account;
            _gFish.GetFishTackleInShopByThread();
        }

        private void _gFish_FishTacklesFetched(Collection<FishTackleInfo> fishtackles)
        {
            if (FishTacklesFetched != null)
                FishTacklesFetched(fishtackles);
        }

        //菜肴
        public void RefreshDishes()
        {
            _gCafe = new GameCafe();
            _gCafe.DishesFetched += new GameCafe.DishesInMenuFetchedEventHandler(_gCafe_DishesFetched);
            _gCafe.Clone(this);
            _gCafe.CurrentAccount = _account;
            _gCafe.GetDishInMenuByThread();
        }

        private void _gCafe_DishesFetched(Collection<DishInfo> dishes)
        {
            if (DishesFetched != null)
                DishesFetched(dishes);
        }

        //菜肴交易价格表
        public void RefreshTransactionDishes()
        {
            _gCafe = new GameCafe();
            _gCafe.TransactionDishesFetched += new GameCafe.TransactionDishesFetchedEventHandler(_gCafe_TransactionDishesFetched);
            _gCafe.Clone(this);
            _gCafe.CurrentAccount = _account;
            _gCafe.GetTransactionDishesInMarketByThread();
        }

        private void _gCafe_TransactionDishesFetched(Collection<DishInfo> transactiondishes)
        {
            if (TransactionDishesFetched != null)
                TransactionDishesFetched(transactiondishes);
        }

        public new void StopThread()
        {
            if (_threadMain != null && _threadMain.ThreadState != ThreadState.Stopped)
            {
                _threadMain.Abort();
            }
            if (_gPark != null)
                _gPark.StopThread();
            if (_gGarden != null)
                _gGarden.StopThread();
            if (_gRanch != null)
                _gRanch.StopThread();
            if (_gFish != null)
                _gFish.StopThread();
            if (_gCafe != null)
                _gCafe.StopThread();
        }

        public void UpdateValidationCode(string validationcode)
        {
            if (_gPark != null)
                _gPark.ValidationCode = validationcode;
            if (_gGarden != null)
                _gGarden.ValidationCode = validationcode;
            if (_gRanch != null)
                _gRanch.ValidationCode = validationcode;
            if (_gFish != null)
                _gFish.ValidationCode = validationcode;
            if (_gCafe != null)
                _gCafe.ValidationCode = validationcode;
        }
    }
}
