import { useState } from "react";
import TopExpenses from "./TopExpenses";
import DeputiesExpenses from "./DeputiesExpenses";
import YearMonthSelector from "../components/YearMonthComponent";

export default function Dashboard() {
  // The parent still owns these states
  const [ano, setAno] = useState("2024");
  const [mes, setMes] = useState("1");
  const [fetchTrigger, setFetchTrigger] = useState(0);


  const handleFetchData = () => {
    setFetchTrigger((prev) => prev + 1);
  };

  return (
    <div className="container py-4">
      <h1 className="mb-4">Dashboard de Despesas</h1>
      
      {/* Replace old numeric inputs with YearMonthSelector */}
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

      {/* The child components that consume the chosen ano/mes */}
      <TopExpenses ano={ano} mes={mes} fetchTrigger={fetchTrigger} />
      <DeputiesExpenses ano={ano} mes={mes} fetchTrigger={fetchTrigger} />
    </div>
  );
}
