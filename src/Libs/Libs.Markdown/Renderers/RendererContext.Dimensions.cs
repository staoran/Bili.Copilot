﻿// Copyright (c) Bili Copilot. All rights reserved.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace Bili.Copilot.Libs.Markdown.Renderers;

internal sealed partial class RendererContext
{
    /// <summary>
    /// Gets or sets the distance between the border and its child object.
    /// </summary>
    public Thickness Padding { get; set; }

    /// <summary>
    /// Gets or sets the content corner radius.
    /// </summary>
    public CornerRadius CornerRadius { get; set; }

    /// <summary>
    /// Gets or sets the border thickness of a control.
    /// </summary>
    public Thickness BorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the border around inline code.
    /// </summary>
    public Thickness InlineCodeBorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the space outside of code blocks.
    /// </summary>
    public Thickness CodeMargin { get; set; }

    /// <summary>
    /// Gets or sets the space between the code border and the text.
    /// </summary>
    public Thickness CodePadding { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the border around code blocks.
    /// </summary>
    public Thickness CodeBorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the space between the code border and the text.
    /// </summary>
    public Thickness InlineCodePadding { get; set; }

    /// <summary>
    /// Gets or sets the margin of inline code.
    /// </summary>
    public Thickness InlineCodeMargin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 1 headers.
    /// </summary>
    public double Header1FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 1 headers.
    /// </summary>
    public Thickness Header1Margin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 2 headers.
    /// </summary>
    public double Header2FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 2 headers.
    /// </summary>
    public Thickness Header2Margin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 3 headers.
    /// </summary>
    public double Header3FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 3 headers.
    /// </summary>
    public Thickness Header3Margin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 4 headers.
    /// </summary>
    public double Header4FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 4 headers.
    /// </summary>
    public Thickness Header4Margin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 5 headers.
    /// </summary>
    public double Header5FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 5 headers.
    /// </summary>
    public Thickness Header5Margin { get; set; }

    /// <summary>
    /// Gets or sets the font size for level 6 headers.
    /// </summary>
    public double Header6FontSize { get; set; }

    /// <summary>
    /// Gets or sets the margin for level 6 headers.
    /// </summary>
    public Thickness Header6Margin { get; set; }

    /// <summary>
    /// Gets or sets the margin used for horizontal rules.
    /// </summary>
    public Thickness HorizontalRuleMargin { get; set; }

    /// <summary>
    /// Gets or sets the vertical thickness of the horizontal rule.
    /// </summary>
    public double HorizontalRuleThickness { get; set; }

    /// <summary>
    /// Gets or sets the margin used by lists.
    /// </summary>
    public Thickness ListMargin { get; set; }

    /// <summary>
    /// Gets or sets the width of the space used by list item bullets/numbers.
    /// </summary>
    public double ListGutterWidth { get; set; }

    /// <summary>
    /// Gets or sets the space between the list item bullets/numbers and the list item content.
    /// </summary>
    public double ListBulletSpacing { get; set; }

    /// <summary>
    /// Gets or sets the margin used for paragraphs.
    /// </summary>
    public Thickness ParagraphMargin { get; set; }

    /// <summary>
    /// Gets or sets the line height used for paragraphs.
    /// </summary>
    public int ParagraphLineHeight { get; set; }

    /// <summary>
    /// Gets or sets the thickness of quote borders.
    /// </summary>
    public Thickness QuoteBorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the space outside of quote borders.
    /// </summary>
    public Thickness QuoteMargin { get; set; }

    /// <summary>
    /// Gets or sets the space between the quote border and the text.
    /// </summary>
    public Thickness QuotePadding { get; set; }

    /// <summary>
    /// Gets or sets the thickness of any table borders.
    /// </summary>
    public double TableBorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the padding inside each cell.
    /// </summary>
    public Thickness TableCellPadding { get; set; }

    /// <summary>
    /// Gets or sets the margin used by tables.
    /// </summary>
    public Thickness TableMargin { get; set; }

    /// <summary>
    /// Gets or sets the size of the text in this control.
    /// </summary>
    public double FontSize { get; set; }

    /// <summary>
    /// Gets or sets the uniform spacing between characters, in units of 1/1000 of an em.
    /// </summary>
    public int CharacterSpacing { get; set; }

    /// <summary>
    /// Gets or sets the word wrapping behavior.
    /// </summary>
    public TextWrapping TextWrapping { get; set; }

    /// <summary>
    /// Gets or sets the degree to which a font is condensed or expanded on the screen.
    /// </summary>
    public FontStretch FontStretch { get; set; }

    /// <summary>
    /// Gets or sets the stretch used for images.
    /// </summary>
    public Stretch ImageStretch { get; set; }

    /// <summary>
    /// Gets or sets the MaxHeight for images.
    /// </summary>
    public double ImageMaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the MaxWidth for images.
    /// </summary>
    public double ImageMaxWidth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to wrap text in the Code Block, or use Horizontal Scroll.
    /// </summary>
    public bool WrapCodeBlock { get; set; }
}
