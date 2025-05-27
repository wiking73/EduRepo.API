import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useParams, Link } from 'react-router-dom';
import "./Styles/OcenZadanie.css";

function OcenOdpowiedz() {
    const [odpowiedz, setOdpowiedz] = useState({
        idOdpowiedzi: "",
        idZadania: "",
        wlascicielId: "",
        userName: "",
        dataOddania: "",
        komentarzNauczyciela: "",
        nazwaPliku: "",
        ocena: ""
    });
    const [zadanie, setZadanie] = useState(null);
    const [error, setError] = useState('');
    const { IdOdpowiedzi, IdZadania } = useParams();
    const navigate = useNavigate();

    const fetchOdpowiedz = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(`https://localhost:7157/api/Odpowiedz/${IdOdpowiedzi}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setOdpowiedz(response.data);
        } catch (err) {
            setError('Błąd podczas pobierania danych.');
        }
    };

    const fetchZadanie = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(`https://localhost:7157/api/Zadanie/${IdZadania}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setZadanie(response.data);
        } catch (err) {
            setError('Błąd podczas pobierania danych.');
        }
    };

    useEffect(() => {
        fetchZadanie();
        fetchOdpowiedz();
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    }, [IdOdpowiedzi]);


    const getDateClass = () => {
        if (zadanie && odpowiedz.dataOddania) {
            const termin = new Date(zadanie.terminOddania);
            const dataOddania = new Date(odpowiedz.dataOddania);
            return dataOddania > termin ? 'overdue' : 'on-time';
        }
        return '';
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setOdpowiedz({
            ...odpowiedz,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const updatedOdpowiedz = {
            idOdpowiedzi: odpowiedz.idOdpowiedzi,
            idZadania: odpowiedz.idZadania,
            dataOddania: odpowiedz.dataOddania,
            komentarzNauczyciela: odpowiedz.komentarzNauczyciela,
            nazwaPliku: odpowiedz.nazwaPliku,
            ocena: odpowiedz.ocena
        };

        const token = localStorage.getItem('authToken');

        axios
            .put(`https://localhost:7157/api/Odpowiedz/${IdOdpowiedzi}`, updatedOdpowiedz, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            })
            .then(() => {
                navigate(`/kursy`);
            })
            .catch((error) => {
                console.error('Błąd podczas aktualizacji użytkownika:', error);
                if (error.response) {
                    setError(error.response.data);
                } else {
                    setError('Nie udało się zaktualizować użytkownika. Sprawdź czy prawidłowo wpisałeś wszystkie parametry');
                }
                setTimeout(() => {
                    setError('');
                }, 3000);
            });
    };

    return (
        <div className="user-form">
            {error && <div className="error-message">{error}</div>}
            <div className="odpowiedz-info">
                <p><strong>Użytkownik:</strong> {odpowiedz.userName}</p>
                <p><strong>Plik: </strong>
                    <a href={`https://localhost:7157${odpowiedz.nazwaPliku}`} target="_blank" rel="noopener noreferrer">
                        Otwórz
                    </a></p>
                <p className={getDateClass()}><strong>Data oddania:</strong> {new Date(odpowiedz.dataOddania).toLocaleDateString()}</p>
                {(new Date(zadanie?.terminOddania) < new Date(odpowiedz.dataOddania)) && <p style={{ color: "red" }}>Oddane z opóźnieniem</p>}

            </div>

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <p className="h2">Oceń Odpowiedź</p>
                    <label>Ocena:</label>
                    <input type="text" name="ocena" value={odpowiedz.ocena} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label>Dodaj Komentarz:</label>
                    <input type="text" name="komentarzNauczyciela" value={odpowiedz.komentarzNauczyciela} onChange={handleChange} required />
                </div>

                <button type="submit" className="btn btn-primary">
                    Zapisz zmiany
                </button>
                <Link to="/kursy" className="btn btn-secondary">
                    Powrót
                </Link>
            </form>
        </div>
    );
}

export default OcenOdpowiedz;
