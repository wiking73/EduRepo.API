import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, Link, useParams } from 'react-router-dom';

const token = localStorage.getItem('authToken');

function CreateOdpowiedz() {
    const [odpowiedz, setOdpowiedz] = useState({
        NazwaPliku: "",
        DataOddania: "",
        Ocena: "",
        KomentarzNauczyciela: "",
        CzyObowiazkowe: false,
        wlascicielUserId: "",
        wlascicielUserName: "",
        IdZadania: "",
    });

    const [zadanie, setZadanie] = useState(null);  // Przechowujemy dane o zadaniu
    const [unique_name, setUserName] = useState(null);
    const [userId, setUserId] = useState(null);
    const { kursId, zadanieId } = useParams();  // Pobieramy kursId i zadanieId z URL
    const navigate = useNavigate();
    const [error, setError] = useState(null);

    useEffect(() => {
        // Funkcja do pobierania szczegółów zadania z API
        const fetchZadanie = async () => {
            try {
                const response = await axios.get(`https://localhost:7157/api/Zadanie/${kursId}/${zadanieId}`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });
                setZadanie(response.data);  // Ustawiamy dane zadania
                setOdpowiedz((prevState) => ({
                    ...prevState,
                    IdZadania: zadanieId,  // Ustawiamy ID zadania w odpowiedzi
                }));
            } catch (error) {
                console.error('Błąd podczas pobierania zadania:', error);
                setError('Wystąpił błąd podczas pobierania zadania.');
            }
        };

        fetchZadanie();
        fetchUserData();
    }, [kursId, zadanieId]);

    const parseJwt = (token: string | null) => {
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

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value, type, checked } = e.target;
        let newValue = value;
        if (type === 'checkbox') {
            newValue = checked;
        }

        setOdpowiedz({
            ...odpowiedz,
            [name]: newValue,
        });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!userId) {
            alert("Nie udało się odczytać UserId.");
            return;
        }

        const odpowiedzToSend = {
            NazwaPliku: odpowiedz.NazwaPliku,
            DataOddania: odpowiedz.DataOddania,
            KomentarzNauczyciela: odpowiedz.KomentarzNauczyciela,
            Ocena: odpowiedz.Ocena,
            CzyObowiazkowe: odpowiedz.CzyObowiazkowe,
            WlascicielId: userId,
            UserName: unique_name,
            IdZadania: zadanieId,  // Przekazujemy zadanieId, które zostało pobrane
        };

        axios.post('https://localhost:7157/api/odpowiedz', odpowiedzToSend, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(() => navigate(`/kurs/${kursId}/zadanie/${zadanieId}`))  // Po utworzeniu odpowiedzi wracamy do zadania
            .catch((error) => {
                console.error('Błąd podczas tworzenia odpowiedzi:', error);
                setError('Wystąpił błąd podczas tworzenia odpowiedzi.');
            });
    };

    return (
        <div className="bike-form">
            <h6>Dodaj odpowiedź do zadania</h6>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {zadanie ? (
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Nazwa pliku:</label>
                        <input
                            type="text"
                            name="NazwaPliku"
                            value={odpowiedz.NazwaPliku}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Data oddania:</label>
                        <input
                            type="datetime-local"
                            name="DataOddania"
                            value={odpowiedz.DataOddania}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Ocena:</label>
                        <input
                            type="text"
                            name="Ocena"
                            value={odpowiedz.Ocena}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Komentarz nauczyciela:</label>
                        <textarea
                            name="KomentarzNauczyciela"
                            value={odpowiedz.KomentarzNauczyciela}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Czy obowiązkowe:</label>
                        <input
                            type="checkbox"
                            name="CzyObowiazkowe"
                            checked={odpowiedz.CzyObowiazkowe}
                            onChange={handleChange}
                        />
                    </div>

                    <button type="submit" className="btn btn-primary">
                        Dodaj odpowiedź
                    </button>
                    <Link to={`/kurs/${kursId}/zadanie/${zadanieId}`} className="btn btn-secondary">
                        Powrót do zadania
                    </Link>
                </form>
            ) : (
                <p>Ładowanie danych zadania...</p>
            )}
        </div>
    );
}

export default CreateOdpowiedz;
