public static class DataBucketConstant
{
    public class EventName
    {
        public const string LevelStart = "level_start";
        public const string LevelEnter = "level_enter";
        public const string LevelEnd = "level_end";
        public const string LevelExit = "level_exit";
        public const string LevelReopen = "level_reopen";
        
        public const string ResourceEarn = "resource_earn";
        public const string ResourceSpend =  "resource_spend";
        
        public const string IAPShow = "iap_show";
        public const string IAPClick = "iap_click";
        public const string IAPPurchase = "iap_purchase";
        public const string IAPClose = "iap_close";
        
        public const string AdRequest = "ad_request";
        public const string AdImpression =  "ad_impression";
        public const string AdClick = "ad_click";
        public const string AdComplete = "ad_complete";
        
        public const string TutorialAction = "tutorial_action";
    }
    
    public class Placement
    {
        public const string PanelDailyLogin = "panel_daily_login_reward";
        public const string PanelDailyPremium = "panel_daily_premium_reward";
        public const string PanelGiftBox = "panel_gift_box";
        public const string PanelGameOver = "panel_game_over";
        public const string PanelLeague = "panel_league_result";
        public const string PanelLuckyWheel = "panel_lucky_wheel";
        public const string PanelMapBuyConsumable = "panel_map_buy_consumable";
        public const string PanelMap = "panel_map";
        public const string PanelIAP = "panel_iap";
        public const string HomeWeapon = "home_weapon";
        public const string PanelCashShopChest = "panel_cash_shop_chest";
        public const string PanelUpgrade = "panel_upgrade";
        public const string PanelPromptForResource = "panel_prompt_for_resource";
        public const string HomeShop = "home_shop";
    }
    
    public class ResourceAnalytics
    {
        public class PlacementDetail
        {
            public const string FinishLeagueMode = "finish_league_mode";
            public const string SubmitScoreLeagueMode = "submit_score_league_mode";
            public const string SubmitStoryMode = "submit_story_mode";
            public const string SubmitBossRaidMode = "submit_boss_raid_mode";
            public const string VideoAdsRewardOffer =  "video_ads_reward_offer";
        
            public const string UpgradeCardDetail = "upgrade_card_detail";
            public const string ConsumeCardDetail = "consume_card_detail";
            public const string UpgradeWeapon = "upgrade_weapon";
            public const string UpgradeBullet = "upgrade_bullet";
            public const string UpgradeTutorial = "upgrade_tutorial";
        }

        public class Reason
        {
            public const string Reward = "reward";
            public const string WatchAds = "watch_ads";
            public const string Exchange = "exchange";
            public const string Summon = "summon";
            public const string Purchase = "purchase";
            public const string Use = "use";
        }
    }

    public class IAPAnalytics
    {
        public enum ShowType
        {
            Shop, Pack,
        }

        public enum TriggerType
        {
            Popup, Click
        }
    }

    public class AdsAnalytics
    {
        public enum AdFormat
        {
            Native,
            Banner,
            Interstitial,
            VideoRewarded
        }

        public enum AdPlatform
        {
            Admob,
            Max,
            IronSource
        }
        
        public enum EndType
        {
            Quit,
            Done,
        }
    }

    public class Other
    {
        public enum ActionCategory
        {
            Click,
            UseBooster
        }
    }
}
