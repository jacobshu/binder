namespace testapp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnDecrementClicked(object sender, EventArgs e)
	{
		count--;

    CounterTracker.Text = $"The count is {count}";

		SemanticScreenReader.Announce(CounterTracker.Text);
	}

  private void OnIncrementClicked(object sender, EventArgs e)
	{
		count++;

		CounterTracker.Text = $"The count is {count}";

    SemanticScreenReader.Announce(CounterTracker.Text);
	}

}

