using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
            db.Database.EnsureCreated();

            var registro = new Registro
            {
                Placa = placa,
                Veiculo = veiculo,
                Nome = nome,
                Endereco = endereco,
                Telefone = telefone
            };
            db.Registro.Add(registro);
            db.SaveChanges();
        }
        public void SaveInitTime(string placa, DateTime init_time)
        {
            using var db = new AppDBContext();
            db.Database.EnsureCreated();

            var reg_time = new Historico
            {
                Placa = placa,
                Entrada = init_time
            };
            db.Historico.Add(reg_time);
            db.SaveChanges();
        }

        public void FinishTimeParking(int id, DateTime finish_time, decimal value)
        {
            using var db = new AppDBContext();

            var reg_time = db.Historico.Find(id);

            if (reg_time != null)
            {
                reg_time.Saida = finish_time;
                reg_time.Valor = value;
                db.SaveChanges();
            }
        }
    }
}