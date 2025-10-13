using DemoProject.Repositories;
using Microsoft.AspNetCore.Components;

namespace DemoProject.Components.Pages;

using DemoProject.Entities;

public partial class Home : ComponentBase
{
	[Inject] public NavigationManager NavigationManager { get; set; } = null!;
	[Inject] public ISnackRepository SnackRepository { get; set; } = null!;
	
	[SupplyParameterFromForm(FormName = "AddSnackForm")]
	public Snack NewSnack { get; set; } = new();
	
	public static List<Snack>? Snacks { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Snacks = (await SnackRepository.GetAllAsync()).ToList();
	}

	public async Task AddSnack()
	{
		Console.WriteLine($"Snack toevoegen: {NewSnack.Name}");
		await SnackRepository.AddAsync(NewSnack);
		// NewSnack = new();
		NavigationManager.NavigateTo("/");
	}
}