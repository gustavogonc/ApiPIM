using ApiPIM.Models;
using Microsoft.Data.SqlClient;
namespace ApiPIM.Repository
{
    public class ObterPagamentoRepository
    {
        string connectionString = @"Data Source=20.14.87.19,1433;Initial Catalog=PIM;User ID=sa1;Password=1234;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;";

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
          


            List<HistPagamentoModel> pagamentos = new List<HistPagamentoModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT I.ID, I.id_funcionario, I.data, i.nome_valor, i.valor  FROM 
                                    tb_info_pagamento I
                                    INNER JOIN tb_funcionarios F ON F.id_funcionario = I.id_funcionario
                                    INNER JOIN tb_cargos C ON C.id_cargo = F.cargo_id ";

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
                                Texto = reader.GetString(3),
                                Valor = reader.GetDecimal(4),
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
        
            List<DashboardModel> pagamentos = new List<DashboardModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT 
                                    DATENAME(MONTH, data_pagamento) + '/' + DATENAME(YEAR, data_pagamento) AS nome_mes,
                                    SUM(valor_liquido) AS somatoria_total_liquido
                                FROM [dbo].[tb_valores_pagamento]
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
                                totalLiq = (float)reader.GetDecimal(1)
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
         

            List<DashboardModel> pagamentos = new List<DashboardModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT d.nome_departamento,
                                   
                                    SUM(valor_liquido) AS somatoria_total_liquido
                                FROM [dbo].[tb_valores_pagamento] h
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
