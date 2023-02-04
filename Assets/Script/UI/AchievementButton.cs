using GameJam.GameData;
using GameJam.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class AchievementButton : Button
{
    [SerializeField]
    protected Image displayItemImage;

    [SerializeField]
    protected TMP_Text displayItemDetail;

    protected Sprite defaultSprite;

    protected GameDataManager gameDataManager;

    protected bool isHandleHasValue;

    protected AsyncOperationHandle<SpriteAtlas> handle;

    protected Coroutine loadingSpriteCoroutine;

    private AchievementModel achievementInfo; /*====>  put some item info here See in Game Data*/
    public AchievementModel AchievementInfo => achievementInfo;

    protected virtual void OnDestroy()
    {
        ClearDisplayItemInfo();
        this.onClick.RemoveAllListeners();
    }
    public void ClearDisplayItemInfo()
    {
        ClearSpriteHandle();

        if (loadingSpriteCoroutine != null)
        {
            StopCoroutine(loadingSpriteCoroutine);
            loadingSpriteCoroutine = null;
        }

        achievementInfo = null;
    }

    private void ClearSpriteHandle()
    {
        if (!isHandleHasValue) return;

        displayItemImage.sprite = defaultSprite;

        //reset information here

        Addressables.Release(handle);

        handle = default;
        isHandleHasValue = false;
    }

    public void SetDisplayItem(string itemId)
    {
        //var exist = gameDataManager.TryGetEmojiInfo(emojiId, out var emojiInfo);   ============ use try get to get itemInfo

        if (/*!exist*/ false)
        {
            Debug.LogError("Fail to find info for a itemId: " + itemId);
            displayItemImage.gameObject.SetActive(false);
            displayItemDetail.gameObject.SetActive(false);
            return;
        }

        displayItemImage.gameObject.SetActive(true);
        displayItemDetail.gameObject.SetActive(false);

        SetAchievementInfo(achievementInfo);
    }

    private void SetAchievementInfo(AchievementModel achievementInfo)
    {
        ClearDisplayItemInfo();

        this.achievementInfo = achievementInfo;

        loadingSpriteCoroutine = StartCoroutine(LoadingSpriteRoutine());
    }

    private IEnumerator LoadingSpriteRoutine()
    {
        handle = Addressables.LoadAssetAsync<SpriteAtlas>(achievementInfo.AtlasIcon);

        isHandleHasValue = true;

        yield return new WaitUntil(() => handle.IsDone);

        var atlas = handle.Result;

        var sprite = atlas.GetSprite(achievementInfo.IconName);

        displayItemImage.sprite = sprite;

        // Set other information here

        loadingSpriteCoroutine = null;
    }
}
