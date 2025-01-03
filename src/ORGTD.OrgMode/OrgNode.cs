namespace ORGTD.OrgMode
{
	public class OrgNode
	{
		public OrgNode(string content, OrgNode? parent)
		{
			if (parent != null)
			{
				_parent = parent;
				parent.AddChild(this);
			}
			var lines = content.Split("\n");
			_title = new OrgTitle(lines[0]);
			_content = string.Join("\n", lines.Skip(1));
			_children = new List<OrgNode>();
		}
		public string Title
		{
			get => _title.title;
			set => _title.title = value;
		}
		public int HeaderLevel
		{
			get => _title.level;
			set => _title.level = value;
		}
		public List<string> GetTags()
		{
			return _title.Tags();
		}
		public void AddTag(string tag)
		{
			_title.AddTag(tag);
		}
		public void RemoveTag(string tag)
		{
			_title.RemoveTag(tag);
		}
		public bool IsTask()
		{
			return _title.IsTask();
		}

		public string Task
		{
			get => _title.Task;
			set => _title.Task = value;
		}
		public string Description
		{
			get => _content;
			set => _content = value;
		}
		public void AddChild(OrgNode child)
		{
			_children.Add(child);
		}

		private string _content;
		private OrgTitle _title;
		private OrgNode? _parent;
		private List<OrgNode> _children;
	}
}
