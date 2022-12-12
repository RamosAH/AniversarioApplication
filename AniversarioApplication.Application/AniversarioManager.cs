using AniversarioApplication.Database;
using AniversarioApplication.Entidade;
using System;
using System.Collections.Generic;

namespace AniversarioApplication.Application {
    public class AniversarioManager {

        AniversarioDatabase database = new AniversarioDatabase();

        public List<Amigo> ObterTodos() {
            return database.ObterTodos();
        }

        public void Salvar(Amigo amigo) {
            database.Salvar(amigo);
        }

        public Amigo ObterPorId(int id) {
            return database.ObterPorId(id);
        }

        public void Excluir(int id) {
            database.Excluir(id);
        }

        public void SalvarEdit(Amigo amigo) {
            database.SalvarEdit(amigo);
        }
        
        public List<Amigo> Buscar(List<Amigo> lista, string part) {
            List<Amigo> encontrados = new List<Amigo>();

            foreach (Amigo amigo in lista.FindAll(x => (x.Nome.ToUpper().Contains(part.ToUpper()) || x.Sobrenome.ToUpper().Contains(part.ToUpper())))) {
                encontrados.Add(amigo);
            };
            return encontrados;
        }

        public List<Amigo> AniversarioHoje(List<Amigo> amigos) {
            List<Amigo> encontrados = new List<Amigo>();

            foreach (Amigo amigo in amigos) {
                bool verificado = VerificarAniversario(amigo.Aniversario.ToString());
                if (verificado == true) {
                    encontrados.Add(amigo);
                }
            }
            return encontrados;
        }

        public double FaltaParaAniversario(Amigo amigo) {
            string data = amigo.Aniversario.ToString();
            string[] num = data.Split('/');
            int dia = int.Parse(num[0]);
            int mes = int.Parse(num[1]);
            
            DateTime DataAniversario = new DateTime(DateTime.Today.Year, mes, dia);
            double result = DataAniversario.Subtract(DateTime.Today).TotalDays;
            if(result < 0) {
                result += 365;
                return result;
            } else {
                return result;
            }
        }

        public bool VerificarAniversario(string data) {
            var num = data.Split('/');
            int dia = int.Parse(num[0]);
            int mes = int.Parse(num[1]);

            DateTime DataAniversario = new DateTime(DateTime.Today.Year, mes, dia);
            if (DataAniversario == DateTime.Today) {
                return true;
            } else {
                return false;
            }
        }
    }
}
