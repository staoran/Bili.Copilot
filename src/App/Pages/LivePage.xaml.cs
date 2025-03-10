﻿// Copyright (c) Bili Copilot. All rights reserved.

using System;
using Bili.Copilot.App.Controls.Base;
using Bili.Copilot.Models.Constants.App;
using Bili.Copilot.ViewModels;

namespace Bili.Copilot.App.Pages;

/// <summary>
/// 直播间页面.
/// </summary>
public sealed partial class LivePage : LivePageBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LivePage"/> class.
    /// </summary>
    public LivePage()
    {
        InitializeComponent();
        ViewModel = LivePageViewModel.Instance;
    }

    /// <inheritdoc/>
    protected override void OnPageLoaded()
    {
        CoreViewModel.IsBackButtonShown = ViewModel.IsPartitionDetailShown;
        CoreViewModel.BackRequest += OnCoreViewModelBackRequest;
        LiveTypeSelection.SelectedIndex = (int)ViewModel.CurrentType;
        ViewModel.InitializeCommand.Execute(default);
    }

    /// <inheritdoc/>
    protected override void OnPageUnloaded()
        => CoreViewModel.BackRequest -= OnCoreViewModelBackRequest;

    private void OnCoreViewModelBackRequest(object sender, EventArgs e)
        => ViewModel.ClosePartitionDetailCommand.Execute(default);

    private void OnLiveTypeSegmentedSelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        => ViewModel.CurrentType = (LiveDisplayType)LiveTypeSelection.SelectedIndex;
}

/// <summary>
/// <see cref="LivePage"/> 的基类.
/// </summary>
public abstract class LivePageBase : PageBase<LivePageViewModel>
{
}
