import { useState } from "react";

export default function TopExpenses({ ano, mes }) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchData = () => {
    setLoading(true);
    setError(null);
    setData([]);

    const paddedMonth = String(mes).padStart(2, "0");
    const filePath = `/data/monthly_expenses/top_expenses_${ano}-${paddedMonth}.json`;

    fetch(filePath)
      .then((res) => {
        if (!res.ok) {
          throw new Error(
            `Arquivo não encontrado para ano=${ano} e mês=${mes}`
          );
        }
        return res.json();
      })
      .then((result) => {
        // If you only want the top 10, slice here:
        setData(result.slice(0, 10));
      })
      .catch(() => {
        setError(
          "Erro ao buscar dados de despesas. Verifique se o arquivo JSON existe."
        );
      })
      .finally(() => {
        setLoading(false);
      });
  };

  return (
    <div className="mb-5">
      <h2>Top 10 Despesas</h2>
      <button onClick={fetchData} className="btn btn-primary mb-3">
        Buscar Top 10 Despesas
      </button>
      {loading && (
        <div className="alert alert-info">Carregando despesas...</div>
      )}
      {error && <div className="alert alert-danger">{error}</div>}
      {data.length > 0 && !error && (
        <div className="table-responsive">
          <table className="table table-bordered align-middle">
            <thead className="table-light">
              <tr>
                <th>Nome Fornecedor</th>
                <th>Tipo de Despesa</th>
                <th>Valor</th>
                <th>Documento</th>
              </tr>
            </thead>
            <tbody>
              {data.map((item, i) => (
                <tr key={i}>
                  <td>{item.nome_fornecedor}</td>
                  <td>{item.expense_type}</td>
                  <td>
                    {item.valor_documento?.toLocaleString("pt-BR", {
                      style: "currency",
                      currency: "BRL",
                    })}
                  </td>
                  <td>
                    {item.url_documento ? (
                      <a
                        href={item.url_documento}
                        target="_blank"
                        rel="noreferrer"
                      >
                        {item.url_documento}
                      </a>
                    ) : (
                      "N/A"
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
