using UnityEngine;

public static class TextProvider
{
    private const string OUT_OF_TEN_TEXT = "<size=50%> /10</size>";
    private const string DEFAULT_ORANGE_COLOUR = "FF8300";

    public static string GetPromptText(CurrentTurnData turn) =>
        WrapWithSize("Name something that is", 70) + " " + WrapWithColour(turn.ActualValue.ToString()) +
        OUT_OF_TEN_TEXT + " " + WrapWithColour(turn.CurrentScale);

    public static string GetQuestionText(CurrentTurnData turn) =>
        WrapWithSize("How", 70) + " " + WrapWithColour(turn.CurrentScale) + " " +
        WrapWithSize("does", 70) + " " + GetPlayerNameLabel(turn.CurrentPlayer) + " " +
        WrapWithSize("think", 70) + " " + WrapWithColour(turn.PlayerInput) + " " +
        WrapWithSize("is?", 70);

    public static string GetResultText(CurrentTurnData turn) =>
        WrapWithSize("How", 70) + " " + WrapWithColour(turn.CurrentScale) + " " +
        WrapWithSize("is", 70) + " " + WrapWithColour(turn.PlayerInput) +
        WrapWithSize("?", 70);

    public static string GetModalText(CurrentTurnData turn)
    {
        switch (turn.CurrentState)
        {
            case GameState.PlayerInput:
                return "Are you happy with this answer?";
            case GameState.Argument:
                return "Are you happy with this guess?";
            default:
                throw new System.NotImplementedException();
        }
    }

    public static string GetPlayerNameLabel(Player player) => WrapWithColour(player.Name, ColorUtility.ToHtmlStringRGB(player.Color));

    public static string GetSliderValueText(float value) => WrapWithColour(Mathf.RoundToInt(value).ToString()) + OUT_OF_TEN_TEXT;

    public static string WrapWithColour(string text, string colourHex = DEFAULT_ORANGE_COLOUR) => $"<color=#{colourHex}>{text}</color>";

    private static string WrapWithSize(string text, int sizePercentage) => $"<size={sizePercentage}%>{text}</size>";
}
