﻿// Copyright (c) Bili Copilot. All rights reserved.

using Bili.Copilot.App.Controls.Base;
using Bili.Copilot.ViewModels;
using Microsoft.UI.Xaml;

namespace Bili.Copilot.App.Controls.Modules;

/// <summary>
/// PGC区块视图.
/// </summary>
public sealed partial class PgcExtraView : PgcExtraViewBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PgcExtraView"/> class.
    /// </summary>
    public PgcExtraView() => InitializeComponent();

    private void OnEpisodeItemClick(object sender, RoutedEventArgs e)
    {
        var card = sender as CardPanel;
        var data = card.DataContext as EpisodeItemViewModel;
        if (!ViewModel.CurrentEpisode.Equals(data.Data))
        {
            ViewModel.ChangeEpisodeCommand.Execute(data.Data);
        }
    }
}

/// <summary>
/// <see cref="PgcExtraView"/> 的基类.
/// </summary>
public abstract class PgcExtraViewBase : ReactiveUserControl<PgcPlayerPageViewModel>
{
}
