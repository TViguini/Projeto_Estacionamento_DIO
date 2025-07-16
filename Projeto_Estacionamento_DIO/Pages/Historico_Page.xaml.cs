using Microsoft.EntityFrameworkCore;
using Projeto_Estacionamento_DIO.DB;
namespace Projeto_Estacionamento_DIO.Pages;
[QueryProperty(nameof(Placa), "placa")]

public partial class Historico_Page : ContentPage
{
    private string _placa;
    public string Placa
    {
        get => _placa;
        set
        {
            _placa = value;
            CarregarHistorico();
        }
    }
    public Historico_Page()
	{
		InitializeComponent();
	}
    private async void CarregarHistorico()
    {
        PlacaLabel.Text = $"Histórico para: {_placa}";
        using var db = new AppDBContext();
        var resultado = await db.Historico.Where(r => r.Placa == _placa).ToListAsync();

        HistoricoCollectionView.ItemsSource = resultado;
    }
    private async void OnReturnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Init_Page");
    }
}