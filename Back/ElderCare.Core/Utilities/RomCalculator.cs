namespace ElderCare.Core.Utilities;

public static class RomCalculator
{
    /// <summary>
    /// Calculates ROM status based on angle range
    /// Range = MaxAngle - MinAngle
    /// </summary>
    public static string CalculateStatus(double maxAngle, double minAngle)
    {
        var range = maxAngle - minAngle;

        if (range > 120)
            return "normal";
        else if (range >= 60)
            return "limited";
        else
            return "restricted";
    }

    /// <summary>
    /// Gets the expected normal range for a joint type (degrees)
    /// This can be expanded with more joint-specific data
    /// </summary>
    public static (double Min, double Max) GetNormalRange(string jointType)
    {
        // Basic ranges - can be made more sophisticated
        return jointType.ToLower() switch
        {
            "shoulder" => (0, 180),
            "knee" => (0, 140),
            "hip" => (0, 120),
            "ankle" => (0, 50),
            "spine" => (0, 90),
            _ => (0, 120) // default
        };
    }
}