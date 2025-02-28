import { useState } from "react";
import TopExpenses from "./TopExpenses";
import DeputiesExpenses from "./DeputiesExpenses";
import AverageSpentByParty from "./AverageSpentByParty";
import YearMonthSelector from "../components/YearMonthComponent";

export default function Dashboard() {
  const [ano, setAno] = useState("2024");
  const [mes, setMes] = useState("1");
  const [fetchTrigger, setFetchTrigger] = useState(0);

  const handleFetchData = () => {
    setFetchTrigger((prev) => prev + 1);
  };

  return (
    <div className="container py-4">
      <h1 className="mb-4">Dashboard de Despesas</h1>
      
      <div className="row mb-3">
        <YearMonthSelector 
          ano={ano}
          setAno={setAno}
          mes={mes}
          setMes={setMes}
        />
      </div>

      <button onClick={handleFetchData} className="btn btn-primary mb-3">
        Buscar Dados
      </button>

      <TopExpenses ano={ano} mes={mes} fetchTrigger={fetchTrigger} />
      <DeputiesExpenses ano={ano} mes={mes} fetchTrigger={fetchTrigger} />
      <AverageSpentByParty ano={ano} mes={mes} fetchTrigger={fetchTrigger} />
    </div>
  );
}
