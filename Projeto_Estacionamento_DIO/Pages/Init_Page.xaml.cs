using Microsoft.EntityFrameworkCore;
using Projeto_Estacionamento_DIO.DB;
using System;
using System.Net;
namespace Projeto_Estacionamento_DIO.Pages;

public partial class Init_Page : ContentPage
{
    public Init_Page()
	{
		InitializeComponent();
    }

    private async void CadastroClicked(object sender, EventArgs e)
	{
        string placa = await DisplayPromptAsync("Registrar Ve�culo", "Digite a placa do ve�culo:", "OK", "Cancelar", "AAA0000", 7, Keyboard.Default);

        if (!string.IsNullOrWhiteSpace(placa))
        {
            using var db = new AppDBContext();
            var resultado = await db.Registro
                                     .Where(r => r.Placa == placa)
                                     .ToListAsync();
            if (resultado.Count != 0)
            {
                await DisplayAlert("Veiculo j� possui cadastro.", $"A placa {placa.ToUpper()} foi registrada.", "OK");
            }
            else
            {
                await DisplayAlert("Veiculo n�o possui cadastro.", $"Realize o cadastro.", "OK");
            }
            
        }
        else
        {
            await DisplayAlert("Opera��o Cancelada", "O registro de entrada foi cancelado.", "OK");
        }
    }
    private void CansultarClicked(object sender, EventArgs e)
    {

    }
    private void CobrancaClicked(object sender, EventArgs e)
    {

    }
}