import { useState } from "react";
import TopExpenses from "./TopExpenses";
import DeputiesExpenses from "./DeputiesExpenses";

export default function Dashboard() {
  const [ano, setAno] = useState("2024");
  const [mes, setMes] = useState("1");

  return (
    <div className="container py-4">
      <h1 className="mb-4">Dashboard de Despesas</h1>
      <div className="row mb-3">
        <div className="col-auto">
          <label className="form-label">Ano</label>
          <input
            type="number"
            className="form-control"
            value={ano}
            onChange={(e) => setAno(e.target.value)}
          />
        </div>
        <div className="col-auto">
          <label className="form-label">Mês</label>
          <input
            type="number"
            className="form-control"
            value={mes}
            onChange={(e) => setMes(e.target.value)}
          />
        </div>
      </div>

      <TopExpenses ano={ano} mes={mes} />
      <DeputiesExpenses ano={ano} mes={mes} />
    </div>
  );
}
