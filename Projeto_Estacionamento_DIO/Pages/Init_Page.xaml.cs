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
            string placaMaiuscula = placa.Trim().ToUpper();
            using var db = new AppDBContext();
            var banco = new BancoDados();
            var resultado = await db.Registro
                                     .Where(r => r.Placa == placaMaiuscula)
                                     .ToListAsync();
            if (resultado.Count == 0)
            {
                bool resposta = await DisplayAlert(
                    "Ve�culo n�o Cadastrado",
                    $"Deseja cadastrar a placa {placaMaiuscula}?",
                    "Sim, continuar",
                    "N�o, voltar" 
                );

                if (resposta == true) 
                {
                    await Shell.Current.GoToAsync($"///Cadastro_Page?placa={placaMaiuscula}");
                }
            }
            else
            {
                bool resposta = await DisplayAlert(
                    "Veiculo j� possui cadastro.", 
                    $"Iniciar tempo de cobran�a.", 
                    "Sim, continuar",
                    "N�o, voltar");
                if (resposta == true)
                {
                    var resultado_hist = await db.Historico
                                     .Where(r => r.Placa == placaMaiuscula && r.Saida == DateTime.MinValue)
                                     .ToListAsync();
                    if(resultado_hist.Count == 0)
                    {
                        DateTime dateTime = DateTime.Now;
                        string dataFormatada = dateTime.ToString("dd/MM/yyyy '�s' HH:mm");
                        await DisplayAlert(
                            "In�cio da Cobran�a",
                            $"Cobran�a iniciada em {dataFormatada}.",
                            "OK");
                        banco.SaveInitTime(placaMaiuscula, dateTime);
                    }
                    else
                    {
                        await DisplayAlert(
                            "Veiculo j� possui estacionamento em aberto",
                            "",
                            "OK");
                    }
                    
                }
            }
            
        }
        else
        {
            await DisplayAlert("Opera��o Cancelada", "O registro de entrada invalido.", "OK");
        }
    }

    private async void CobrancaClicked(object sender, EventArgs e)
    {
        string placa = await DisplayPromptAsync("Finalizar Cobran�a", "Digite a placa do ve�culo:", "OK", "Cancelar", "AAA0000", 7, Keyboard.Default);
        if (!string.IsNullOrWhiteSpace(placa))
        {
            string placaMaiuscula = placa.Trim().ToUpper();
            using var db = new AppDBContext();
            var banco = new BancoDados();
            var resultado = await db.Historico
                                     .Where(r => r.Placa == placaMaiuscula && r.Saida == DateTime.MinValue)
                                     .ToListAsync();
            if (resultado.Count != 0)
            {
                DateTime finish_time = DateTime.Now;
                DateTime init_time = resultado[0].Entrada;
                decimal value = Value_pay(init_time, finish_time);
                await DisplayAlert("Valor Cobrado:", $"{value}", "OK");
                banco.FinishTimeParking(resultado[0].Id, finish_time, value);
            }
            else
            {
                await DisplayAlert(
                            "Veiculo n�o possui estacionamento em aberto",
                            "",
                            "OK");
            }
        }
    }

    private decimal Value_pay(DateTime init, DateTime finish)
    {
        TimeSpan duracao = finish - init;
        if (duracao.TotalSeconds <= 0)
        {
            return 0.0m;
        }
        double totalHoras = Math.Ceiling(duracao.TotalHours);
        decimal valorTotal;

        if (totalHoras <= 1)
        {
            valorTotal = 8.0m; 
        }
        else
        {
            decimal horasAdicionais = (decimal)totalHoras - 1;
            valorTotal = 8.0m + (horasAdicionais * 2.0m);
        }
        return valorTotal;
    }
    private async void CansultarClicked(object sender, EventArgs e)
    {
        string placa = await DisplayPromptAsync("Digite a placa do ve�culo", "Para visualizar hist�rico:", "OK", "Cancelar", "AAA0000", 7, Keyboard.Default);
        if (!string.IsNullOrWhiteSpace(placa))
        {
            string placaMaiuscula = placa.Trim().ToUpper();
            using var db = new AppDBContext();

            var resultado = await db.Historico
                                     .Where(r => r.Placa == placaMaiuscula)
                                     .ToListAsync();
            if (resultado.Count != 0)
            {
                await Shell.Current.GoToAsync($"///Historico_Page?placa={placaMaiuscula}");
            }
            else
            {
                await DisplayAlert(
                            "Veiculo n�o possui hist�rico de cobran�a.",
                            "",
                            "OK");
            }
        }
    }
}