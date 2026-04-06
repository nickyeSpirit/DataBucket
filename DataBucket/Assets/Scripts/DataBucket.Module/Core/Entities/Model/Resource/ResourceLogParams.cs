using System;

[Serializable]
public class ResourceLogParams
{
    public string resourcesDetails;
    public string placement;
    public string placementDetail;
    public string reason;

    #region Earn

    public static ResourceLogParams EarnLeague()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelGameOver,
            placementDetail = DataBucketConstant.ResourceAnalytics.PlacementDetail.SubmitScoreLeagueMode,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnStoryMode()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelGameOver,
            placementDetail = DataBucketConstant.ResourceAnalytics.PlacementDetail.SubmitStoryMode,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnBossRaid()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelGameOver,
            placementDetail = DataBucketConstant.ResourceAnalytics.PlacementDetail.SubmitBossRaidMode,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnDailyLogin(int idxPlacement)
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelDailyLogin,
            placementDetail = $"day-{idxPlacement + 1}",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnDailyLoginAds(int idxPlacement)
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelDailyLogin,
            placementDetail = $"day-{idxPlacement + 1}",
            reason = DataBucketConstant.ResourceAnalytics.Reason.WatchAds,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnDailyPremium()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelDailyPremium,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Purchase,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnGiftBoxOpenForEndlessMode()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelGiftBox,
            placementDetail = DataBucketConstant.ResourceAnalytics.PlacementDetail.FinishLeagueMode,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnVideoAdsRewardOffer()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelGameOver,
            placementDetail = DataBucketConstant.ResourceAnalytics.PlacementDetail.VideoAdsRewardOffer,
            reason = DataBucketConstant.ResourceAnalytics.Reason.WatchAds,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnLeagueResult()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelLeague,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnLuckyWheel()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelLuckyWheel,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnLuckyWheelAds()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelLuckyWheel,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.WatchAds,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnConsumable()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelMapBuyConsumable,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnIdle()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelMap,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnIAP(string productId)
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelIAP,
            placementDetail = productId,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Purchase,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams EarnHomeWeapon()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.HomeWeapon,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Reward,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams CashShopChest()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelCashShopChest,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null"
        };
    }

    #endregion

    #region Sink

    public static ResourceLogParams SpendUpgrade(string placementDetail)
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelUpgrade,
            placementDetail = placementDetail,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null"
        };
    }
    
    public static ResourceLogParams SpendLuckyWheel()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelLuckyWheel,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null"
        };
    }
    
    public static ResourceLogParams SpendMapBuyConsumable()
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelMapBuyConsumable,
            placementDetail = "null",
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null"
        };
    }

    public static ResourceLogParams PromptForResources(string placementDetail)
    {
        return new ResourceLogParams
        {
            placement = DataBucketConstant.Placement.PanelPromptForResource,
            placementDetail = placementDetail,
            reason = DataBucketConstant.ResourceAnalytics.Reason.Exchange,
            resourcesDetails = "null",
        };
    }

    #endregion
}