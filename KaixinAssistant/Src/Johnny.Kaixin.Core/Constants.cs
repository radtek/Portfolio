using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Kaixin.Core
{
    public class Constants
    {
        //common
        public const string MESSAGEBOX_CAPTION = "开心助手";
        public const string CONFIG_ROOT = "ZrAssistant";
        public const string COMMAND_CLEARLOG = "Johnny.Command.ClearLog";

        //file
        public const string FILE_ASSISTANTCONFIG = "AssistantConfig.xml";
        public const string FILE_GROUPCONFIG = "GroupConfig.xml";
        public const string FILE_ACCOUNTCONFIG = "AccountConfig.xml";
        public const string FILE_TASKCONFIG = "TaskConfig.xml";
        public const string FILE_CARSINMARKETMASTERDATA = "CarsInMarketMasterData.xml";
        public const string FILE_MATCHESMASTERDATA = "MatchesMasterData.xml";
        public const string FILE_SEEDSLISTMASTERDATA = "SeedsInShopMasterData.xml";
        public const string FILE_CALFSLISTMASTERDATA = "CalfsInShopMasterData.xml";
        public const string FILE_ANIMALPRODUCTMASTERDATA = "AnimalProductMasterData.xml";
        public const string FILE_RANKSEEDSMASTERDATA = "RankSeedsMasterData.xml";
        public const string FILE_FRUITSMASTERDATA = "FruitsMasterData.xml";
        public const string FILE_FISHFRYSMASTERDATA = "FishFrysMasterData.xml";
        public const string FILE_FISHTACKLESMASTERDATA = "FishTacklesMasterData.xml";
        public const string FILE_FISHMATUREDMASTERDATA = "FishMaturedMasterData.xml";
        public const string FILE_ASSETSMASTERDATA = "AssetsMasterData.xml";        
        public const string FILE_ADVANCEDPURCHASEMASTERDATA = "AdvancedPurchaseMasterData.xml";
        public const string FILE_CAFEDISHESMASTERDATA = "CafeDishesMasterData.xml";
        public const string FILE_CAFEDISHESTRANSACTIONMASTERDATA = "CafeDishesTransactionMasterData.xml";

        //folder
        public const string FOLDER_TASKS = "Tasks";
        public const string FOLDER_ACCOUNTS = "Accounts";
        public const string FOLDER_MASTERDATA = "MasterData";

        //proxy        
        public const string PROXY_PROXY = "Proxy";
        public const string PROXY_ENABLE = "Enable";
        public const string PROXY_SERVER = "ProxyServer";
        public const string PROXY_PORT = "ProxyPort";
        public const string PROXY_USER = "ProxyUser";
        public const string PROXY_PASS = "ProxyPass";

        //delay
        public const string DELAY_DELAY = "Delay";
        public const string DELAY_DELAYEDTIME = "DelayedTime";
        public const string DELAY_TIMEOUT = "TimeOut";
        public const string DELAY_TRYTIMES = "TryTimes";

        //account
        public const string ACCOUNT_ACCOUNTS = "Accounts";
        public const string ACCOUNT_ACCOUNT = "Account";
        public const string ACCOUNT_EMAIL = "Email";
        public const string ACCOUNT_PASSWORD = "Password";
        public const string ACCOUNT_USERNAME = "UserName";
        public const string ACCOUNT_USERID = "UserId";
        public const string ACCOUNT_GENDER = "Gender";

        //park        
        public const string PARK_PARKWHITE = "ParkWhite";
        public const string PARK_PARKBLACK = "ParkBlack";
        public const string PARK_POSTLIST = "PostList";
        public const string PARK_POSTALL = "PostAll";

        //bite
        public const string BITE_BITEWHITE = "BiteWhite";
        public const string BITE_BITEBLACK = "BiteBlack";
        public const string BITE_RECOVERWHITE = "RecoverWhite";
        public const string BITE_RECOVERBLACK = "RecoverBlack";
        public const string BITE_BITEALL = "BiteAll";
        public const string BITE_STATUS_NOROOM = "NoRoom";
        public const string BITE_STATUS_ISRECOVERING = "IsRecovering";
        public const string BITE_PROTECTID = "ProtectId";

        //slave
        public const string SLAVE_BUYWHITE = "BuyWhite";
        public const string SLAVE_BUYBLACK = "BuyBlack";

        //house
        public const string HOUSE_STAYWHITE = "StayWhite";
        public const string HOUSE_STAYBLACK = "StayBlack";
        public const string HOUSE_ROBWHITE = "RobWhite";
        public const string HOUSE_ROBBLACK = "RobBlack";
        
        //garden
        public const string GARDEN_STEALWHITE = "StealWhite";
        public const string GARDEN_STEALBLACK = "StealBlack";
        public const string GARDEN_STEALALL = "StealAll";
        public const string GARDEN_FARMWHITE = "FarmWhite";
        public const string GARDEN_FARMBLACK = "FarmBlack";
        public const string GARDEN_FARMALL = "FarmAll";
        public const string GARDEN_PRESENTID = "PresentId";

        //ranch
        public const string RANCH_HELPRANCHWHITE = "HelpRanchWhite";
        public const string RANCH_HELPRANCHBLACK = "HelpRanchBlack";
        public const string RANCH_HELPRANCHALL = "HelpRanchAll";
        public const string RANCH_STEALPRODUCTWHITE = "StealProductWhite";
        public const string RANCH_STEALPRODUCTBLACK = "StealProductBlack";
        public const string RANCH_STEALPRODUCTALL = "StealProductAll";
        public const string RANCH_PRESENTPRODUCTID = "PresentProductId";

        //fish
        public const string FISH_FISHINGWHITE = "FishingWhite";
        public const string FISH_FISHINGBLACK = "FishingBlack";
        public const string FISH_FISHINGALL = "FishingAll";
        public const string FISH_HELPFISHWHITE = "HelpFishWhite";
        public const string FISH_HELPFISHBLACK = "HelpFishBlack";
        public const string FISH_HELPFISHALL = "HelpFishAll";
        public const string FISH_PRESENTFISHID = "PresentFishId";
        
        //cafe
        public const string CAFE_HIREWHITE = "HireWhite";
        public const string CAFE_HIREBLACK = "HireBlack";
        public const string CAFE_HIREALL = "HireAll";
        public const string CAFE_PURCHASEWHITE = "PurchaseWhite";
        public const string CAFE_PURCHASEBLACK = "PurchaseBlack";
        public const string CAFE_PURCHASEALL = "PurchaseAll";
        public const string CAFE_PRESENTFOODID = "PresentFoodId";

        //garden
        public const string GARDEN_ROOT = "conf";
        public const string GARDEN_ACCOUNT = "account";
        public const string GARDEN_RANK = "rank";
        public const string GARDEN_RANKTIP = "ranktip";
        public const string GARDEN_NAME = "name";
        public const string GARDEN_CASHTIP = "cashtip";
        public const string GARDEN_TCHARMS = "tcharms";
        public const string GARDEN_CAREURL = "careurl";
        public const string GARDEN_GARDEN = "garden";
        public const string GARDEN_ITEM = "item";
        public const string GARDEN_WATER = "water";
        public const string GARDEN_FARMNUM = "farmnum";
        public const string GARDEN_VERMIN = "vermin";
        public const string GARDEN_CROPSID = "cropsid";
        public const string GARDEN_FUID = "fuid";
        public const string GARDEN_STATUS = "status";
        public const string GARDEN_GRASS = "grass";
        public const string GARDEN_SHARED = "shared";
        public const string GARDEN_PIC = "pic";
        public const string GARDEN_FRUITPIC = "fruitpic";
        public const string GARDEN_PICWIDTH = "picwidth";
        public const string GARDEN_PICHEIGHT = "picheight";
        public const string GARDEN_CROPSSTATUS = "cropsstatus";
        public const string GARDEN_GROW = "grow";
        public const string GARDEN_TOTALGROW = "totalgrow";
        public const string GARDEN_FRUITNUM = "fruitnum";
        public const string GARDEN_SEEDID = "seedid";
        public const string GARDEN_CROPS = "crops";
        public const string GARDEN_FARM = "farm";
        public const string GARDEN_SEED_ROOT = "data";
        public const string GARDEN_SEED_TOTALPAGE = "totalpage";
        public const string GARDEN_SEED_SEED = "seed";
        public const string GARDEN_SEED_ITEM = "item";
        public const string GARDEN_SEED_NAME = "name";
        public const string GARDEN_SEED_SEEDID = "seedid";
        public const string GARDEN_SEED_PRICE = "price";
        public const string GARDEN_SEED_SELLPRICE = "sellprice";
        public const string GARDEN_SEED_FRUIT = "fruit";

        public const string GARDEN_STEAL_ROOT = "data";
        public const string GARDEN_STEAL_ANTI = "anti";
        public const string GARDEN_STEAL_LEFTNUM = "leftnum";
        public const string GARDEN_STEAL_STEALNUM = "stealnum";
        public const string GARDEN_STEAL_NUM = "num";
        public const string GARDEN_STEAL_SEEDNAME = "seedname";
        public const string GARDEN_STEAL_FRUITPIC = "fruitpic";
        public const string GARDEN_STEAL_RET = "ret";

        public const string GARDEN_SELL_ROOT = "data";
        public const string GARDEN_SELL_RET = "ret";
        public const string GARDEN_SELL_GOODSNAME = "goodsname";
        public const string GARDEN_SELL_TOTALPRICE = "totalprice";
        public const string GARDEN_SELL_NUM = "num";
        public const string GARDEN_SELL_PIC = "pic";
        public const string GARDEN_SELL_ALL = "all";

        public const string GARDEN_PRESENT_ROOT = "data";
        public const string GARDEN_PRESENT_RET = "ret";
        public const string GARDEN_PRESENT_NAME = "name";
        public const string GARDEN_PRESENT_FRUITPIC = "fruitpic";
        public const string GARDEN_PRESENT_FRUITMINPRICE = "fruit_minprice";
        public const string GARDEN_PRESENT_FRUITMAXPRICE = "fruit_maxprice";
        public const string GARDEN_PRESENT_FRUITNUM = "fruitnum";
        public const string GARDEN_PRESENT_SELFNUM = "selfnum";
        public const string GARDEN_PRESENT_BPRESENT = "bpresent";
        public const string GARDEN_PRESENT_FRUITPRICE = "fruitprice";

        public const string GARDEN_RANK_DATA = "data";
        public const string GARDEN_RANK_SEED = "seed";
        public const string GARDEN_RANK_ITEM = "item";
        public const string GARDEN_RANK_RANK = "rank";
        public const string GARDEN_RANK_SEEDID = "seedid";
        public const string GARDEN_RANK_SEEDNAME = "seedname";

        public const string GARDEN_SUCCESS = "succ";

        //ranch
        public const string RANCH_ROOT = "conf";
        public const string RANCH_ACCOUNT = "account";
        public const string RANCH_RANK = "rank";
        public const string RANCH_RANKTIP = "ranktip";
        public const string RANCH_NAME = "name";
        public const string RANCH_CASHTIP = "cashtip";
        public const string RANCH_TCHARMS = "tcharms";
        public const string RANCH_WATER = "water";
        public const string RANCH_WATERTIPS = "watertips";
        public const string RANCH_GRASS = "grass";
        public const string RANCH_GRASSTIPS = "grasstips";
        public const string RANCH_ANIMALS = "animals";

        //ranch product
        public const string RANCH_PRODUCT2 = "product2";
        //public const string RANCH_PRODUCT2_ITEM = "item";
        //public const string RANCH_PRODUCT2_UID = "uid";
        //public const string RANCH_PRODUCT2_AID = "aid";
        //public const string RANCH_PRODUCT2_TYPE = "type";
        //public const string RANCH_PRODUCT2_NUM = "num";
        //public const string RANCH_PRODUCT2_STEALNUM = "stealnum";
        //public const string RANCH_PRODUCT2_MTIME = "mtime";
        //public const string RANCH_PRODUCT2_PPIC = "ppic";
        //public const string RANCH_PRODUCT2_TNAME = "tname";
        //public const string RANCH_PRODUCT2_SKEY = "skey";
        //public const string RANCH_PRODUCT2_PNAME = "pname";
        //public const string RANCH_PRODUCT2_TIPS = "tips";

        //ranch foods
        public const string RANCH_FOODS = "foods";

        //ranch breedable list
        public const string RANCH_BREED_ROOT = "data";
        public const string RANCH_BREED_BREED = "breed";

        //ranch breed card list
        public const string RANCH_BREEDCARD_ROOT = "data";

        //ranch food
        public const string RANCH_FOOD_ROOT = "data";
        public const string RANCH_FOOD_TOTALPAGE = "totalpage";
        public const string RANCH_FOOD_FOOD = "food";
        public const string RANCH_FOOD_ITEM = "item";
        public const string RANCH_FOOD_NAME = "name";
        public const string RANCH_FOOD_SEEDID = "seedid";
        public const string RANCH_FOOD_NUM = "num";

        //ranch feed
        public const string RANCH_FEED_ROOT = "data";
        public const string RANCH_FEED_RET = "ret";
        public const string RANCH_FEED_GRASSTIPS = "grasstips";
        public const string RANCH_FEED_GRASS = "grass";
        public const string RANCH_FEED_ANIMALSTIPS = "animalstips";

        //ranch water
        public const string RANCH_WATER_ROOT = "data";
        public const string RANCH_WATER_RET = "ret";
        public const string RANCH_WATER_WATERTIPS = "watertips";
        public const string RANCH_WATER_TIPS = "tips";

        //ranch steal
        public const string RANCH_STEAL_ROOT = "data";
        public const string RANCH_STEAL_PTYPE = "ptype";
        public const string RANCH_STEAL_SKEY = "skey";
        public const string RANCH_STEAL_ACTION = "action";
        public const string RANCH_STEAL_RET = "ret";
        public const string RANCH_STEAL_NUM = "num";        
        public const string RANCH_STEAL_REASON = "reason";

        //ranch make product
        public const string RANCH_MAKEPRODUCT_ROOT = "data";
        public const string RANCH_MAKEPRODUCT_ACTION = "action";
        public const string RANCH_MAKEPRODUCT_RET = "ret";
        public const string RANCH_MAKEPRODUCT_REASON = "reason";
        public const string RANCH_MAKEPRODUCT_SKEY = "skey";
        public const string RANCH_MAKEPRODUCT_PTIPS = "ptips";
        public const string RANCH_MAKEPRODUCT_BPRODUCT = "bproduct";
        public const string RANCH_MAKEPRODUCT_LEFTPTIME = "leftptime";
        public const string RANCH_MAKEPRODUCT_TIPS = "tips";
        public const string RANCH_MAKEPRODUCT_PIC = "pic";
        public const string RANCH_MAKEPRODUCT_TNAME = "tname";

        //ranch calf in shop
        public const string RANCH_CALF_ROOT = "data";
        public const string RANCH_CALF_ANIMALS = "animals";
        public const string RANCH_CALF_ITEM = "item";
        public const string RANCH_CALF_NAME = "name";
        public const string RANCH_CALF_AID = "aid";
        public const string RANCH_CALF_PRICE = "price";

        //ranch breed animal
        public const string RANCH_BREEDANIMAL_ROOT = "data";
        public const string RANCH_BREEDANIMAL_RET = "ret";
        public const string RANCH_BREEDANIMAL_SUCCTIPS = "succtips";
        public const string RANCH_BREEDANIMAL_BPRODUCT = "bproduct";
        public const string RANCH_BREEDANIMAL_LEFTPTIME = "leftptime";
        public const string RANCH_BREEDANIMAL_TIPS = "tips";
        public const string RANCH_BREEDANIMAL_SKEY = "skey";
        public const string RANCH_BREEDANIMAL_PIC = "pic";
        public const string RANCH_BREEDANIMAL_TNAME = "tname";
        public const string RANCH_BREEDANIMAL_ANIMALSID = "animalsid";

        //ranch warehouse
        public const string GARDEN_WAREHOUSE_ROOT = "data";
        public const string GARDEN_WAREHOUSE_FRUIT = "fruit";

        //task        
        public const string TASK_TASKS = "Tasks";
        public const string TASK_TASK = "Task";
        public const string TASK_TASKID = "TaskId";
        public const string TASK_TASKNAME = "TaskName";
        public const string TASK_GROUPNAME = "GroupName";

        public const string TASK_NODE_EXECUTINGMODE = "ExecutingMode";
        public const string GAME_PARK = "Park";
        public const string GAME_BITE = "Bite";
        public const string GAME_SLAVE = "Slave";
        public const string GAME_HOUSE = "House";
        public const string GAME_GARDEN = "Garden";
        public const string GAME_RANCH = "Ranch";
        public const string GAME_FISH = "Fish";
        public const string GAME_RICH = "Rich";
        public const string GAME_CAFE = "Cafe";
        public const string TASK_NODE_MISCELLANEOUS = "Miscellaneous";

        public const string TASK_EXECUTINGMODE_RUNMODE = "RunMode";
        public const string TASK_EXECUTINGMODE_RUNINLOOP = "RunInLoop";
        public const string TASK_EXECUTINGMODE_RUNINTIME = "RunInTime";
        public const string TASK_RUNINLOOP_ROUNDTIME = "RoundTime";
        public const string TASK_RUNINLOOP_FOBIDDEN = "Fobidden";
        public const string TASK_RUNINLOOP_FOBIDDENSTART = "ForbiddenStart";
        public const string TASK_RUNINLOOP_FOBIDDENEND = "ForbiddenEnd";
        public const string TASK_RUNINTIME_STARTTIMES = "StartTimes";
        public const string TASK_RUNINTIME_DATETIME = "DateTime";

        public const string TASK_PARK_EXECUTEPARK = "ExecutePark";
        public const string TASK_PARK_PARKMYCARS = "ParkMyCars";
        public const string TASK_PARK_POSTOTHERSCARS = "PostOthersCars";
        public const string TASK_PARK_BUYNEWCARS = "BuyNewCars";
        public const string TASK_PARK_UPGRADEGARAGE = "UpgradeGarage";
        public const string TASK_PARK_MAXCARS = "MaxCars";
        public const string TASK_PARK_JOINMATCH = "JoinMatch";
        public const string TASK_PARK_ORIGINATEMATCH = "OriginateMatch";
        public const string TASK_PARK_ORIGINATEMATCHID = "OriginateMatchId";
        public const string TASK_PARK_ORIGINATETEAMNUM = "OriginateTeamNum";
        public const string TASK_PARK_STARTCAR = "StartCar";
        public const string TASK_PARK_CHEERUP = "CheerUp";
        public const string TASK_PARK_STARTCARTIME = "StartCarTime";

        public const string TASK_BITE_EXECUTEBITE = "ExecuteBite";
        public const string TASK_BITE_APPROVERECOVERY = "ApproveRecovery";
        public const string TASK_BITE_BITEOTHERS = "BiteOthers";
        public const string TASK_BITE_AUTORECOVER = "AutoRecover";
        public const string TASK_BITE_SENDREMINDMESSAGE = "SendRemindMessage";
        public const string TASK_BITE_MESSAGECONTENT = "MessageContent";
        public const string TASK_BITE_PROTECTFRIEND = "ProtectFriend";

        public const string TASK_SLAVE_EXECUTESLAVE = "ExecuteSlave";
        public const string TASK_SLAVE_MAXSLAVES = "MaxSlaves";
        public const string TASK_SLAVE_NICKNAME = "NickName";
        public const string TASK_SLAVE_BUYSLAVE = "BuySlave";
        public const string TASK_SLAVE_BUYLOWPRICESLAVE = "BuyLowPriceSlave";
        public const string TASK_SLAVE_FAWNMASTER = "FawnMaster";
        public const string TASK_SLAVE_PROPITIATESLAVE = "PropitiateSlave";
        public const string TASK_SLAVE_AFFLICTSLAVE = "AfflictSlave";
        public const string TASK_SLAVE_RELEASESLAVE = "ReleaseSlave";

        public const string TASK_HOUSE_EXECUTEHOUSE = "ExecuteHouse";
        public const string TASK_HOUSE_DOJOB = "DoJob";
        public const string TASK_HOUSE_STAYHOUSE = "StayHouse";
        public const string TASK_HOUSE_ROBFRIENDS = "RobFriends";
        public const string TASK_HOUSE_ROBFREEFRIENDS = "RobFreeFriends";
        public const string TASK_HOUSE_DRIVEFRIENDS = "DriveFriends";

        //task garden
        public const string TASK_GARDEN_EXECUTEGARDEN = "ExecuteGarden";
        public const string TASK_GARDEN_FARMSELF = "FarmSelf";
        public const string TASK_GARDEN_EXPENSIVEFARMSELF = "ExpensiveFarmSelf";
        public const string TASK_GARDEN_CUSTOMFARMSELF = "CustomFarmSelf";
        public const string TASK_GARDEN_FARMSHARED = "FarmShared";
        public const string TASK_GARDEN_EXPENSIVEFARMSHARED = "ExpensiveFarmShared";
        public const string TASK_GARDEN_CUSTOMFARMSHARED = "CustomFarmShared";        
        public const string TASK_GARDEN_HARVESTFRUIT = "HarvestFruit";
        public const string TASK_GARDEN_BUYSEED = "BuySeed";
        public const string TASK_GARDEN_BUYSEEDCOUNT = "BuySeedCount";
        public const string TASK_GARDEN_HELPOTHERS = "HelpOthers";        
        public const string TASK_GARDEN_STEALFRUIT = "StealFruit";
        public const string TASK_GARDEN_STEALPRICE = "StealPrice";
        public const string TASK_GARDEN_PRESENTFRUIT = "PresentFruit";
        public const string TASK_GARDEN_PRESENTFRUITBYPRICE = "PresentFruitByPrice";
        public const string TASK_GARDEN_PRESENTFRUITCHECKVALUE = "PresentFruitCheckValue";
        public const string TASK_GARDEN_PRESENTFRUITVALUE = "PresentFruitValue";
        public const string TASK_GARDEN_PRESENTFRUITID = "PresentFruitId";
        public const string TASK_GARDEN_PRESENTFRUITCHECKNUM = "PresentFruitCheckNum";
        public const string TASK_GARDEN_PRESENTFRUITNUM = "PresentFruitNum";
        public const string TASK_GARDEN_SELLFRUIT = "SellFruit";
        public const string TASK_GARDEN_LOWCASH = "LowCash";
        public const string TASK_GARDEN_LOWCASHLIMIT = "LowCashLimit";
        public const string TASK_GARDEN_SELLALLFRUIT = "SellAllFruit";
        public const string TASK_GARDEN_MAXSELLLIMIT = "MaxSellLimit";
        public const string TASK_GARDEN_SELLFORBIDDENNFRUITSLIST = "SellForbiddennFruitsList";
        public const string TASK_GARDEN_SOWMYSEEDSFIRST = "SowMySeedsFirst";
        public const string TASK_GARDEN_STEALUNKNOWFRUIT = "StealUnknowFruit";
        public const string TASK_GARDEN_STEALFORBIDDENFRUITSLIST = "StealForbiddenFruitsList";
        public const string TASK_GARDEN_SEEDID = "SeedId";

        //task ranch
        public const string TASK_RANCH_EXECUTERANCH = "ExecuteRanch";
        public const string TASK_RANCH_HARVESTPRODUCT = "HarvestProduct";
        public const string TASK_RANCH_HARVESTANIMAL = "HarvestAnimal";
        public const string TASK_RANCH_ADDWATER = "AddWater";
        public const string TASK_RANCH_HELPADDWATER = "HelpAddWater";
        public const string TASK_RANCH_ADDGRASS = "AddGrass";
        public const string TASK_RANCH_HELPADDGRASS = "HelpAddGrass";
        public const string TASK_RANCH_BUYCALF = "BuyCalf";
        public const string TASK_RANCH_BUYCALFBYPRICE = "BuyCalfByPrice";
        public const string TASK_RANCH_BUYCALFCUSTOM = "BuyCalfCustom";
        public const string TASK_RANCH_STEALPRODUCT = "StealProduct";
        public const string TASK_RANCH_MAKEPRODUCT = "MakeProduct";
        public const string TASK_RANCH_HELPMAKEPRODUCT = "HelpMakeProduct";
        public const string TASK_RANCH_BREEDANIMAL = "BreedAnimal";
        public const string TASK_RANCH_FOODNUM = "FoodNum";
        public const string TASK_RANCH_PRESENTPRODUCT = "PresentProduct";
        public const string TASK_RANCH_PRESENTPRODUCTBYPRICE = "PresentProductByPrice";
        public const string TASK_RANCH_PRESENTPRODUCTCHECKVALUE = "PresentProductCheckValue";
        public const string TASK_RANCH_PRESENTPRODUCTVALUE = "PresentProductValue";
        public const string TASK_RANCH_PRESENTPRODUCTAID = "PresentProductAId";
        public const string TASK_RANCH_PRESENTPRODUCTTYPE = "PresentProductType";
        public const string TASK_RANCH_PRESENTPRODUCTCHECKNUM = "PresentProductCheckNum";
        public const string TASK_RANCH_PRESENTPRODUCTNUM = "PresentProductNum";
        public const string TASK_RANCH_SELLPRODUCT = "SellProduct";
        public const string TASK_RANCH_SELLPRODUCTLOWCASH = "SellProductLowCash";
        public const string TASK_RANCH_SELLPRODUCTLOWCASHLIMIT = "SellProductLowCashLimit";
        public const string TASK_RANCH_SELLALLPRODUCTS = "SellAllProducts";
        public const string TASK_RANCH_SELLPRODUCTMAXLIMIT = "SellProductMaxLimit";
        public const string TASK_RANCH_SELLPRODUCTFORBIDDENLIST = "SellProductForbiddenList";
        public const string TASK_RANCH_ADDCARROT = "AddCarrot";
        public const string TASK_RANCH_HELPADDCARROT = "HelpAddCarrot";
        public const string TASK_RANCH_CARROTNUM = "CarrotNum";
        public const string TASK_RANCH_ADDBAMBOO = "AddBamboo";
        public const string TASK_RANCH_HELPADDBAMBOO = "HelpAddBamboo";
        public const string TASK_RANCH_BAMBOONUM = "BambooNum";
        public const string TASK_RANCH_PRODUCTAID = "ProductAId";
        public const string TASK_RANCH_PRODUCTTYPE = "ProductType";

        //task fish
        public const string TASK_FISH_EXECUTEFISH = "ExecuteFish";
        public const string TASK_FISH_SHAKE = "Shake";
        public const string TASK_FISH_TREATFISH = "TreatFish";
        public const string TASK_FISH_UPDATEFISHPOND = "UpdateFishPond";
        public const string TASK_FISH_BANGKEJING = "BangKeJing";
        public const string TASK_FISH_BUYFISH = "BuyFish";
        public const string TASK_FISH_MAXFISHES = "MaxFishes";
        public const string TASK_FISH_BUYFISHBYRANK = "BuyFishByRank";
        public const string TASK_FISH_BUYFISHFISHID = "BuyFishFishId";
        public const string TASK_FISH_FISHING = "Fishing";
        public const string TASK_FISH_BUYUPDATETACKLE = "BuyUpdateTackle";
        public const string TASK_FISH_MAXTACKLES = "MaxTackles";
        public const string TASK_FISH_HARVESTFISH = "HarvestFish";
        public const string TASK_FISH_NETSELFFISH = "NetSelfFish";
        public const string TASK_FISH_NETSELFFISHCHEAP = "NetSelfFishCheap";
        public const string TASK_FISH_NETSELFFISHMATURE = "NetSelfFishMature";
        public const string TASK_FISH_HELPFISH = "HelpFish";
        public const string TASK_FISH_PRESENTFISH = "PresentFish";
        public const string TASK_FISH_PRESENTFISHCHEAP = "PresentFishCheap";
        public const string TASK_FISH_PRESENTFISHCHECKVALUE = "PresentFishCheckValue";
        public const string TASK_FISH_PRESENTFISHVALUE = "PresentFishValue";
        public const string TASK_FISH_PRESENTFISHFORBIDDENLIST = "PresentFishForbiddenList";
        public const string TASK_FISH_SELLFISH = "SellFish";
        public const string TASK_FISH_SELLFISHLOWCASH = "SellFishLowCash";
        public const string TASK_FISH_SELLFISHLOWCASHLIMIT = "SellFishLowCashLimit";
        public const string TASK_FISH_SELLFISHCHECKVALUE = "SellFishCheckValue";
        public const string TASK_FISH_SELLFISHVALUE = "SellFishValue";
        public const string TASK_FISH_SELLALLFISH = "SellAllFish";
        public const string TASK_FISH_SELLFISHMAXLIMIT = "SellFishMaxLimit";
        public const string TASK_FISH_SELLFISHFORBIDDENLIST = "SellFishForbiddenList";
        public const string TASK_FISH_FISHID = "FishId";

        //task rich
        public const string TASK_RICH_EXECUTERICH = "ExecuteRich";
        public const string TASK_RICH_SELLASSET = "SellAsset";
        public const string TASK_RICH_BUYASSET = "BuyAsset";
        public const string TASK_RICH_BUYASSETCHEAP = "BuyAssetCheap";
        public const string TASK_RICH_GIVEUPIFRATIO = "GiveUpIfRatio";
        public const string TASK_RICH_GIVEUPRATIO = "GiveUpRatio";
        public const string TASK_RICH_GIVEUPIFMINIMUM = "GiveUpIfMinimum";
        public const string TASK_RICH_GIVEUPMINIMUM = "GiveUpMinimum";
        public const string TASK_RICH_GIVEUPIFMYASSET = "GiveUpIfMyAsset";
        public const string TASK_RICH_GIVEUPASSETCOUNT = "GiveUpAssetCount";        
        public const string TASK_RICH_ADVANCEDPURCHASE = "AdvancedPurchase";
        public const string TASK_RICH_BUYASSETSLIST = "BuyAssetsList";
        public const string TASK_RICH_ASSETID = "AssetId";

        //task cafe
        public const string TASK_CAFE_EXECUTECAFE = "ExecuteCafe";
        public const string TASK_CAFE_BOXCLEAN = "BoxClean";
        public const string TASK_CAFE_COOK = "Cook";
        public const string TASK_CAFE_COOKTOMATOFIRST = "CookTomatoFirst";
        public const string TASK_CAFE_COOKMEDLARFIRST = "CookMedlarFirst";
        public const string TASK_CAFE_COOKCRABFIRST = "CookCrabFirst";
        public const string TASK_CAFE_COOKPINEAPPLEFIRST = "CookPineappleFirst";
        public const string TASK_CAFE_COOKDISHID = "CookDishId";
        public const string TASK_CAFE_COOKLOWCASH = "CookLowCash";
        public const string TASK_CAFE_COOKLOWCASHLIMIT = "CookLowCashLimit";
        public const string TASK_CAFE_HIRE = "Hire";
        public const string TASK_CAFE_MAXEMPLOYEES = "MaxEmployees";
        public const string TASK_CAFE_HELPFRIEND = "HelpFriend";
        public const string TASK_CAFE_PRESENTFOOD = "PresentFood";
        public const string TASK_CAFE_PRESENTFORBIDDENFOODLIST = "PresentForbiddenFoodList";
        public const string TASK_CAFE_PRESENTFOODBYCOUNT = "PresentFoodByCount";
        public const string TASK_CAFE_PRESENTFOODDISHID = "PresentFoodDishId";
        public const string TASK_CAFE_PRESENTFOODMESSAGE = "PresentFoodMessage";
        public const string TASK_CAFE_PRESENTFOODRATIO = "PresentFoodRatio";
        public const string TASK_CAFE_PRESENTLOWCASH = "PresentLowCash";
        public const string TASK_CAFE_PRESENTLOWCASHLIMIT = "PresentLowCashLimit";
        public const string TASK_CAFE_PRESENTFOODLOWCOUNT = "PresentFoodLowCount";
        public const string TASK_CAFE_PRESENTFOODLOWCOUNTLIMIT = "PresentFoodLowCountLimit";
        public const string TASK_CAFE_PURCHASEFOOD = "PurchaseFood";
        public const string TASK_CAFE_PURCHASEFOODBYREFPRICE = "PurchaseFoodByRefPrice";
        public const string TASK_CAFE_SELLFOOD = "SellFood";
        public const string TASK_CAFE_SELLFOODBYREFPRICE = "SellFoodByRefPrice";

        public const string TASK_MISCELLANEOUS_GROUP = "Group";
        public const string TASK_MISCELLANEOUS_SENDLOG = "SendLog";
        public const string TASK_MISCELLANEOUS_RECEIVEREMAIL = "ReceiverEmail";
        public const string TASK_MISCELLANEOUS_WRITELOGTOFILE = "WriteLogToFile";
        public const string TASK_MISCELLANEOUS_SKIPVALIDATION = "SkipValidation";

        //message prefix
        public const string MSG_LOGIN = "[登录]";
        public const string MSG_INITIALIZE = "[初始化]";
        public const string MSG_MYKAIXIN = "[我的开心网]";
        public const string MSG_PARKING = "[争车位]";
        public const string MSG_BITING = "[咬人]";
        public const string MSG_SLAVE = "[朋友买卖]";
        public const string MSG_HOUSE = "[买房子]";
        public const string MSG_GARDEN = "[花园]";
        public const string MSG_RANCH = "[牧场]";
        public const string MSG_FISH = "[钓鱼]";
        public const string MSG_RICH = "[超级大亨]";
        public const string MSG_CAFE = "[开心餐厅]";
        public const string MSG_TASK = "[任务]";
        public const string MSG_ADDFRIENDS = "[互加好友]";
        public const string MSG_SENDMESSAGE = "[群发消息]";
        public const string MSG_BUILDTEAM = "[组建车队]";
        public const string MSG_BUYCARDS = "[购买道具]";
        public const string MSG_TOOLPARK = "[争车位工具]";
        public const string MSG_MAINTAINCONTACT = "[维护联系人]";
        public const string MSG_UPDATEDATA = "[更新数据]";
        public const string MSG_SYSTEMERROR = "系统错误";
        public const string MSG_ERROR = "错误";

        //status
        public const string STATUS_FORMLOAD = "FormLoad";
        public const string STATUS_CONNECTING = "Connecting";
        public const string STATUS_LOGINFAILED = "LoginFailed";
        public const string STATUS_AFTERLOGIN = "AfterLogin";
        public const string STATUS_PROCESSING = "Processing";
        public const string STATUS_NORMAL = "Normal";
        public const string STATUS_TASKSTARTED = "TaskStarted";
        public const string STATUS_TASKSTOPPED = "TaskStopped";
        public const string STATUS_SUCCESS = "Ok";
        public const string STATUS_FAIL = "Fail";

        //cars in the market
        public const string CARSINMARKET_CARS = "Cars";
        public const string CARSINMARKET_CAR = "Car";
        public const string CARSINMARKET_CARID = "CarId";
        public const string CARSINMARKET_CARNAME = "CarName";
        public const string CARSINMARKET_CARPRICE = "CarPrice";

        //Smtp
        public const string SMTP_SMTP = "Smtp";
        public const string SMTP_HOST = "SmtpHost";
        public const string SMTP_PORT = "SmtpPort";
        public const string SMTP_SENDERNAME = "SenderName";
        public const string SMTP_SENDEREMAIL = "SenderEmail";
        public const string SMTP_USERNAME = "Username";
        public const string SMTP_PASSWORD = "Password";

        //tools
        public const string TOOL_ADDFRIENDS = "互加好友";
        public const string TOOL_SENDMESSAGE = "群发消息";
        public const string TOOL_BUILDTEAM = "组建车队";
        public const string TOOL_BUYCARDS = "购买道具";
        public const string TOOL_UPGRADEGARAGE = "争车位工具";
        public const string TOOL_MAINTAINCONTACT = "维护联系人";
        public const string TOOL_UPDATEDATA = "更新数据";
        public const string TOOL_CHINESSWORD = "汉字<->拼音";

        // Char
        public const string CHAR_SLASH = "/";
        public const string CHAR_DOUBLEBACKSLASH = "\\";

        public const int DELAY_5SECONDS = 5;
        public const int DELAY_4SECONDS = 4;
        public const int DELAY_3SECONDS = 3;
        public const int DELAY_2SECONDS = 2;
        public const int DELAY_1SECONDS = 1;

    }
}
