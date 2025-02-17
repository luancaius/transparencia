import { useState, useEffect } from "react";

export default function DeputiesExpenses({ ano, mes, fetchTrigger }) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (fetchTrigger === 0) return;
    fetchData();
  }, [fetchTrigger]);

  const fetchData = () => {
    setLoading(true);
    setError(null);
    setData([]);

    const paddedMonth = String(mes).padStart(2, "0");
    const filePath = `/data/monthly_expenses/top_deputies_${ano}-${paddedMonth}.json`;

    fetch(filePath)
      .then((res) => {
        if (!res.ok) {
          throw new Error(`Arquivo não encontrado para ano=${ano} e mês=${mes}`);
        }
        return res.json();
      })
      .then((result) => {
        setData(result);
      })
      .catch(() => {
        setError("Erro ao buscar dados dos deputados.");
      })
      .finally(() => {
        setLoading(false);
      });
  };

  return (
    <div>
      <h2>Despesas dos Deputados</h2>
      {loading && <div className="alert alert-info">Carregando deputados...</div>}
      {error && <div className="alert alert-danger">{error}</div>}
      {data.length > 0 && !error && (
        <div className="table-responsive">
          <table className="table table-bordered align-middle">
            <thead className="table-light">
              <tr>
                <th>Deputado</th>
                <th>Partido</th>
                <th>Total Gasto</th>
              </tr>
            </thead>
            <tbody>
              {data.map((deputado) => (
                <tr key={deputado.deputy_id}>
                  <td>{deputado.deputy_name}</td>
                  <td>{deputado.party}</td>
                  <td>
                    {deputado.total_spent.toLocaleString("pt-BR", {
                      style: "currency",
                      currency: "BRL",
                    })}
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
