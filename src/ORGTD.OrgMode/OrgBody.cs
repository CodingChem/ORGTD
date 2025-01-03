using System.Text.RegularExpressions;

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
	public override string ToString()
	{
		return _content;
	}
	public DateTime? ScheduledDate
	{
		get => GetDate("SCHEDULED");
		set => SetDate("SCHEDULED", value);
	}

	public DateTime? DeadlineDate
	{
		get => GetDate("DEADLINE");
		set => SetDate("DEADLINE", value);
	}
	public int Effort
	{
		get
		{
			var match = Regex.Match(_content, @":Effort: (\d+)m");
			if (match.Success)
			{
				return int.Parse(match.Groups[1].Value);
			}
			return 0;
		}
		set
		{
			var match = Regex.Match(_content, @":Effort: (\d+)m");
			if (match.Success)
			{
				_content = _content.Replace(match.Value, $":Effort: {value}m");
			}
			else
			{
				_content = $":Effort: {value}m\n" + _content;
			}
		}
	}

	private DateTime? GetDate(string keyword)
	{
		var match = Regex.Match(_content, $@"{keyword}: <(\d{{4}}-\d{{2}}-\d{{2}}) \w{{3}} \d{{2}}:\d{{2}}>");
		if (match.Success)
		{
			return DateTime.Parse(match.Groups[1].Value);
		}
		return null;
	}

	private void SetDate(string keyword, DateTime? value)
	{
		var match = Regex.Match(_content, $@"{keyword}: <(\d{{4}}-\d{{2}}-\d{{2}}) \w{{3}} \d{{2}}:\d{{2}}>");
		if (match.Success)
		{
			_content = _content.Replace(match.Value, $"{keyword}: <{value:yyyy-MM-dd E HH:mm}>");
		}
		else
		{
			_content = $"{keyword}: <{value:yyyy-MM-dd E HH:mm}>\n" + _content;
		}
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
