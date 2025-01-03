namespace ORGTD.OrgMode;

public class OrgConfig
{
	private static readonly Lazy<OrgConfig> _instance = new(() => new OrgConfig());
	private OrgConfig() 
	{
		TodoKeys = new List<string> { "TODO", "NEXT", "WAIT" };
		DoneKeys = new List<string> { "DONE" };
	}
	public static OrgConfig Instance => _instance.Value;
	public static string tags_regex = @"(?<=:)([^:]+?)(?=:)";
	public static string trim_tags_regex = @":.*?:\s*$";
	public List<string> TodoKeys { get; set; }
	public List<string> DoneKeys { get; set; }
	public List<string> TaskKeys => TodoKeys.Concat(DoneKeys).ToList();
}
