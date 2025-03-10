﻿// Copyright (c) Bili Copilot. All rights reserved.

using System.Threading.Tasks;
using Bili.Copilot.App.Controls.Base;
using Bili.Copilot.Models.Constants.Bili;
using Bili.Copilot.ViewModels;
using Microsoft.UI.Xaml;

namespace Bili.Copilot.App.Controls.Modules;

/// <summary>
/// 剧集/系列视图.
/// </summary>
public sealed partial class PgcSeasonView : PgcSeasonViewBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PgcSeasonView"/> class.
    /// </summary>
    public PgcSeasonView() => InitializeComponent();

    private async void OnSeasonItemClickAsync(object sender, RoutedEventArgs e)
    {
        var card = sender as CardPanel;
        var data = card.DataContext as VideoIdentifierSelectableViewModel;
        if (!data.Data.Id.Equals(ViewModel.View.Information.Identifier.Id) || ViewModel.CurrentEpisode.IsPreviewVideo)
        {
            ViewModel.SetSnapshot(new Models.Data.Local.PlaySnapshot(default, data.Data.Id, VideoType.Pgc)
            {
                Title = data.Data.Title,
            });
        }
        else
        {
            data.IsSelected = false;
            await Task.Delay(100);
            data.IsSelected = true;
        }
    }
}

/// <summary>
/// <see cref="PgcSeasonView"/> 的基类.
/// </summary>
public abstract class PgcSeasonViewBase : ReactiveUserControl<PgcPlayerPageViewModel>
{
}
