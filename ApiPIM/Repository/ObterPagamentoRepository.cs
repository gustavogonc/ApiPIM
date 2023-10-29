using ApiPIM.Models;
using Microsoft.Data.SqlClient;
namespace ApiPIM.Repository
{
    public class ObterPagamentoRepository
    {

        public IEnumerable<HistPagamentoModel> ObterPagamentosFiltrados(int mes)
        {
            var todosPagamentos = ObterPagamentos();

            if (mes != 13)  // Se não for "Todos"
            {
                return todosPagamentos.Where(p => p.DataPagamento.Month == mes);
            }

            return todosPagamentos;
        }






        public List<HistPagamentoModel> ObterPagamentos()
        {
            string connectionString = @"Data Source=20.206.249.21,1433;Initial Catalog=PIM;User ID=sa1;Password=Pim123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;";


            List<HistPagamentoModel> pagamentos = new List<HistPagamentoModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT [id_hist], h.[id_funcionario], [data_pagamento], c.salario, [hora_ex], [beneficios]
                         FROM [PIM].[dbo].[tb_histpagment] h
                         inner join tb_funcionarios f on h.id_funcionario = f.id_funcionario
                        inner join tb_cargos c on f.cargo_id = c.id_cargo ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistPagamentoModel pagamento = new HistPagamentoModel
                            {
                                IdHist = reader.GetInt32(0),
                                IdFuncionario = reader.GetInt32(1),
                                DataPagamento = reader.GetDateTime(2),
                                SalarioBase = reader.GetDecimal(3),
                                HoraEx = reader.GetTimeSpan(4),
                                Beneficios = (float)reader.GetDouble(5)
                            };
                            pagamentos.Add(pagamento);
                        }
                    }
                }
            }
            return pagamentos;
        }







        public List<DashboardModel> ObterPagamentoMensal()
        {
            string connectionString = @"Data Source=20.206.249.21,1433;Initial Catalog=PIM;User ID=sa1;Password=Pim123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;";


            List<DashboardModel> pagamentos = new List<DashboardModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                                    DATENAME(MONTH, data_pagamento) + '/' + DATENAME(YEAR, data_pagamento) AS nome_mes,
                                    SUM(total_liq) AS somatoria_total_liquido
                                FROM [dbo].[tb_histpagment]
                                GROUP BY YEAR(data_pagamento), MONTH(data_pagamento), DATENAME(MONTH, data_pagamento), DATENAME(YEAR, data_pagamento)
                                ORDER BY YEAR(data_pagamento), MONTH(data_pagamento)
                                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DashboardModel pagamento = new DashboardModel
                            {
                                texto = reader.GetString(0),
                                totalLiq = (float)reader.GetDouble(1)
                            };
                            pagamentos.Add(pagamento);
                        }
                    }
                }
            }
            return pagamentos;
        }





        public List<DashboardModel> ObterPagamentosDepartamento()
        {
            string connectionString = @"Data Source=20.206.249.21,1433;Initial Catalog=PIM;User ID=sa1;Password=Pim123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;";


            List<DashboardModel> pagamentos = new List<DashboardModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT d.nome_departamento,
                                   
                                    SUM(total_liq) AS somatoria_total_liquido
                                FROM [dbo].[tb_histpagment] h
								inner join tb_funcionarios f on h.id_funcionario = f.id_funcionario
                                inner join tb_cargos c on f.cargo_id = c.id_cargo
                                inner join tb_departamento d on c.DepartamentoId = d.id_departamento
                                GROUP BY d.nome_departamento
                                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DashboardModel pagamentoDep = new DashboardModel
                            {
                                texto = reader.GetString(0),
                                totalLiq = (float)reader.GetDouble(1)
                            };
                            pagamentos.Add(pagamentoDep);
                        }
                    }
                }
            }
            return pagamentos;
        }
    }
}
