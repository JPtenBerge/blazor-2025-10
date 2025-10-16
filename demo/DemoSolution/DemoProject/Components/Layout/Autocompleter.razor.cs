using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DemoProject.Components.Layout;

// please don't use this in production

public partial class Autocompleter<T> : ComponentBase
{
	public string Query { get; set; }
	[Parameter] public List<T> Data { get; set; }
	public List<T>? Suggestions { get; set; }
	public int? ActiveSuggestionIndex { get; set; }
	[Parameter] public EventCallback<T> OnSelect { get; set; }
	[Parameter] public RenderFragment<T> ItemTemplate { get; set; }

	public void Autocomplete()
	{
		Suggestions = [];
		foreach (var item in Data)
		{
			// reflection
			var props = item.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
			foreach (var prop in props)
			{
				var value = prop.GetValue(item) as string;
				if (value.Contains(Query, StringComparison.OrdinalIgnoreCase))
				{
					Suggestions.Add(item);
					break;
				}
			}
		}
	}

	public async Task HandleKeyDown(KeyboardEventArgs args)
	{
		if (args.Key == "ArrowDown")
		{
			Next();
		}
		else if (args.Key == "Enter")
		{
			await Select();
		}
	}

	public void Next()
	{
		if (Suggestions is null) return;

		ActiveSuggestionIndex ??= -1;
		ActiveSuggestionIndex = (ActiveSuggestionIndex + 1) % Suggestions.Count;
	}

	public async Task Select()
	{
		if (Suggestions is null || ActiveSuggestionIndex is null) return;

		var suggestion = Suggestions[ActiveSuggestionIndex.Value];
		await OnSelect.InvokeAsync(suggestion);
	}
}