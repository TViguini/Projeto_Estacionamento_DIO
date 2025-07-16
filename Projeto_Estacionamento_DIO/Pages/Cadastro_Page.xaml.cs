using Projeto_Estacionamento_DIO.DB;
namespace Projeto_Estacionamento_DIO.Pages;
[QueryProperty(nameof(Placa), "placa")]
public partial class Cadastro_Page : ContentPage
{
    private string _placa;
    public string Placa
    {
        get => _placa;
        set
        {
            _placa = value;
            PlacaEntry.Text = _placa;
        }
    }
    public Cadastro_Page()
	{
		InitializeComponent();
	}
    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        var banco = new BancoDados();
        string placa = PlacaEntry.Text;
        string veiculo = VeiculoEntry.Text;
        string nome = NomeEntry.Text;
        string endereco = EnderecoEntry.Text;
        string telefone = TelefoneEntry.Text;
        if (string.IsNullOrWhiteSpace(placa) ||
        string.IsNullOrWhiteSpace(veiculo) ||
        string.IsNullOrWhiteSpace(nome) ||
        string.IsNullOrWhiteSpace(endereco) ||
        string.IsNullOrWhiteSpace(telefone))
        {
            await DisplayAlert("Erro", "Por favor, preencha todos os campos antes de salvar.", "OK");
        }
        else
        {
            banco.SalvarRegistro(placa, veiculo, nome, endereco, telefone);
            bool resposta = await DisplayAlert(
                "Registro salvo.",
                "Deseja iniciar cobran�a?",
                "Sim, continuar",
                "N�o, voltar");
            if (resposta)
            {
                DateTime dateTime = DateTime.Now;
                string dataFormatada = dateTime.ToString("dd/MM/yyyy '�s' HH:mm");
                await DisplayAlert(
                    "In�cio da Cobran�a",
                    $"Cobran�a iniciada em {dataFormatada}.",
                    "OK");
                banco.SaveInitTime(placa, dateTime);
                await Shell.Current.GoToAsync("///Init_Page");
            }
            else
            {
                await Shell.Current.GoToAsync("///Init_Page");
            }
                
        }
    }
}