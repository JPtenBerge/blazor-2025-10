namespace DemoProject.Components.Pages;

using DemoProject.Entities;

public partial class Home
{
	public string Name { get; set; } = "Martin";

	public List<Snack>? Snacks { get; set; } =
	[
		new()
		{
			Id = 4, Name = "Filet o Fish", Rating = 1.5m,
			PhotoUrl = "https://kwokspots.com/wp-content/uploads/2022/01/filet-o-fish-resized.png"
		},
		new()
		{
			Id = 8, Name = "Kipnuggets", Rating = 4m,
			PhotoUrl =
				"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fmeatingpoint.be%2Fwp-content%2Fuploads%2F2022%2F12%2Fmeatingpoint-kipnuggets-1024x1024.jpg&f=1&nofb=1&ipt=99f484f77aacbf1f3db3730f8a7d312f7c021c33ae1c7822a1bc99b21777c33e"
		},
		new()
		{
			Id = 15, Name = "Crizly", Rating = 3.5m,
			PhotoUrl =
				"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.explicit.bing.net%2Fth%2Fid%2FOIP.h2ZBNdjuxvqgKoB9JT8zZgHaFX%3Fcb%3D12%26pid%3DApi&f=1&ipt=85d98b5f02eed9423ebcf591fd7500267a28a092da84a035eab8d64f9bf270a7"
		},
	];
}