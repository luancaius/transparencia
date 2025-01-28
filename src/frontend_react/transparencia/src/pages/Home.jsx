import { useState } from 'react'

export default function Home() {
  const [ano, setAno] = useState('2024')
  const [mes, setMes] = useState('1')
  const [data, setData] = useState([])
  const [carregando, setCarregando] = useState(false)
  const [erro, setErro] = useState(null)

  const buscarDespesas = () => {
    setCarregando(true)
    setErro(null)

    const url = `http://localhost:5068/api/deputies/${ano}/${mes}/top-expenses`
    fetch(url)
      .then(res => res.json())
      .then(result => setData(result.slice(0, 10)))
      .catch(() => setErro('Erro ao buscar dados'))
      .finally(() => setCarregando(false))
  }

  return (
    <div className="container py-4">
      <h1 className="mb-4">Maiores Gastos</h1>
      <div className="row mb-3">
        <div className="col-auto">
          <label className="form-label">Ano</label>
          <input
            type="number"
            className="form-control"
            value={ano}
            onChange={e => setAno(e.target.value)}
          />
        </div>
        <div className="col-auto">
          <label className="form-label">Mês</label>
          <input
            type="number"
            className="form-control"
            value={mes}
            onChange={e => setMes(e.target.value)}
          />
        </div>
        <div className="col-auto d-flex align-items-end">
          <button onClick={buscarDespesas} className="btn btn-primary">
            Buscar Top 10
          </button>
        </div>
      </div>
      {carregando && <div className="alert alert-info">Carregando...</div>}
      {erro && <div className="alert alert-danger">{erro}</div>}
      {data.length > 0 && !erro && (
        <div className="table-responsive">
          <table className="table table-bordered align-middle">
            <thead className="table-light">
              <tr>
                <th>Deputado</th>
                <th>Tipo de Despesa</th>
                <th>Valor</th>
                <th>Documento</th>
              </tr>
            </thead>
            <tbody>
              {data.map((item, i) => (
                <tr key={i}>
                  <td>{item.deputyName}</td>
                  <td>{item.expenseType}</td>
                  <td>
                    {item.expenseValue?.toLocaleString('pt-BR', {
                      style: 'currency',
                      currency: 'BRL'
                    })}
                  </td>
                  <td>
                    {item.urlDocumento ? (
                      <a href={item.urlDocumento} target="_blank" rel="noreferrer">
                        {item.urlDocumento}
                      </a>
                    ) : (
                      'N/A'
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}
