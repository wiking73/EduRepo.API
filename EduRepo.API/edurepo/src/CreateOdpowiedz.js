import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import "./Styles/CreateZadanie.css";

function CreateOdpowiedz() {
    const { id, IdZadania } = useParams();
    const token = localStorage.getItem('authToken');

    const navigate = useNavigate();
    const [error, setError] = useState(null);
    const [userId, setUserId] = useState(null);
    const [unique_name, setUserName] = useState(null);
    const [file, setFile] = useState(null);
    const [zadanie, setZadanie] = useState(null);

    useEffect(() => {
        fetchUserData();
        fetchZadanie(IdZadania);
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

    const fetchZadanie = async (idZadania) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }
            const response = await axios.get(`https://localhost:7157/api/Zadanie/${idZadania}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
            setZadanie(response.data);
        } catch (err) {
            console.error('Błąd pobierania zadania:', err);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!file) {
            alert("Nie wybrano pliku");
            return;
        }

        const formData = new FormData();
        formData.append("IdZadania", IdZadania);
        formData.append("UserId", userId);
        formData.append("Name", unique_name);

        const currentDate = new Date();
        currentDate.setHours(currentDate.getHours() + 2);
        formData.append("DataOddania", currentDate.toISOString());

        formData.append("KomentarzNauczyciela", "brak");
        formData.append("NazwaPliku", file.name);
        formData.append("Ocena", "brak");
        formData.append("file", file);

        try {
            await axios.post(`https://localhost:7157/api/Upload/upload?idZadania=${IdZadania}`, formData, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data",
                },
            });

            await axios.post('https://localhost:7157/api/Odpowiedz', formData, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            alert("Odpowiedź została dodana.");
            navigate(`/details/${id}`);

        } catch (error) {
            console.error("Błąd podczas dodawania odpowiedzi:", error);
            alert("Wystąpił błąd podczas dodawania odpowiedzi lub przesyłania pliku.");
        }
    };

    return (
        <div className="form-container">
            <h4>Dodaj Odpowiedź</h4>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>{zadanie?.nazwa}</label>
                </div>
                <input
                    type="file"
                    onChange={(e) => setFile(e.target.files[0])}
                    required
                />
                <button type="submit" className="btn btn-primary">Dodaj</button>
                <Link to={`/details/${id}`} className="btn btn-secondary">Anuluj</Link>
            </form>
        </div>
    );
}

export default CreateOdpowiedz;
