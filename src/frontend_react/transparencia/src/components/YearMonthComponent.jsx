import React, { useState, useEffect } from "react";

export default function YearMonthSelector({ ano, setAno, mes, setMes }) {
  // Local state to store the fetched JSON structure
  const [yearMonthData, setYearMonthData] = useState(null);

  // Fetch the JSON with years/months in Portuguese from public folder
  useEffect(() => {
    fetch("/data/monthly_expenses/available_data.json")
      .then((res) => {
        if (!res.ok) {
          throw new Error(`Failed to fetch available_data.json: ${res.status}`);
        }
        return res.json();
      })
      .then((data) => {
        setYearMonthData(data);
      })
      .catch((error) => {
        console.error("Error loading year-month data:", error);
      });
  }, []);

  // While the JSON file is loading, show a placeholder
  if (!yearMonthData) {
    return <div>Carregando dados de ano e mês...</div>;
  }

  // List of available years from the fetched data
  const years = Object.keys(yearMonthData);

  // Months for the currently selected year; fall back to empty array if none
  const monthsForSelectedYear = yearMonthData[ano] || [];

  // Update the parent’s `ano` when the user picks a different year
  const handleYearChange = (e) => {
    const selectedYear = e.target.value;
    setAno(selectedYear);

    // Optionally reset month if you want it to always jump to the first available:
    // setMes(yearMonthData[selectedYear][0].value);
  };

  // Update the parent’s `mes` when the user picks a different month
  const handleMonthChange = (e) => {
    setMes(e.target.value);
  };

  return (
    <>
      {/* Year Selector */}
      <div className="col-auto">
        <label className="form-label">Ano</label>
        <select className="form-select" value={ano} onChange={handleYearChange}>
          {years.map((year) => (
            <option key={year} value={year}>
              {year}
            </option>
          ))}
        </select>
      </div>

      {/* Month Selector */}
      <div className="col-auto">
        <label className="form-label">Mês</label>
        <select className="form-select" value={mes} onChange={handleMonthChange}>
          {monthsForSelectedYear.map((month) => (
            <option key={month.value} value={month.value}>
              {month.label}
            </option>
          ))}
        </select>
      </div>
    </>
  );
}
