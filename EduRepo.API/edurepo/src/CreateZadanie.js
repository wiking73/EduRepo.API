import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import "./Styles/CreateZadanie.css";
const token = localStorage.getItem('authToken');
//const role = localStorage.getItem('role');
function CreateZadanie() {
    const { id } = useParams(); // kursId
    const navigate = useNavigate();
    const [error, setError] = useState(null);
    const [userId, setUserId] = useState(null);
    const [unique_name, setUserName] = useState(null);

    const [zadanie, setZadanie] = useState({
        "idZadania": "",
        "idKursu": "",
        "nazwa": "",
        "tresc": "",
        "terminOddania": "",
        "plikPomocniczy": "",
        "czyObowiazkowe": false,
        "wlascicielId": "",
        "userName": ""
        
    });

    useEffect(() => {
        fetchUserData();
    }, []);

    const parseJwt = (token) => {
        if (!token) return null;
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split('')
                    .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
                    .join('')
            );
            return JSON.parse(jsonPayload);
        } catch (e) {
            return null;
        }
    };

    const fetchUserData = () => {
        const userData = parseJwt(token);
        if (!userData || !userData.nameid) {
            alert("Nie udało się odczytać UserId z tokena.");
            return;
        }
        setUserId(userData.nameid);
        setUserName(userData.unique_name);
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;

        setZadanie(prev => ({
            ...prev,
            [name]: newValue,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const zadanieToSend = {
            idKursu: id,
            nazwa: zadanie.nazwa,
            tresc: zadanie.tresc,
            terminOddania: zadanie.terminOddania,
            plikPomocniczy: zadanie.plikPomocniczy,
            czyObowiazkowe: zadanie.czyObowiazkowe,
            userId: userId,
            name: unique_name,
        };

        axios.post('https://localhost:7157/api/Zadanie', zadanieToSend, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(() => navigate(`/details/${id}`))
            .catch((error) => {
                console.error('Błąd podczas tworzenia zadania:', error);
                alert('Wystąpił błąd podczas tworzenia zadania.');
            });
    };

    return (
        <div className="form-container">
            <h4>Utwórz nowe zadanie</h4>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Nazwa zadania:</label>
                    <input
                        type="text"
                        name="nazwa"
                        value={zadanie.nazwa}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Treść:</label>
                    <textarea
                        name="tresc"
                        value={zadanie.tresc}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Termin oddania:</label>
                    <input
                        type="datetime-local"
                        name="terminOddania"
                        value={zadanie.terminOddania}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Plik pomocniczy (nazwa lub URL):</label>
                    <input
                        type="text"
                        name="plikPomocniczy"
                        value={zadanie.plikPomocniczy}
                        onChange={handleChange}
                    />
                </div>

                <div className="form-group">
                    <label>Czy obowiązkowe:</label>
                    <input
                        type="checkbox"
                        name="czyObowiazkowe"
                        checked={zadanie.czyObowiazkowe}
                        onChange={handleChange}
                    />
                </div>

                <button type="submit" className="btn btn-primary">Utwórz zadanie</button>
                <Link to={`/details/${id}`} className="btn btn-secondary">Anuluj</Link>
            </form>
        </div>
    );
}

export default CreateZadanie;
