import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import "./Styles/CreateZadanie.css";

const token = localStorage.getItem("authToken");

function EditZadanie() {
  const { IdZadania } = useParams();
  const navigate = useNavigate();
  const [zadanie, setZadanie] = useState(null);
  const [error, setError] = useState(null);
  const [userId, setUserId] = useState(null);

  useEffect(() => {
    const fetchZadanie = async () => {
      try {
        const response = await axios.get(`https://localhost:7157/api/Zadanie/${IdZadania}`);
        setZadanie(response.data);
      } catch (err) {
        setError("Nie udało się pobrać danych zadania.");
      }
    };

    const parseJwt = (token) => {
      if (!token) return null;
      try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
        return JSON.parse(jsonPayload);
      } catch (e) {
        return null;
      }
    };

    const userData = parseJwt(token);
    if (userData) setUserId(userData.nameid);

    fetchZadanie();
  }, [IdZadania]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setZadanie((prev) => ({ ...prev, [name]: newValue }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.put(
        `https://localhost:7157/api/Zadanie/${IdZadania}`,
        { ...zadanie, userId: userId },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      alert("Zadanie zaktualizowane.");
      navigate(-1);
    } catch (err) {
      console.error("Błąd podczas edycji zadania:", err);
      setError("Wystąpił błąd podczas edycji zadania.");
    }
  };

  if (!zadanie) return <p>Trwa ładowanie...</p>;

  return (
    <div className="form-container">
      <h4>Edytuj zadanie</h4>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Nazwa zadania:</label>
          <input type="text" name="nazwa" value={zadanie.nazwa} onChange={handleChange} required />
        </div>

        <div className="form-group">
          <label>Treść:</label>
          <textarea name="tresc" value={zadanie.tresc} onChange={handleChange} required />
        </div>

        <div className="form-group">
          <label>Termin oddania:</label>
          <input type="datetime-local" name="terminOddania" value={zadanie.terminOddania} onChange={handleChange} required />
        </div>

        <div className="form-group">
          <label>Czy obowiązkowe:</label>
          <input type="checkbox" name="czyObowiazkowe" checked={zadanie.czyObowiazkowe} onChange={handleChange} />
        </div>

        <button type="submit" className="btn btn-primary">Zapisz zmiany</button>
        <Link to={-1} className="btn btn-secondary">Anuluj</Link>
      </form>
    </div>
  );
}

export default EditZadanie;
