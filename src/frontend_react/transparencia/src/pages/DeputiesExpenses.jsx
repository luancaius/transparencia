import { useState, useEffect } from "react";

export default function DeputiesExpenses({ ano, mes, fetchTrigger }) {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [selectedDeputy, setSelectedDeputy] = useState(null);
  const [showModal, setShowModal] = useState(false);

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

  const handleViewExpenses = (deputy) => {
    setSelectedDeputy(deputy);
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
    setSelectedDeputy(null);
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
                <th>Ação</th>
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
                  <td>
                    <button
                      className="btn btn-primary"
                      onClick={() => handleViewExpenses(deputado)}
                    >
                      Ver Despesas
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {showModal && selectedDeputy && (
        <>
          <div className="modal-backdrop fade show"></div>
          <div
            className="modal show fade"
            style={{ display: "block", zIndex: 1055 }}
          >
            <div className="modal-dialog">
              <div className="modal-content">
                <div className="modal-header">
                  <h5 className="modal-title">
                    Despesas de {selectedDeputy.deputy_name}
                  </h5>
                  <button
                    type="button"
                    className="btn-close"
                    onClick={closeModal}
                  ></button>
                </div>
                <div className="modal-body">
                  {selectedDeputy.expenses &&
                  selectedDeputy.expenses.length > 0 ? (
                    <table className="table table-bordered">
                      <thead>
                        <tr>
                          <th>Ano</th>
                          <th>Mês</th>
                          <th>Total</th>
                          <th>Tipo</th>
                          <th>Documento</th>
                        </tr>
                      </thead>
                      <tbody>
                        {selectedDeputy.expenses
                          .slice()
                          .sort((a, b) => b.total - a.total)
                          .map((expense, index) => (
                            <tr key={index}>
                              <td>{expense.year}</td>
                              <td>{expense.month}</td>
                              <td>
                                {expense.total.toLocaleString("pt-BR", {
                                  style: "currency",
                                  currency: "BRL",
                                })}
                              </td>
                              <td>{expense.type}</td>
                              <td>
                                {expense.url ? (
                                  <a
                                    href={expense.url}
                                    target="_blank"
                                    rel="noreferrer"
                                  >
                                    Visualizar
                                  </a>
                                ) : (
                                  "N/A"
                                )}
                              </td>
                            </tr>
                          ))}
                      </tbody>
                    </table>
                  ) : (
                    <p>Nenhuma despesa encontrada.</p>
                  )}
                </div>
                <div className="modal-footer">
                  <button className="btn btn-secondary" onClick={closeModal}>
                    Fechar
                  </button>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
}
