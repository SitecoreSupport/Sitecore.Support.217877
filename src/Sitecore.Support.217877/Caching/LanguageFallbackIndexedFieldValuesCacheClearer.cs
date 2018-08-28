namespace Sitecore.Support.Caching
{
  using Sitecore.Caching;
  using Sitecore.Data.Events;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using System;
  using System.Reflection;

  public class LanguageFallbackIndexedFieldValuesCacheClearer
  {
    public void ClearLanguageFallbackCache(object sender, EventArgs args)
    {
      try
      {
        Item item = null;

        if (args is ItemSavedRemoteEventArgs)
        {
          item = ((ItemSavedRemoteEventArgs)args).Item;
        }

        else if (args is ItemDeletedRemoteEventArgs)
        {
          item = ((ItemDeletedRemoteEventArgs)args).Item;
        }

        else if (args is VersionRemovedRemoteEventArgs)
        {
          item = ((VersionRemovedRemoteEventArgs)args).Item;
        }

        if (item != null && item.Database != null && item.Database.Caches != null && item.Database.Caches.FallbackFieldValuesCache != null)
        {
          LanguageFallbackIndexedFieldValuesCache cache = item.Database.Caches.FallbackFieldValuesCache as LanguageFallbackIndexedFieldValuesCache;
          MethodInfo ClearCacheMethod = typeof(LanguageFallbackIndexedFieldValuesCache).GetMethod("ClearCache", BindingFlags.NonPublic | BindingFlags.Instance);
          ClearCacheMethod.Invoke(cache, new object[] { item });
        }
      }
      catch
      {
        Log.Warn("There was an error while clearing LanguageFallbackIndexedFieldValuesCache", this);
      }
    }
  }
}