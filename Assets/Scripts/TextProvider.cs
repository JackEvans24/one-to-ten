using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextProvider
{
    private static string outOfTenText = "<size=50%> /10</size>";

    public static string GetPromptText(CurrentTurnData turn) =>
        WrapWithSize("Name something that is", 70) + " " + WrapWithColour("FF8300", turn.ActualValue.ToString()) +
        WrapWithSize("/10", 70) + " " + WrapWithColour("FF8300", turn.CurrentScale);

    public static string GetQuestionText(CurrentTurnData turn) =>
        WrapWithSize("How", 70) + " " + WrapWithColour("FF8300", turn.CurrentScale) + " " +
        WrapWithSize("does", 70) + " " + GetPlayerNameLabel(turn.CurrentPlayer) + " " +
        WrapWithSize("think", 70) + " " + WrapWithColour("FF8300", turn.PlayerInput) + " " +
        WrapWithSize("is?", 70);

    public static string GetResultText(CurrentTurnData turn) =>
        WrapWithSize("How", 70) + " " + WrapWithColour("FF8300", turn.CurrentScale) + " " +
        WrapWithSize("is", 70) + " " + WrapWithColour("FF8300", turn.PlayerInput) +
        WrapWithSize("?", 70);

    public static string GetPlayerNameLabel(Player player) => WrapWithColour(ColorUtility.ToHtmlStringRGB(player.Color), player.Name);

    public static string GetSliderValueText(float value) => WrapWithColour("FF8300", Mathf.RoundToInt(value).ToString()) + outOfTenText;

    private static string WrapWithColour(string colourHex, string text) => $"<color=#{colourHex}>{text}</color>";

    private static string WrapWithSize(string text, int sizePercentage) => $"<size={sizePercentage}%>{text}</size>";
}
