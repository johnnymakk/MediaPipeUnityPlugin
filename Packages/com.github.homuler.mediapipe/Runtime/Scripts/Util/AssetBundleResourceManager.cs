// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Mediapipe.Unity
{
  public class AssetBundleResourceManager : ResourceManager
  {
    private static readonly string _TAG = nameof(AssetBundleResourceManager);

    private static string _AssetBundlePath;
    private static string _CachePathRoot;

    public override PathResolver pathResolver => PathToResourceAsFile;

    public override ResourceProvider resourceProvider => GetResourceContents;

    public AssetBundleResourceManager(string assetBundleName, string cachePath = "Cache") : base()
    {
      // It's safe to update static members because at most one RsourceManager can be initialized.
      _AssetBundlePath = Path.Combine(Application.streamingAssetsPath, assetBundleName);
      _CachePathRoot = Path.Combine(Application.persistentDataPath, cachePath);
    }

    public override bool IsPrepared(string name)
    {
      var path = GetCachePathFor(name);

      return File.Exists(path);
    }

    private AssetBundle _assetBundle;
    private AssetBundle assetBundle
    {
      get => _assetBundle;
      set
      {
        if (_assetBundle != null)
        {
          _assetBundle.Unload(false);
        }
        _assetBundle = value;
      }
    }

    public void ClearAllCacheFiles()
    {
      if (Directory.Exists(_CachePathRoot))
      {
        Directory.Delete(_CachePathRoot, true);
      }
    }

    public IEnumerator LoadAssetBundleAsync()
    {
      if (assetBundle != null)
      {
        Logger.LogWarning(_TAG, "AssetBundle is already loaded");
        yield break;
      }

      var bundleLoadReq = AssetBundle.LoadFromFileAsync(_AssetBundlePath);
      yield return bundleLoadReq;

      if (bundleLoadReq.assetBundle == null)
      {
        throw new IOException($"Failed to load {_AssetBundlePath}");
      }

      assetBundle = bundleLoadReq.assetBundle;
    }

    public override IEnumerator PrepareAssetAsync(string name, string uniqueKey, bool overwrite = true)
    {
      var destFilePath = GetCachePathFor(uniqueKey);

      if (File.Exists(destFilePath) && !overwrite)
      {
        Logger.LogInfo(_TAG, $"{name} will not be copied to {destFilePath} because it already exists");
        yield break;
      }

      if (assetBundle == null)
      {
        yield return LoadAssetBundleAsync();
      }

      var assetLoadReq = assetBundle.LoadAssetAsync<TextAsset>(name);
      yield return assetLoadReq;

      if (assetLoadReq.asset == null)
      {
        throw new IOException($"Failed to load {name} from {assetBundle.name}");
      }

      Logger.LogVerbose(_TAG, $"Writing {name} data to {destFilePath}...");
      if (!Directory.Exists(_CachePathRoot))
      {
        var _ = Directory.CreateDirectory(_CachePathRoot);
      }
      var bytes = (assetLoadReq.asset as TextAsset).bytes;
      File.WriteAllBytes(destFilePath, bytes);
      Logger.LogVerbose(_TAG, $"{name} is saved to {destFilePath} (length={bytes.Length})");
    }

    [AOT.MonoPInvokeCallback(typeof(PathResolver))]
    protected static string PathToResourceAsFile(string assetPath)
    {
      var assetName = GetAssetNameFromPath(assetPath);
      return GetCachePathFor(assetName);
    }

    [AOT.MonoPInvokeCallback(typeof(ResourceProvider))]
    protected static bool GetResourceContents(string path, IntPtr dst)
    {
      try
      {
        Logger.LogDebug($"{path} is requested");

        var cachePath = PathToResourceAsFile(path);
        if (!File.Exists(cachePath))
        {
          Logger.LogError(_TAG, $"{cachePath} is not found");
          return false;
        }

        var asset = File.ReadAllBytes(cachePath);
        using (var srcStr = new StdString(asset))
        {
          srcStr.Swap(new StdString(dst, false));
        }

        return true;
      }
      catch (Exception e)
      {
        Logger.LogException(e);
        return false;
      }
    }

    private static string GetCachePathFor(string assetName)
    {
      return Path.Combine(_CachePathRoot, assetName);
    }
  }
}
