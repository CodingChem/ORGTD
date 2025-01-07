namespace ORGTD.MAUI.Views;

public partial class CaptureNotePage : ContentPage
{
	public CaptureNotePage()
	{
		InitializeComponent();
	}
	public string RawText = "";
	public void OnNoteEditorFocused(object sender, EventArgs e)
	{
		NoteEditor.Text = RawText;
		return;
	}

	public void OnNoteEditorUnfocused(object sender, EventArgs e)
	{
		RawText = NoteEditor.Text;
		NoteEditor.Text = "This is rendered text!";
		return;
	}
}