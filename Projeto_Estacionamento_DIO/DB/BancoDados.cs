using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Estacionamento_DIO.DB
{
    public class BancoDados
    {

        public void SalvarRegistro(string placa, string veiculo, string nome, string endereco, string telefone)
        {
            using var db = new AppDBContext();
            db.Database.EnsureCreated(); // Cria o banco se não existir

            var registro = new Registro
            {
                Placa = placa,
                Veiculo = veiculo,
                Nome = nome,
                Endereco = endereco,
                Telefone = telefone
            };
        }

    }
}