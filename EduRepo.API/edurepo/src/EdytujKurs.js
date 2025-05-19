import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useParams, Link } from 'react-router-dom';

function EditKurs() {
    const [kurs, setKurs] = useState({
        IdKursu: "",
        Nazwa: "",
        OpisKursu: "",
        RokAkademicki: "",
        Klasa: "",
        CzyZarchiwizowany: false,  
        WlascicielId: "",
        UserName : "",
       
    });
    const [error, setError] = useState('');
    const { id } = useParams(); // Capture the 'id' from the URL using useParams
    const navigate = useNavigate();

   
    const fetchKurs = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(`https://localhost:7157/api/kurs/${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setKurs(response.data);
        } catch (err) {
            console.error('Błąd podczas pobierania kursu:', err);
            setError('Błąd podczas pobierania danych.');
        }
    };

    useEffect(() => {
        fetchKurs();
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    }, [id]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setKurs({
            ...kurs,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!kurs.Nazwa) {
            alert('Nazwa jest wymagana');
            return;
        }
        if (!kurs.OpisKursu) {
            alert('Opis jest wymagany');
            return;
        }

        const updatedKurs = {
            ...kurs,
            userId: parseInt(id),
        };

        const token = localStorage.getItem('authToken');

        axios
            .put(`https://localhost:7157/api/kurs/${id}`, updatedKurs, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            })
            .then(() => {
                navigate(`/kursy`);
            })
            .catch((error) => {
                console.error('Błąd podczas aktualizacji kursu:', error);
                if (error.response) {
                    setError(error.response.data); 
                } else {
                    setError('Nie udało się zaktualizować kursu. Sprawdź czy prawidłowo wpisałeś wszystkie parametry');
                }
                setTimeout(() => {
                    setError('');
                }, 3000);
            });
    };

    return (
        <div className="user-form">
            {error && <div className="error-message">{error}</div>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <p className="h2">Edytuj Kurs</p>
                    <label>Nazwa:</label>
                    <input type="text" name="Nazwa" value={kurs.Nazwa} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label>Opis Kursu:</label>
                    <input type="text" name="OpisKursu" value={kurs.OpisKursu} onChange={handleChange} required />
                </div>


                <div className="form-group">
                    <label>Rok Akademicki:</label>
                    <input type="text" name="RokAkademicki" value={kurs.RokAkademicki} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label>Klasa:</label>
                    <input type="text" name="klasa" value={kurs.Klasa} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label>Czy Zaarchiwizowany:</label>
                    <input
                        type="checkbox"
                        name="CzyZarchiwizowany"
                        checked={kurs.CzyZarchiwizowany}
                        onChange={handleChange}
                    />
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

export default EditKurs;
