using System.IO;

namespace Webinex.Temply.Tests.E2e.Templates;

public static class TemplatesInfo
{
    public static object Values => new { valueOne = "One", valueTwo = "Two" };

    public static string Directory
    {
        get
        {
            var assemblyLocation = typeof(TemplatesInfo).Assembly.Location;
            var assemblyDir = Path.GetDirectoryName(assemblyLocation)!;
            return Path.Combine(assemblyDir, "E2e", "Templates");
        }
    }

    public static class ExpectedResult
    {
        public static readonly string JsonTemplate1 = "JsonTemplate1 One";
        public static readonly string JsonTemplate2 = "JsonTemplate2 Two";
        public static readonly string TxtTemplate = "TxtTemplate One, Two";
        public static readonly string YamlTemplate1 = "YamlTemplate1 One";
        public static readonly string YamlTemplate2 = "YamlTemplate2 Two";
    }

    public static class Keys
    {
        public static readonly string JsonTemplate1 = nameof(JsonTemplate1);
        public static readonly string JsonTemplate2 = nameof(JsonTemplate2);
        public static readonly string TxtTemplate = nameof(TxtTemplate);
        public static readonly string YamlTemplate1 = nameof(YamlTemplate1);
        public static readonly string YamlTemplate2 = nameof(YamlTemplate2);
    }
}