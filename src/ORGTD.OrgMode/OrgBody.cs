namespace ORGTD.OrgMode;

internal class OrgBody
{
	public OrgBody(string content)
	{
		_content = content;
	}
	public string Body
	{
		get => FilterEmacsMetaData(_content);
		set => _content = ReplaceAfterEmacsMetaData(_content, value);
	}

	private string ReplaceAfterEmacsMetaData(string originalContent, string newContent)
	{
		var lines = originalContent.Split('\n');
		var filteredLines = lines.TakeWhile(line =>
							OrgConfig.OrgModeMetaDataKeywords.Any(keyword => line.StartsWith(keyword)) &&
							line.TrimStart().StartsWith("#")
		);
		return string.Join('\n', filteredLines) + newContent;
	}

	private string FilterEmacsMetaData(string content)
	{
		var lines = content.Split('\n');

		var filteredLines = lines.Where(line =>
							!OrgConfig.OrgModeMetaDataKeywords.Any(keyword => line.StartsWith(keyword)) &&
							!line.TrimStart().StartsWith("#")
		);
		return string.Join('\n', filteredLines);
	}
	private string _content;
}
