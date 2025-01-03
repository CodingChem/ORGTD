using System.Text.RegularExpressions;

namespace ORGTD.OrgMode;


internal class OrgTitle
{
	public OrgTitle(string raw)
	{
		_raw = raw;
	}
	public string title
	{
		get => Regex.Replace(strip_task_keys(_raw.TrimStart('*')), OrgConfig.trim_tags_regex, "").Trim();
		set => _raw.Replace(title, value);
	}
	public int level
	{
		get => _raw.TakeWhile(c => c == '*').Count();
		set => _raw = new string('*', value) + _raw.TrimStart('*');
	}

	public List<string> Tags
	{
		get => Regex.Matches(_raw, OrgConfig.tags_regex).Select(m => m.Value).ToList();
		set
		{
			var tags = " :" +string.Join(":", value) + ":";
			_raw = Regex.Replace(_raw, OrgConfig.trim_tags_regex, "");
			_raw = _raw.TrimEnd() + tags;
		}
	}
	public void AddTag(string tag)
	{
		Tags = Tags.Append(tag).ToList();
	}
	public void RemoveTag(string tag)
	{
		Tags = Tags.Where(t => t != tag).ToList();
	}
	public bool IsTask()
	{
		return OrgConfig.Instance.TaskKeys.Any(key => _raw.Contains(key));
	}
	public string Task
	{
		get => IsTask() ? TaskType() : "";
		set
		{
			if (IsTask())
			{
				_raw = _raw.Replace(TaskType(), value);
			}
			else
			{
				// Insert task type after level
				_raw = _raw.Insert(level, value + " ");
			}
		}
	}
	public override string ToString()
	{
		return _raw;
	}
	private string _raw;
	private string TaskType()
	{
		if (!IsTask()) return "";
		return OrgConfig.Instance.TaskKeys.FirstOrDefault(key => _raw.Contains(key))!;
	}
	private string strip_task_keys(string title)
	{
		foreach (var key in OrgConfig.Instance.TaskKeys)
		{
			title = title.Replace(key, "");
		}
		return title;
	}
}
