using System;
using System.Text.RegularExpressions;

namespace AssetStudio;

public class UnityVersion : IComparable<UnityVersion>, IEquatable<UnityVersion>
{
    public int Major { get; }
    public int Minor { get; }
    public int Patch { get; }
    public string Extra { get; }
    
    private static readonly Regex VersionRegex = new Regex(
        @"^(?<major>\d+)(?:\.(?<minor>\d+))?(?:\.(?<patch>\d+))?(?<extra>[a-zA-Z0-9]*)", 
        RegexOptions.Compiled);
    
    public UnityVersion(int major, int minor, int patch, string extra = "")
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        Extra = extra;
    }
    
    public UnityVersion(string versionString)
    {
        var match = VersionRegex.Match(versionString);
        if (!match.Success)
            throw new FormatException($"Invalid Unity version format: {versionString}");

        Major = int.Parse(match.Groups["major"].Value);
        Minor = match.Groups["minor"].Success ? int.Parse(match.Groups["minor"].Value) : 0;
        Patch = match.Groups["patch"].Success ? int.Parse(match.Groups["patch"].Value) : 0;
        Extra = match.Groups["extra"].Value;
    }
    
    public static implicit operator UnityVersion(string versionString)
    {
        return new UnityVersion(versionString);
    }
    
    public int CompareTo(UnityVersion? other)
    {
        if (other is null) return 1;
        
        int majorComparison = Major.CompareTo(other.Major);
        if (majorComparison != 0) return majorComparison;
        
        int minorComparison = Minor.CompareTo(other.Minor);
        if (minorComparison != 0) return minorComparison;
        
        return Patch.CompareTo(other.Patch); 
    }
    
    public bool Equals(UnityVersion? other)
    {
        if (other is null) return false;
        return Major == other.Major && 
               Minor == other.Minor && 
               Patch == other.Patch;
    }
    
    public override bool Equals(object? obj) => Equals(obj as UnityVersion);
    
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Major.GetHashCode();
            hash = hash * 23 + Minor.GetHashCode();
            hash = hash * 23 + Patch.GetHashCode();
            hash = hash * 23 + Extra.ToLowerInvariant().GetHashCode();
            return hash;
        }
    }
    
    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}{Extra}";
    }
    
    public static bool operator ==(UnityVersion left, UnityVersion right) => Equals(left, right);
    public static bool operator !=(UnityVersion left, UnityVersion right) => !Equals(left, right);
    public static bool operator <(UnityVersion left, UnityVersion right) => left.CompareTo(right) < 0;
    public static bool operator >(UnityVersion left, UnityVersion right) => left.CompareTo(right) > 0;
    public static bool operator <=(UnityVersion left, UnityVersion right) => left.CompareTo(right) <= 0;
    public static bool operator >=(UnityVersion left, UnityVersion right) => left.CompareTo(right) >= 0;

    public bool IsTuanjie => Extra.Contains("t");
    private static readonly Regex BuildRegex = new Regex(
        @"^[^\d]*(\d+)", 
        RegexOptions.Compiled);
    public int Build
    {
        get
        {
            if (string.IsNullOrEmpty(Extra))
            {
                return 0;
            }
            Match match = BuildRegex.Match(Extra);
            if (match.Success)
            {
                if (int.TryParse(match.Groups[1].Value, out int number))
                {
                    return number;
                }
            }
            return 0;
        }
    }
}