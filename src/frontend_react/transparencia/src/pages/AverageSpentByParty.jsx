import { useState, useEffect } from "react";

export default function AverageSpentByParty({ ano, mes, fetchTrigger }) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (fetchTrigger === 0) return;
    fetchData();
    // Optionally, include ano and mes in the dependency array if they can change
  }, [fetchTrigger, ano, mes]);

  const fetchData = () => {
    setLoading(true);
    setError(null);
    setData([]);

    const paddedMonth = String(mes).padStart(2, "0");
    const filePath = `/data/monthly_expenses/average_costs_by_party_${ano}-${paddedMonth}.json`;

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
        setError("Erro ao buscar dados de gastos médios por partido.");
      })
      .finally(() => {
        setLoading(false);
      });
  };

  return (
    <div className="mb-5">
      <h2>Gastos Médios por Partido</h2>
      {loading && <div className="alert alert-info">Carregando dados...</div>}
      {error && <div className="alert alert-danger">{error}</div>}
      {data.length > 0 && !error && (
        <div className="table-responsive">
          <table className="table table-bordered align-middle">
            <thead className="table-light">
              <tr>
                <th>Partido</th>
                <th>Média Gasta</th>
              </tr>
            </thead>
            <tbody>
              {data.map((item, i) => (
                <tr key={i}>
                  <td>{item.party}</td>
                  <td>
                    {item.average_spent.toLocaleString("pt-BR", {
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
