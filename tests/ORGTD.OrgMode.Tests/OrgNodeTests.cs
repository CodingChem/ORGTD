namespace ORGTD.OrgMode.Tests;

[TestClass]
public sealed class OrgNodeTests
{
	[TestMethod]
	[DataRow("Title", "* Title")]
	[DataRow("Title", "* Title :tag1:")]
	[DataRow("Title", "** Title :tag1: :tag2:")]
	[DataRow("Title", "*** Title :tag1:tag2:")]
	[DataRow("Title", "**** TODO Title :tag1:tag2:")]
	[DataRow("Title", "***** DONE Title :tag1:tag2:")]
	[DataRow("Title", "****** NEXT Title :tag1:tag2:")]
	[DataRow("Title", "****** WAIT Title :tag1:tag2:")]
	public void OrgNode_CanGetTitle(string want, string raw_title)
	{
		// Arrange
		var orgNode = new OrgNode(raw_title, null);
		// Assert
		Assert.AreEqual(want, orgNode.Title);
	}
	[TestMethod]
	[DataRow(1, "* Title")]
	[DataRow(1, "* Title :tag1:")]
	[DataRow(2, "** Title :tag1: :tag2:")]
	[DataRow(3, "*** Title :tag1:tag2:")]
	[DataRow(4, "**** TODO Title :tag1:tag2:")]
	[DataRow(5, "***** DONE Title :tag1:tag2:")]
	public void OrgNode_CanGetLevel(int want, string raw_title)
	{
		// Arrange
		var orgNode = new OrgNode(raw_title, null);
		// Assert
		Assert.AreEqual(want, orgNode.HeaderLevel);
	}
	[TestMethod]
	[DataRow(new string[] { }, "* Title")]
	[DataRow(new string[] { "tag1" }, "* Title :tag1:")]
	[DataRow(new string[] { "tag1", "tag2" }, "** Title :tag1:tag2:")]
	[DataRow(new string[] { "tag1", "tag2" }, "** Title :tag1::tag2:")]
	public void OrgNode_CanGetTags(string[] want, string raw_title)
	{
		// Arrange
		var orgNode = new OrgNode(raw_title, null);
		// Assert
		CollectionAssert.AreEqual(want, orgNode.GetTags(), $"Want: {string.Join(",", want)}\nGot: {string.Join(",", orgNode.GetTags())}");
	}
	[TestMethod]
	public void OrgNodeDescription_CanGet()
	{
		// Arrange
		string description = "Content";
		var node = new OrgNode("Title\n" + description, null);
		// Assert
		Assert.AreEqual(description, node.Description, $"Want: {description}, Got: {node.Description}");
	}
	[TestMethod]
	public void OrgNodeDescription_CanSet()
	{
		// Arrange
		string description = "Content";
		var node = new OrgNode("Title\nOldContent", null);
		// Act
		node.Description = description;
		// Assert
		Assert.AreEqual(description, node.Description);
		Assert.AreEqual("Title", node.Title);
	}
}
