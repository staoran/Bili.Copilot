﻿// Copyright (c) Bili Copilot. All rights reserved.

using System.Threading.Tasks;
using Bili.Copilot.Libs.Toolkit;
using Bili.Copilot.Models.Constants.App;
using CommunityToolkit.Mvvm.Input;

namespace Bili.Copilot.ViewModels;

/// <summary>
/// 动漫页面的视图模型.
/// </summary>
public sealed partial class AnimePageViewModel : ViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnimePageViewModel"/> class.
    /// </summary>
    private AnimePageViewModel()
    {
        CurrentType = SettingsToolkit.ReadLocalSetting(SettingNames.LastAnimeType, AnimeDisplayType.Timeline);
        CheckModuleStateAsync();

        AttachIsRunningToAsyncCommand(p => IsReloading = p, ReloadCommand, InitializeCommand);
    }

    [RelayCommand]
    private async Task ReloadAsync()
    {
        if (IsTimelineShown)
        {
            await TimelineViewModel.Instance.ReloadCommand.ExecuteAsync(default);
        }
        else if (IsBangumiShown)
        {
            await BangumiRecommendDetailViewModel.Instance.ReloadCommand.ExecuteAsync(default);
        }
        else if (IsDomesticShown)
        {
            await DomesticRecommendDetailViewModel.Instance.ReloadCommand.ExecuteAsync(default);
        }
        else if (IsFavoriteShown)
        {
            await AnimeFavoriteDetailViewModel.Instance.ReloadCommand.ExecuteAsync(default);
        }
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        if (_isInitialized)
        {
            return;
        }

        await InitializeCurrentModuleAsync();
        _isInitialized = true;
    }

    private async Task InitializeCurrentModuleAsync()
    {
        if (IsTimelineShown)
        {
            await TimelineViewModel.Instance.InitializeCommand.ExecuteAsync(default);
        }
        else if (IsBangumiShown)
        {
            await BangumiRecommendDetailViewModel.Instance.InitializeCommand.ExecuteAsync(default);
        }
        else if (IsDomesticShown)
        {
            await DomesticRecommendDetailViewModel.Instance.InitializeCommand.ExecuteAsync(default);
        }
        else if (IsFavoriteShown)
        {
            await AnimeFavoriteDetailViewModel.Instance.InitializeCommand.ExecuteAsync(default);
        }
    }

    private async void CheckModuleStateAsync()
    {
        IsTimelineShown = CurrentType == AnimeDisplayType.Timeline;
        IsBangumiShown = CurrentType == AnimeDisplayType.Bangumi;
        IsDomesticShown = CurrentType == AnimeDisplayType.Domestic;
        IsFavoriteShown = CurrentType == AnimeDisplayType.Favorite;

        Title = CurrentType switch
        {
            AnimeDisplayType.Timeline => ResourceToolkit.GetLocalizedString(StringNames.TimeChart),
            AnimeDisplayType.Bangumi => ResourceToolkit.GetLocalizedString(StringNames.Bangumi),
            AnimeDisplayType.Domestic => ResourceToolkit.GetLocalizedString(StringNames.DomesticAnime),
            AnimeDisplayType.Favorite => ResourceToolkit.GetLocalizedString(StringNames.MyFavoriteAnime),
            _ => string.Empty,
        };

        await InitializeCurrentModuleAsync();
    }

    partial void OnCurrentTypeChanged(AnimeDisplayType value)
    {
        CheckModuleStateAsync();
        SettingsToolkit.WriteLocalSetting(SettingNames.LastAnimeType, value);
    }
}
